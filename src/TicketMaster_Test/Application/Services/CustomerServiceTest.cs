using AutoFixture;
using AutoMapper;
using Moq;
using TicketMaster_Application.DTOs.Customer;
using TicketMaster_Application.Interfaces;
using TicketMaster_Application.Services;
using TicketMaster_Domain.Entities;
using TicketMaster_Infra.Data.Interfaces;

namespace TicketMaster_Test.Application.Services;
public class CustomerServiceTest {

    [Fact]
    public void TestCustomerIdAddService() {
        var mapperMock = new Mock<IMapper>();
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var emailServiceMock = new Mock<IEmailService>();

        var customerDTO = GenerateCustomerDTOFixture();

        var customer = new Customer(customerDTO.Name, customerDTO.Email, customerDTO.CPF, customerDTO.Password, null);

        mapperMock.Setup(m => m.Map<Customer>(customerDTO)).Returns(customer);
        customerRepositoryMock.Setup(c => c.Add(customer)).Returns(Task.CompletedTask);
        emailServiceMock.Setup(e => e.SendEmailNewUser(customerDTO.Name, customer.Email));

        var customerService = new CustomerService(customerRepositoryMock.Object, mapperMock.Object, emailServiceMock.Object);

        var result = customerService.Add(customerDTO);

        Assert.NotNull(customerDTO);
        Assert.NotNull(customer);
        Assert.Equal(customerDTO.Name, result.Result.Name);
        Assert.Equal(customerDTO.CPF, result.Result.CPF);
        Assert.Equal(customerDTO.Password, result.Result.Password);
        Assert.Equal(customerDTO.Email, result.Result.Email);

        mapperMock.Verify(m => m.Map<Customer>(customerDTO), Times.Once);
        customerRepositoryMock.Verify(c => c.Add(customer), Times.Once);
        emailServiceMock.Verify(e => e.SendEmailNewUser(customerDTO.Name, customer.Email), Times.Once);
    }

    [Fact]
    public void TestCustomerIdUpdateService() {
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var mapperMock = new Mock<IMapper>();

        var customerDTO = GenerateCustomerDTOFixture();
        var customer = new Customer(customerDTO.Name, customerDTO.Email, customerDTO.CPF, customerDTO.Password);

        mapperMock.Setup(m => m.Map<Customer>(customerDTO)).Returns(customer);
        customerRepositoryMock.Setup(r => r.Update(customer)).Returns(Task.CompletedTask);

        var customerService = new CustomerService(customerRepositoryMock.Object, mapperMock.Object);

        var result = customerService.Update(customerDTO);

        Assert.NotNull(customerDTO);
        Assert.NotNull(customer);
        Assert.Equal(customerDTO.Name, result.Result.Name);
        Assert.Equal(customerDTO.Email, result.Result.Email);
        Assert.Equal(customerDTO.CPF, result.Result.CPF);
        Assert.Equal(customerDTO.Password, result.Result.Password);

        mapperMock.Verify(m => m.Map<Customer>(customerDTO), Times.Once);
        customerRepositoryMock.Verify(c => c.Update(customer), Times.Once);
    }

    [Fact]
    public void TestCustomerIdGetByIdService() {
        var customerRepositoryMock = new Mock<ICustomerRepository>();

        var customerDTO = GenerateCustomerDTOFixture();
        var customer = new Customer(customerDTO.Name, customerDTO.Email, customerDTO.CPF, customerDTO.Password);

        customerRepositoryMock.Setup(c => c.GetById(customerDTO.Id)).Returns(Task.FromResult(customer));

        var customerService = new CustomerService(customerRepositoryMock.Object);

        var result = customerService.GetById(customerDTO.Id);

        Assert.NotNull(customerDTO);
        Assert.NotNull(customer);
        Assert.NotNull(result);
        Assert.Equal(customerDTO.Name, result.Result.Name);
        Assert.Equal(customerDTO.Email, result.Result.Email);
        Assert.Equal(customerDTO.CPF, result.Result.CPF);
        Assert.Equal(customerDTO.Password, result.Result.Password);

        customerRepositoryMock.Verify(c => c.GetById(customerDTO.Id), Times.Once);
    }

    [Fact]
    public void TestCustomerIdDeleteService() {
        var customerRepositoryMock = new Mock<ICustomerRepository>();

        var customerDTO = GenerateCustomerDTOFixture();
        var customer = new Customer(customerDTO.Name, customerDTO.Email, customerDTO.CPF, customerDTO.Password);

        customerRepositoryMock.Setup(c => c.GetById(customerDTO.Id)).Returns(Task.FromResult(customer));
        customerRepositoryMock.Setup(c => c.Delete(customer)).Returns(Task.CompletedTask);

        var customerService = new CustomerService(customerRepositoryMock.Object);

        var result = customerService.Delete(customerDTO.Id);

        customerRepositoryMock.Verify(c => c.GetById(customerDTO.Id), Times.Once);
        customerRepositoryMock.Verify(c => c.Delete(customer), Times.Once);
    }

    public static CustomerDTO GenerateCustomerDTOFixture() {
        var customerDTO = new Fixture().Create<CustomerDTO>();
        customerDTO.Name = customerDTO.Name.Substring(0, Math.Min(customerDTO.Name.Length, 10));
        customerDTO.CPF = customerDTO.CPF.Substring(0, Math.Min(customerDTO.CPF.Length, 11));
        customerDTO.Password = customerDTO.Password.Substring(0, Math.Min(customerDTO.Password.Length, 10));
        customerDTO.Email = customerDTO.Email.Substring(0, Math.Min(customerDTO.Email.Length, 10));
        customerDTO.Email = customerDTO.Email.Insert(4, "@");

        return customerDTO;
    }
}

