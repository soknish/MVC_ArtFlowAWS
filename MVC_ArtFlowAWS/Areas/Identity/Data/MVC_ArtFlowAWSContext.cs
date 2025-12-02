using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_ArtFlowAWS.Areas.Identity.Data;
using MVC_ArtFlowAWS.Models;

namespace MVC_ArtFlowAWS.Data;

public class MVC_ArtFlowAWSContext : IdentityDbContext<MVC_ArtFlowAWSUser>
{
    public MVC_ArtFlowAWSContext(DbContextOptions<MVC_ArtFlowAWSContext> options)
        : base(options)
    {
    }

    public DbSet<Flower> ArtTable { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Ensure ArtPrice uses an appropriate SQL type/precision to avoid silent truncation.
        builder.Entity<Flower>()
               .Property(f => f.ArtPrice)
               .HasPrecision(18, 2); // maps to decimal(18,2) in SQL Server
    }
}
