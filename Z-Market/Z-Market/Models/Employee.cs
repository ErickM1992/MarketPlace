using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Z_Market.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Display(Name ="Nombre")]
        [Required(ErrorMessage ="Se necesita el {0} para continuar")]
        [StringLength(30, ErrorMessage ="El campo {0} debe de tener entre 3 a 30 caracteres", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Se necesita el {0} para continuar")]
        [StringLength(30, ErrorMessage = "El campo {0} debe de tener entre 3 a 30 caracteres", MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Salario")]
        [Required(ErrorMessage = "Se necesita el {0} para continuar")]
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode = false)]//Aplica un formato al valor con el formato de moneda de la la pc actual
        public decimal Salary { get; set; }                                     // y dos decimales.

        [Display(Name = "Procentaje")]
        [Required(ErrorMessage = "Se necesita el {0} para continuar")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]//Aplica un formato al valor con el formato de moneda de la la pc actual y dos decimales.
        public float BonusPercent { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "Se necesita el {0} para continuar")]
        [DataType(DataType.Date)]//Indica que procede del formato fecha y no de hora.
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]//Aplica formato de fecha para validacion.
        public DateTime DayOfBirth { get; set; }

        [Display(Name = "Hora de Comienzo")]
        [Required(ErrorMessage = "Se necesita el {0} para continuar")]
        [DataType(DataType.Time)]//Indica que el campo procede del formato hora para que ponga solo la hora y no la fecha.
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime StarTime { get; set; }

        [Display(Name = "Correo Eléctronico")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Se necesita el {0} para continuar")]
        public string EMail { get; set; }

        [Display(Name = "Url")]
        [DataType(DataType.Url)]
        [Required(ErrorMessage = "Se necesita el {0} para continuar")]
        public string Url { get; set; }

        [Display(Name = "Tipo de documento")]
        public int DocumentTypeId { get; set; }

        [NotMapped]//Data anotation para que no se creado en la base de datos.
        public int Age { get { return DateTime.Now.Year - DayOfBirth.Year; } }

        //Relacion de 1 con el tipo de documento.
        public virtual DocumentType DocumentType { get; set; }

    }
}