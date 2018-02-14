using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public class OrderApi
    {

        [Key]
        public int OrderID { get; set; }

        public DateTime DateOrder { get; set; }

        public Customer Customer { get; set; }//ID de la tabla de relacion de customer.

        public OrderStatus OrderStatus { get; set; }//Clase enum.

        public ICollection<OrderDetail> Details { get; set; }


    }
}