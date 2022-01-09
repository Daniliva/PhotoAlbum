using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL
{
    public class AlbumContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            /* modelBuilder.Entity<PhotoUsers>()
                   .HasOptional(c => c.User)
            ;
               modelBuilder.Entity<PhotoUsers>()
                   .HasMany(c => c.Photo)
                   .WithOptional(o => o.PhotoUsers);*/
            /* modelBuilder.Entity<User>()
                .HasRequired<ApplicationUser>(s => s.ApplicationUser)
                      modelBuilder.Entity<User>()
                         .HasKey(x => new { x.ApplicationUserId }); modelBuilder.Entity<User>()
                              .HasRequired<ApplicationUser>(s => s.ApplicationUser)

                             ;
                      modelBuilder.Entity<ApplicationUser>()
                          .HasOptional(c => c.User)
                          .WithRequired(d => d.ApplicationUser);
                      //  modelBuilder.Entity<User>().HasRequired(x => x.ApplicationUser).WithOptional().Map(c => c.MapKey("LogRecordId"));
                    modelBuilder.Entity<TUserRole>()
                          .HasKey(r => new { r.UserId, r.RoleId })
                          .ToTable("AspNetUserRoles");

                      modelBuilder.Entity<TUserLogin>()
                          .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                          .ToTable("AspNetUserLogins");

                            modelBuilder.Entity<Test>()
                              .HasMany<Answer>(g => g.Answer)
                              .WithRequired(s => s.Test)
                              .WillCascadeOnDelete();
                       base.OnModelCreating(modelBuilder);*/

            /*  modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
              modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
              modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
              modelBuilder.Entity<User>()
                  .HasOptional(m => m.ApplicationUser)
                  .WithRequired();*/
            base.OnModelCreating(modelBuilder); // I had removed this
            Database.SetInitializer<AlbumContext>(new DropCreateDatabaseIfModelChanges<AlbumContext>());
        }

        public DbSet<User> Users
        {
            get;
            set;
        }

        public DbSet<PhotoUser> PhotoUsers
        {
            get;
            set;
        }

        public DbSet<Photo> Photos
        {
            get;
            set;
        }
    }

    public class UserStore : UserStore<IdentityUser>
    {
    }

    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                IdentityUser admin = new IdentityUser() { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin.Id, "admin");
                }
            }
        }
    }

    public class RoleStore : RoleStore<IdentityRole>
    {
    }
}