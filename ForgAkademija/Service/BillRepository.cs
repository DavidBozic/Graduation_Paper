using ForgAkademija.Areas.Identity.Data;
using ForgAkademija.Infrastructure;
using ForgAkademija.Models;
using Microsoft.AspNetCore.Identity;

namespace ForgAkademija.Service
{
    public class BillRepository : IBillRepository
    {
        private readonly ApplicationDbContext _context;

        public BillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Bill ReturnBill(int id, string userId)
        {
            return _context.Bill.Where(r => r.billId == id && r.isDeleted == false && r.userId == userId).FirstOrDefault();
        }

        public void DeleteBill(Bill bill)
        {
            bill.isDeleted = true;
            _context.Bill.Update(bill);
            _context.SaveChanges();
        }

        public void InsertBill(Bill bill, string userId)
        {
            bill.isDeleted = false;
            bill.userId = userId;

            _context.Bill.Add(bill);
            _context.SaveChanges();
        }

        public Bill CheckBill(int id, string userId)
        {
            return _context.Bill.Where(r => r.courseId == id && r.isDeleted == false && r.userId == userId).FirstOrDefault() as Bill;
        }
    }
}
