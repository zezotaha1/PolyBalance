using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PolyBalance.Models
{
    public partial class PolyBalanceDbContext : DbContext
    {
        public PolyBalanceDbContext(DbContextOptions<PolyBalanceDbContext> options) : base(options) { }

        public virtual required DbSet<AccountDetail> AccountDetails { get; set; }
        public virtual required DbSet<Expense> Expenses { get; set; }
        public virtual required DbSet<InventoryAdjustment> InventoryAdjustments { get; set; }
        public virtual required DbSet<Item> Items { get; set; }
        public virtual required DbSet<ItemPrice> ItemsPrices { get; set; }
        public virtual required DbSet<Order> Orders { get; set; }
        public virtual required DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual required DbSet<Overhead> Overheads { get; set; }
        public virtual required DbSet<Party> Parties { get; set; }
        public virtual required DbSet<PartyType> PartyTypes { get; set; }
        public virtual required DbSet<PaymentDetail> PaymentDetails { get; set; }
        public virtual required DbSet<ProductionMaterial> ProductionMaterials { get; set; }
        public virtual required DbSet<ProductionOrder> ProductionOrders { get; set; }
        public virtual required DbSet<Store> Stores { get; set; }
        public virtual required DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountDetail>(entity =>
            {
                entity.HasOne(d => d.Party)
                      .WithMany(p => p.AccountDetails)
                      .HasForeignKey(d => d.PartyId);
                entity.ToTable(tb => tb.HasTrigger("UpdateAccountDetailsAmount"));
            });

            modelBuilder.Entity<Store>()
            .HasIndex(s => new { s.StoreName, s.StoreAddress })
            .IsUnique();

            modelBuilder.Entity<Item>().HasIndex(item => item.ItemName).IsUnique();

            modelBuilder.Entity<ItemPrice>(entity =>
            {
                entity.HasOne(d => d.Item)
                      .WithMany(p => p.ItemsPrices)
                      .HasForeignKey(d => d.ItemId);
                entity.HasIndex(d =>new { d.ItemId,d.ItemPriceUnitCost }).IsUnique();
                entity.ToTable(tb => tb.HasTrigger("UpdateItemCurrentStock"));
            });

            modelBuilder.Entity<InventoryAdjustment>(entity =>
            {
                entity.HasOne(d => d.ItemPrice)
                      .WithMany(p => p.InventoryAdjustments)
                      .HasForeignKey(d => d.ItemsPricesId);
            });

            modelBuilder.Entity<Order>(entity =>
            {

                entity.HasOne(d => d.Party)
                      .WithMany(p => p.Orders)
                      .HasForeignKey(d => d.PartyId);

            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {

                entity.HasOne(d => d.Order)
                      .WithMany(p => p.OrderDetails)
                      .HasForeignKey(d => d.OrderId);
                entity.HasOne(d => d.ItemPrice)
                      .WithMany(p => p.OrderDetail)
                      .HasForeignKey(d => d.ItemPriceId);
                entity.HasIndex(d => new { d.OrderId, d.ItemPriceId}).IsUnique();
                entity.ToTable(tb => tb.HasTrigger("UpdateItemPriceCorntStockWhenInsert"));

            });

            modelBuilder.Entity<Overhead>(entity =>
            {
                entity.HasOne(d => d.ProductionOrder)
                      .WithMany(p => p.Overheads)
                      .HasForeignKey(d => d.ProductionOrderId);
            });


            modelBuilder.Entity<Party>(entity =>
            {
                entity.HasOne(d => d.PartyType)
                      .WithMany(p => p.Parties)
                      .HasForeignKey(d => d.PartyTypeId);

                entity.HasIndex(p => p.PartyPhoneNumber).IsUnique();
                entity.ToTable(tb => tb.HasTrigger("UpdatePartyAmount"));
            });

            modelBuilder.Entity<PartyType>().HasIndex(pt => pt.PartyTypeName).IsUnique();

            modelBuilder.Entity<PaymentDetail>(entity =>
            {
                entity.HasOne(d => d.Transaction)
                      .WithMany(p => p.PaymentDetails)
                      .HasForeignKey(d => d.TransactionId);
            });

            modelBuilder.Entity<ProductionMaterial>(entity =>
            {
                entity.HasOne(d => d.ItemPrice)
                      .WithMany(p => p.ProductionMaterials)
                      .HasForeignKey(d => d.ItemsPricesAndStoreId);

                entity.HasOne(d => d.ProductionOrder)
                      .WithMany(p => p.ProductionMaterials)
                      .HasForeignKey(d => d.ProductionOrderId);
            });

            modelBuilder.Entity<ProductionOrder>(entity =>
            {
                entity.HasOne(d => d.Item)
                      .WithMany(p => p.ProductionOrders)
                      .HasForeignKey(d => d.InventoryItemId);
            });
        }
    }
}
