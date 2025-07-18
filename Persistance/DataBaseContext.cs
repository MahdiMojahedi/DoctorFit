using Application.Interfaces;
using Entities;
using Entities.Enums;
using IDP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistance
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }


        public DbSet<Movement> Movements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<ProgramDays> ProgramDays { get; set; }
        public DbSet<ProgramDayMovement> ProgramDayMovements { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movement>().ToTable("tblMovement");
            modelBuilder.Entity<Category>().ToTable("tblCategory");
            modelBuilder.Entity<Program>().ToTable("tblProgram");
            modelBuilder.Entity<ProgramDays>().ToTable("tblProgramDays");
            modelBuilder.Entity<ProgramDayMovement>().ToTable("tblProgramDayMovement");
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");


            modelBuilder.Entity<Movement>().HasOne(o => o.category).WithMany(o => o.movements).HasForeignKey(p => p.FK_categoryID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProgramDays>().HasOne(o => o.Program).WithMany(o => o.ProgramDays).HasForeignKey(p => p.FK_ProgramID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProgramDays>().Property(o => o.DayOfWeek).HasConversion(new EnumToStringConverter<EnumWeekDay>());
            modelBuilder.Entity<ProgramDayMovement>().HasOne(o => o.Movement).WithMany(o => o.ProgramDayMovement).HasForeignKey(p => p.FK_MovementID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProgramDayMovement>().HasOne(o => o.ProgramDays).WithMany(o => o.ProgramDayMovement).HasForeignKey(p => p.FK_ProgramDaysID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Program>(entity =>
            {
                entity.HasOne(e => e.Student)
                      .WithMany(p => p.ProgramCoachStudentsAsStudent)
                      .HasForeignKey(e => e.FK_StudentID)
                      .HasPrincipalKey(u => u.Id)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired();

                entity.HasOne(e => e.Coach)
                      .WithMany(p => p.ProgramCoachStudentsAsCoach)
                      .HasForeignKey(e => e.FK_CoachID)
                      .HasPrincipalKey(u => u.Id)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired();
            });


        }



    }
}
