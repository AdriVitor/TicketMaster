using TicketMaster_Domain.Entities;

namespace TicketMaster_Infra.Data.Interfaces;
public interface IProducerRepository {
    public Task<Producer> GetById(int id);
    public Task Add(Producer producer);
    public Task Update(Producer producer);
    public Task Delete(Producer producer);
    public Task<string> GetEmailProducer(int ProducerId);
    public Task<string> GetNameProducer(int ProducerId);
}

