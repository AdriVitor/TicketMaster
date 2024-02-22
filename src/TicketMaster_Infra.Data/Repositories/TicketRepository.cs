using Microsoft.EntityFrameworkCore;
using TicketMaster_Domain.Entities;
using TicketMaster_Infra.Data.Context;
using TicketMaster_Infra.Data.Interfaces;

namespace TicketMaster_Infra.Data.Repositories {
    public class TicketRepository : ITicketRepository {
        private readonly AppDbContext _context;
        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Ticket ticket) {
            await _context.Ticket.AddAsync(ticket);
        }

        public async Task Delete(Ticket ticket) {
            _context.Ticket.Remove(ticket);
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsActives(int CustomerId) {
            IEnumerable<Ticket> tickets = await _context.Ticket.Where(t => t.CustomerId == CustomerId && t.FlagConsumed == false).ToListAsync();
            return tickets;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsNotActives(int CustomerId) {
            IEnumerable<Ticket> tickets = await _context.Ticket.Where(t => t.CustomerId == CustomerId && t.FlagConsumed == true).ToListAsync();
            return tickets;
        }

        public async Task<Ticket> GetById(int id) {
            var ticket = await _context.Ticket.FirstOrDefaultAsync(t => t.Id == id);
            return ticket;
        }

        public async Task Update(Ticket ticket) {
            _context.Ticket.Update(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
