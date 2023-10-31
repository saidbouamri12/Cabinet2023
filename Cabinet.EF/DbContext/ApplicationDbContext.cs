using Cabinet.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.EF.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

		public DbSet<Patient> patients { get; set; }

        public DbSet<Appointment> Appointment { get; set; }

        public DbSet<WaitingRoom> WaitingRoom { get; set; }

        public DbSet<Pack> Packs { get; set; }

        public DbSet<TypePack> typePacks { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Consultation> Consultation { get; set; }
		public DbSet<IdentityRole> IdentityRoles { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           
        }
		
	}
}
