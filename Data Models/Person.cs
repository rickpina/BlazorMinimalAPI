using System.ComponentModel.DataAnnotations;

namespace BlazorMinimalAPI.Data_Models
{
    public class Person
    {
        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string? SSN { get; set; }

        public string? Occupation { get; set; }
    }
}
