using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace flightmvc.Models;

public partial class Ace52024Context : DbContext
{
    public Ace52024Context()
    {
    }

    public Ace52024Context(DbContextOptions<Ace52024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<HetalBooking> HetalBookings { get; set; }

    public virtual DbSet<HetalCustomer> HetalCustomers { get; set; }

    public virtual DbSet<HetalFlight> HetalFlights { get; set; }

    public virtual DbSet<HetalUsertable> HetalUsertables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DEVSQL.Corp.local;Database=ACE 5- 2024;Trusted_Connection=True;encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HetalBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId);

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");

            entity.HasOne(d => d.Customer).WithMany(p => p.HetalBookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HetalBookings_HetalCustomers");

            entity.HasOne(d => d.Flight).WithMany(p => p.HetalBookings)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HetalBookings_HetalFlights");
        });

        modelBuilder.Entity<HetalCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(50);
        });

        modelBuilder.Entity<HetalFlight>(entity =>
        {
            entity.HasKey(e => e.FlightId);

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.Airline).HasMaxLength(30);
            entity.Property(e => e.Arrival)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Departure)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Destination).HasMaxLength(50);
            entity.Property(e => e.FlightName).HasMaxLength(15);
            entity.Property(e => e.Source).HasMaxLength(50);
        });

        modelBuilder.Entity<HetalUsertable>(entity =>
        {
            entity.HasKey(e => e.Email);

            entity.ToTable("HetalUsertable");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
