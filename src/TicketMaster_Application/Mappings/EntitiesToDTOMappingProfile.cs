using AutoMapper;
using TicketMaster_Application.DTOs.Customer;
using TicketMaster_Application.DTOs.Event;
using TicketMaster_Application.DTOs.Producer;
using TicketMaster_Application.DTOs.Ticket;
using TicketMaster_Domain.Entities;

namespace TicketMaster_Application.Mappings;
public class EntitiesToDTOMappingProfile : Profile {
    public EntitiesToDTOMappingProfile()
    {
        CreateMap<Customer, CustomerDTO>().ReverseMap();
        CreateMap<Producer, ProducerDTO>().ReverseMap();
        CreateMap<Event, EventReadDTO>().ReverseMap();
        CreateMap<Event, EventCreateDTO>().ReverseMap();
        CreateMap<EventReadDTO, EventCreateDTO>().ReverseMap();
        CreateMap<Ticket, TicketReadDTO>().ReverseMap();
        CreateMap<Ticket, TicketReadListDTO>().ReverseMap();
        CreateMap<Ticket, TicketCreateDTO>().ReverseMap();
    }
}

