using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Cata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; } // Fecha de la cata

        [Required]
        public string Username { get; set; } // Usuario organizador

        [Required]
        public string Wines { get; set; } // Vinos (almacenados como cadena separada por comas)

        public string Invitados { get; set; } // Invitados (almacenados como cadena separada por comas)
    }
}
