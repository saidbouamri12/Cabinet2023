using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public  class ApplicationUser : IdentityUser
    {
        public string fullname { get; set; }
		//public string username { get; set; }
		public ICollection<Pack> Pack { get; set; }


	}
}
