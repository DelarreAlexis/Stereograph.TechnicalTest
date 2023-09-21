using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Stereograph.TechnicalTest.Api.ModelsCVS
{
    public class PersonCSV
    {
        public string last_name { get; set; }

        public string first_name { get; set; }

        public string email { get; set; }

        public string address { get; set; }

        public string city { get; set; }
    }
}
