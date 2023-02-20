using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Invoice_API.Controllers.Data;

namespace Invoice_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly DataContext _context;
        public InvoiceController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Invoices>>> GetInvoices()
        {
            return Ok(await _context.Invoices.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Invoices>>> CreateInvoice(Invoices Invoice)
        {
            _context.Invoices.Add(Invoice);
            await _context.SaveChangesAsync();
            return Ok(await _context.Invoices.ToListAsync());

        }
        [HttpPut]
        public async Task<ActionResult<List<Invoices>>> UpdateInvoice(Invoices Invoice)
        {
            var dbInvoice = await _context.Invoices.FindAsync(Invoice.Id);
            if (dbInvoice == null)
                return BadRequest("Invoice not Found");

            dbInvoice.Date = Invoice.Date;
            dbInvoice.Statuses = Invoice.Statuses;
            dbInvoice.Amount = Invoice.Amount;
            await _context.SaveChangesAsync();
            return Ok(await _context.Invoices.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Invoices>>> DeleteInvoice(int id)
        {
            var dbInvoice = await _context.Invoices.FindAsync(id);
            if (dbInvoice == null)
                return BadRequest("Invoice not Found");

            _context.Invoices.Remove(dbInvoice);
            await _context.SaveChangesAsync();
            return Ok(await _context.Invoices.ToListAsync());
        }
    }
}
