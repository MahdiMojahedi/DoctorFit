using IDP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
        public class Program
        {
            public int ProgramID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int FK_StudentID { get; set; }
            public ApplicationUser Student { get; set; }
            public int FK_CoachID { get; set; }
            public ApplicationUser Coach { get; set; }
            public ICollection<ProgramDays> ProgramDays { get; set; }
        }
}
