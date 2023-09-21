using Stereograph.TechnicalTest.Api.Models;
using System.Linq;

namespace Stereograph.TechnicalTest.Api.Repository
{
    public interface IPersonRepository
    {
        bool IsEmptyTable();
    }

    public class PersonRepository: IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsEmptyTable()
        {
           return !_context.Persons.Any();
        }
    }
}
