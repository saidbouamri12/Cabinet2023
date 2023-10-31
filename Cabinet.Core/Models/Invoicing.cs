using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Core.Models
{
    public class Invoicing
    {
        //A unique identifier for each invoice
        public int InvoiceID { get; set; }
        //A reference to the patient associated with the invoice
        public int PatientID { get; set; }
        //The date when the invoice is generated
        public DateTime InvoiceDate { get; set; }
        //The total amount due for the invoice
        public decimal TotalAmount { get; set; }
      
        //The method used for payment (e.g., cash, credit card)
        public string PaymentMethod { get; set; }
        //The status of payment for the invoice (e.g., paid, unpaid)
        public string PaymentStatus { get; set; }
        //The due date for payment of the invoice
        public DateTime DueDate { get; set; }
        //The due date for payment of the invoice
        public string Description { get; set; }
    }
}
