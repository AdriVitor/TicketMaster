using TicketMaster_Domain.Entities;

namespace TicketMaster_Infra.Data.Interfaces;
public interface ICustomerRepository {
    public Task<Customer> GetById(int id);
    public Task Add(Customer customer);
    public Task Update(Customer customer);
    public Task Delete(Customer customer);
}

