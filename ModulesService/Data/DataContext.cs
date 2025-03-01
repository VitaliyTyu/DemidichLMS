using Microsoft.EntityFrameworkCore;
using ModulesService.Models;

namespace ModulesService.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Test> Tests { get; set; }
}