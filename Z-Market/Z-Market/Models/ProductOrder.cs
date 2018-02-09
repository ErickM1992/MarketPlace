using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public class ProductOrder:Product
    {

        /*Clase que hereda las propiedades del modelo product y ademas le agrega mas propiedades*/

        [Display(Name = "Cantidad")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Es necesario llenar el campo {0} para continuar")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public float Quantity { get; set; }


        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Valor { get { return price * (decimal)Quantity; } }//Castea el valor de Quatity a decimal para hacer los calculos.



    }
}