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
    public class customer
    {
        [Key]
        public int customerId { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string fName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string lName { get; set; }
        [Required]
        [DisplayName("Mobile Number")]
        public string mobile { get; set; }
        [Required]
        [DisplayName("Email")]
        public string email { get; set; }
        [Required]
        [DisplayName("Address")]
        public string address { get; set; }
        [Required][DisplayName("Insurance Comapny Name")]
        public string insuranceCompany { get; set; }
        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)][DisplayName("Insurance Id")]
        public string insuranceId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Warranty Start Date")]
        public DateTime warrentyS { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Warranty End Date")]
        public DateTime warrentyE { get; set; }

        [Required]
        [DisplayName("Vehicle ID")]
        public int vehicleId { get; set; }
        [Required]
        [DisplayName("Selling Price $")]
        [Range(0, 999999999)]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Invalid Selling Price Given!")]
        public decimal sprice { get; set; }


        [Required]
        [DisplayName("Payment Type")]
        public string paymentType { get; set; }
        [ForeignKey("vehicleId")]                       //DataAnnotation system e fk making
        public virtual vehicle vehicle { get; set; }


    }
}
