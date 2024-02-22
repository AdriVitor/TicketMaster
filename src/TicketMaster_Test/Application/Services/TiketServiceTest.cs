using AutoFixture;
using AutoMapper;
using Moq;
using TicketMaster_Application.DTOs.Customer;
using TicketMaster_Application.DTOs.Event;
using TicketMaster_Application.DTOs.Ticket;
using TicketMaster_Application.Interfaces;
using TicketMaster_Application.Services;
using TicketMaster_Domain.Entities;
using TicketMaster_Infra.Data.Interfaces;

namespace TicketMaster_Test.Application.Services;
public class TiketServiceTest {
    [Fact]
    public void TestTicketGetByIdService() {
        var ticketMockRepository = new Mock<ITicketRepository>();
        var customerMockService = new Mock<ICustomerService>();
        var eventMockService = new Mock<IEventService>();
        var mapperMock = new Mock<IMapper>();

        var ticketReadDTO = GenerateTicketReadDTOFixture();
        var ticket = ConvertTicketReadDTOinTicket(ticketReadDTO);
        var customerDTO = CustomerServiceTest.GenerateCustomerDTOFixture();
        var customer = new Customer(customerDTO.Name, customerDTO.Email, customerDTO.CPF, customerDTO.Password);
        var eventReadDTO = EventServiceTest.GenerateEventReadDTOFixture();

        ticketReadDTO.Customer = customerDTO;
        ticketReadDTO.Event = eventReadDTO;

        ticketMockRepository.Setup(t => t.GetById(ticketReadDTO.Id))
            .Returns(Task.FromResult(ticket));
        customerMockService.Setup(c => c.GetById(ticket.CustomerId))
            .Returns(Task.FromResult(customer));
        eventMockService.Setup(e => e.GetById(ticket.Id))
            .Returns(Task.FromResult(eventReadDTO));
        mapperMock.Setup(m => m.Map<TicketReadDTO>(ticket))
            .Returns(ticketReadDTO);
        mapperMock.Setup(m => m.Map<CustomerDTO>(customer))
            .Returns(ticketReadDTO.Customer);

        var ticketService = new TicketService(ticketMockRepository.Object,
                                              customerMockService.Object,
                                              eventMockService.Object,
                                              mapperMock.Object);

        var result = ticketService.GetById(ticketReadDTO.Id);

        Assert.NotNull(result);
        Assert.Equal(result.Result, ticketReadDTO);
        Assert.Equal(result.Result.Event, ticketReadDTO.Event);
        Assert.Equal(result.Result.Customer, ticketReadDTO.Customer);

        ticketMockRepository.Verify(t => t.GetById(ticketReadDTO.Id), Times.Once);
        customerMockService.Verify(c => c.GetById(ticket.CustomerId), Times.Once);
        eventMockService.Verify(e => e.GetById(ticket.EventId), Times.Once);
        mapperMock.Verify(m => m.Map<TicketReadDTO>(ticket), Times.Once);
        mapperMock.Verify(m => m.Map<CustomerDTO>(customer), Times.Once);
    }

    [Fact]
    public void TestEventIdUpdateService() {
        var ticketMockRepository = new Mock<ITicketRepository>();
        var mapperMock = new Mock<IMapper>();

        var ticketCreateDTO = GenerateTicketCreateDTOFixture();
        var ticket = ConvertTicketCreateDTOinTicket(ticketCreateDTO);

        mapperMock.Setup(m => m.Map<Ticket>(ticketCreateDTO))
            .Returns(ticket);
        ticketMockRepository.Setup(t => t.Update(ticket))
            .Returns(Task.CompletedTask);

        var ticketService = new TicketService(ticketMockRepository.Object, mapperMock.Object);

        var result = ticketService.Update(ticketCreateDTO);

        Assert.NotNull(result);
        Assert.Equal(result, Task.CompletedTask);
        Assert.Equal(ticketCreateDTO.CustomerId, ticket.CustomerId);
        Assert.Equal(ticketCreateDTO.EventId, ticket.EventId);
        Assert.Equal(ticketCreateDTO.FlagConsumed, ticket.FlagConsumed);

        mapperMock.Verify(m => m.Map<Ticket>(ticketCreateDTO), Times.Once);
        ticketMockRepository.Verify(t => t.Update(ticket), Times.Once);
    }

    [Fact]
    public void TestTicketGetTicketActivesService() {
        var ticketMockRepository = new Mock<ITicketRepository>();
        var mapperMock = new Mock<IMapper>();

        var CustomerId = 15;
        var tickets = GenerateTicketListFixture();
        var ticketsReadListDTO = GenerateTicketReadListDTOFixture();

        foreach (var ticket in ticketsReadListDTO) {
            ticket.FlagConsumed = false;
        }

        ticketMockRepository.Setup(t => t.GetAllTicketsActives(CustomerId))
            .Returns(Task.FromResult(tickets));

        mapperMock.Setup(m => m.Map<IEnumerable<TicketReadListDTO>>(tickets))
            .Returns(ticketsReadListDTO);

        var ticketService = new TicketService(ticketMockRepository.Object, mapperMock.Object);

        var result = ticketService.GetTicketsActives(CustomerId);

        Assert.NotNull(result);
        Assert.Equal(tickets.Count(), ticketsReadListDTO.Count());
        Assert.True(ticketsReadListDTO.All(t => t.FlagConsumed == false));

        ticketMockRepository.Verify(t => t.GetAllTicketsActives(CustomerId), Times.Once);
        mapperMock.Verify(m => m.Map<IEnumerable<TicketReadListDTO>>(tickets), Times.Once);
    }

    [Fact]
    public void TestTicketGetTicketNotActivesService() {
        var ticketMockRepository = new Mock<ITicketRepository>();
        var mapperMock = new Mock<IMapper>();

        var CustomerId = 15;
        var tickets = GenerateTicketListFixture();
        var ticketsReadListDTO = GenerateTicketReadListDTOFixture();

        foreach (var ticket in ticketsReadListDTO) {
            ticket.FlagConsumed = true;
        }

        ticketMockRepository.Setup(t => t.GetAllTicketsActives(CustomerId))
            .Returns(Task.FromResult(tickets));

        mapperMock.Setup(m => m.Map<IEnumerable<TicketReadListDTO>>(tickets))
            .Returns(ticketsReadListDTO);

        var ticketService = new TicketService(ticketMockRepository.Object, mapperMock.Object);

        var result = ticketService.GetTicketsActives(CustomerId);

        Assert.NotNull(result);
        Assert.Equal(tickets.Count(), ticketsReadListDTO.Count());
        Assert.True(ticketsReadListDTO.All(t => t.FlagConsumed));

        ticketMockRepository.Verify(t => t.GetAllTicketsActives(CustomerId), Times.Once);
        mapperMock.Verify(m => m.Map<IEnumerable<TicketReadListDTO>>(tickets), Times.Once);
    }

    private TicketReadDTO GenerateTicketReadDTOFixture() {
        return new Fixture().Create<TicketReadDTO>();
    }

    private TicketCreateDTO GenerateTicketCreateDTOFixture() {
        return new Fixture().Create<TicketCreateDTO>();
    }

    private Ticket ConvertTicketReadDTOinTicket(TicketReadDTO ticketReadDTO) {
        return new Ticket(ticketReadDTO.Customer.Id, ticketReadDTO.Event.Id, ticketReadDTO.FlagConsumed);
    }

    private Ticket ConvertTicketCreateDTOinTicket(TicketCreateDTO ticketCreateDTO) {
        return new Ticket(ticketCreateDTO.CustomerId, ticketCreateDTO.EventId, ticketCreateDTO.FlagConsumed);
    }

    private IEnumerable<TicketReadListDTO> GenerateTicketReadListDTOFixture() {
        var listTicketReadListDTO = new List<TicketReadListDTO>{
            new Fixture().Create<TicketReadListDTO>(),
            new Fixture().Create<TicketReadListDTO>(),
            new Fixture().Create<TicketReadListDTO>()
        };

        IEnumerable<TicketReadListDTO> iEnumerableTicketReadListDTO = listTicketReadListDTO;

        return iEnumerableTicketReadListDTO;
    }

    private IEnumerable<Ticket> GenerateTicketListFixture() {
        var listTicket = new List<Ticket>{
            new Fixture().Create<Ticket>(),
            new Fixture().Create<Ticket>(),
            new Fixture().Create<Ticket>()
        };

        IEnumerable<Ticket> iEnumerableTicketReadListDTO = listTicket;

        return iEnumerableTicketReadListDTO;
    }
}

