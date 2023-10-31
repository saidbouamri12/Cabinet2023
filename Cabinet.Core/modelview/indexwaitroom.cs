using Cabinet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.modelview
{
	public class indexwaitroom
	{
		public int WaitingRoomID { get; set; }
		public int AppointmentID { get; set; }
		public int packid { get; set; }
		public string namepack { get; set; }	
		public string doctorid { get; set; }
		public int PatientID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string CheckInStatus { get; set; }
		public DateTime AppointmentDate { get; set; }
		public TimeSpan AppointmentTime { get; set; }
		public string AppointmentStatus { get; set; }
		//public string AppointmentType { get; set; }
		public string Statuspaitent { get; set; }
	}
}
