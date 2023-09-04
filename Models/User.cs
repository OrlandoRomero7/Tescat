
namespace Tescat.Models
{
    public class User
    {
        public int IdUser { get; set; }

        public string? Name { get; set; } 


        public string? Dept { get; set; } 


        public string? Position { get; set; } 


        public DateTime? EntryDate { get; set; }

        public string? Tel { get; set; }

        public bool? WebPrivileges { get; set; }

        public DateTime? LastModif { get; set; }

        public virtual Email? Email { get; set; }

        public virtual ICollection<Pc> Pcs { get; set; } = new List<Pc>();

        public virtual UserCredential? UserCredential { get; set; }
    }
}