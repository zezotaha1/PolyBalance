﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PolyBalance.Models;

#nullable disable

namespace PolyBalance.Migrations
{
    [DbContext(typeof(PolyBalanceDbContext))]
    [Migration("20241230200311_addUniqueConstrainToPartyTypeName")]
    partial class addUniqueConstrainToPartyTypeName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PolyBalance.Models.AccountDetail", b =>
                {
                    b.Property<int>("AccountDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountDetailId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OprationId")
                        .HasColumnType("int");

                    b.Property<string>("OprationType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("PartyId")
                        .HasColumnType("int");

                    b.Property<bool?>("Type")
                        .HasColumnType("bit");

                    b.HasKey("AccountDetailId");

                    b.HasIndex("PartyId");

                    b.ToTable("AccountDetails");
                });

            modelBuilder.Entity("PolyBalance.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateOnly>("ExpenseDate")
                        .HasColumnType("date");

                    b.Property<string>("ExpenseType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("PolyBalance.Models.InventoryAdjustment", b =>
                {
                    b.Property<int>("InventoryAdjustmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventoryAdjustmentId"));

                    b.Property<DateOnly>("AdjustmentDate")
                        .HasColumnType("date");

                    b.Property<bool>("AdjustmentType")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ItemsPricesAndStoreId")
                        .HasColumnType("int");

                    b.Property<double>("QuantityChange")
                        .HasColumnType("float");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InventoryAdjustmentId");

                    b.HasIndex("ItemsPricesAndStoreId");

                    b.ToTable("InventoryAdjustments");
                });

            modelBuilder.Entity("PolyBalance.Models.InventoryItem", b =>
                {
                    b.Property<int>("InventoryItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventoryItemId"));

                    b.Property<double?>("CurrentStock")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double?>("ReorderLevel")
                        .HasColumnType("float");

                    b.Property<bool>("Type")
                        .HasColumnType("bit");

                    b.Property<string>("Unit")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("InventoryItemId");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("PolyBalance.Models.ItemsPricesAndStore", b =>
                {
                    b.Property<int>("ItemsPricesAndStoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemsPricesAndStoreId"));

                    b.Property<double>("CurrentStock")
                        .HasColumnType("float");

                    b.Property<int?>("InventoryItemId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateOnly>("StorageDate")
                        .HasColumnType("date");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int");

                    b.Property<double>("UnitCost")
                        .HasColumnType("float");

                    b.Property<double?>("UnitSellingPrice")
                        .HasColumnType("float");

                    b.HasKey("ItemsPricesAndStoreId");

                    b.HasIndex("InventoryItemId");

                    b.HasIndex("StoreId");

                    b.ToTable("ItemsPricesAndStores");
                });

            modelBuilder.Entity("PolyBalance.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double>("LineTotal")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("float")
                        .HasComputedColumnSql("[TotalAmount]-([TotalAmount]*[Discount])", true);

                    b.Property<DateOnly>("OrderDate")
                        .HasColumnType("date");

                    b.Property<bool>("OrderType")
                        .HasColumnType("bit");

                    b.Property<int?>("PartyId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.HasKey("OrderId");

                    b.HasIndex("PartyId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PolyBalance.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"));

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ItemsPricesAndStoreId")
                        .HasColumnType("int");

                    b.Property<double>("LineTotal")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("float")
                        .HasComputedColumnSql("([Quantity]*[UnitPrice])-(([Quantity]*[UnitPrice])*[Discount])", true);

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<double>("TotalPrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("float")
                        .HasComputedColumnSql("([Quantity]*[UnitPrice])", true);

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("ItemsPricesAndStoreId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("PolyBalance.Models.Overhead", b =>
                {
                    b.Property<int>("OverheadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OverheadId"));

                    b.Property<double?>("Cost")
                        .HasColumnType("float");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("OverheadType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ProductionOrderId")
                        .HasColumnType("int");

                    b.HasKey("OverheadId");

                    b.HasIndex("ProductionOrderId");

                    b.ToTable("Overheads");
                });

            modelBuilder.Entity("PolyBalance.Models.Party", b =>
                {
                    b.Property<int>("PartyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PartyId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PartyAddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PartyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PartyPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("PartyRateing")
                        .HasColumnType("int");

                    b.Property<double>("PartyTotalAmount")
                        .HasColumnType("float");

                    b.Property<int>("PartyTypeId")
                        .HasColumnType("int");

                    b.HasKey("PartyId");

                    b.HasIndex("PartyPhoneNumber")
                        .IsUnique();

                    b.HasIndex("PartyTypeId");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("PolyBalance.Models.PartyType", b =>
                {
                    b.Property<int>("PartyTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PartyTypeId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PartyTypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PartyTypeId");

                    b.HasIndex("PartyTypeName")
                        .IsUnique();

                    b.ToTable("PartyTypes");
                });

            modelBuilder.Entity("PolyBalance.Models.PaymentDetail", b =>
                {
                    b.Property<int>("PaymentDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentDetailId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("PaymentDetailId");

                    b.HasIndex("TransactionId");

                    b.ToTable("PaymentDetails");
                });

            modelBuilder.Entity("PolyBalance.Models.ProductionMaterial", b =>
                {
                    b.Property<int>("ProductionMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductionMaterialId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ItemsPricesAndStoreId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductionOrderId")
                        .HasColumnType("int");

                    b.Property<double>("QuantityUsedPerUnit")
                        .HasColumnType("float");

                    b.HasKey("ProductionMaterialId");

                    b.HasIndex("ItemsPricesAndStoreId");

                    b.HasIndex("ProductionOrderId");

                    b.ToTable("ProductionMaterials");
                });

            modelBuilder.Entity("PolyBalance.Models.ProductionOrder", b =>
                {
                    b.Property<int>("ProductionOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductionOrderId"));

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<int?>("InventoryItemId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double>("ProductionQuantity")
                        .HasColumnType("float");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("UintCost")
                        .HasColumnType("float");

                    b.HasKey("ProductionOrderId");

                    b.HasIndex("InventoryItemId");

                    b.ToTable("ProductionOrders");
                });

            modelBuilder.Entity("PolyBalance.Models.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("StoreAddress")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("StoreCapacity")
                        .HasColumnType("float");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("StoreId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("PolyBalance.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OprationId")
                        .HasColumnType("int");

                    b.Property<string>("OprationType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateOnly>("TransactionDate")
                        .HasColumnType("date");

                    b.Property<bool>("TransactionType")
                        .HasColumnType("bit");

                    b.HasKey("TransactionId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("PolyBalance.Models.AccountDetail", b =>
                {
                    b.HasOne("PolyBalance.Models.Party", "Party")
                        .WithMany("AccountDetails")
                        .HasForeignKey("PartyId");

                    b.Navigation("Party");
                });

            modelBuilder.Entity("PolyBalance.Models.InventoryAdjustment", b =>
                {
                    b.HasOne("PolyBalance.Models.ItemsPricesAndStore", "ItemPrice")
                        .WithMany("InventoryAdjustments")
                        .HasForeignKey("ItemsPricesAndStoreId");

                    b.Navigation("ItemPrice");
                });

            modelBuilder.Entity("PolyBalance.Models.ItemsPricesAndStore", b =>
                {
                    b.HasOne("PolyBalance.Models.InventoryItem", "Item")
                        .WithMany("ItemsPricesAndStores")
                        .HasForeignKey("InventoryItemId");

                    b.HasOne("PolyBalance.Models.Store", "Store")
                        .WithMany("ItemsPricesAndStores")
                        .HasForeignKey("StoreId");

                    b.Navigation("Item");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("PolyBalance.Models.Order", b =>
                {
                    b.HasOne("PolyBalance.Models.Party", "Party")
                        .WithMany("Orders")
                        .HasForeignKey("PartyId");

                    b.Navigation("Party");
                });

            modelBuilder.Entity("PolyBalance.Models.OrderDetail", b =>
                {
                    b.HasOne("PolyBalance.Models.ItemsPricesAndStore", "ItemPrice")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ItemsPricesAndStoreId");

                    b.HasOne("PolyBalance.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId");

                    b.Navigation("ItemPrice");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PolyBalance.Models.Overhead", b =>
                {
                    b.HasOne("PolyBalance.Models.ProductionOrder", "ProductionOrder")
                        .WithMany("Overheads")
                        .HasForeignKey("ProductionOrderId");

                    b.Navigation("ProductionOrder");
                });

            modelBuilder.Entity("PolyBalance.Models.Party", b =>
                {
                    b.HasOne("PolyBalance.Models.PartyType", "PartyType")
                        .WithMany("Parties")
                        .HasForeignKey("PartyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PartyType");
                });

            modelBuilder.Entity("PolyBalance.Models.PaymentDetail", b =>
                {
                    b.HasOne("PolyBalance.Models.Transaction", "Transaction")
                        .WithMany("PaymentDetails")
                        .HasForeignKey("TransactionId");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("PolyBalance.Models.ProductionMaterial", b =>
                {
                    b.HasOne("PolyBalance.Models.ItemsPricesAndStore", "ItemPrice")
                        .WithMany("ProductionMaterials")
                        .HasForeignKey("ItemsPricesAndStoreId");

                    b.HasOne("PolyBalance.Models.ProductionOrder", "ProductionOrder")
                        .WithMany("ProductionMaterials")
                        .HasForeignKey("ProductionOrderId");

                    b.Navigation("ItemPrice");

                    b.Navigation("ProductionOrder");
                });

            modelBuilder.Entity("PolyBalance.Models.ProductionOrder", b =>
                {
                    b.HasOne("PolyBalance.Models.InventoryItem", "Item")
                        .WithMany("ProductionOrders")
                        .HasForeignKey("InventoryItemId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("PolyBalance.Models.InventoryItem", b =>
                {
                    b.Navigation("ItemsPricesAndStores");

                    b.Navigation("ProductionOrders");
                });

            modelBuilder.Entity("PolyBalance.Models.ItemsPricesAndStore", b =>
                {
                    b.Navigation("InventoryAdjustments");

                    b.Navigation("OrderDetails");

                    b.Navigation("ProductionMaterials");
                });

            modelBuilder.Entity("PolyBalance.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("PolyBalance.Models.Party", b =>
                {
                    b.Navigation("AccountDetails");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PolyBalance.Models.PartyType", b =>
                {
                    b.Navigation("Parties");
                });

            modelBuilder.Entity("PolyBalance.Models.ProductionOrder", b =>
                {
                    b.Navigation("Overheads");

                    b.Navigation("ProductionMaterials");
                });

            modelBuilder.Entity("PolyBalance.Models.Store", b =>
                {
                    b.Navigation("ItemsPricesAndStores");
                });

            modelBuilder.Entity("PolyBalance.Models.Transaction", b =>
                {
                    b.Navigation("PaymentDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
