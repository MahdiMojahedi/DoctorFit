using Duende.IdentityServer.EntityFramework.Entities;
using Entities;
using Entities.Enums;
using IDP.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;

namespace DoctorFit.IDP.Data
{
    public class IDPDbContext : IdentityDbContext<
     ApplicationUser,
     IdentityRole<int>,
     int,
     IdentityUserClaim<int>,
     IdentityUserRole<int>,
     IdentityUserLogin<int>,
     IdentityRoleClaim<int>,
     IdentityUserToken<int>>
    {
        public IDPDbContext(DbContextOptions<IDPDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().Property(o => o.Gender).HasConversion(new BoolToStringConverter("Female", "Male"));
            builder.Entity<ApplicationUser>().Property(o => o.EducationDegree).HasConversion(new EnumToStringConverter<EducationDegree>());
            builder.Entity<ApplicationUser>().Property(o => o.UserType).HasConversion(new EnumToStringConverter<EnumUserType>());

            builder.Entity<Entities.Program>(entity =>
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
