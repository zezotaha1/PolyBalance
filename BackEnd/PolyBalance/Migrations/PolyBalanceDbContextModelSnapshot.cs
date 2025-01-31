﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PolyBalance.Models;

#nullable disable

namespace PolyBalance.Migrations
{
    [DbContext(typeof(PolyBalanceDbContext))]
    partial class PolyBalanceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<decimal>("AccountDetailAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("AccountDetailCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("AccountDetailNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AccountDetailType")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PartyId")
                        .HasColumnType("int");

                    b.HasKey("AccountDetailId");

                    b.HasIndex("PartyId");

                    b.ToTable("AccountDetails", t =>
                        {
                            t.HasTrigger("UpdateAccountDetailsAmount");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("PolyBalance.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("ExpenseDate")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime>("AdjustmentDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("AdjustmentType")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ItemsPricesId")
                        .HasColumnType("int");

                    b.Property<double>("QuantityChange")
                        .HasColumnType("float");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InventoryAdjustmentId");

                    b.HasIndex("ItemsPricesId");

                    b.ToTable("InventoryAdjustments");
                });

            modelBuilder.Entity("PolyBalance.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ItemCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("ItemCurrentStock")
                        .HasColumnType("float");

                    b.Property<string>("ItemDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("ItemReorderLevel")
                        .HasColumnType("float");

                    b.Property<bool>("ItemType")
                        .HasColumnType("bit");

                    b.Property<string>("ItemUnit")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ItemId");

                    b.HasIndex("ItemName")
                        .IsUnique();

                    b.ToTable("Items");
                });

            modelBuilder.Entity("PolyBalance.Models.ItemPrice", b =>
                {
                    b.Property<int>("ItemPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemPriceId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ItemPriceCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ItemPriceCurrentStock")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ItemPriceUnitCost")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ItemPriceId");

                    b.HasIndex("ItemId", "ItemPriceUnitCost")
                        .IsUnique();

                    b.ToTable("ItemsPrices", t =>
                        {
                            t.HasTrigger("UpdateItemCurrentStock");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("PolyBalance.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OrderPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("OrderType")
                        .HasColumnType("bit");

                    b.Property<int>("PartyId")
                        .HasColumnType("int");

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

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ItemPriceId")
                        .HasColumnType("int");

                    b.Property<decimal>("OrderDetailDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OrderDetailQuantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OrderDetailUnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("ItemPriceId");

                    b.HasIndex("OrderId", "ItemPriceId")
                        .IsUnique();

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

                    b.Property<DateTime>("PartyCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PartyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PartyPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("PartyRating")
                        .HasColumnType("int");

                    b.Property<double>("PartyTotalAmount")
                        .HasColumnType("float");

                    b.Property<int>("PartyTypeId")
                        .HasColumnType("int");

                    b.HasKey("PartyId");

                    b.HasIndex("PartyPhoneNumber")
                        .IsUnique();

                    b.HasIndex("PartyTypeId");

                    b.ToTable("Parties", t =>
                        {
                            t.HasTrigger("UpdatePartyAmount");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
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

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InventoryItemId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double>("ProductionQuantity")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

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
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("StoreCapacity")
                        .HasColumnType("float");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("StoreId");

                    b.HasIndex("StoreName", "StoreAddress")
                        .IsUnique();

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

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TransactionType")
                        .HasColumnType("bit");

                    b.HasKey("TransactionId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("PolyBalance.Models.AccountDetail", b =>
                {
                    b.HasOne("PolyBalance.Models.Party", "Party")
                        .WithMany("AccountDetails")
                        .HasForeignKey("PartyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Party");
                });

            modelBuilder.Entity("PolyBalance.Models.InventoryAdjustment", b =>
                {
                    b.HasOne("PolyBalance.Models.ItemPrice", "ItemPrice")
                        .WithMany("InventoryAdjustments")
                        .HasForeignKey("ItemsPricesId");

                    b.Navigation("ItemPrice");
                });

            modelBuilder.Entity("PolyBalance.Models.ItemPrice", b =>
                {
                    b.HasOne("PolyBalance.Models.Item", "Item")
                        .WithMany("ItemsPrices")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("PolyBalance.Models.Order", b =>
                {
                    b.HasOne("PolyBalance.Models.Party", "Party")
                        .WithMany("Orders")
                        .HasForeignKey("PartyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Party");
                });

            modelBuilder.Entity("PolyBalance.Models.OrderDetail", b =>
                {
                    b.HasOne("PolyBalance.Models.ItemPrice", "ItemPrice")
                        .WithMany("OrderDetail")
                        .HasForeignKey("ItemPriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PolyBalance.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
                    b.HasOne("PolyBalance.Models.ItemPrice", "ItemPrice")
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
                    b.HasOne("PolyBalance.Models.Item", "Item")
                        .WithMany("ProductionOrders")
                        .HasForeignKey("InventoryItemId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("PolyBalance.Models.Item", b =>
                {
                    b.Navigation("ItemsPrices");

                    b.Navigation("ProductionOrders");
                });

            modelBuilder.Entity("PolyBalance.Models.ItemPrice", b =>
                {
                    b.Navigation("InventoryAdjustments");

                    b.Navigation("OrderDetail");

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

            modelBuilder.Entity("PolyBalance.Models.Transaction", b =>
                {
                    b.Navigation("PaymentDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
