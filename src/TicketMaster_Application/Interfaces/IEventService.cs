using TicketMaster_Domain.Entities.Enums;
using TicketMaster_Domain.Entities;
using TicketMaster_Application.DTOs.Event;

namespace TicketMaster_Application.Interfaces;
public interface IEventService {
    public Task<EventReadDTO> GetById(int id);
    public Task<IEnumerable<EventReadDTO>> GetByFederativeUnit(EnumFederativeUnit federativeUnit);
    public Task Add(EventCreateDTO eventCreateDTO);
    public Task Update(EventCreateDTO eventCreateDTO);
    public Task Delete(int id);
    public Task<EventCreateDTO> GetByIdUpdate(int id);
}

