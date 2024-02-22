using TicketMaster_Domain.Entities;

namespace TicketMaster_Infra.Data.Interfaces;
public interface ITicketRepository {
    public Task<Ticket> GetById(int id);
    public Task<IEnumerable<Ticket>> GetAllTicketsActives(int CustomerId);
    public Task<IEnumerable<Ticket>> GetAllTicketsNotActives(int CustomerId);
    public Task Add(Ticket ticket);
    public Task Update(Ticket ticket);
    public Task Delete(Ticket ticket);
}

