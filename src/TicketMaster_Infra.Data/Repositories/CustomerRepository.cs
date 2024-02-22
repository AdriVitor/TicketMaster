using Microsoft.EntityFrameworkCore;
using TicketMaster_Domain.Entities;
using TicketMaster_Infra.Data.Context;
using TicketMaster_Infra.Data.Interfaces;

namespace TicketMaster_Infra.Data.Repositories {
    public class CustomerRepository : ICustomerRepository {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context) {
            _context = context;
        }

        public async Task Add(Customer customer) {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Customer customer) {
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetById(int id) {
            var customer = await _context.Customer.FirstOrDefaultAsync(c=>c.Id == id);
            return customer;
        }

        public async Task Update(Customer customer) {
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
