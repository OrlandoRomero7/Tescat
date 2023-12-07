﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tescat.Models;

#nullable disable

namespace Tescat.Migrations
{
    [DbContext(typeof(TescatDbContext))]
    partial class TescatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Tescat.Models.Cpu", b =>
                {
                    b.Property<Guid>("IdCpu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Benchmark")
                        .HasColumnType("int");

                    b.Property<Guid?>("IdPc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Socket")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCpu")
                        .HasName("PK__CPU__2BF960E00CF1C646");

                    b.HasIndex("IdPc")
                        .IsUnique()
                        .HasFilter("[IdPc] IS NOT NULL");

                    b.ToTable("Cpus");
                });

            modelBuilder.Entity("Tescat.Models.Email", b =>
                {
                    b.Property<Guid>("UserEmailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("Pass1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pass2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pass3")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserEmailsId")
                        .HasName("PK__Emails__6157943A85AC7E2B");

                    b.HasIndex("IdUser")
                        .IsUnique()
                        .HasFilter("[IdUser] IS NOT NULL");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("Tescat.Models.Gpu", b =>
                {
                    b.Property<Guid>("IdGpu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdPc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdGpu")
                        .HasName("PK__GPU__2EC9F1AE63690165");

                    b.HasIndex("IdPc")
                        .IsUnique()
                        .HasFilter("[IdPc] IS NOT NULL");

                    b.ToTable("Gpus");
                });

            modelBuilder.Entity("Tescat.Models.LaptopBattery", b =>
                {
                    b.Property<Guid>("IdBattery")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Capacity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FabDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("IdPc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdBattery")
                        .HasName("PK__Laptop_B__D29CDF3DF08F6F95");

                    b.HasIndex("IdPc")
                        .IsUnique()
                        .HasFilter("[IdPc] IS NOT NULL");

                    b.ToTable("LaptopBatteries");
                });

            modelBuilder.Entity("Tescat.Models.MemoryRam", b =>
                {
                    b.Property<Guid>("IdRam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdPc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeRam")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRam")
                        .HasName("PK__Memory_R__203A81B1E2A57360");

                    b.HasIndex("IdPc");

                    b.ToTable("MemoryRams");
                });

            modelBuilder.Entity("Tescat.Models.Motherboard", b =>
                {
                    b.Property<Guid>("IdMotherboard")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FormFactor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("IdPc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("M2Slot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RamSlotsNumb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocketCpu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeSlot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdMotherboard")
                        .HasName("PK__Motherbo__93E84B89D72E7BFF");

                    b.HasIndex("IdPc")
                        .IsUnique()
                        .HasFilter("[IdPc] IS NOT NULL");

                    b.ToTable("Motherboards");
                });

            modelBuilder.Entity("Tescat.Models.Pc", b =>
                {
                    b.Property<Guid>("IdPc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastMaint")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastUser")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PcName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PcType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsedRamSlots")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPc")
                        .HasName("PK__PC__8B639036F1738436");

                    b.HasIndex("IdUser");

                    b.ToTable("Pcs");
                });

            modelBuilder.Entity("Tescat.Models.PcCredential", b =>
                {
                    b.Property<Guid>("IdPc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnydeskPass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComunPass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComunUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DarwinVantecPass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DarwinVantecUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdAnydesk")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModif")
                        .HasColumnType("datetime2");

                    b.Property<string>("MozartRdpPass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MozartRdpUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NetworkUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PcPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PcUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPc")
                        .HasName("PK__PC_Crede__8B6390365925DDE7");

                    b.ToTable("PcCredentials");
                });

            modelBuilder.Entity("Tescat.Models.PowerSupply", b =>
                {
                    b.Property<Guid>("IdPsu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdPc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPsu")
                        .HasName("PK__Power_Su__20AFB118886356B6");

                    b.HasIndex("IdPc")
                        .IsUnique()
                        .HasFilter("[IdPc] IS NOT NULL");

                    b.ToTable("PowerSupplies");
                });

            modelBuilder.Entity("Tescat.Models.Storage", b =>
                {
                    b.Property<Guid>("IdStorage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AvailableStrge")
                        .HasColumnType("int");

                    b.Property<string>("Gbw")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("IdPc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberRead")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberWrite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReadSpeed")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TotalStrge")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UseTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsedStrge")
                        .HasColumnType("int");

                    b.Property<int?>("WriteSpeed")
                        .HasColumnType("int");

                    b.HasKey("IdStorage")
                        .HasName("PK__Storage__8E1CE8BCFDF8FD2E");

                    b.HasIndex("IdPc");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("Tescat.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dept")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IMAGE_NAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModif")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastWorkingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Office")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Tel")
                        .HasColumnType("int");

                    b.Property<int?>("TelKey")
                        .HasColumnType("int");

                    b.Property<bool?>("WebPrivileges")
                        .HasColumnType("bit");

                    b.HasKey("IdUser")
                        .HasName("PK__Users__95F48440FC0DD4DB");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tescat.Models.UserCredential", b =>
                {
                    b.Property<Guid>("IdCredentials")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CasaPass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CasaUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DarwinPass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DarwinUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModif")
                        .HasColumnType("datetime2");

                    b.Property<string>("MozartPass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MozartUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PortalPass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PortalUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VpnPass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VpnUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCredentials")
                        .HasName("PK__User_Cre__AA91C2A1EFBD4928");

                    b.HasIndex("IdUser")
                        .IsUnique()
                        .HasFilter("[IdUser] IS NOT NULL");

                    b.ToTable("UserCredentials");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tescat.Models.Cpu", b =>
                {
                    b.HasOne("Tescat.Models.Pc", "IdPcNavigation")
                        .WithOne("Cpu")
                        .HasForeignKey("Tescat.Models.Cpu", "IdPc")
                        .HasConstraintName("FK_CPU_PC");

                    b.Navigation("IdPcNavigation");
                });

            modelBuilder.Entity("Tescat.Models.Email", b =>
                {
                    b.HasOne("Tescat.Models.User", "IdUserNavigation")
                        .WithOne("Email")
                        .HasForeignKey("Tescat.Models.Email", "IdUser")
                        .HasConstraintName("FK_Emails_Users");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("Tescat.Models.Gpu", b =>
                {
                    b.HasOne("Tescat.Models.Pc", "IdPcNavigation")
                        .WithOne("Gpu")
                        .HasForeignKey("Tescat.Models.Gpu", "IdPc")
                        .HasConstraintName("FK_GPU_PC");

                    b.Navigation("IdPcNavigation");
                });

            modelBuilder.Entity("Tescat.Models.LaptopBattery", b =>
                {
                    b.HasOne("Tescat.Models.Pc", "IdPcNavigation")
                        .WithOne("LaptopBattery")
                        .HasForeignKey("Tescat.Models.LaptopBattery", "IdPc")
                        .HasConstraintName("FK_Laptop_Battery_PC");

                    b.Navigation("IdPcNavigation");
                });

            modelBuilder.Entity("Tescat.Models.MemoryRam", b =>
                {
                    b.HasOne("Tescat.Models.Pc", "IdPcNavigation")
                        .WithMany("MemoryRams")
                        .HasForeignKey("IdPc")
                        .HasConstraintName("FK_Memory_RAM_PC");

                    b.Navigation("IdPcNavigation");
                });

            modelBuilder.Entity("Tescat.Models.Motherboard", b =>
                {
                    b.HasOne("Tescat.Models.Pc", "IdPcNavigation")
                        .WithOne("Motherboard")
                        .HasForeignKey("Tescat.Models.Motherboard", "IdPc")
                        .HasConstraintName("FK_Motherboard_PC");

                    b.Navigation("IdPcNavigation");
                });

            modelBuilder.Entity("Tescat.Models.Pc", b =>
                {
                    b.HasOne("Tescat.Models.User", "IdUserNavigation")
                        .WithMany("Pcs")
                        .HasForeignKey("IdUser")
                        .HasConstraintName("FK_PC_Users");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("Tescat.Models.PcCredential", b =>
                {
                    b.HasOne("Tescat.Models.Pc", "IdPcNavigation")
                        .WithOne("PcCredential")
                        .HasForeignKey("Tescat.Models.PcCredential", "IdPc")
                        .IsRequired()
                        .HasConstraintName("FK_PC_Credentials_PC");

                    b.Navigation("IdPcNavigation");
                });

            modelBuilder.Entity("Tescat.Models.PowerSupply", b =>
                {
                    b.HasOne("Tescat.Models.Pc", "IdPcNavigation")
                        .WithOne("PowerSupply")
                        .HasForeignKey("Tescat.Models.PowerSupply", "IdPc")
                        .HasConstraintName("FK_Power_Supply_PC");

                    b.Navigation("IdPcNavigation");
                });

            modelBuilder.Entity("Tescat.Models.Storage", b =>
                {
                    b.HasOne("Tescat.Models.Pc", "IdPcNavigation")
                        .WithMany("Storages")
                        .HasForeignKey("IdPc")
                        .HasConstraintName("FK_Storage_PC");

                    b.Navigation("IdPcNavigation");
                });

            modelBuilder.Entity("Tescat.Models.UserCredential", b =>
                {
                    b.HasOne("Tescat.Models.User", "IdUserNavigation")
                        .WithOne("UserCredential")
                        .HasForeignKey("Tescat.Models.UserCredential", "IdUser")
                        .HasConstraintName("FK_User_Credentials_Users");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("Tescat.Models.Pc", b =>
                {
                    b.Navigation("Cpu");

                    b.Navigation("Gpu");

                    b.Navigation("LaptopBattery");

                    b.Navigation("MemoryRams");

                    b.Navigation("Motherboard");

                    b.Navigation("PcCredential");

                    b.Navigation("PowerSupply");

                    b.Navigation("Storages");
                });

            modelBuilder.Entity("Tescat.Models.User", b =>
                {
                    b.Navigation("Email");

                    b.Navigation("Pcs");

                    b.Navigation("UserCredential");
                });
#pragma warning restore 612, 618
        }
    }
}
