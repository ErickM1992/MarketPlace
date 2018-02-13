using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Z_Market.Models
{
    public class DocumentType
    {
        [Key]
        public int DocumentTypeId { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage ="Debes seleccionar un documento")]
        [StringLength(30, ErrorMessage ="La descripcion debe ser de 30 caracteres.")]
        public String Description { get; set; }

        public virtual ICollection<Employee> Employees{ get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

    }
}