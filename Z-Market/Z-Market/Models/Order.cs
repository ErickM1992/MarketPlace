using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public DateTime DateOrder { get; set; }

        public int CustomerID { get; set; }//ID de la tabla de relacion de customer.

        public OrderStatus OrderStatus { get; set; }//Clase enum.

        public virtual Customer Customer { get; set; } //Relacion de uno a varios.
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }//Lado varios de la clase.


    }
}