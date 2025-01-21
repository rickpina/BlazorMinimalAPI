using System.ComponentModel.DataAnnotations;

namespace BlazorMinimalAPI.Data_Models
{
    public class GeneratePerson
    {
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public string? DateOfBirth { get; set; }

        public string? SSN { get; set; }

        public string? Occupation { get; set; }

        [Display(Name = "Amount")]
        public int GenerateAmount { get; set; }
        public string? Title { get; set; }
    }
}
