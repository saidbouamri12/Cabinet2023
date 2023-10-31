using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		[ForeignKey("Pack")]
		public int packid { get; set; }
        [Required]
        public int Amount { get; set; }
        
        public DateTime PaymentDate { get; set; }
		
		
        public virtual Pack Pack { get; set; }
    }
}
