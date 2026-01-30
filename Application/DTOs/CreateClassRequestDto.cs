using System.ComponentModel.DataAnnotations;

namespace Lisport.API.Application.DTOs
{
    public class CreateClassRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Modality { get; set; }

        [Required]
        public string AgeRange { get; set; }

        [Required]
        public string DaysAndTimes { get; set; }
    }
}
