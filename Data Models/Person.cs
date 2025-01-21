using System.ComponentModel.DataAnnotations;

namespace BlazorMinimalAPI.Data_Models
{
    public class Person
    {

        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public string? SSN { get; set; }

        public string? Occupation { get; set; }
        public string? Title { get; set; }
    }

}
