using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class Pack
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		[ForeignKey("TypePack")]
		public int idtypepack { get; set; }
		[ForeignKey("Patient")]
		public int idpatient { get; set; }
		[ForeignKey("ApplicationUser")]
		public string doctorid { get; set; }

		public virtual TypePack TypePack { get; set; }

		public virtual Patient Patient { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }
		public ICollection<Payment> Payments { get; set; }

	}
}
