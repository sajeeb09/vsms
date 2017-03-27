using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSMS.Data
{
    public class user
    {

        [Key]
        public int userId { get; set; }

        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        [DisplayName("User Name")]
        // [Index("IX_userName", 1, IsUnique = true)]
        public string userName { get; set; }
        [Required]
        [DisplayName("Password")]
        public string userPass { get; set; }
        [Required]
        [DisplayName("Role")]
        public string userType { get; set; }
        [Required]
        [DisplayName("Security Question")]
        public string sQuestion { get; set; }
        [Required]
        [DisplayName("Question Answer")]
        public string aQuestion { get; set; }
    }
}
