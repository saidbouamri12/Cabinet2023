using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class Appointment
    {
        //A unique identifier for each appointment
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentID { get; set; }
        //A reference to the Pack associated with the appointment
        [ForeignKey("Pack")]
        public int packid { get; set; }
        [Required]
		//The date of the appointment
		public DateTime AppointmentDate { get; set; }
		[Required]
		//The start time of the appointment
		public TimeSpan AppointmentTime { get; set; }
		[Required]
		//The status of the appointment (e.g., scheduled, confirmed, canceled)
		public string AppointmentStatus { get; set; }
		

        public string? Notes { get; set; }
		[NotMapped]
		public Patient patient { get; set; }
        public virtual Pack Pack { get; set; }
		public ICollection<WaitingRoom> WaitingRoom { get; set; }
		public ICollection<Consultation> Consultation { get; set; }
	}
}
