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
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
