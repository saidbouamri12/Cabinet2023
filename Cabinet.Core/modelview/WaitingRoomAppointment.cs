using Cabinet.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.modelview
{
	public class WaitingRoomAppointment
	{
		public int PatientID { get; set; }
		//The status of the patient's check-in (e.g., checked-in, called)
		public string CheckInStatus { get; set; }

		// 2 attribut in class appoi and WaitingRoom
		public DateTime AppointmentDate { get; set; }

		public TimeSpan AppointmentTime { get; set; } 

		public string AppointmentStatus { get; set; }

		public string Notes { get; set; }

		public string AppointmentType { get; set; }
	}




}

