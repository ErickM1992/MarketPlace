using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Z_Market.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage ="Es necesario llenar el campo {0} para continuar")]
        public String Description { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Es necesario llenar el campo {0} para continuar")]
        public decimal price { get; set; }

        [Required(ErrorMessage = "Es necesario llenar el campo {0} para continuar")]
        [Display(Name = "Ultima Compra")]
        [DataType(DataType.Date)]
        public DateTime LastBuy { get; set; }

        [Required(ErrorMessage = "Es necesario llenar el campo {0} para continuar")]
        [Display(Name = "Cantidad")]
        [DataType(DataType.Text)]
        public float Stock { get; set; }

        [Required(ErrorMessage = "Es necesario llenar el campo {0} para continuar")]
        [Display(Name = "Procedencia")]
        [DataType(DataType.MultilineText)]
        public String Remarks { get; set; }

        //Relacion de muchos con la tabla supplier
        [JsonIgnore]
        public virtual ICollection<SupplierProduct> SupplierProduct { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}