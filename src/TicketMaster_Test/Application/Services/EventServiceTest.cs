using AutoFixture;
using AutoMapper;
using Moq;
using TicketMaster_Application.DTOs.Event;
using TicketMaster_Application.Interfaces;
using TicketMaster_Application.Services;
using TicketMaster_Domain.Entities;
using TicketMaster_Domain.Entities.Enums;
using TicketMaster_Infra.Data.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TicketMaster_Test.Application.Services;
public class EventServiceTest {

    [Fact]
    public void TestEventIdDeleteService() {
        var eventMockRepository = new Mock<IEventRepository>();
        var eventReadDTO = GenerateEventReadDTOFixture();
        var @event = ConvertEventReadDTOInEvent(eventReadDTO);

        eventMockRepository.Setup(e => e.GetById(eventReadDTO.Id)).Returns(Task.FromResult(@event));
        eventMockRepository.Setup(e => e.Delete(@event)).Returns(Task.CompletedTask);

        var eventService = new EventService(eventMockRepository.Object);

        var result = eventService.Delete(eventReadDTO.Id);

        Assert.NotNull(result);
        Assert.Equal(result, Task.CompletedTask);

        eventMockRepository.Verify(e => e.GetById(eventReadDTO.Id), Times.Once);
        eventMockRepository.Verify(e => e.Delete(@event), Times.Once);
    }

    [Fact]
    public void TestEventIdGetByIdService() {
        var eventMockRepository = new Mock<IEventRepository>();
        var mapperMock = new Mock<IMapper>();

        var eventReadDTO = GenerateEventReadDTOFixture();
        var @event = ConvertEventReadDTOInEvent(eventReadDTO);

        eventMockRepository.Setup(e => e.GetById(eventReadDTO.Id)).Returns(Task.FromResult(@event));
        mapperMock.Setup(m => m.Map<EventReadDTO>(@event)).Returns(eventReadDTO);

        var eventService = new EventService(eventMockRepository.Object, mapperMock.Object);

        var result = eventService.GetById(eventReadDTO.Id);

        Assert.NotNull(result);
        Assert.Equal(eventReadDTO.Name, result.Result.Name);
        Assert.Equal(eventReadDTO.Description, result.Result.Description);
        Assert.Equal(eventReadDTO.FederativeUnit, result.Result.FederativeUnit);
        Assert.Equal(eventReadDTO.Date, result.Result.Date);
        Assert.Equal(eventReadDTO.TotalAmount, result.Result.TotalAmount);

        eventMockRepository.Verify(e => e.GetById(eventReadDTO.Id), Times.Once);
        mapperMock.Verify(m => m.Map<EventReadDTO>(@event), Times.Once);
    }

    [Fact]
    public async void TestEventIdGetByFederativeUnitService() {
        var eventMockRepository = new Mock<IEventRepository>();
        var mapperMock = new Mock<IMapper>();

        List<EventReadDTO> listEventsReadDTO = new() {
            GenerateEventReadDTOFixture(),
            GenerateEventReadDTOFixture(),
            GenerateEventReadDTOFixture(),
        };

        List<Event> listEvents = new() {
            ConvertEventReadDTOInEvent(listEventsReadDTO[0]),
            ConvertEventReadDTOInEvent(listEventsReadDTO[1]),
            ConvertEventReadDTOInEvent(listEventsReadDTO[2])
        };

        IEnumerable<Event> iEnumerablevents = listEvents;
        IEnumerable<EventReadDTO> iEnumerableventReadDTO = listEventsReadDTO;

        eventMockRepository.Setup(e => e.GetByFederativeUnit(EnumFederativeUnit.DistritoFederal)).Returns(Task.FromResult(iEnumerablevents));
        mapperMock.Setup(e => e.Map<IEnumerable<EventReadDTO>>(iEnumerablevents)).Returns(iEnumerableventReadDTO);

        var eventService = new EventService(eventMockRepository.Object, mapperMock.Object);

        var result = await eventService.GetByFederativeUnit(EnumFederativeUnit.DistritoFederal);

        Assert.True(result.Any());
        Assert.Equal(3, result.Count());

        eventMockRepository.Verify(e => e.GetByFederativeUnit(EnumFederativeUnit.DistritoFederal), Times.Once());
        mapperMock.Verify(m => m.Map<IEnumerable<EventReadDTO>>(iEnumerablevents), Times.Once());
    }

    [Fact]
    public void TestEventIdUpdateService() {
        var eventMockRepository = new Mock<IEventRepository>();
        var mapperMock = new Mock<IMapper>();

        var eventCreateDTO = GenerateEventCreateDTOFixture();
        var @event = ConvertEventCreateDTOInEvent(eventCreateDTO);

        mapperMock.Setup(m => m.Map<Event>(eventCreateDTO)).Returns(@event);
        eventMockRepository.Setup(e => e.Update(@event)).Returns(Task.CompletedTask);

        var eventService = new EventService(eventMockRepository.Object, mapperMock.Object);

        var result = eventService.Update(eventCreateDTO);

        Assert.Equal(eventCreateDTO.Name, @event.Name);
        Assert.Equal(eventCreateDTO.Description, @event.Description);
        Assert.Equal(eventCreateDTO.FederativeUnit, @event.FederativeUnit);
        Assert.Equal(eventCreateDTO.Date, @event.Date);
        Assert.Equal(eventCreateDTO.TotalAmount, @event.TotalAmount);
        Assert.Equal(result, Task.CompletedTask);

        mapperMock.Verify(m => m.Map<Event>(eventCreateDTO), Times.Once);
        eventMockRepository.Verify(e => e.Update(@event), Times.Once);
    }

    [Fact]
    public void TestEventIdAddService() {
        var eventMockRepository = new Mock<IEventRepository>();
        var producerMockRepository = new Mock<IProducerRepository>();
        var mapperMock = new Mock<IMapper>();
        var emailMockService = new Mock<IEmailService>();

        var eventCreateDTO = GenerateEventCreateDTOFixture();
        var @event = ConvertEventCreateDTOInEvent(eventCreateDTO);
        @event.CurrentQuantityTicketInitial(@event.TotalAmount);

        mapperMock.Setup(m => m.Map<Event>(eventCreateDTO))
                  .Returns(@event);

        eventMockRepository.Setup(e => e.Add(@event))
                            .Returns(Task.CompletedTask);


        var eventService = new EventService(eventMockRepository.Object,
                                            producerMockRepository.Object,
                                            mapperMock.Object,
                                            emailMockService.Object);

        var result = eventService.Add(eventCreateDTO);

        Assert.Equal(eventCreateDTO.Name, @event.Name);
        Assert.Equal(eventCreateDTO.Description, @event.Description);
        Assert.Equal(eventCreateDTO.FederativeUnit, @event.FederativeUnit);
        Assert.Equal(eventCreateDTO.Date, @event.Date);
        Assert.Equal(eventCreateDTO.TotalAmount, @event.TotalAmount);
        Assert.Equal(@event.TotalAmount, @event.CurrentQuantityTickets);
        Assert.Equal(result, Task.CompletedTask);

        mapperMock.Verify(m => m.Map<Event>(eventCreateDTO), Times.Once);
        eventMockRepository.Verify(e => e.Add(@event), Times.Once);
        producerMockRepository.Verify(e => e.GetEmailProducer(eventCreateDTO.ProducerId), Times.Once);
        producerMockRepository.Verify(e => e.GetNameProducer(eventCreateDTO.ProducerId), Times.Once);
    }

    public static EventReadDTO GenerateEventReadDTOFixture() {
        var eventReadDTO = new Fixture().Create<EventReadDTO>();
        eventReadDTO.Date = DateTime.Now.AddDays(15);
        return eventReadDTO;
    }

    public static EventCreateDTO GenerateEventCreateDTOFixture() {
        var eventReadDTO = new Fixture().Create<EventCreateDTO>();
        eventReadDTO.Date = DateTime.Now.AddDays(15);
        eventReadDTO.ProducerId = 15;
        return eventReadDTO;
    }

    public static Event ConvertEventReadDTOInEvent(EventReadDTO eventReadDTO) {
        return Event.Create(eventReadDTO.Id, 2, eventReadDTO.Name, eventReadDTO.Description, eventReadDTO.FederativeUnit, eventReadDTO.Date, eventReadDTO.TotalAmount);
    }
    public static Event ConvertEventCreateDTOInEvent(EventCreateDTO eventCreateDTO) {
        return Event.Create(eventCreateDTO.Id, 2, eventCreateDTO.Name, eventCreateDTO.Description, eventCreateDTO.FederativeUnit, eventCreateDTO.Date, eventCreateDTO.TotalAmount);
    }
}

