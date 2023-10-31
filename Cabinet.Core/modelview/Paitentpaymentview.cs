using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet.Core.Models;

namespace Cabinet.Core.modelview
{
	public  class Paitentpaymentview
	{
		
		public int PatientID { get; set; }
		//The first name of the patient
		public string FirstName { get; set; }
		//The last name of the patient
		public string LastName { get; set; }
		//The CIN of the patient
		public string? CIN { get; set; }
		//The date of birth of the patient
		public DateTime? DateOfBirth { get; set; }
		//The gender of the patient
		public string? Gender { get; set; }
		//The contact number of the patient
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
		public int Idpayment { get; set; }
		public int packid { get; set; }
		public int Amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public string PaymentStatus { get; set; }
		public int change { get; set; }
		public TypePack typePacks { get; set; }
		public Payment payments { get; set; }
		//public Pack Pack { get; set; }

	}
}
