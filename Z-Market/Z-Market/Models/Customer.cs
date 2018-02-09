using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public class Customer
    {

        [Key]
        public int CustomerID  { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 30, ErrorMessage = "El {0} debe tener entre 3 y 30 caracteres", MinimumLength = 3)]
        public String FirtsName { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 30, ErrorMessage = "El {0} debe tener entre 3 y 30 caracteres", MinimumLength = 3)]
        public String LastName { get; set; }

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String Address { get; set; }

        [Display(Name = "Correo Electrónico")]
        [DataType(DataType.EmailAddress)]
        public String EMail { get; set; }


        [Display(Name = "Documento")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 20, ErrorMessage = "El {0} debe tener entre 5 y 20 caracteres", MinimumLength = 5)]
        public String Document { get; set; }

        [NotMapped]
        public String FullName { get { return String.Format("{0} {1}", FirtsName, LastName); }} //Combinacion de propiedades tipo String.

        public int DocumentTypeId { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}