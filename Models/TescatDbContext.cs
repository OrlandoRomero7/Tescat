using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tescat.Models;

public partial class TescatDbContext : DbContext
{
    public TescatDbContext()
    {
    }

    public TescatDbContext(DbContextOptions<TescatDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cpu> Cpus { get; set; }

    public virtual DbSet<Email> Emails { get; set; }

    public virtual DbSet<Gpu> Gpus { get; set; }

    public virtual DbSet<LaptopBattery> LaptopBatteries { get; set; }

    public virtual DbSet<MemoryRam> MemoryRams { get; set; }

    public virtual DbSet<Motherboard> Motherboards { get; set; }

    public virtual DbSet<Pc> Pcs { get; set; }

    public virtual DbSet<PcCredential> PcCredentials { get; set; }

    public virtual DbSet<PowerSupply> PowerSupplies { get; set; }

    public virtual DbSet<Storage> Storages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCredential> UserCredentials { get; set; }
    /*
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pc>()
            .HasOne(p => p.Cpu)             // Un Pc tiene un Cpu
            .WithOne(c => c.Pc)             // Un Cpu está asociado con un Pc
            .HasForeignKey<Cpu>(c => c.IdPc);  // Cpu tiene la clave externa IdPc
    }
    */

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cpu>(entity =>
        {
            entity.HasKey(e => e.IdCpu).HasName("PK__CPU__2BF960E00CF1C646");

            entity.ToTable("CPU");

            entity.HasIndex(e => e.IdPc, "UQ__CPU__8B6390372FAE55C4").IsUnique();

            entity.Property(e => e.IdCpu)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_CPU");
            entity.Property(e => e.Benchmark).HasColumnName("BENCHMARK");
            entity.Property(e => e.IdPc).HasColumnName("ID_PC");
            entity.Property(e => e.Model)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("MODEL");
            entity.Property(e => e.Socket)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SOCKET");

            entity.HasOne(d => d.IdPcNavigation).WithOne(p => p.Cpu)
                .HasForeignKey<Cpu>(d => d.IdPc)
                .HasConstraintName("FK_CPU_PC");
        });

        modelBuilder.Entity<Email>(entity =>
        {
            entity.HasKey(e => e.UserEmailsId).HasName("PK__Emails__6157943A85AC7E2B");

            entity.HasIndex(e => e.IdUser, "UQ__Emails__95F48441D1A5E7FF").IsUnique();

            entity.Property(e => e.UserEmailsId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("USER_EMAILS_ID");
            entity.Property(e => e.Email1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL_1");
            entity.Property(e => e.Email2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL_2");
            entity.Property(e => e.Email3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL_3");
            entity.Property(e => e.IdUser).HasColumnName("ID_USER");
            entity.Property(e => e.Pass1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASS_1");
            entity.Property(e => e.Pass2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASS_2");
            entity.Property(e => e.Pass3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASS_3");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.Email)
                .HasForeignKey<Email>(d => d.IdUser)
                .HasConstraintName("FK_Emails_Users");
        });

        modelBuilder.Entity<Gpu>(entity =>
        {
            entity.HasKey(e => e.IdGpu).HasName("PK__GPU__2EC9F1AE63690165");

            entity.ToTable("GPU");

            entity.HasIndex(e => e.IdPc, "UQ__GPU__8B639037B9A01D51").IsUnique();

            entity.Property(e => e.IdGpu)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_GPU");
            entity.Property(e => e.IdPc).HasColumnName("ID_PC");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL");

            entity.HasOne(d => d.IdPcNavigation).WithOne(p => p.Gpu)
                .HasForeignKey<Gpu>(d => d.IdPc)
                .HasConstraintName("FK_GPU_PC");
        });

        modelBuilder.Entity<LaptopBattery>(entity =>
        {
            entity.HasKey(e => e.IdBattery).HasName("PK__Laptop_B__D29CDF3DF08F6F95");

            entity.ToTable("Laptop_Battery");

            entity.HasIndex(e => e.IdPc, "UQ__Laptop_B__8B639037DADE5B53").IsUnique();

            entity.Property(e => e.IdBattery)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_BATTERY");
            entity.Property(e => e.Capacity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CAPACITY");
            entity.Property(e => e.FabDate)
                .HasColumnType("datetime")
                .HasColumnName("FAB_DATE");
            entity.Property(e => e.IdPc).HasColumnName("ID_PC");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL");

            entity.HasOne(d => d.IdPcNavigation).WithOne(p => p.LaptopBattery)
                .HasForeignKey<LaptopBattery>(d => d.IdPc)
                .HasConstraintName("FK_Laptop_Battery_PC");
        });

        modelBuilder.Entity<MemoryRam>(entity =>
        {
            entity.HasKey(e => e.IdRam).HasName("PK__Memory_R__203A81B1E2A57360");

            entity.ToTable("Memory_RAM");

            entity.Property(e => e.IdRam)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_RAM");
            entity.Property(e => e.IdPc).HasColumnName("ID_PC");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL");
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SIZE");
            entity.Property(e => e.Speed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SPEED");
            entity.Property(e => e.TypeRam)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TYPE_RAM");

            entity.HasOne(d => d.IdPcNavigation).WithMany(p => p.MemoryRams)
                .HasForeignKey(d => d.IdPc)
                .HasConstraintName("FK_Memory_RAM_PC");
        });

        modelBuilder.Entity<Motherboard>(entity =>
        {
            entity.HasKey(e => e.IdMotherboard).HasName("PK__Motherbo__93E84B89D72E7BFF");

            entity.ToTable("Motherboard");

            entity.HasIndex(e => e.IdPc, "UQ__Motherbo__8B639037275AC6CC").IsUnique();

            entity.Property(e => e.IdMotherboard)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_MOTHERBOARD");
            entity.Property(e => e.FormFactor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FORM_FACTOR");
            entity.Property(e => e.IdPc).HasColumnName("ID_PC");
            entity.Property(e => e.M2Slot)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("M2_SLOT");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL");
            entity.Property(e => e.RamSlotsNumb)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RAM_SLOTS_NUMB");
            entity.Property(e => e.SocketCpu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SOCKET_CPU");
            entity.Property(e => e.TypeSlot)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TYPE_SLOT");
            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VERSION");

            entity.HasOne(d => d.IdPcNavigation).WithOne(p => p.Motherboard)
                .HasForeignKey<Motherboard>(d => d.IdPc)
                .HasConstraintName("FK_Motherboard_PC");
        });

        modelBuilder.Entity<Pc>(entity =>
        {
            entity.HasKey(e => e.IdPc).HasName("PK__PC__8B639036F1738436");

            entity.ToTable("PC");

            entity.Property(e => e.IdPc)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_PC");
            entity.Property(e => e.IdUser).HasColumnName("ID_USER");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IP");
            entity.Property(e => e.LastMaint)
                .HasColumnType("datetime")
                .HasColumnName("LAST_MAINT");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL");
            entity.Property(e => e.PcName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PC_NAME");
            entity.Property(e => e.PcType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PC_TYPE");
            entity.Property(e => e.UsedRamSlots)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USED_RAM_SLOTS");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Pcs)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_PC_Users");
        });

        modelBuilder.Entity<PcCredential>(entity =>
        {
            entity.HasKey(e => e.IdPc).HasName("PK__PC_Crede__8B6390365925DDE7");

            entity.ToTable("PC_Credentials");

            entity.Property(e => e.IdPc)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_PC");
            entity.Property(e => e.AnydeskPass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ANYDESK_PASS");
            entity.Property(e => e.ComunPass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COMUN_PASS");
            entity.Property(e => e.ComunUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COMUN_USER");
            entity.Property(e => e.DarwinVantecPass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DARWIN_VANTEC_PASS");
            entity.Property(e => e.DarwinVantecUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DARWIN_VANTEC_USER");
            entity.Property(e => e.IdAnydesk)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ID_ANYDESK");
            entity.Property(e => e.LastModif)
                .HasColumnType("datetime")
                .HasColumnName("LAST_MODIF");
            entity.Property(e => e.MozartRdpPass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MOZART_RDP_PASS");
            entity.Property(e => e.MozartRdpUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MOZART_RDP_USER");
            entity.Property(e => e.NetworkUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NETWORK_USER");
            entity.Property(e => e.PcPassword)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PC_PASSWORD");
            entity.Property(e => e.PcUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PC_USER");

            entity.HasOne(d => d.IdPcNavigation).WithOne(p => p.PcCredential)
                .HasForeignKey<PcCredential>(d => d.IdPc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PC_Credentials_PC");
        });

        modelBuilder.Entity<PowerSupply>(entity =>
        {
            entity.HasKey(e => e.IdPsu).HasName("PK__Power_Su__20AFB118886356B6");

            entity.ToTable("Power_Supply");

            entity.HasIndex(e => e.IdPc, "UQ__Power_Su__8B6390374008CB01").IsUnique();

            entity.Property(e => e.IdPsu)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_PSU");
            entity.Property(e => e.IdPc).HasColumnName("ID_PC");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL");

            entity.HasOne(d => d.IdPcNavigation).WithOne(p => p.PowerSupply)
                .HasForeignKey<PowerSupply>(d => d.IdPc)
                .HasConstraintName("FK_Power_Supply_PC");
        });

        modelBuilder.Entity<Storage>(entity =>
        {
            entity.HasKey(e => e.IdStorage).HasName("PK__Storage__8E1CE8BCFDF8FD2E");

            entity.ToTable("Storage");

            entity.Property(e => e.IdStorage)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_STORAGE");
            entity.Property(e => e.AvailableStrge)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AVAILABLE_STRGE");
            entity.Property(e => e.Gbw)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GBW");
            entity.Property(e => e.IdPc).HasColumnName("ID_PC");
            entity.Property(e => e.Model)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("MODEL");
            entity.Property(e => e.NumberRead)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMBER_READ");
            entity.Property(e => e.NumberWrite)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMBER_WRITE");
            entity.Property(e => e.ReadSpeed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("READ_SPEED");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATE");
            entity.Property(e => e.TotalStrge)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TOTAL_STRGE");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TYPE");
            entity.Property(e => e.UseTime)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USE_TIME");
            entity.Property(e => e.UsedStrge)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USED_STRGE");
            entity.Property(e => e.WriteSpeed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WRITE_SPEED");

            entity.HasOne(d => d.IdPcNavigation).WithMany(p => p.Storages)
                .HasForeignKey(d => d.IdPc)
                .HasConstraintName("FK_Storage_PC");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__95F48440FC0DD4DB");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("ID_USER");
            entity.Property(e => e.Dept)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DEPT");
            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("ENTRY_DATE");
            entity.Property(e => e.LastModif)
                .HasColumnType("datetime")
                .HasColumnName("LAST_MODIF");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POSITION");
            entity.Property(e => e.Tel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEL");
            entity.Property(e => e.WebPrivileges).HasColumnName("WEB_PRIVILEGES");
        });

        modelBuilder.Entity<UserCredential>(entity =>
        {
            entity.HasKey(e => e.IdCredentials).HasName("PK__User_Cre__AA91C2A1EFBD4928");

            entity.ToTable("User_Credentials");

            entity.HasIndex(e => e.IdUser, "UQ__User_Cre__95F4844187426EF0").IsUnique();

            entity.Property(e => e.IdCredentials)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_CREDENTIALS");
            entity.Property(e => e.CasaPass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CASA_PASS");
            entity.Property(e => e.CasaUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CASA_USER");
            entity.Property(e => e.DarwinPass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DARWIN_PASS");
            entity.Property(e => e.DarwinUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DARWIN_USER");
            entity.Property(e => e.IdUser).HasColumnName("ID_USER");
            entity.Property(e => e.LastModif)
                .HasColumnType("datetime")
                .HasColumnName("LAST_MODIF");
            entity.Property(e => e.MozartPass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MOZART_PASS");
            entity.Property(e => e.MozartUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MOZART_USER");
            entity.Property(e => e.PortalPass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PORTAL_PASS");
            entity.Property(e => e.PortalUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PORTAL_USER");
            entity.Property(e => e.VpnPass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VPN_PASS");
            entity.Property(e => e.VpnUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VPN_USER");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.UserCredential)
                .HasForeignKey<UserCredential>(d => d.IdUser)
                .HasConstraintName("FK_User_Credentials_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    
}
