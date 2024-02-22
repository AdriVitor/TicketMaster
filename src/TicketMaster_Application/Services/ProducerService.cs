using AutoMapper;
using TicketMaster_Application.DTOs.Customer;
using TicketMaster_Application.DTOs.Producer;
using TicketMaster_Application.Interfaces;
using TicketMaster_Domain.Entities;
using TicketMaster_Infra.Data.Interfaces;

namespace TicketMaster_Application.Services;
public class ProducerService : IProducerService {
    private readonly IProducerRepository _producerRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public ProducerService(IProducerRepository producerRepository)
    {
        _producerRepository = producerRepository;
    }

    public ProducerService(IProducerRepository producerRepository, IMapper mapper)
    {
        _producerRepository = producerRepository;
        _mapper = mapper;
    }

    public ProducerService(IProducerRepository producerRepository, IMapper mapper, IEmailService emailService) {
        _producerRepository = producerRepository;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task Add(ProducerDTO producerDTO) {
        var producer = _mapper.Map<Producer>(producerDTO);
        await _producerRepository.Add(producer);

        /*var SubjectAndBodyEmail = _emailService.DefaultSujectAndEmailNewUser(producer.Name);
        var subjectEmail = SubjectAndBodyEmail.Keys.First();
        var bodyEmail = SubjectAndBodyEmail.Values.First();

        _emailService.SendEmail(producer.Email, subjectEmail, bodyEmail);*/

        _emailService.SendEmailNewUser(producer.Name, producer.Email);
    }

    public async Task Delete(int id) {
        var producer = await GetById(id);
        await _producerRepository.Delete(producer);
    }

    public async Task<Producer> GetById(int id) {
        var producer = await _producerRepository.GetById(id);

        if (producer is null) {
            throw new Exception("Não foi encontrado nenhum produtor de eventos");
        }

        return producer;
    }

    public async Task Update(ProducerDTO producerDTO) {
        var producer = _mapper.Map<Producer>(producerDTO);
        await _producerRepository.Update(producer);
    }
}

