using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TicketMaster_Domain.Entities;
using TicketMaster_Domain.Entities.Enums;
using TicketMaster_Infra.Data.Context;
using TicketMaster_Infra.Data.Interfaces;

namespace TicketMaster_Infra.Data.Repositories {
    public class EventRepository : IEventRepository {
        private readonly AppDbContext _context;
        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Event @event) {
            await _context.Event.AddAsync(@event);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Event @event) {
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetByFederativeUnit(EnumFederativeUnit federativeUnit) {
            IEnumerable<Event> eventsList = await _context.Event.Where(e => e.FederativeUnit == federativeUnit).ToListAsync();
            return eventsList;
        }

        public async Task<Event> GetById(int id) {

            var query = await (from e in _context.Event
                                where e.Id == id
                                select new {
                                    e.Id, 
                                    e.ProducerId, 
                                    e.Name, 
                                    e.Description, 
                                    e.FederativeUnit, 
                                    e.Date, 
                                    e.TotalAmount
                                }).FirstOrDefaultAsync();

            if(query == null){
                throw new Exception("Nenhum evento foi encontrado");
            }

            return Event.Create(query.Id, query.ProducerId, query.Name, query.Description, query.FederativeUnit, query.Date, query.TotalAmount);
        }

        public async Task Update(Event @event) {
            _context.Event.Update(@event);
            await _context.SaveChangesAsync();
        }
    }
}
