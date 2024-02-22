using Microsoft.EntityFrameworkCore;
using TicketMaster_Domain.Entities;

namespace TicketMaster_Infra.Data.Context;
    public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Customer> Customer { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<Producer> Producer { get; set; }
    public DbSet<Ticket> Ticket { get; set; }
}

