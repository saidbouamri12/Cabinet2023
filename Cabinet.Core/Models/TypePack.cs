using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class TypePack
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string label { get; set; }
        [Required]
        public int prix { get; set; }

		public ICollection<Pack> Pack { get; set; }

	}
}
