using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class MedicationConsultation
    {
        //A unique identifier for each medication-consultation relationship
        public int MedicationConsultationID { get; set; }
        //A reference to the medication associated with the consultation
        public int MedicationID { get; set; }
        //A reference to the consultation associated with the medication
        public int ConsultationID { get; set; }
    }
}
