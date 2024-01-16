using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace firstmvc.Models;

public partial class Ace52024Context : DbContext
{
    public Ace52024Context()
    {
    }

    public Ace52024Context(DbContextOptions<Ace52024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<HetalSbaccount> HetalSbaccounts { get; set; }

    public virtual DbSet<HetalSbtransaction> HetalSbtransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DEVSQL.Corp.local;Database=ACE 5- 2024;Trusted_Connection=True;encrypt=false;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HetalSbaccount>(entity =>
        {
            entity.HasKey(e => e.AccountNo);

            entity.ToTable("HetalSBAccount");

            entity.Property(e => e.CustomerAddress).HasMaxLength(50);
            entity.Property(e => e.CustomerName).HasMaxLength(50);
        });

        modelBuilder.Entity<HetalSbtransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.ToTable("HetalSBTransaction");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionType).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
