using ForgAkademija.Models;
using Microsoft.AspNetCore.Identity;

namespace ForgAkademija.Infrastructure
{
    public interface IBillRepository
    {
        Bill ReturnBill(int id, string userId);
        Bill CheckBill(int id, string userId);
        void InsertBill(Bill bill, string userId);
        void DeleteBill(Bill bill);
    }
}
