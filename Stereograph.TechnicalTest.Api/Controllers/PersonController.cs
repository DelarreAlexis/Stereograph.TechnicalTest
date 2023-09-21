using Microsoft.AspNetCore.Mvc;
using Stereograph.TechnicalTest.Api.Models;
using Stereograph.TechnicalTest.Api.Repository;
using System.Collections.Generic;

namespace Stereograph.TechnicalTest.Api.Controllers;

[Route("api/persons")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository _personRepository;

    public PersonController(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Person> persons = _personRepository.GetAll();
        if(persons.Count == 0)
            return NotFound();
        return Ok(persons);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Person person = _personRepository.Get(id);
        if(person == null)
            return NotFound();
        return Ok(person);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_personRepository.Exist(id))
            return NotFound();

        return Ok();
    }

    [HttpPost]
    public IActionResult Create([FromBody]Person person)
    {
        if(person == null)
            return BadRequest();

        if(_personRepository.Exist(person.Id))
            return Conflict();

        _personRepository.Create(person);
        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody]Person person)
    {
        if(person == null)
            return BadRequest();

        if (!_personRepository.Exist(id))
            return NotFound();

        Person newPerson = new()
        {
            FirstName = person.FirstName,
            LastName = person.LastName,
            Email = person.Email,
            City = person.City,
            Address = person.Address,
        };

        _personRepository.Update(person);
        return Ok(newPerson);
    }

}
