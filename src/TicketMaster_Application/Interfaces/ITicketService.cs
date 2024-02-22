using TicketMaster_Application.DTOs.Ticket;

namespace TicketMaster_Application.Interfaces;
public interface ITicketService {
    public Task<TicketReadDTO> GetById(int id);
    public Task<IEnumerable<TicketReadListDTO>> GetTicketsActives(int CustomerId);
    public Task<IEnumerable<TicketReadListDTO>> GetTicketsNotActives(int CustomerId);
    public Task Add(TicketCreateDTO ticketCreateDTO);
    public Task Update(TicketCreateDTO ticketCreateDTO);
    public Task Delete(int id);
}

