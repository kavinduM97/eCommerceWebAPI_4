﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eCommerceWebAPI.DataAccess;

#nullable disable

namespace eCommerceWebAPI.DataAccess.Migrations
{
    [DbContext(typeof(DbbContext))]
    [Migration("20230110164254_orderchange")]
    partial class orderchange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("eCommerceWebAPI.Models.Category", b =>
                {
                    b.Property<int>("categoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("categoryId"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("categoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            categoryId = 1,
                            description = "This is and 15w",
                            name = "Electrical Db"
                        });
                });

            modelBuilder.Entity("eCommerceWebAPI.Models.Order", b =>
                {
                    b.Property<int>("orderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("orderId"));

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("state")
                        .HasColumnType("int");

                    b.Property<string>("useremail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("orderId");

                    b.HasIndex("useremail");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("eCommerceWebAPI.Models.Product", b =>
                {
                    b.Property<int>("productId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("productId"));

                    b.Property<int>("categoryId")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("stock")
                        .HasColumnType("int");

                    b.HasKey("productId");

                    b.HasIndex("categoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("eCommerceWebAPI.Models.ProductOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("orderId")
                        .HasColumnType("int");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("orderId");

                    b.HasIndex("productId");

                    b.ToTable("ProductOrders");
                });

            modelBuilder.Entity("eCommerceWebAPI.Models.User", b =>
                {
                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("eCommerceWebAPI.Models.Order", b =>
                {
                    b.HasOne("eCommerceWebAPI.Models.User", "user")
                        .WithMany("orders")
                        .HasForeignKey("useremail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("eCommerceWebAPI.Models.Product", b =>
                {
                    b.HasOne("eCommerceWebAPI.Models.Category", "category")
                        .WithMany("Products")
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");
                });

            modelBuilder.Entity("eCommerceWebAPI.Models.ProductOrder", b =>
                {
                    b.HasOne("eCommerceWebAPI.Models.Order", "Order")
                        .WithMany("productorders")
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eCommerceWebAPI.Models.Product", "product")
                        .WithMany("productorders")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("product");
                });

            modelBuilder.Entity("eCommerceWebAPI.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("eCommerceWebAPI.Models.Order", b =>
                {
                    b.Navigation("productorders");
                });

            modelBuilder.Entity("eCommerceWebAPI.Models.Product", b =>
                {
                    b.Navigation("productorders");
                });

            modelBuilder.Entity("eCommerceWebAPI.Models.User", b =>
                {
                    b.Navigation("orders");
                });
#pragma warning restore 612, 618
        }
    }
}
