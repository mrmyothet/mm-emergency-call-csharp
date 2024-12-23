﻿using Microsoft.EntityFrameworkCore;

namespace MMEmergencyCall.Databases.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmergencyRequest> EmergencyRequests { get; set; }

    public virtual DbSet<EmergencyService> EmergencyServices { get; set; }

    public virtual DbSet<StateRegion> StateRegions { get; set; }

    public virtual DbSet<Township> Townships { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmergencyRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Emergenc__33A8517A96B44B30");

            entity.HasIndex(e => e.ProviderId, "idx_ProviderId");

            entity.HasIndex(e => e.ServiceId, "idx_ServiceId");

            entity.HasIndex(e => e.UserId, "idx_UserId");

            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.RequestTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ResponseTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TownshipCode).HasMaxLength(100);
        });

        modelBuilder.Entity<EmergencyService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Emergenc__C51BB00A372E00BB");

            entity.HasIndex(e => e.ServiceType, "idx_ServiceType");

            entity.Property(e => e.Availability)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Lng).HasColumnType("decimal(18, 7)");
            entity.Property(e => e.Ltd)
                .HasColumnType("decimal(18, 7)")
                .HasColumnName("ltd");
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.ServiceGroup).HasMaxLength(50);
            entity.Property(e => e.ServiceName).HasMaxLength(100);
            entity.Property(e => e.ServiceStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('Pending')");
            entity.Property(e => e.ServiceType).HasMaxLength(50);
            entity.Property(e => e.TownshipCode).HasMaxLength(200);
        });

        modelBuilder.Entity<StateRegion>(entity =>
        {
            entity.HasKey(e => e.StateRegionId).HasName("PK__StateReg__D8A834D4F243667F");

            entity.Property(e => e.StateRegionCode).HasMaxLength(50);
            entity.Property(e => e.StateRegionNameEn).HasMaxLength(200);
            entity.Property(e => e.StateRegionNameMm).HasMaxLength(200);
        });

        modelBuilder.Entity<Township>(entity =>
        {
            entity.HasKey(e => e.TownshipId).HasName("PK__Township__8399C0933CD7D010");

            entity.Property(e => e.StateRegionCode).HasMaxLength(50);
            entity.Property(e => e.TownshipCode).HasMaxLength(50);
            entity.Property(e => e.TownshipNameEn).HasMaxLength(200);
            entity.Property(e => e.TownshipNameMm).HasMaxLength(200);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C78D5D195");

            entity.HasIndex(e => e.PhoneNumber, "idx_PhoneNumber");

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EmergencyType).HasMaxLength(50);
            entity.Property(e => e.IsVerified)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Otp)
                .HasMaxLength(50)
                .HasColumnName("OTP");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(100);
            entity.Property(e => e.TownshipCode).HasMaxLength(100);
            entity.Property(e => e.UserStatus).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
