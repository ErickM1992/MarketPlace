using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        public int OrderID { get; set; }//ID de ordenes

        public int ID { get; set; }//ID de Productos.

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Es necesario llenar el campo {0} para continuar")]
        public String Description { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Es necesario llenar el campo {0} para continuar")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode =false)]
        public decimal price { get; set; }

        [Display(Name = "Cantidad")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Es necesario llenar el campo {0} para continuar")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Quantity { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }



    }
}