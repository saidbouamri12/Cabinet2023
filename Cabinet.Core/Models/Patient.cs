using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class Patient
    {
        //A unique identifier for each patient
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientID { get; set; }
        //The first name of the patient
        [Required]
        public string FirstName { get; set; }
        //The last name of the patient
        [Required]
        public string LastName { get; set; }
        //The CIN of the patient
        public string? CIN { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
		//The date of birth of the patient
		public DateTime? DateOfBirth { get; set; }
        //The gender of the patient
        [Required]
        public string? Gender { get; set; }
        //The contact number of the patient
        [Required]
        public string ContactNumber { get; set; }
        //The contact number of the patient
        public string? ContactNumber2 { get; set; }
        //The email address of the patient
        public string? Email { get; set; }
        //The residential address of the patient
        public string? Address { get; set; }
        //Comprehensive medical history and relevant information
        public string? MedicalHistory { get; set; }
        //Any allergies or sensitivities the patient may have
        public string? Allergies { get; set; }
        //Any chronic conditions or illnesses the patient may have
        public string? ChronicConditions { get; set; }
        //Name of the parent or guardian if the patient is a child
        public string? ParentGuardian { get; set; }

		public ICollection<Pack> Pack { get; set; }
		

	}
}
