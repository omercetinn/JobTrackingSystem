using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }    
        public bool Status { get; set; }
        public  virtual Department? Departments { get; set; }
    }
}
