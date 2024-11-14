using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeamAPI.Models;

public partial class TeamContext : DbContext
{
    public TeamContext()
    {
    }

    public TeamContext(DbContextOptions<TeamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("server=localhost;database=Team;user=root;password=password;sslmode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Player", "Team");

            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(37);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
