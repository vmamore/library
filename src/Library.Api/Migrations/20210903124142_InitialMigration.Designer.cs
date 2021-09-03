﻿// <auto-generated />
using System;
using Library.Api.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Library.Api.Migrations
{
    [DbContext(typeof(UsersDbContext))]
    [Migration("20210903124142_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "6.0.0-preview.6.21352.1")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Library.Api.Domain.BookRentals.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.ToTable("books", "rentals");
                });

            modelBuilder.Entity("Library.Api.Domain.BookRentals.BookRental", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LibrarianId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LocatorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RentedDay")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("ReturnedDay")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LibrarianId");

                    b.HasIndex("LocatorId");

                    b.ToTable("rentals", "rentals");
                });

            modelBuilder.Entity("Library.Api.Domain.BookRentals.Locator", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("locators", "rentals");
                });

            modelBuilder.Entity("Library.Api.Domain.BookRentals.Penalty", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("LocatorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LocatorId");

                    b.ToTable("penalties", "rentals");
                });

            modelBuilder.Entity("Library.Api.Domain.Inventory.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("Pages")
                        .HasColumnType("integer");

                    b.Property<string>("ReleasedYear")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("books", "inventory");
                });

            modelBuilder.Entity("Library.Api.Domain.Users.Librarian", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("librarians", "users");
                });

            modelBuilder.Entity("booksrentals", b =>
                {
                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookRentalId")
                        .HasColumnType("uuid");

                    b.HasKey("BookId", "BookRentalId");

                    b.HasIndex("BookRentalId");

                    b.ToTable("booksrentals");
                });

            modelBuilder.Entity("Library.Api.Domain.BookRentals.BookRental", b =>
                {
                    b.HasOne("Library.Api.Domain.Users.Librarian", "_librarian")
                        .WithMany()
                        .HasForeignKey("LibrarianId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Library.Api.Domain.BookRentals.Locator", "_locator")
                        .WithMany()
                        .HasForeignKey("LocatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("Library.Api.Domain.BookRentals.BookReturnDate", "DayToReturn", b1 =>
                        {
                            b1.Property<Guid>("BookRentalId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("DayToReturn");

                            b1.HasKey("BookRentalId");

                            b1.ToTable("rentals");

                            b1.WithOwner()
                                .HasForeignKey("BookRentalId");
                        });

                    b.Navigation("DayToReturn");

                    b.Navigation("_librarian");

                    b.Navigation("_locator");
                });

            modelBuilder.Entity("Library.Api.Domain.BookRentals.Locator", b =>
                {
                    b.OwnsOne("Library.Api.Domain.Shared.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("LocatorId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("City");

                            b1.Property<string>("District")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("District");

                            b1.Property<string>("Number")
                                .HasMaxLength(10)
                                .HasColumnType("character varying(10)")
                                .HasColumnName("Number");

                            b1.Property<string>("Street")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("Street");

                            b1.HasKey("LocatorId");

                            b1.ToTable("locators");

                            b1.WithOwner()
                                .HasForeignKey("LocatorId");
                        });

                    b.OwnsOne("Library.Api.Domain.Shared.ValueObjects.Age", "Age", b1 =>
                        {
                            b1.Property<Guid>("LocatorId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("BirthDate")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("BirthDate");

                            b1.HasKey("LocatorId");

                            b1.ToTable("locators");

                            b1.WithOwner()
                                .HasForeignKey("LocatorId");
                        });

                    b.OwnsOne("Library.Api.Domain.Shared.ValueObjects.CPF", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("LocatorId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .HasColumnType("text")
                                .HasColumnName("Cpf");

                            b1.HasKey("LocatorId");

                            b1.ToTable("locators");

                            b1.WithOwner()
                                .HasForeignKey("LocatorId");
                        });

                    b.OwnsOne("Library.Api.Domain.Shared.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("LocatorId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FirstName")
                                .HasColumnType("text")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .HasColumnType("text")
                                .HasColumnName("LastName");

                            b1.HasKey("LocatorId");

                            b1.ToTable("locators");

                            b1.WithOwner()
                                .HasForeignKey("LocatorId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Age");

                    b.Navigation("Cpf");

                    b.Navigation("Name");
                });

            modelBuilder.Entity("Library.Api.Domain.BookRentals.Penalty", b =>
                {
                    b.HasOne("Library.Api.Domain.BookRentals.Locator", "Locator")
                        .WithMany("Penalties")
                        .HasForeignKey("LocatorId");

                    b.Navigation("Locator");
                });

            modelBuilder.Entity("Library.Api.Domain.Inventory.Book", b =>
                {
                    b.OwnsOne("Library.Api.Domain.Shared.ValueObjects.ISBN", "ISBN", b1 =>
                        {
                            b1.Property<Guid>("BookId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("ISBN");

                            b1.HasKey("BookId");

                            b1.ToTable("books");

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.Navigation("ISBN");
                });

            modelBuilder.Entity("Library.Api.Domain.Users.Librarian", b =>
                {
                    b.OwnsOne("Library.Api.Domain.Shared.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("LibrarianId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("City");

                            b1.Property<string>("District")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("District");

                            b1.Property<string>("Number")
                                .HasMaxLength(10)
                                .HasColumnType("character varying(10)")
                                .HasColumnName("Number");

                            b1.Property<string>("Street")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("Street");

                            b1.HasKey("LibrarianId");

                            b1.ToTable("librarians");

                            b1.WithOwner()
                                .HasForeignKey("LibrarianId");
                        });

                    b.OwnsOne("Library.Api.Domain.Shared.ValueObjects.Age", "Age", b1 =>
                        {
                            b1.Property<Guid>("LibrarianId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("BirthDate")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("BirthDate");

                            b1.HasKey("LibrarianId");

                            b1.ToTable("librarians");

                            b1.WithOwner()
                                .HasForeignKey("LibrarianId");
                        });

                    b.OwnsOne("Library.Api.Domain.Shared.ValueObjects.CPF", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("LibrarianId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .HasColumnType("text")
                                .HasColumnName("Cpf");

                            b1.HasKey("LibrarianId");

                            b1.ToTable("librarians");

                            b1.WithOwner()
                                .HasForeignKey("LibrarianId");
                        });

                    b.OwnsOne("Library.Api.Domain.Shared.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("LibrarianId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FirstName")
                                .HasColumnType("text")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .HasColumnType("text")
                                .HasColumnName("LastName");

                            b1.HasKey("LibrarianId");

                            b1.ToTable("librarians");

                            b1.WithOwner()
                                .HasForeignKey("LibrarianId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Age");

                    b.Navigation("Cpf");

                    b.Navigation("Name");
                });

            modelBuilder.Entity("booksrentals", b =>
                {
                    b.HasOne("Library.Api.Domain.BookRentals.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Api.Domain.BookRentals.BookRental", null)
                        .WithMany()
                        .HasForeignKey("BookRentalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Library.Api.Domain.BookRentals.Locator", b =>
                {
                    b.Navigation("Penalties");
                });
#pragma warning restore 612, 618
        }
    }
}
