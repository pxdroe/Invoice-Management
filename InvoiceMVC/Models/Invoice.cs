using Microsoft.EntityFrameworkCore;

namespace InvoiceMVC.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public string Number { get; set; } = "";
        public string Status { get; set; } = ""; // Paid or Pending
        public DateTime? IssueDate { get; set; }
        public DateTime? DueDate { get; set; }

        // service details
        public List<InvoiceItem> InvoiceItems { get; set; } = [];
        /*
        public string Service { get; set; } = "";
        [Precision(16, 2)]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }*/

        // client details
        public string ClientName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
    }
}
