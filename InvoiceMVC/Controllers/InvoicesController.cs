using InvoiceMVC.Models;
using InvoiceMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceMVC.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext context;

        public InvoicesController(ApplicationDbContext context)
        {
            this.context = context;
        }



        // carrega do banco as faturas e exibe na view por ordem de id
        public IActionResult Index()
        {
            var invoices = context.Invoices.Include(inv => inv.InvoiceItems)
                .OrderByDescending(inv => inv.Id).ToList();

            return View(invoices);
        }


        //prepara a visualização do formulário de criação de uma nova fatura
        public IActionResult Create()
        {
            var invoiceDto = new InvoiceDto();
            invoiceDto.InvoiceItems.Add(new InvoiceItem());
            return View(invoiceDto);
        }

        //cria a fatura
        [HttpPost]
        public IActionResult Create(InvoiceDto invoiceDto)
        {
            if (invoiceDto.InvoiceItems.Count < 1)
            {
                ModelState.AddModelError("", "The Invoice must have at least one service");
            }

            if (!ModelState.IsValid)
            {
                return View(invoiceDto);
            }

            // cria um novo serviço
            var invoice = new Invoice
            {
                Number = invoiceDto.Number,
                Status = invoiceDto.Status,
                IssueDate = invoiceDto.IssueDate,
                DueDate = invoiceDto.DueDate,

                InvoiceItems = invoiceDto.InvoiceItems,

                ClientName = invoiceDto.ClientName,
                Email = invoiceDto.Email,
                Phone = invoiceDto.Phone,
                Address = invoiceDto.Address ?? "",
            };

            context.Invoices.Add(invoice);
            context.SaveChanges();

            return RedirectToAction("Index");
        }



        // carrega a página de edição

        public IActionResult Edit(int id)
        {
            var invoice = context.Invoices.Include(inv => inv.InvoiceItems).FirstOrDefault(inv => inv.Id == id);
            if (invoice == null)
            {
                return RedirectToAction("Index");
            }

            var invoiceDto = new InvoiceDto
            {
                Number = invoice.Number,
                Status = invoice.Status,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,

                InvoiceItems = invoice.InvoiceItems,

                ClientName = invoice.ClientName,
                Email = invoice.Email,
                Phone = invoice.Phone,
                Address = invoice.Address,
            };

            ViewBag.InvoiceId = invoice.Id;

            return View(invoiceDto);
        }


        // recebe os dados editados

        [HttpPost]
        public IActionResult Edit(int id, InvoiceDto invoiceDto)
        {
            var invoice = context.Invoices.Include(inv => inv.InvoiceItems).FirstOrDefault(inv => inv.Id == id);
            if (invoice == null)
            {
                return RedirectToAction("Index");
            }

            if (invoiceDto.InvoiceItems.Count < 1)
            {
                ModelState.AddModelError("", "The Invoice must have at least one service");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.InvoiceId = invoice.Id;
                return View(invoiceDto);
            }

            // edit the invoice
            invoice.Number = invoiceDto.Number;
            invoice.Status = invoiceDto.Status;
            invoice.IssueDate = invoiceDto.IssueDate;
            invoice.DueDate = invoiceDto.DueDate;

            invoice.InvoiceItems.Clear();
            invoice.InvoiceItems = invoiceDto.InvoiceItems;

            invoice.ClientName = invoiceDto.ClientName;
            invoice.Email = invoiceDto.Email;
            invoice.Phone = invoiceDto.Phone;
            invoice.Address = invoiceDto.Address ?? "";

            context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var invoice = context.Invoices.Find(id);
            if (invoice != null)
            {
                context.Invoices.Remove(invoice);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
