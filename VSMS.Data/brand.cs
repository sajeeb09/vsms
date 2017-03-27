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
    public class brand
    {
        [Key]
        public int brandId { get; set; }
        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        [DisplayName("Brand Name")]
        public string vehiclebrand { get; set; }
        [Required]
        [DisplayName("Manufacturar")]
        public int manufacturarId { get; set; }

        [ForeignKey("manufacturarId")]
        public manufacturar manufacturar { get; set; }

    }
}
