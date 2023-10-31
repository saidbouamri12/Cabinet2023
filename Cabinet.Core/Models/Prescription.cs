using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class Prescription
    {
        //A unique identifier for each prescribed medication/treatment
        public int PrescriptionID { get; set; }
        //A reference to the consultation associated with the prescription
        public int ConsultationID { get; set; }
        //The name of the prescribed medication or treatment
        public string MedicationProcedure { get; set; }
        //The recommended dosage or instructions for the medication/treatment
        public string Dosage { get; set; }
        //The frequency at which the medication/treatment should be taken or performed
        public string Frequency { get; set; }
        //The frequency at which the medication/treatment should be taken or performed
        public string Notes { get; set; }
    }
}
