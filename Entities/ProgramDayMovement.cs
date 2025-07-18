using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ProgramDayMovement
    {
        public int ProgramDayMovementID { get; set; }
        public string OrderInDay { get; set; }
        public Int16 Sets { get; set; }
        public Int16 Reps { get; set; }
        public int DurationSeconds { get; set; }
        public int FK_ProgramDaysID { get; set; }
        public ProgramDays ProgramDays { get; set; }
        public int FK_MovementID { get; set; }
        public Movement Movement { get; set; }
    }
}
