using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;
namespace IDP.Entities
{

    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set;}
        public EnumUserType UserType { get; set; }
        public EducationDegree? EducationDegree { get; set; }
        public int Age { get; set; }
        public double? Height { get; set; }
        public string? Target { get; set; }
        public ICollection<Program> ProgramCoachStudentsAsCoach { get; set; }
        public ICollection<Program> ProgramCoachStudentsAsStudent { get; set; }
    }
    
}
