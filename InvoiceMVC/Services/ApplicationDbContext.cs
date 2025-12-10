using InvoiceMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceMVC.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //tabela
        public DbSet<Invoice> Invoices { get; set; } = null!;

        public DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;
    }
}
