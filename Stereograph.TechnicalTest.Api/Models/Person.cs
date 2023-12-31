﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Stereograph.TechnicalTest.Api.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
        [JsonIgnore]
        public int Id { get; set; }

        [Display(Name = "last_name")]
        public string LastName { get; set; }

        [Display(Name = "first_name")]
        public string FirstName { get; set; }

        [Display(Name = "email")]
        public string Email { get; set; }

        [Display(Name = "address")]
        public string Address { get; set; }

        [Display(Name = "city")]
        public string City { get; set; }

        public List<PersonFollow> Followers { get; set; } = new List<PersonFollow>();
        public List<PersonFollow> Followings { get; set; } = new List<PersonFollow>();
    }
}
