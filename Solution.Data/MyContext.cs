using Solution.Data.Configurations;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;


namespace Solution.Data
{
    public class MyContext: IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {

        public static MyContext Create()
        {
            return new MyContext();
        }
        static MyContext()
        {
            Database.SetInitializer<MyContext>(null);
        }

        public MyContext():base("kindergarten")
        {

        }
        //les dbsets

        public DbSet<Reclamation> Reclamations { get; set; }
        public DbSet<ReclamationP> ReclamationsP { get; set; }




        public DbSet<Publication> Publications { get; set; }
        public DbSet<Like> Likes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());
            modelBuilder.Configurations.Add(new PublicationConfiguration());
            modelBuilder.Configurations.Add(new LikeConfiguration());
            modelBuilder.Configurations.Add(new ReplyConfiguration());
            //config + conventions
            //modelBuilder.Configurations.Add(...);
            //modelBuilder.Conventions.Add(...);
        }

        public class ContexInit : DropCreateDatabaseIfModelChanges<MyContext>
        {
            protected override void Seed(MyContext context)
            {
                /*   List<Patient> patients = new List<Patient>() {
                       new Patient {PatientId=1
                                   }

                   };
                   context.Patients.AddRange(patients);
                   context.SaveChanges();*/

            }
        }
    }
}
