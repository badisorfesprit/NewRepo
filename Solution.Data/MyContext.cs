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

        public MyContext():base("name=kindergarten")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

        }
        //les dbsets

        public DbSet<Reclamation> Reclamations { get; set; }
        public DbSet<Response> Response { get; set; }
       // public DbSet<ReclamationP> ReclamationsP { get; set; }




        public DbSet<Publication> Publications { get; set; }
       public DbSet<Like> Likes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());
            modelBuilder.Configurations.Add(new PublicationConfiguration());
            modelBuilder.Configurations.Add(new LikeConfiguration());
            modelBuilder.Configurations.Add(new ReplyConfiguration());
            modelBuilder.Configurations.Add(new ReclamationConfiguration());
            modelBuilder.Configurations.Add(new ResponseConfiguration());
            //config + conventions
            //modelBuilder.Configurations.Add(...);
            //modelBuilder.Conventions.Add(...);
        }

        public class ContexInit : DropCreateDatabaseIfModelChanges<MyContext>
        {
            protected override void Seed(MyContext context)
            {

            }
        }
    }
}
