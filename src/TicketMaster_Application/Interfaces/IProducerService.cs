using TicketMaster_Application.DTOs.Producer;
using TicketMaster_Domain.Entities;

namespace TicketMaster_Application.Interfaces;
public interface IProducerService {
    public Task<Producer> GetById(int id);
    public Task Add(ProducerDTO producerDTO);
    public Task Update(ProducerDTO producerDTO);
    public Task Delete(int id);
}

