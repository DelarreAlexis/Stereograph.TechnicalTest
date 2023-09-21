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
        bool Delete(int id);
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

        public bool Delete(int id)
        {
            Person person = Get(id);
            if (person == null)
                return false;
            _context.Persons.Remove(person);
            _context.SaveChanges();
            return true;
        }

        public bool IsEmptyTable()
        {
           return !_context.Persons.Any();
        }
    }
}
