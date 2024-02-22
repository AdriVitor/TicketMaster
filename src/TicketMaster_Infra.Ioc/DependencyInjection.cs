using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketMaster_Application.Interfaces;
using TicketMaster_Application.Mappings;
using TicketMaster_Application.Services;
using TicketMaster_Infra.Data.Context;
using TicketMaster_Infra.Data.Interfaces;
using TicketMaster_Infra.Data.Repositories;

namespace TicketMaster_Infra.Ioc;
public static class DependencyInjection {

    public static IServiceCollection AddInfraestructure(this IServiceCollection service, IConfiguration configuration) {
        service.AddDbContext<AppDbContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        //Services
        service.AddScoped<ICustomerService, CustomerService>();
        service.AddScoped<IProducerService, ProducerService>();
        service.AddScoped<IEventService, EventService>();
        service.AddScoped<ITicketService, TicketService>();
        service.AddScoped<IEmailService, EmailService>();

        //Repositories
        service.AddScoped<ICustomerRepository, CustomerRepository>();
        service.AddScoped<IProducerRepository, ProducerRepository>();
        service.AddScoped<IEventRepository, EventRepository>();
        service.AddScoped<ITicketRepository, TicketRepository>();

        //Mappings
        service.AddAutoMapper(typeof(EntitiesToDTOMappingProfile));

        return service;
    }
}

