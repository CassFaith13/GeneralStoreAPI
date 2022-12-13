using GeneralStoreAPI.Data;
using GeneralStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private GeneralStoreDbContext _db;
        public TransactionController(GeneralStoreDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactions([FromForm] TransactionEdit newTransaction)
        {
            Transaction transaction = new Transaction()
            {
                ProductId = newTransaction.ProductId,
                CustomerId = newTransaction.CustomerId,
                Quantity = newTransaction.Quantity,
                DateOfTransaction = newTransaction.DateOfTransaction
            };

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _db.Transactions.ToListAsync();
            return Ok(transactions);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetTransactionByID(int id)
        {
            var transaction = await _db.Transactions.FindAsync(id);

            if (transaction is null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, TransactionEdit updateTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactionInDB = await _db.Transactions.FindAsync(id);

            if (transactionInDB is null)
            {
                return NotFound();
            }

            transactionInDB.ProductId = updateTransaction.ProductId;
            transactionInDB.CustomerId = updateTransaction.CustomerId;
            transactionInDB.Quantity = updateTransaction.Quantity;

            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transactionInDB = await _db.Transactions.FindAsync(id);

            if(transactionInDB is null)
            {
                return NotFound();
            }

            _db.Transactions.Remove(transactionInDB);

            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}