using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Z_Market.Models;

namespace Z_Market.ViewModels
{
    public class OrderView
    {
        public Customer Customer { get; set; }
        public ProductOrder Product { get; set; }//Usado solo para "Pintar" los titulos.
        public List<ProductOrder> Products { get; set; }//Aqui es donde se almacenan los datos finales.



    }
}