using Microsoft.EntityFrameworkCore;
using Stereograph.TechnicalTest.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Stereograph.TechnicalTest.Api.Repository
{
    public interface IPersonRepository
    {
        bool IsEmptyTable();
        List<Person> GetAll();
        Person Get(int id);
        Person GetWithFollowersAndFollowings(int id);
        Person GetWithFollowers(int id);
        Person GetWithFollowings(int id);
        void Delete(int id);
        void Create(Person person);
        bool Exist(int id);
        void Update(Person person);
    }

    public class PersonRepository: IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Person> GetAll()
        {
            return _context.Persons.ToList();
        }

        public Person Get(int id)
        {
            return _context.Persons.Where(person => person.Id == id).FirstOrDefault();
        }

        public Person GetWithFollowersAndFollowings(int id)
        {
            return _context.Persons.Include(p => p.Followers).Include(p => p.Followings).FirstOrDefault(p => p.Id == id);
        }

        public Person GetWithFollowers(int id)
        {
            return _context.Persons.Include(p=>p.Followers).FirstOrDefault(p=>p.Id == id);
        }

        public Person GetWithFollowings(int id)
        {
            return _context.Persons.Include(p => p.Followings).FirstOrDefault(p => p.Id == id);
        }

        public void Delete(int id)
        {
            Person person = Get(id);
            _context.Persons.Remove(person);
            _context.SaveChanges();
        }

        public void Create(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public bool Exist(int id)
        {
            return _context.Persons.Any(person => person.Id == id);
        }

        public void Update(Person person)
        {
            _context.Update(person);
            _context.SaveChanges();
        }

        public bool IsEmptyTable()
        {
           return !_context.Persons.Any();
        }
    }
}
