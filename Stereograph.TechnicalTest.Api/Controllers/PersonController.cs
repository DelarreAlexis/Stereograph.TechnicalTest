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
        if (_personRepository.Delete(id))
            return Ok();
        return NotFound();
    }



}
