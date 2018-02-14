using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Display(Name ="Nombre")]
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [StringLength(maximumLength:30, ErrorMessage ="El {0} debe tener entre 3 y 30 caracteres",MinimumLength =3)]
        public String Name { get; set; }

        [Display(Name = "Nombre Contacto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 30, ErrorMessage = "El {0} debe tener entre 3 y 30 caracteres", MinimumLength = 3)]
        public String ContacFirtsName { get; set; }

        [Display(Name = "Apellido Contacto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 30, ErrorMessage = "El {0} debe tener entre 3 y 30 caracteres", MinimumLength = 3)]
        public String ContactLastName { get; set; }

        [Display(Name="Telefono")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String Address { get; set; }

        [Display(Name = "Correo Electrónico")]
        [DataType(DataType.EmailAddress)]
        public String EMail { get; set; }

        //Relacion de muchos.
        [JsonIgnore]
        public virtual ICollection<SupplierProduct> SupplierProduct { get; set; }

    }
}