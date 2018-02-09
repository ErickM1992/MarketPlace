using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Z_Market.Models;
using Z_Market.ViewModels;

namespace Z_Market.Controllers
{
    public class UsersController : Controller
    {
        /// <summary>
        /// Nota: Para enviar un modelo que no estan en la base datos es necesario pasar ese modelo de base de datos
        /// a un nuevo modelo de vista, en este caso existen los usuarios de la base de datos y los usuarios de la vista.
        /// La forma de hacerlo es hacer un tipo de copia de los usuarios de la basedatos para poder utilizarlos en la vista.
        /// </summary>

        //Entro o abro mi base de datos para poder manipular o copiar a los usuarios existentes.
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users sd
        public ActionResult Index()
        {
            //Para pasar la vista de mis usuarios de tal forma que esten en el modelo de vista y no en el de la base de datos
            //Primero se debe crear un tipo de dato que me tome los usuarios de la base de datos.
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //Creo una variable que me permita tener a los usaurios de la base de datos en forma de lista.
            //El metodo user de lavariable userManager esta por defecto entre sus propiedades.
            var users = userManager.Users.ToList();

            //Una vez que tengo a los usaurios de la base de datos en forma de lista, necesito una variable que permita
            //Almacenar una lista de los usaurios que seran utilizados en la vista.
            var usersView = new List<UserView>();

            //Una vez que tengo mi lista de usaurios de la base de datos y mi lista que se llenara de usuarios para que sirvan
            //en mi vista, es hora de recorre los usaurios de las base de datos para crear un usuario de vista por cada uno de
            //los usaurios de la base de datos.

            foreach (var user in users)
            {
                //Crando el objeto que se estara pasandon o copiando en el modelo.
                var userView = new UserView
                {
                    EMail = user.Email,
                    Name = user.UserName,
                    UserID = user.Id
                };

                //A mi lista que tendra los usuarios de vista le agrego el objeto que acabo de crear.
                usersView.Add(userView);

            }
            //Una vez que tengo la lista que enviare a la vista, se la envio por medio de retorno.
            return View(usersView);
        }


        public ActionResult Roles(String userID)
        {
            //Variable para manipular los usuarios
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //Variable para manipular los roles.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));


            /*Creamos un conjunto de varaibles que me permitaran manipular tanto usaurios como roles
             en los cuales voy a otorgar o quitar permisos*/

            //Primero creo una variable que me de una lista de los usuarios  con la ayuda del "userManager".
            // y una variable que me de una lista de los roles existentes con la yuda del "roleManager".
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();

            //Segundo busco al usuario del cual se me esta pasando el ID. esto lo hago con la ayuda de mi lista "users".
            //se busaca con la ayuda de linq. un ID que sea igual al que me estan pasando por paramtro.
            var user = users.Find(u => u.Id == userID);


            //Tercero recorro los roles para saber a cuales de ellos pertenece mi usuraio y creo una lista de los roles 
            //para mostrar en la vista..
            var rolesView = new List<RoleView>();

            //user -> es el usuario del cual se envio el ID y su propiedad Roles mme regresa los roles a los cuales pertenece.
            foreach (var item in user.Roles)
            {

                //Antes de crear el objeto necesito el nombre del rol, el cual se obtiene gracias al linq
                var role = roles.Find(r => r.Id == item.RoleId);

                //Creo un objeto de los roles que ira a la vista.
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    RoleName = role.Name
                };

                //Agrego el objeto que estoy creando a mi lista de roles que seran enviados a la vista.
                rolesView.Add(roleView);
            }

            

            //Cuarto creo el objeto de usaurios que ira a la vista.
            var userView = new UserView
            {
                EMail = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                //Envio una lista de roles existentes.
                Roles = rolesView

            };


            return View(userView);

        }



        //Metodo get de la adicion de los roles.
        public ActionResult AddRole(String userID)
        {

            if (String.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Variable para manipular los usuarios
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //creo una variable que me de una lista de los usuarios con la ayuda del "userManager".
            var users = userManager.Users.ToList();

            //Segundo busco al usuario del cual se me esta pasando el ID. esto lo hago con la ayuda de mi lista "users".
            //se busca con la ayuda de linq. un ID que sea igual al que me estan pasando por parametro.
            var user = users.Find(u => u.Id == userID);

            //Si la respuesta es mala, se envia a una vista 
            if (user == null)
            {
                return HttpNotFound();
            }



            //Creo el objeto de usaurios que ira a la vista.
            var userView = new UserView
            {
                EMail = user.Email,
                Name = user.UserName,
                UserID = user.Id,
            };

            //Variable para manipular los roles.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var List = roleManager.Roles.ToList();
            List.Add(new IdentityRole { Id = "", Name = "[Seleccione un role]" });
            List = List.OrderBy(r => r.Name).ToList();
            ViewBag.RoleID = new SelectList(List, "Id", "Name");

            return View(userView);
        }




        [HttpPost]
        public ActionResult AddRole(String userID, FormCollection form)
        {

            //Variable para manipular los roles.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //Variable para manipular los usuarios
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //creo una variable que me de una lista de los usuarios con la ayuda del "userManager".
            var users = userManager.Users.ToList();

            //Segundo busco al usuario del cual se me esta pasando el ID. esto lo hago con la ayuda de mi lista "users".
            //se busca con la ayuda de linq. un ID que sea igual al que me estan pasando por parametro.
            var user = users.Find(u => u.Id == userID);

            var roleID = Request["RoleID"];

            //Creo el objeto de usaurios que ira a la vista.
            var userView = new UserView
            {
                EMail = user.Email,
                Name = user.UserName,
                UserID = user.Id,
            };



            if (String.IsNullOrEmpty(roleID))
            {
                ViewBag.Error = "Debes seleccionar un rol";
                var List = roleManager.Roles.ToList();
                List.Add(new IdentityRole { Id = "", Name = "[Seleccione un role]" });
                List = List.OrderBy(r => r.Name).ToList();
                ViewBag.RoleID = new SelectList(List, "Id", "Name");

                return View(userView);
            }


            //Armo la lista y busco el id del role para poder enviarlo por parametro.
            var roles = roleManager.Roles.ToList();
            var role = roles.Find(r => r.Id == roleID);

            if (!userManager.IsInRole(userID, role.Name))
            {
                //Adicionando el rol.
                userManager.AddToRole(user.Id, role.Name);
            }


            var rolesView = new List<RoleView>();

            //user -> es el usuario del cual se envio el ID y su propiedad Roles mme regresa los roles a los cuales pertenece.
            foreach (var item in user.Roles)
            {

                //Antes de crear el objeto necesito el nombre del rol, el cual se obtiene gracias al linq
                 role = roles.Find(r => r.Id == item.RoleId);

                //Creo un objeto de los roles que ira a la vista.
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    RoleName = role.Name
                };

                //Agrego el objeto que estoy creando a mi lista de roles que seran enviados a la vista.
                rolesView.Add(roleView);
            }



            //Cuarto creo el objeto de usaurios que ira a la vista.
            userView = new UserView
            {
                EMail = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                //Envio una lista de roles existentes.
                Roles = rolesView

            };

            return View("Roles",userView);

        }



        public ActionResult Delete(String userID, String roleID)
        {

            if (String.IsNullOrEmpty(userID) || String.IsNullOrEmpty(roleID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            //Variable para manipular los roles.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //Variable para manipular los usuarios
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //Buscando el rol y el usuarios.
            var user = userManager.Users.ToList().Find(u => u.Id == userID);
            var role = roleManager.Roles.ToList().Find(r => r.Id == roleID);


            if (userManager.IsInRole(user.Id, role.Name))
            {

                userManager.RemoveFromRole(user.Id, role.Name);

            }


            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            //Tercero recorro los roles para saber a cuales de ellos pertenece mi usuraio y creo una lista de los roles 
            //para mostrar en la vista..
            var rolesView = new List<RoleView>();

            //user -> es el usuario del cual se envio el ID y su propiedad Roles mme regresa los roles a los cuales pertenece.
            foreach (var item in user.Roles)
            {

                //Antes de crear el objeto necesito el nombre del rol, el cual se obtiene gracias al linq
                 role = roles.Find(r => r.Id == item.RoleId);

                //Creo un objeto de los roles que ira a la vista.
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    RoleName = role.Name
                };

                //Agrego el objeto que estoy creando a mi lista de roles que seran enviados a la vista.
                rolesView.Add(roleView);
            }



            //Cuarto creo el objeto de usaurios que ira a la vista.
            var userView = new UserView
            {
                EMail = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                //Envio una lista de roles existentes.
                Roles = rolesView

            };


            return View("Roles",userView);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}