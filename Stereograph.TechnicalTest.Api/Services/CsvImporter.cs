using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Stereograph.TechnicalTest.Api.Constantes;
using Stereograph.TechnicalTest.Api.Extension;
using Stereograph.TechnicalTest.Api.Models;
using Stereograph.TechnicalTest.Api.ModelsCVS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Stereograph.TechnicalTest.Api.Services
{
    public class CsvImporter
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        
        public CsvImporter(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Import()
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string path = Path.Combine(contentRootPath, CONSTANTE.RESSOURCES, CONSTANTE.ORIGIN_DATA);

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            {
                List<PersonCSV> personsCSV = csv.GetRecords<PersonCSV>().ToList();

                List<Person> persons = personsCSV.ToPersons();

                _context.Persons.AddRange(persons);
                _context.SaveChanges();
            }
        }
    }
}
