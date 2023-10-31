using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class WaitingRoom
    {
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int WaitingRoomID { get; set; }
		[ForeignKey("Appointment")]
		//A unique identifier for each appointment
		
		public int AppointmentID { get; set; }
        //The status of the patient's check-in (e.g., checked-in, called, calceled)
        [Required]
        public string CheckInStatus { get; set; }
        //The scheduled appointment time for the patient
        [Required]
        public DateTime AppointmentTime { get; set; }
        [Required]
        public string? Notes { get; set; }
		//dont have appointment ,, danger ,, have appointment 
		public string Statuspaitent { get; set; }
		[NotMapped]
		public virtual Appointment Appointment { get; set; }
		
	}
}
