using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public class Category
    {

        [Key]
        public int CategoryID { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Debes seleccionar un documento")]
        [StringLength(30, ErrorMessage = "La descripcion debe ser de 30 caracteres.")]
        public String Description { get; set; }


    }
}