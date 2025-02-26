﻿// <auto-generated />
using System;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Backend.Models.CasaDeMarcat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DestinatieAmef")
                        .HasColumnType("text");

                    b.Property<string>("NUI")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("NrMinuteReconectare")
                        .HasColumnType("integer");

                    b.Property<int>("TipProfil")
                        .HasColumnType("integer");

                    b.Property<bool>("TipReset")
                        .HasColumnType("boolean");

                    b.Property<string>("URLAmef")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CaseDeMarcat");
                });

            modelBuilder.Entity("Backend.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Backend.Models.XML.Msj", b =>
                {
                    b.Property<string>("IdM")
                        .HasColumnType("text");

                    b.Property<int>("CasaDeMarcatId")
                        .HasColumnType("integer");

                    b.HasKey("IdM");

                    b.HasIndex("CasaDeMarcatId");

                    b.ToTable("Mesaje");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Backend.Models.CasaDeMarcat", b =>
                {
                    b.HasOne("Backend.Models.User", "User")
                        .WithMany("CaseDeMarcat")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend.Models.XML.Msj", b =>
                {
                    b.HasOne("Backend.Models.CasaDeMarcat", "CasaDeMarcat")
                        .WithMany("MesajXML")
                        .HasForeignKey("CasaDeMarcatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Backend.Models.XML.Bon.Bon", "Bon", b1 =>
                        {
                            b1.Property<string>("MsjIdM")
                                .HasColumnType("text");

                            b1.Property<string>("IdB")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<decimal>("TotB")
                                .HasColumnType("numeric");

                            b1.Property<decimal>("TotTva")
                                .HasColumnType("numeric");

                            b1.HasKey("MsjIdM");

                            b1.ToTable("Mesaje");

                            b1.WithOwner()
                                .HasForeignKey("MsjIdM");

                            b1.OwnsMany("Backend.Models.XML.Bon.Cote", "Cote", b2 =>
                                {
                                    b2.Property<string>("BonMsjIdM")
                                        .HasColumnType("text");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b2.Property<int>("Id"));

                                    b2.Property<int>("Cota")
                                        .HasColumnType("integer");

                                    b2.Property<decimal>("Tva")
                                        .HasColumnType("numeric");

                                    b2.HasKey("BonMsjIdM", "Id");

                                    b2.ToTable("Cote");

                                    b2.WithOwner()
                                        .HasForeignKey("BonMsjIdM");
                                });

                            b1.Navigation("Cote");
                        });

                    b.OwnsOne("Backend.Models.XML.Me.ME", "ME", b1 =>
                        {
                            b1.Property<string>("MsjIdM")
                                .HasColumnType("text");

                            b1.Property<int>("NrB")
                                .HasColumnType("integer");

                            b1.HasKey("MsjIdM");

                            b1.ToTable("Mesaje");

                            b1.WithOwner()
                                .HasForeignKey("MsjIdM");

                            b1.OwnsMany("Backend.Models.XML.Me.Ev", "Ev", b2 =>
                                {
                                    b2.Property<string>("MEMsjIdM")
                                        .HasColumnType("text");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b2.Property<int>("Id"));

                                    b2.Property<DateTime>("DataF")
                                        .HasColumnType("timestamp with time zone");

                                    b2.Property<DateTime>("DataI")
                                        .HasColumnType("timestamp with time zone");

                                    b2.Property<int>("TipE")
                                        .HasColumnType("integer");

                                    b2.HasKey("MEMsjIdM", "Id");

                                    b2.ToTable("Ev");

                                    b2.WithOwner()
                                        .HasForeignKey("MEMsjIdM");
                                });

                            b1.Navigation("Ev");
                        });

                    b.OwnsOne("Backend.Models.XML.rB.RB", "RB", b1 =>
                        {
                            b1.Property<string>("MsjIdM")
                                .HasColumnType("text");

                            b1.Property<string>("IdR")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("MonRef")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<int>("NrA")
                                .HasColumnType("integer");

                            b1.Property<int>("NrAv")
                                .HasColumnType("integer");

                            b1.Property<int>("NrB")
                                .HasColumnType("integer");

                            b1.Property<int>("NrBC")
                                .HasColumnType("integer");

                            b1.Property<int>("NrM")
                                .HasColumnType("integer");

                            b1.Property<int>("NrR")
                                .HasColumnType("integer");

                            b1.Property<float>("SumeServIn")
                                .HasColumnType("real");

                            b1.Property<float>("SumeServOut")
                                .HasColumnType("real");

                            b1.Property<float>("TotA")
                                .HasColumnType("real");

                            b1.Property<float>("TotB")
                                .HasColumnType("real");

                            b1.Property<float>("TotBC")
                                .HasColumnType("real");

                            b1.Property<float>("TotM")
                                .HasColumnType("real");

                            b1.Property<float>("TotNet")
                                .HasColumnType("real");

                            b1.Property<float>("TotR")
                                .HasColumnType("real");

                            b1.Property<float>("TotTaxe")
                                .HasColumnType("real");

                            b1.Property<float>("TotTva")
                                .HasColumnType("real");

                            b1.Property<float>("TotTvaC")
                                .HasColumnType("real");

                            b1.HasKey("MsjIdM");

                            b1.ToTable("Mesaje");

                            b1.WithOwner()
                                .HasForeignKey("MsjIdM");

                            b1.OwnsOne("Backend.Models.XML.rB.AV", "Av", b2 =>
                                {
                                    b2.Property<string>("RBMsjIdM")
                                        .HasColumnType("text");

                                    b2.Property<DateTime>("Data")
                                        .HasColumnType("timestamp with time zone");

                                    b2.HasKey("RBMsjIdM");

                                    b2.ToTable("Mesaje");

                                    b2.WithOwner()
                                        .HasForeignKey("RBMsjIdM");
                                });

                            b1.OwnsMany("Backend.Models.XML.rB.CoteZ", "CoteZList", b2 =>
                                {
                                    b2.Property<string>("RBMsjIdM")
                                        .HasColumnType("text");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b2.Property<int>("Id"));

                                    b2.Property<int>("Cota")
                                        .HasColumnType("integer");

                                    b2.Property<float>("Tva")
                                        .HasColumnType("real");

                                    b2.Property<float>("ValOp")
                                        .HasColumnType("real");

                                    b2.HasKey("RBMsjIdM", "Id");

                                    b2.ToTable("CoteZ");

                                    b2.WithOwner()
                                        .HasForeignKey("RBMsjIdM");
                                });

                            b1.OwnsOne("Backend.Models.XML.rB.PL", "Pl", b2 =>
                                {
                                    b2.Property<string>("RBMsjIdM")
                                        .HasColumnType("text");

                                    b2.Property<string>("MonPl")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<int>("TipP")
                                        .HasColumnType("integer");

                                    b2.Property<double>("ValPl")
                                        .HasColumnType("double precision");

                                    b2.HasKey("RBMsjIdM");

                                    b2.ToTable("Mesaje");

                                    b2.WithOwner()
                                        .HasForeignKey("RBMsjIdM");
                                });

                            b1.Navigation("Av")
                                .IsRequired();

                            b1.Navigation("CoteZList");

                            b1.Navigation("Pl")
                                .IsRequired();
                        });

                    b.Navigation("Bon")
                        .IsRequired();

                    b.Navigation("CasaDeMarcat");

                    b.Navigation("ME")
                        .IsRequired();

                    b.Navigation("RB")
                        .IsRequired();
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
                    b.HasOne("Backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Backend.Models.User", null)
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

                    b.HasOne("Backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Models.CasaDeMarcat", b =>
                {
                    b.Navigation("MesajXML");
                });

            modelBuilder.Entity("Backend.Models.User", b =>
                {
                    b.Navigation("CaseDeMarcat");
                });
#pragma warning restore 612, 618
        }
    }
}
