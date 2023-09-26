
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Tescat.Models
{
    public class User
    {
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Debes ingresar un nombre")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un area")]
        public string? Area { get; set; }

        //[Required(ErrorMessage = "Debe seleccionar un puesto")]
        public string? Position { get; set; }

        public DateTime? EntryDate { get; set; }

        public int? Tel { get; set; }

        public bool? WebPrivileges { get; set; }

        public DateTime? LastModif { get; set; }

        public virtual Email? Email { get; set; }

        public virtual ICollection<Pc> Pcs { get; set; } = new List<Pc>();

        public virtual UserCredential? UserCredential { get; set; }

        public string? Dept { get; set; }
        public string? Office { get; set; }
        public DateTime? LastWorkingDate { get; set; }
        public int? TelKey { get; set; }
        public string? Cel { get; set; }

        public string? IMAGE_NAME { get; set; }
    }
}