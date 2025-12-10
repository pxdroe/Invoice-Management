using System.ComponentModel.DataAnnotations;

namespace InvoiceMVC.Models
{
    public class InvoiceDto
    {
        [Required]
        public string Number { get; set; } = "";
        [Required]
        public string Status { get; set; } = ""; // Paid or Pending
        public DateTime? IssueDate { get; set; }
        public DateTime? DueDate { get; set; }

        // services details
        public List<InvoiceItem> InvoiceItems { get; set; } = [];

        // client details
        [Required, Display(Name = "Client Name")]
        public string ClientName { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required, Phone]
        public string Phone { get; set; } = "";

        public string? Address { get; set; }
    }
}
