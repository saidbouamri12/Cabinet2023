using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class Medication
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //A unique identifier for each medication
        public int MedicationID { get; set; }
        [Required]
        //The name of the medication
        public string MedicationName { get; set; }
        //A brief description or information about the medication
        [Required]
        public string Description { get; set; }
        //	The recommended dosage or instructions for taking the medication
        [Required]
        public string Dosage { get; set; }
        //The route of administration for the medication (e.g., oral, topical, injection)
        [Required]
        public string Route { get; set; }
        [Required]
        //The frequency at which the medication should be taken
        public string Frequency { get; set; }
        // Additional notes or comments related to the medication
        public string Notes { get; set; }
    }
}
