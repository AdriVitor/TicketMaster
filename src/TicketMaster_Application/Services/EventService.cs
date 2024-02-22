using AutoMapper;
using TicketMaster_Application.DTOs.Event;
using TicketMaster_Application.Interfaces;
using TicketMaster_Domain.Entities;
using TicketMaster_Domain.Entities.Enums;
using TicketMaster_Infra.Data.Interfaces;

namespace TicketMaster_Application.Services;
public class EventService : IEventService {
    private readonly IEventRepository _eventRepository;
    private readonly IProducerRepository _producerRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public EventService(
        IEventRepository eventRepository,
        IMapper mapper
        ) {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public EventService(
        IEventRepository eventRepository,
        IProducerRepository producerRepository,
        IMapper mapper, 
        IEmailService emailService
        ) {
        _eventRepository = eventRepository;
        _producerRepository = producerRepository;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task Add(EventCreateDTO eventCreateDTO) {
        var @event = _mapper.Map<Event>(eventCreateDTO);
        @event.CurrentQuantityTicketInitial(@event.TotalAmount);
        await _eventRepository.Add(@event);

        await SendEmailNewEvent(eventCreateDTO.ProducerId);
    }

    private async Task SendEmailNewEvent(int producerId) {
        var emailProducer = await _producerRepository.GetEmailProducer(producerId);
        var nameProducer = await _producerRepository.GetNameProducer(producerId);

        _emailService.SendEmailNewEvent(nameProducer, emailProducer);
    }

    public async Task Delete(int id) {
        var @event = await _eventRepository.GetById(id);

        if (@event != null) {
            await _eventRepository.Delete(@event);
        }       
    }

    public async Task<IEnumerable<EventReadDTO>> GetByFederativeUnit(EnumFederativeUnit federativeUnit) {
        var eventList = await _eventRepository.GetByFederativeUnit(federativeUnit);

        var eventReadDTOList = _mapper.Map<IEnumerable<EventReadDTO>>(eventList);

        return eventReadDTOList;
    }

    public async Task<EventReadDTO> GetById(int id) {
        var @event = await _eventRepository.GetById(id);
        var eventReadDTO = _mapper.Map<EventReadDTO>(@event);
        if (eventReadDTO is null) {
            throw new Exception("Evento não encontrado");
        }

        return eventReadDTO;
    }

    public async Task<EventCreateDTO> GetByIdUpdate(int id) {
        var @event = await _eventRepository.GetById(id);
        var eventReadDTO = _mapper.Map<EventCreateDTO>(@event);
        if (eventReadDTO is null) {
            throw new Exception("Evento não encontrado");
        }

        return eventReadDTO;
    }

    public async Task Update(EventCreateDTO eventCreateDTO) {
        var eventUpdated = _mapper.Map<Event>(eventCreateDTO);

        await _eventRepository.Update(eventUpdated);
    }
}






