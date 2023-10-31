using Cabinet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.modelview
{
	public class packpatientview
	{
		public int Packid { get; set; }
		public Patient patient { get; set; }
		public ApplicationUser user { get; set; }
		public TypePack TypePack { get; set; }

		public Payment payment { get; set; }
		public List<Appointment> appointment { get; set; }

	}
}
