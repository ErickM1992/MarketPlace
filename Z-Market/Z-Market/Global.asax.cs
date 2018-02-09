using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Z_Market.Models;

namespace Z_Market
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<Models.Z_MarketContext, 
                Migrations.Configuration>());

            ApplicationDbContext db = new ApplicationDbContext();//Esto es una conexion a la base de datos, es decir para conectarse a una base de datos basta con implentar un objeto del contexto.
            CreateRoles(db);//Metodo que crear los roles. 
            CreateSuperuser(db);//Metodo para crear al super usuario. 
            AddPermissionsToSuperuser(db);//Metodo para otorgar permisos a los usaurios.
            db.Dispose();//cierra la base de datos.
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        //Metodo que otorgara permisos a los usaurios.
        private void AddPermissionsToSuperuser(ApplicationDbContext db)
        {

            //Creamos la variable que manipulara los usaurios.
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //Variable que me va a permitir manipular los roles, despues de esto ya se pueden crear los roles.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //Buscamos el nombre del usuario para saber si existe 
            var user = userManager.FindByName("moreiraerick1@hotmail.com");

            //Preguntamos si el usuario esta en el rol o si el usaurio ya se le asigno el permiso hacia un rol.
            //El metodo me pide como parametro que envie el id del usuario que ando buscando y como segundo parametro
            //me pide el rol que este deberia o no tener.
            if (!userManager.IsInRole(user.Id, "View"))
            {
                //Si el usuario que estoy buscando no tiene permiso para este rol, entonces se le otorga dicho permiso.

                //Agregueme este usaurio con dicho id al rol llamado View.
                userManager.AddToRole(user.Id,"View");

            }

            if (!userManager.IsInRole(user.Id, "Create"))
            {
                //Si el usuario que estoy buscando no tiene permiso para este rol, entonces se le otorga dicho permiso.

                //Agregueme este usaurio con dicho id al rol llamado Create.
                userManager.AddToRole(user.Id, "Create");

            }


            if (!userManager.IsInRole(user.Id, "Edit"))
            {
                //Si el usuario que estoy buscando no tiene permiso para este rol, entonces se le otorga dicho permiso.

                //Agregueme este usaurio con dicho id al rol llamado Edit.
                userManager.AddToRole(user.Id, "Edit");

            }


            if (!userManager.IsInRole(user.Id, "Delete"))
            {
                //Si el usuario que estoy buscando no tiene permiso para este rol, entonces se le otorga dicho permiso.

                //Agregueme este usaurio con dicho id al rol llamado Delete.
                userManager.AddToRole(user.Id, "Delete");

            }


            if (!userManager.IsInRole(user.Id, "Admin"))
            {
                //Si el usuario que estoy buscando no tiene permiso para este rol, entonces se le otorga dicho permiso.

                //Agregueme este usaurio con dicho id al rol llamado Delete.
                userManager.AddToRole(user.Id, "Admin");

            }




        }



        //Metodo para crear al super usaurio
        private void CreateSuperuser(ApplicationDbContext db)
        {

            //Creamos la variable que manipulara los usaurios.
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //Buscamos el nombre del usuario para saber si existe 
            var user = userManager.FindByName("moreiraerick1@hotmail.com");

            //Verificamos que exista y si NO existe creamos al usuario.
            if (user == null)
            {
                //Crando el objeto de usuario que se pasara por parametro.
                user = new ApplicationUser
                {
                    UserName = "moreiraerick1@hotmail.com",
                    Email = "moreiraerick1@hotmail.com"
                };

                //Esta variable agrega al usuario que resive como primer parametro, el segundo parametro equivale a la 
                //contraseña que tendra dicho usaurio.
                userManager.Create(user,"Erick123.");
            }

        }




        //Metodo para crear los roles del programa.
        private void CreateRoles(ApplicationDbContext db)
        {
            //Variable que me va a permitir manipular los roles, despues de esto ya se pueden crear los roles.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //Creando los roles que resiven como parametro el nombre del rol que se le asignara.
            //Antes de crear el rol hay que saber si existe o no, lo cual se logra con el if.

            //Creando el role para la vista.
            if (!roleManager.RoleExists("View"))
            {
                roleManager.Create(new IdentityRole("View"));
            }

            //Creando el role Edit
            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }

            //Creando el rol Create
            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }

            //Creando el rol Delete
            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }


            //Creando el rol Admin
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }


            //Creando el rol Admin
            if (!roleManager.RoleExists("User"))
            {
                roleManager.Create(new IdentityRole("User"));
            }


        }
    }
}
