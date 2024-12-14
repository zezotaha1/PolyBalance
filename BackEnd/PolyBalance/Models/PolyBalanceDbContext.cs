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
        public virtual required DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual required DbSet<ItemsPricesAndStore> ItemsPricesAndStores { get; set; }
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
            });

            modelBuilder.Entity<InventoryAdjustment>(entity =>
            {
                entity.HasOne(d => d.ItemPrice)
                      .WithMany(p => p.InventoryAdjustments)
                      .HasForeignKey(d => d.ItemsPricesAndStoreId);
            });

            modelBuilder.Entity<ItemsPricesAndStore>(entity =>
            {
                entity.HasOne(d => d.Item)
                      .WithMany(p => p.ItemsPricesAndStores)
                      .HasForeignKey(d => d.InventoryItemId);

                entity.HasOne(d => d.Store)
                      .WithMany(p => p.ItemsPricesAndStores)
                      .HasForeignKey(d => d.StoreId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(d => d.LineTotal)
                      .HasComputedColumnSql("[TotalAmount]-([TotalAmount]*[Discount])", true);

                entity.HasOne(d => d.Party)
                      .WithMany(p => p.Orders)
                      .HasForeignKey(d => d.PartyId);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.TotalPrice)
                      .HasComputedColumnSql("([Quantity]*[UnitPrice])", true);

                entity.Property(d => d.LineTotal)
                      .HasComputedColumnSql("([Quantity]*[UnitPrice])-(([Quantity]*[UnitPrice])*[Discount])", true);

                entity.HasOne(d => d.ItemPrice)
                      .WithMany(p => p.OrderDetails)
                      .HasForeignKey(d => d.ItemsPricesAndStoreId);

                entity.HasOne(d => d.Order)
                      .WithMany(p => p.OrderDetails)
                      .HasForeignKey(d => d.OrderId);
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
            });

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
