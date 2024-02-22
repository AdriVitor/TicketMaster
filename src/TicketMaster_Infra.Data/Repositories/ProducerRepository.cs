using Microsoft.EntityFrameworkCore;
using TicketMaster_Domain.Entities;
using TicketMaster_Infra.Data.Context;
using TicketMaster_Infra.Data.Interfaces;

namespace TicketMaster_Infra.Data.Repositories {
    public class ProducerRepository : IProducerRepository {
        private readonly AppDbContext _context;
        public ProducerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Producer producer) {
            await _context.Producer.AddAsync(producer);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Producer producer) {
            _context.Producer.Remove(producer);
            await _context.SaveChangesAsync();
        }

        public async Task<Producer> GetById(int id) {
            var producer = await _context
                                .Producer
                                .FirstOrDefaultAsync(p=>p.Id == id);
            return producer;
        }

        public async Task Update(Producer producer) {
            _context.Producer.Update(producer);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetEmailProducer(int ProducerId) {
            var producer = await GetById(ProducerId);

            return producer.Email;
        }

        public async Task<string> GetNameProducer(int ProducerId) {
            var producer = await GetById(ProducerId);

            return producer.Email;
        }
    }
}
