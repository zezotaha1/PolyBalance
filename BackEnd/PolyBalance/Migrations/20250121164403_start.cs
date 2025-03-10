﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolyBalance.Migrations
{
    /// <inheritdoc />
    public partial class start : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    ExpenseType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemType = table.Column<bool>(type: "bit", nullable: false),
                    ItemUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ItemCurrentStock = table.Column<double>(type: "float", nullable: false),
                    ItemCreatedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    ItemReorderLevel = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "PartyTypes",
                columns: table => new
                {
                    PartyTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyTypes", x => x.PartyTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StoreAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StoreCapacity = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<bool>(type: "bit", nullable: false),
                    OprationId = table.Column<int>(type: "int", nullable: true),
                    OprationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                });

            migrationBuilder.CreateTable(
                name: "ItemsPrices",
                columns: table => new
                {
                    ItemPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemPriceUnitCost = table.Column<double>(type: "float", nullable: false),
                    ItemPriceCurrentStock = table.Column<double>(type: "float", nullable: false),
                    ItemPriceCreatedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsPrices", x => x.ItemPriceId);
                    table.ForeignKey(
                        name: "FK_ItemsPrices_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrders",
                columns: table => new
                {
                    ProductionOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryItemId = table.Column<int>(type: "int", nullable: true),
                    ProductionQuantity = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UintCost = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrders", x => x.ProductionOrderId);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_Items_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId");
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    PartyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyTypeId = table.Column<int>(type: "int", nullable: false),
                    PartyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PartyPhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PartyAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PartyRating = table.Column<int>(type: "int", nullable: false),
                    PartyTotalAmount = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PartyCreatedAt = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.PartyId);
                    table.ForeignKey(
                        name: "FK_Parties_PartyTypes_PartyTypeId",
                        column: x => x.PartyTypeId,
                        principalTable: "PartyTypes",
                        principalColumn: "PartyTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    PaymentDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.PaymentDetailId);
                    table.ForeignKey(
                        name: "FK_PaymentDetails_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId");
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustments",
                columns: table => new
                {
                    InventoryAdjustmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemsPricesId = table.Column<int>(type: "int", nullable: true),
                    AdjustmentType = table.Column<bool>(type: "bit", nullable: false),
                    QuantityChange = table.Column<double>(type: "float", nullable: false),
                    AdjustmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAdjustments", x => x.InventoryAdjustmentId);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustments_ItemsPrices_ItemsPricesId",
                        column: x => x.ItemsPricesId,
                        principalTable: "ItemsPrices",
                        principalColumn: "ItemPriceId");
                });

            migrationBuilder.CreateTable(
                name: "Overheads",
                columns: table => new
                {
                    OverheadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionOrderId = table.Column<int>(type: "int", nullable: true),
                    OverheadType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Overheads", x => x.OverheadId);
                    table.ForeignKey(
                        name: "FK_Overheads_ProductionOrders_ProductionOrderId",
                        column: x => x.ProductionOrderId,
                        principalTable: "ProductionOrders",
                        principalColumn: "ProductionOrderId");
                });

            migrationBuilder.CreateTable(
                name: "ProductionMaterials",
                columns: table => new
                {
                    ProductionMaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionOrderId = table.Column<int>(type: "int", nullable: true),
                    ItemsPricesAndStoreId = table.Column<int>(type: "int", nullable: true),
                    QuantityUsedPerUnit = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionMaterials", x => x.ProductionMaterialId);
                    table.ForeignKey(
                        name: "FK_ProductionMaterials_ItemsPrices_ItemsPricesAndStoreId",
                        column: x => x.ItemsPricesAndStoreId,
                        principalTable: "ItemsPrices",
                        principalColumn: "ItemPriceId");
                    table.ForeignKey(
                        name: "FK_ProductionMaterials_ProductionOrders_ProductionOrderId",
                        column: x => x.ProductionOrderId,
                        principalTable: "ProductionOrders",
                        principalColumn: "ProductionOrderId");
                });

            migrationBuilder.CreateTable(
                name: "AccountDetails",
                columns: table => new
                {
                    AccountDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyId = table.Column<int>(type: "int", nullable: false),
                    AccountDetailType = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    AccountDetailAmount = table.Column<double>(type: "float", nullable: false),
                    AccountDetailNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountDetailCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDetails", x => x.AccountDetailId);
                    table.ForeignKey(
                        name: "FK_AccountDetails_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderType = table.Column<bool>(type: "bit", nullable: false),
                    PartyId = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    OrderTotalAmount = table.Column<double>(type: "float", nullable: false),
                    OrderDiscount = table.Column<float>(type: "real", nullable: false),
                    OrderLineTotal = table.Column<double>(type: "float", nullable: false, computedColumnSql: "[OrderTotalAmount]-([OrderTotalAmount]*[OrderDiscount])", stored: true),
                    OrderPaid = table.Column<double>(type: "float", nullable: false),
                    OrderRemender = table.Column<double>(type: "float", nullable: false, computedColumnSql: "([OrderTotalAmount]-([OrderTotalAmount]*[OrderDiscount]))-[OrderPaid]", stored: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    ItemsPriceId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false, computedColumnSql: "([Quantity]*[UnitPrice])", stored: true),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    LineTotal = table.Column<double>(type: "float", nullable: false, computedColumnSql: "([Quantity]*[UnitPrice])-(([Quantity]*[UnitPrice])*[Discount])", stored: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_ItemsPrices_ItemsPriceId",
                        column: x => x.ItemsPriceId,
                        principalTable: "ItemsPrices",
                        principalColumn: "ItemPriceId");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDetails_PartyId",
                table: "AccountDetails",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustments_ItemsPricesId",
                table: "InventoryAdjustments",
                column: "ItemsPricesId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemName",
                table: "Items",
                column: "ItemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPrices_ItemId_ItemPriceUnitCost",
                table: "ItemsPrices",
                columns: new[] { "ItemId", "ItemPriceUnitCost" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ItemsPriceId",
                table: "OrderDetails",
                column: "ItemsPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PartyId",
                table: "Orders",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Overheads_ProductionOrderId",
                table: "Overheads",
                column: "ProductionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_PartyPhoneNumber",
                table: "Parties",
                column: "PartyPhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_PartyTypeId",
                table: "Parties",
                column: "PartyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyTypes_PartyTypeName",
                table: "PartyTypes",
                column: "PartyTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_TransactionId",
                table: "PaymentDetails",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionMaterials_ItemsPricesAndStoreId",
                table: "ProductionMaterials",
                column: "ItemsPricesAndStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionMaterials_ProductionOrderId",
                table: "ProductionMaterials",
                column: "ProductionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_InventoryItemId",
                table: "ProductionOrders",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreName_StoreAddress",
                table: "Stores",
                columns: new[] { "StoreName", "StoreAddress" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountDetails");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "InventoryAdjustments");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Overheads");

            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropTable(
                name: "ProductionMaterials");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "ItemsPrices");

            migrationBuilder.DropTable(
                name: "ProductionOrders");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "PartyTypes");
        }
    }
}
