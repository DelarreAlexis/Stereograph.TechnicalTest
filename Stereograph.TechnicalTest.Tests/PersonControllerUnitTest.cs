using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Stereograph.TechnicalTest.Api.Controllers;
using Stereograph.TechnicalTest.Api.Models;
using Stereograph.TechnicalTest.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace Stereograph.TechnicalTest.Tests;

public class PersonControllerUnitTest
{
    private List <Person> _people;
    private void Init()
    {
        _people = new List<Person>
        {
            new Person { Id = 1,FirstName="Hiram",LastName="Thain",Email="hthain0@pen.io",Address="61 Armistice Junction",City="Vranje"},
            new Person { Id = 2,FirstName="Demetria",LastName="Ralphs",Email="dralphs1@angelfire.com",Address="6 South Park",City="Udomlya"},
            new Person { Id = 3,FirstName="Hyatt",LastName="Foat",Email="hfoat2@ihg.com",Address="275 Forest Dale Circle",City="Alvaro Obregon"},
            new Person { Id = 4,FirstName="Tabitha",LastName="Boutwell",Email="tboutwell3@mashable.com",Address="1867 Karstens Crossing",City="Sembung"},
        };
    }
 
    [Fact]
    public void ShouldReturnListOfPerson()
    {
        Init();
        var mockPersonRepository = new Mock<IPersonRepository>();
        mockPersonRepository.Setup(service => service.GetAll()).Returns(_people);

        var controller = new PersonController(mockPersonRepository.Object);

        var result = controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<List<Person>>(okResult.Value);

        Assert.Equal(_people, model);
    }

    [Fact]
    public void ShouldReturnOnlyOnePerson()
    {
        Init();
        var mockPersonRepository = new Mock<IPersonRepository>();
        mockPersonRepository.Setup(service => service.Get(2)).Returns(_people[2]);

        var controller = new PersonController(mockPersonRepository.Object);

        var result = controller.GetById(2);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<Person>(okResult.Value);

        Assert.Equal(_people[2],model);
    }

    [Fact]
    public void ShouldNotReturnAnyPerson()
    {
        Init();
        var mockPersonRepository= new Mock<IPersonRepository>();
        mockPersonRepository.Setup(service => service.Get(It.IsAny<int>())).Returns((Person)null);

        var controller = new PersonController(mockPersonRepository.Object);

        var result = controller.GetById(5);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void ShouldBeDeleted()
    {
        var mockPersonRepository = new Mock<IPersonRepository>();
        mockPersonRepository.Setup(repository => repository.Exist(2)).Returns(true); 

        var controller = new PersonController(mockPersonRepository.Object);

        var result = controller.Delete(2);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void ShouldNotBeDeleted()
    {
        var mockPersonRepository = new Mock<IPersonRepository>();
        mockPersonRepository.Setup(repository => repository.Exist(6)).Returns(false);

        var controller = new PersonController(mockPersonRepository.Object);

        var result = controller.Delete(6);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void ShouldBeCreated()
    {
        var mockPersonRepository = new Mock<IPersonRepository>();

        Person person = new Person { Id = 1, FirstName = "Hiram", LastName = "Thain", Email = "hthain0@pen.io", Address = "61 Armistice Junction", City = "Vranje" };

        mockPersonRepository.Setup(repository => repository.Create(person));

        var controller = new PersonController(mockPersonRepository.Object);

        var result = controller.Create(person);

        var okResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedPerson = Assert.IsType<Person>(okResult.Value);
        Assert.Equal("Hiram", returnedPerson.FirstName);
        Assert.Equal("Thain", returnedPerson.LastName);
    }

    [Fact]
    public void ShouldBeUpdated()
    {
        var mockPersonRepository = new Mock<IPersonRepository>();
        var existingPerson = new Person { Id = 1, FirstName = "Hiram", LastName = "Thain", Email = "hthain0@pen.io", Address = "61 Armistice Junction", City = "Vranje" };
        mockPersonRepository.Setup(repository => repository.Exist(1)).Returns(true); 

        var controller = new PersonController(mockPersonRepository.Object);

        var updatedPerson = new Person
        {
            Id = 1,
            FirstName = "UpdatedFirstName",
            LastName = "UpdatedLastName",
            Email = "updated@email.com",
            City = "UpdatedCity",
            Address = "UpdatedAddress"
        };
        var result = controller.Update(1, updatedPerson);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPerson = Assert.IsType<Person>(okResult.Value);
        Assert.Equal("UpdatedFirstName", returnedPerson.FirstName);
        Assert.Equal("UpdatedLastName", returnedPerson.LastName);

    }
}
