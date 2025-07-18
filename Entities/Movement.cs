using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Movement
    {
        public int MovementID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GifFilePath { get; set; }
        public int FK_categoryID { get; set; }
        public Category category { get; set; }
        public ICollection<ProgramDayMovement> ProgramDayMovement { get; set; }

    }
}
