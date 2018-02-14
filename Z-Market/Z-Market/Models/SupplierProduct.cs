using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public class SupplierProduct
    {
        [Key]
        public int SupplierProductID { get; set; }
        //Es necesario el id de las tablas que se quieren referenciar.
        public int SupplierID { get; set; }
        public int ID { get; set; }

        //Relaciones de 1 a muchos. Esta tabla sale la realcion de 1.
        [JsonIgnore]
        public virtual Supplier Supplier { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}