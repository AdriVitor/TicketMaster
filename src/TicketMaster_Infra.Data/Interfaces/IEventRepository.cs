using TicketMaster_Domain.Entities;
using TicketMaster_Domain.Entities.Enums;

namespace TicketMaster_Infra.Data.Interfaces;
public interface IEventRepository {
    public Task<Event> GetById(int id);
    public Task<IEnumerable<Event>> GetByFederativeUnit(EnumFederativeUnit federativeUnit);
    public Task Add(Event @event);
    public Task Update(Event @event);
    public Task Delete(Event @event);
}

