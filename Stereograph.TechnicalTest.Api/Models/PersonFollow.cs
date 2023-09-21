using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Stereograph.TechnicalTest.Api.Models
{
    public class PersonFollow
    {
        public int PersonId { get; set; }
        public int FollowingId { get; set; }

        [JsonIgnore]
        public Person Person { get; set; }

        [JsonIgnore]
        public Person Following { get; set; }
    }
}
