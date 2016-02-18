using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace Projecten2.NET.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
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
            return DependencyResolver.Current.GetService<ApplicationDbContext>();
            //new ApplicationDbContext();
        }

        //static ApplicationDbContext()
        //{
        //    Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        //}
    }

    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        private ApplicationUserManager userManager;
        //private ApplicationRoleManager roleManager;

        protected override void Seed(ApplicationDbContext context)
        {
            userManager =
                HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            //roleManager =
                //HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            InitializeIdentity();
            //InitializeIdentityAndRoles();
            base.Seed(context);
        }

        private void InitializeIdentity()
        {
            CreateUser("docent@hogent.be", "P@ssword1"); //Create user Docent
            CreateUser("student@hogent.be", "P@ssword1");  //Create User Student
        }

        //private void InitializeIdentityAndRoles()
        //{

        //    CreateUserAndRoles("docent@hogent.be", "P@ssword1", "docent");
        //    CreateUserAndRoles("student@hogent.be", "P@ssword1", "student");
        //}

        private void CreateUser(string name, string password)
        {
            ApplicationUser user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, LockoutEnabled = false };
                IdentityResult result = userManager.Create(user, password);
                if (!result.Succeeded)
                    throw new ApplicationException(result.Errors.ToString());
            }
        }

        //private void CreateUserAndRoles(string name, string password, string roleName)
        //{
        //    //Create user
        //    ApplicationUser user = userManager.FindByName(name);
        //    if (user == null)
        //    {
        //        user = new ApplicationUser {UserName = name, Email = name, LockoutEnabled = false};
        //        IdentityResult result = userManager.Create(user, password);
        //        if (!result.Succeeded)
        //            throw new ApplicationException(result.Errors.ToString());
        //    }

        //    //Create roles
        //    IdentityRole role = roleManager.FindByName(roleName);
        //    if (role == null)
        //    {
        //        role = new IdentityRole(roleName);
        //        IdentityResult result = roleManager.Create(role);
        //        if (!result.Succeeded)
        //            throw new ApplicationException(result.Errors.ToString());
        //    }

        //    //Associate user with role
        //    IList<string> rolesForUser = userManager.GetRoles(user.Id);
        //    if (!rolesForUser.Contains(role.Name))
        //    {
        //        IdentityResult result = userManager.AddToRole(user.Id, roleName);
        //        if (!result.Succeeded)
        //            throw new ApplicationException(result.Errors.ToString());
        //    }
        //}

    }
}