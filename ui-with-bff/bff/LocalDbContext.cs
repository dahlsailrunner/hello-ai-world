using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OpenAi.Sample.Bff;

public class LocalDbContext(DbContextOptions<LocalDbContext> options) : DbContext(options), IDataProtectionKeyContext
{
  public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;
}
