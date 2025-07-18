using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ProgramDays
    {
        public int ProgramDaysID { get; set; }
        public EnumWeekDay DayOfWeek { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public bool IsRestDay { get; set; }
        public int FK_ProgramID { get; set; }       
        public Program Program { get; set; }
        public ICollection<ProgramDayMovement> ProgramDayMovement { get; set; }
    }

}
