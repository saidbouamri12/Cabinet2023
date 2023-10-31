using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class Consultation
    {
        //A unique identifier for each consultation
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConsultationID { get; set; }
		[ForeignKey("Appointment")]
		//A reference to the patient associated with the consultation
		public int AppointmentID { get; set; }
       
        //The date when the consultation took place
        public DateTime ConsultationDate { get; set; }
        //Detailed notes and observations from the consultation
        public string Title { get; set; }
        [Required]
        public string ConsultationNotes { get; set; }

        public virtual Appointment Appointment { get; set; }    
   
    }
}
