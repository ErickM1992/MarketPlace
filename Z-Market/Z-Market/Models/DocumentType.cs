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
        public String Description { get; set; }

        public virtual ICollection<Employee> Employees{ get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

    }
}