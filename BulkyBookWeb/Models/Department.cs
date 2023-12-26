using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Department Name")]
        public string? Name { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
