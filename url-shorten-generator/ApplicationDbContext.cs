using Microsoft.EntityFrameworkCore;
using url_shorten_generator.Entities;
using url_shorten_generator.Services;

namespace url_shorten_generator;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortenedUrl>(builder =>
        {
            builder.Property(x => x.Code).HasMaxLength(UrlShorteningService.NumberOfCharacters);
            builder.HasIndex("Code").IsUnique();
        });
    }
}