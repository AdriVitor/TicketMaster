using TicketMaster_Application.DTOs.Customer;
using TicketMaster_Domain.Entities;

namespace TicketMaster_Application.Interfaces;
public interface ICustomerService {
    public Task<Customer> GetById(int id);
    public Task<Customer> Add(CustomerDTO customerDTO);
    public Task<Customer> Update(CustomerDTO customerDTO);
    public Task Delete(int id);
}

