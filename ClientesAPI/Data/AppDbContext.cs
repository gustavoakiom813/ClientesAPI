using ClientesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ClientesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ClienteModel> Clientes { get; set; }
    }
}
