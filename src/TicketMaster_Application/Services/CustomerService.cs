using AutoMapper;
using TicketMaster_Application.DTOs.Customer;
using TicketMaster_Application.Interfaces;
using TicketMaster_Domain.Entities;
using TicketMaster_Infra.Data.Interfaces;

namespace TicketMaster_Application.Services;
public class CustomerService : ICustomerService {
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IEmailService emailService) {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task<Customer> Add(CustomerDTO customerDTO) {
        var customer = _mapper.Map<Customer>(customerDTO);
        await _customerRepository.Add(customer);

        /*var SubjectAndBodyEmail = _emailService.DefaultSujectAndEmailNewUser(customerDTO.Name);
        var subjectEmail = SubjectAndBodyEmail.Keys.FirstOrDefault();
        var bodyEmail = SubjectAndBodyEmail.Values.FirstOrDefault();

        _emailService.SendEmail(customerDTO.Email, subjectEmail, bodyEmail);

        await SendEmailNewUser(customerDTO.Name, customerDTO.Email);*/

        _emailService.SendEmailNewUser(customerDTO.Name, customerDTO.Email);

        return customer;
    }

    public async Task Delete(int id) {
        var customer = await GetById(id);

        await _customerRepository.Delete(customer);
    }

    public Task<Customer> GetById(int id) {
        var customer = _customerRepository.GetById(id);

        if (customer is null) {
            throw new Exception("Não foi encontrado nenhum cliente");
        }

        return customer;
    }

    public async Task<Customer> Update(CustomerDTO customerDTO) {
        var customer = _mapper.Map<Customer>(customerDTO);
        await _customerRepository.Update(customer);

        return customer;
    }
}

