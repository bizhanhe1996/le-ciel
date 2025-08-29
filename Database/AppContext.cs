using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options) { }
}
