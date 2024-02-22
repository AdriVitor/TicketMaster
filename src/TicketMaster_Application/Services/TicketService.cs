using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Net.Sockets;
using TicketMaster_Application.DTOs.Customer;
using TicketMaster_Application.DTOs.Event;
using TicketMaster_Application.DTOs.Ticket;
using TicketMaster_Application.Interfaces;
using TicketMaster_Domain.Entities;
using TicketMaster_Domain.Entities.Enums;
using TicketMaster_Infra.Data.Interfaces;
using TicketMaster_Infra.Data.Repositories;

namespace TicketMaster_Application.Services;
public class TicketService : ITicketService {
    private readonly ITicketRepository _ticketRepository;
    private readonly ICustomerService _customerService;
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public TicketService(
        ITicketRepository ticketRepository,
        IMapper mapper
        ) {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public TicketService(
        ITicketRepository ticketRepository,
        ICustomerService customerService,
        IEventService eventService,
        IMapper mapper
        ) {
        _ticketRepository = ticketRepository;
        _customerService = customerService;
        _eventService = eventService;
        _mapper = mapper;
    }

    public TicketService(
        ITicketRepository ticketRepository,
        IEventService eventService,
        IMapper mapper,
        IEmailService emailService
        ) {
        _ticketRepository = ticketRepository;
        _eventService = eventService;
        _mapper = mapper;
        _emailService = emailService;
    }

    public TicketService(
        ITicketRepository ticketRepository,
        ICustomerService customerService,
        IEventService eventService,
        IMapper mapper,
        IEmailService emailService
        ) {
        _ticketRepository = ticketRepository;
        _customerService = customerService;
        _eventService = eventService;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task Add(TicketCreateDTO ticketCreateDTO) {
        var ticket = _mapper.Map<Ticket>(ticketCreateDTO);

        await _ticketRepository.Add(ticket);

        var eventDTO = await CalculateTicketQuantityOperation(ticketCreateDTO.EventId, EnumOperationType.Subtration);

        await _eventService.Update(eventDTO);

        await SendEmailNewTicket(ticket.Id, eventDTO.Name);
    }

    private async Task SendEmailNewTicket(int ticketId, string eventName) {
        var ticketReadDto = await GetById(ticketId);

        _emailService.SendEmailNewTicket(ticketReadDto.Customer.Name, ticketReadDto.Customer.Email, eventName);
    }

    private async Task<EventCreateDTO> CalculateTicketQuantityOperation(int eventId, EnumOperationType typeOperation) {
        EventCreateDTO eventCreateDTO;
        switch (typeOperation) {
            case EnumOperationType.Addition:
                eventCreateDTO = await _eventService.GetByIdUpdate(eventId);
                eventCreateDTO.CurrentQuantityTickets += 1;
                return eventCreateDTO;

            case EnumOperationType.Subtration:
                eventCreateDTO = await _eventService.GetByIdUpdate(eventId);
                eventCreateDTO.CurrentQuantityTickets -= 1;
                return eventCreateDTO;
            default:
                throw new Exception("Informe um tipo de operação");
        }
    }

    public async Task Delete(int id) {
        var ticketReadDTO = await GetById(id);
        var ticket = _mapper.Map<Ticket>(ticketReadDTO);

        await _ticketRepository.Delete(ticket);

        var @event = await _eventService.GetById(ticket.Id);
        var eventDTO = _mapper.Map<EventCreateDTO>(@event);
        eventDTO.CurrentQuantityTickets += 1;

        await _eventService.Update(eventDTO);
    }

    public async Task<IEnumerable<TicketReadListDTO>> GetTicketsActives(int CustomerId) {
        var tickets = await _ticketRepository.GetAllTicketsActives(CustomerId);
        var ticketsReadDTO = _mapper.Map<IEnumerable<TicketReadListDTO>>(tickets);

        return ticketsReadDTO;
    }

    public async Task<IEnumerable<TicketReadListDTO>> GetTicketsNotActives(int CustomerId) {
        var tickets = await _ticketRepository.GetAllTicketsNotActives(CustomerId);
        var ticketsReadDTO = _mapper.Map<IEnumerable<TicketReadListDTO>>(tickets);

        return ticketsReadDTO;
    }

    public async Task<TicketReadDTO> GetById(int id) {
        var ticket = await _ticketRepository.GetById(id);
        if (ticket is null) {
            throw new Exception("Não foi encontrado um ticket com o Id informado");
        }
        
        var ticketReadDTO = _mapper.Map<TicketReadDTO>(ticket);
        var customer = await _customerService.GetById(ticket.CustomerId);

        ticketReadDTO.Customer = _mapper.Map<CustomerDTO>(customer);         
        ticketReadDTO.Event = await _eventService.GetById(ticket.EventId);

        return ticketReadDTO;
    }

    public async Task Update(TicketCreateDTO ticketCreateDTO) {
        var ticket = _mapper.Map<Ticket>(ticketCreateDTO);
        await _ticketRepository.Update(ticket);
    }
}

