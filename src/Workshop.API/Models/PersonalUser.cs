using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.API.Models
{
    [Table("PersonalUsers")]
    public class PersonalUser : WorkshopUser
    {
        public string Id { get; set; }
        public string? FristName { get; set; }
        public string? LastName { get; set; }
    }
}
