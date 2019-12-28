using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace StorageProject.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<StorageProject.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<StorageProject.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<StorageProject.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<StorageProject.Models.ApplicationUser> ApplicationUsers { get; set; }
    }


    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("DefaultConnection") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        //public System.Data.Entity.DbSet<StorageProject.Models.AppUser> AppUsers { get; set; }
        //public System.Data.Entity.DbSet<StorageProject.Models.ApplicationUser> ApplicationUsers { get; set; }
    }


}