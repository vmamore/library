﻿// <auto-generated />
using System;
using Library.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Library.Api.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    partial class LibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "6.0.0-preview.6.21352.1")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Library.Api.Domain.Books.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("Pages")
                        .HasMaxLength(5)
                        .HasColumnType("integer");

                    b.Property<string>("ReleasedYear")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("Version")
                        .HasMaxLength(5)
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library.Api.Domain.Books.BookRent", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("BookCondition")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DayToReturn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RentedDay")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ReturnedDay")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("BookRent");
                });

            modelBuilder.Entity("Library.Api.Domain.Users.Librarian", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Librarian");
                });

            modelBuilder.Entity("Library.Api.Domain.Users.Locator", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Locator");
                });

            modelBuilder.Entity("Library.Api.Domain.Books.BookRent", b =>
                {
                    b.HasOne("Library.Api.Domain.Books.Book", null)
                        .WithMany("Rents")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Library.Api.Domain.Users.Librarian", b =>
                {
                    b.OwnsOne("Library.Api.Domain.Users.ValueObjects.Address", "Address", b1 =>
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

                            b1.ToTable("Librarian");

                            b1.WithOwner()
                                .HasForeignKey("LibrarianId");
                        });

                    b.OwnsOne("Library.Api.Domain.Users.ValueObjects.Age", "Age", b1 =>
                        {
                            b1.Property<Guid>("LibrarianId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("BirthDate")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("BirthDate");

                            b1.HasKey("LibrarianId");

                            b1.ToTable("Librarian");

                            b1.WithOwner()
                                .HasForeignKey("LibrarianId");
                        });

                    b.OwnsOne("Library.Api.Domain.Users.ValueObjects.CPF", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("LibrarianId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .HasColumnType("text")
                                .HasColumnName("Cpf");

                            b1.HasKey("LibrarianId");

                            b1.ToTable("Librarian");

                            b1.WithOwner()
                                .HasForeignKey("LibrarianId");
                        });

                    b.OwnsOne("Library.Api.Domain.Users.ValueObjects.Name", "Name", b1 =>
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

                            b1.ToTable("Librarian");

                            b1.WithOwner()
                                .HasForeignKey("LibrarianId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Age");

                    b.Navigation("Cpf");

                    b.Navigation("Name");
                });

            modelBuilder.Entity("Library.Api.Domain.Users.Locator", b =>
                {
                    b.OwnsOne("Library.Api.Domain.Users.ValueObjects.Address", "Address", b1 =>
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

                            b1.ToTable("Locator");

                            b1.WithOwner()
                                .HasForeignKey("LocatorId");
                        });

                    b.OwnsOne("Library.Api.Domain.Users.ValueObjects.Age", "Age", b1 =>
                        {
                            b1.Property<Guid>("LocatorId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("BirthDate")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("BirthDate");

                            b1.HasKey("LocatorId");

                            b1.ToTable("Locator");

                            b1.WithOwner()
                                .HasForeignKey("LocatorId");
                        });

                    b.OwnsOne("Library.Api.Domain.Users.ValueObjects.CPF", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("LocatorId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .HasColumnType("text")
                                .HasColumnName("Cpf");

                            b1.HasKey("LocatorId");

                            b1.ToTable("Locator");

                            b1.WithOwner()
                                .HasForeignKey("LocatorId");
                        });

                    b.OwnsOne("Library.Api.Domain.Users.ValueObjects.Name", "Name", b1 =>
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

                            b1.ToTable("Locator");

                            b1.WithOwner()
                                .HasForeignKey("LocatorId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Age");

                    b.Navigation("Cpf");

                    b.Navigation("Name");
                });

            modelBuilder.Entity("Library.Api.Domain.Books.Book", b =>
                {
                    b.Navigation("Rents");
                });
#pragma warning restore 612, 618
        }
    }
}
