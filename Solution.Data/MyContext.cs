using Microsoft.AspNet.Identity.EntityFramework;
using Solution.Data.Configurations;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data
{
    public class MyContext:IdentityDbContext<User>
    {
        public MyContext():base("4arctic1")
        {

            Database.SetInitializer(new ContexInit());

        }
        //les dbsets

     
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());
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
