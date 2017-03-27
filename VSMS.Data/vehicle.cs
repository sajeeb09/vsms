using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSMS.Data
{   
    public class vehicle
    {
        [Key]
        public int vehicleId { get; set; }
        [Required]
        [DisplayName("Model")]
        public string model { get; set; }
        [Required]
        [DisplayName("Chasis #")]
        public string chasisNo { get; set; }
        [Required]
        [DisplayName("Brand")]
        public string vehiclebrand { get; set; }
        [Required]
        [DisplayName("Make")]
        public string vehiclemanufacturar { get; set; }
        [Required]
        [DisplayName("Make Year")]
        [RegularExpression(@".*\d{4}.*", ErrorMessage = "Please enter a 4 digit year")]
        public string mYear { get; set; }
        [Required]
        [DisplayName("Color")]
        public string color { get; set; }
        [Required]
        [DisplayName("Engine Number")]
        public string engineNo { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Adding Date")]
        public DateTime bdate { get; set; }

        [Required]
        [DisplayName("Buying Price $ ")][Range(0, 999999999)]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Invalid Price Given!")]
        public decimal bprice { get; set; }
        public decimal sprice { get; set; }
        public decimal price { get; set; }
    }
}
