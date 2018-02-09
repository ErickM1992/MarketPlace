using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Z_Market.ViewModels
{
    public class UserView
    {
        
        public String UserID { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public String EMail { get; set; }
        
        //Campo para hacer referencia al ID del rol en la vista.
        public RoleView Role { get; set; }

        //No hay relacion aqui ya que estos tipos de modelos no van para la base de datos, sino que son utilizados para
        //generar un tipo de vista.
        public List<RoleView> Roles { get; set; }

    }
}