using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDataBaseContext
    {
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<ProgramDays> ProgramDays { get; set; }
        public DbSet<ProgramDayMovement> ProgramDayMovements { get; set; }

        int SaveChanges();  
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
