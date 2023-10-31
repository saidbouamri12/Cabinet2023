using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; } // A unique identifier for each doctor

        public string FirstName { get; set; } // The first name of the doctor

        public string LastName { get; set; } // The last name of the doctor

        public string Gender { get; set; } // The gender of the doctor

        public DateTime DateOfBirth { get; set; } // The date of birth of the doctor

        public string Specialty { get; set; } // The area of specialization or medical specialty of the doctor

        public string LicenseNumber { get; set; } // The license number of the doctor

        public string ContactNumber { get; set; } // The contact number of the doctor

        public string Email { get; set; } // The email address of the doctor

        public string Address { get; set; } // The address of the doctor
    }
}
