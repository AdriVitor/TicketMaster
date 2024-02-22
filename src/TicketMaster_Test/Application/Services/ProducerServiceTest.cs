using AutoFixture;
using AutoMapper;
using Moq;
using TicketMaster_Application.DTOs.Producer;
using TicketMaster_Application.Interfaces;
using TicketMaster_Application.Services;
using TicketMaster_Domain.Entities;
using TicketMaster_Infra.Data.Interfaces;

namespace TicketMaster_Test.Application.Services;
public class ProducerServiceTest {

    [Fact]
    public void TestProducerIdGetByIdService() {
        var producerRepositoryMock = new Mock<IProducerRepository>();
        var producerDTO = GenerateProducerDTOFixture();
        var producer = new Producer(producerDTO.Name, producerDTO.Email, producerDTO.CPF, producerDTO.Password);

        producerRepositoryMock.Setup(p => p.GetById(producerDTO.Id)).Returns(Task.FromResult(producer));

        var producerService = new ProducerService(producerRepositoryMock.Object);

        var result = producerService.GetById(producerDTO.Id);

        Assert.NotNull(producerDTO);
        Assert.NotNull(producer);
        Assert.NotNull(result);
        Assert.Equal(producer.Name, result.Result.Name);
        Assert.Equal(producer.CPF, result.Result.CPF);
        Assert.Equal(producer.Email, result.Result.Email);
        Assert.Equal(producer.Password, result.Result.Password);

        producerRepositoryMock.Verify(p => p.GetById(producerDTO.Id), Times.Once);
    }

    [Fact]
    public void TestProducerIdUpdateService() {
        var producerMockRepository = new Mock<IProducerRepository>();
        var mapperMock = new Mock<IMapper>();
        var producerDTO = GenerateProducerDTOFixture();
        var producer = new Producer(producerDTO.Name, producerDTO.Email, producerDTO.CPF, producerDTO.Password);

        mapperMock.Setup(m => m.Map<Producer>(producerDTO)).Returns(producer);
        producerMockRepository.Setup(p => p.Update(producer)).Returns(Task.CompletedTask);

        var producerService = new ProducerService(producerMockRepository.Object, mapperMock.Object);

        var result = producerService.Update(producerDTO);

        Assert.NotNull(producerDTO);
        Assert.NotNull(producer);
        Assert.NotNull(result);
        Assert.Equal(producerDTO.Name, producer.Name);
        Assert.Equal(producerDTO.CPF, producer.CPF);
        Assert.Equal(producerDTO.Email, producer.Email);
        Assert.Equal(producerDTO.Password, producer.Password);
        Assert.Equal(result, Task.CompletedTask);

        producerMockRepository.Verify(p => p.Update(producer), Times.Once);
        mapperMock.Verify(m => m.Map<Producer>(producerDTO), Times.Once);
    }

    [Fact]
    public void TestProducerIdDeleteService() {
        var producerMockRepository = new Mock<IProducerRepository>();
        var producerDTO = GenerateProducerDTOFixture();
        var producer = new Producer(producerDTO.Name, producerDTO.Email, producerDTO.CPF, producerDTO.Password);

        producerMockRepository.Setup(p => p.GetById(producerDTO.Id)).Returns(Task.FromResult(producer));
        producerMockRepository.Setup(p => p.Delete(producer)).Returns(Task.CompletedTask);

        var producerService = new ProducerService(producerMockRepository.Object);

        var result = producerService.Delete(producerDTO.Id);

        Assert.NotNull(producerDTO);
        Assert.NotNull(producer);
        Assert.Equal(producerDTO.Name, producer.Name);
        Assert.Equal(producerDTO.Email, producer.Email);
        Assert.Equal(producerDTO.CPF, producer.CPF);
        Assert.Equal(producerDTO.Password, producer.Password);
        Assert.Equal(result, Task.CompletedTask);

        producerMockRepository.Verify(p => p.GetById(producerDTO.Id), Times.Once);
        producerMockRepository.Verify(p => p.Delete(producer), Times.Once);
    }

    [Fact]
    public void TestProducerIdAddService() {
        var producerMockRepository = new Mock<IProducerRepository>();
        var mapperMock = new Mock<IMapper>();
        var emailMockService = new Mock<IEmailService>();

        var producerDTO = GenerateProducerDTOFixture();
        var producer = new Producer(producerDTO.Name, producerDTO.Email, producerDTO.CPF, producerDTO.Password);

        mapperMock.Setup(p => p.Map<Producer>(producerDTO)).Returns(producer);
        producerMockRepository.Setup(p => p.Add(producer)).Returns(Task.CompletedTask);
        emailMockService.Setup(e => e.SendEmailNewUser(producer.Name, producer.Email));

        var producerService = new ProducerService(producerMockRepository.Object, mapperMock.Object, emailMockService.Object);

        var result = producerService.Add(producerDTO);

        Assert.NotNull(producerDTO);
        Assert.NotNull(producer);
        Assert.Equal(producerDTO.Name, producer.Name);
        Assert.Equal(producerDTO.Email, producer.Email);
        Assert.Equal(producerDTO.CPF, producer.CPF);
        Assert.Equal(producerDTO.Password, producer.Password);
        Assert.Equal(result, Task.CompletedTask);

        mapperMock.Verify(m => m.Map<Producer>(producerDTO), Times.Once);
        producerMockRepository.Verify(p => p.Add(producer), Times.Once);
        emailMockService.Verify(e => e.SendEmailNewUser(producer.Name, producer.Email), Times.Once);
    }

    public ProducerDTO GenerateProducerDTOFixture() {
        var producerDTO = new Fixture().Create<ProducerDTO>();
        producerDTO.Name = producerDTO.Name.Substring(0, Math.Min(producerDTO.Name.Length, 10));
        producerDTO.CPF = producerDTO.CPF.Substring(0, Math.Min(producerDTO.CPF.Length, 11));
        producerDTO.Email = producerDTO.Email.Substring(0, Math.Min(producerDTO.Email.Length, 10));
        producerDTO.Email = producerDTO.Email.Insert(4, "@");
        producerDTO.Password = producerDTO.Password.Substring(0, Math.Min(producerDTO.Password.Length, 10));

        return producerDTO;
    }
}

