using Microsoft.AspNetCore.Mvc;
using Stereograph.TechnicalTest.Api.Models;
using Stereograph.TechnicalTest.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stereograph.TechnicalTest.Api.Controllers
{
    [Route("api/follow")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public FollowController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpPost]
        public IActionResult AddFollowing([FromBody] PersonFollow follow)
        {
            try
            {
                Person person = _personRepository.GetWithFollowersAndFollowings(follow.PersonId);
                if (person == null) return NotFound();

                Person personFollow = _personRepository.GetWithFollowersAndFollowings(follow.FollowingId);
                if (personFollow == null) return NotFound();

                if (person.Id == personFollow.Id) return BadRequest();

                if (person.Followers.Any(f => f.FollowingId == personFollow.Id)) return BadRequest();

                person.Followers.Add(follow);
                personFollow.Followings.Add(follow);

                _personRepository.Update(person);
                _personRepository.Update(personFollow);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne est survenue : {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetFollowers(int id)
        {
            try
            {
                Person person = _personRepository.GetWithFollowers(id);
                if (person == null) return NotFound();

                List<Person> followers = new();
                person.Followers.ForEach(x =>
                {
                    followers.Add(_personRepository.Get(x.FollowingId));
                });

                return Ok(followers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne est survenue : {ex.Message}");
            }
        }
    }
}
