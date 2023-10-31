using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class LabResult
    {
        //A unique identifier for each lab result
        public int ResultID { get; set; }
        //A reference to the patient associated with the lab result
        public int PatientID { get; set; }
        //The name or code of the specific lab test
        public string TestName { get; set; }
        //The date when the lab test was conducted
        public DateTime TestDate { get; set; }
        //The numerical or qualitative result of the lab test
        public string TestResult { get; set; }
        //The reference range for the specific lab test
        public string ReferenceRange { get; set; }
        //The unit of measurement for the lab test result
        public string Unit { get; set; }
        //Additional notes or comments related to the lab test result
        public string Notes { get; set; }
    }
}
