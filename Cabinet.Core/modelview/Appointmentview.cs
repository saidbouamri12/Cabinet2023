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
	public class Appointmentview
	{
		
		public int AppointmentID { get; set; }
		//A reference to the patient associated with the appointment
		
		public int PatientID { get; set; }
		//The date of the appointment
		public DateTime AppointmentDate { get; set; }
		//The start time of the appointment
		public TimeSpan AppointmentTime { get; set; }
		//The status of the appointment (e.g., scheduled, confirmed, canceled)
		public string AppointmentStatus { get; set; }
		//The type or reason for the appointment (e.g., check-up, follow-up, consultation)
		public string AppointmentType { get; set; }
		public Patient Patients { get; set; }

		public Payment payment { get; set; } 

		public TypePack TypePack { get; set; }

		public Consultation consultation { get; set; }


	}
}
