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
    public class manufacturar
    {
        [Key]
        public int manufacturarId { get; set; }
        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        //[Remote("IsTagAvailble", "MyController", ErrorMessage = "Tag Already Exist.")]
        [DisplayName("Manufacturar")]
        public string vehiclemanufacturar { get; set; }

        public List<brand> brands { get; set; }
    }
}
