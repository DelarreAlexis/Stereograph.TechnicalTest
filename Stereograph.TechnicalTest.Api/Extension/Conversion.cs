using Stereograph.TechnicalTest.Api.Models;
using Stereograph.TechnicalTest.Api.ModelsCVS;
using System.Collections.Generic;
using System.Linq;

namespace Stereograph.TechnicalTest.Api.Extension
{
    public static class Conversion
    {
        public static List<Person> ToPersons (this List<PersonCSV> listCVS)
        {
            return listCVS.Select(csv => new Person
            {
                LastName = csv.last_name,
                FirstName = csv.first_name,
                Address = csv.address,
                City = csv.city,
                Email = csv.email,
            }).ToList();
        }
    }
}
