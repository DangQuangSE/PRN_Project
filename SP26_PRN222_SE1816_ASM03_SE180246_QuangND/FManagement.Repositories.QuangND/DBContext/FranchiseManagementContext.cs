using System;
using System.Collections.Generic;
using FManagement.Entities.QuangND.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FManagement.Entities.QuangND.Entities;

public partial class FranchiseManagementContext : DbContext
{
    public FranchiseManagementContext()
    {
    }

    public FranchiseManagementContext(DbContextOptions<FranchiseManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CentralKitchen> CentralKitchens { get; set; }

    public virtual DbSet<DeliveryIssue> DeliveryIssues { get; set; }

    public virtual DbSet<DeliverySchedule> DeliverySchedules { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<FranchiseStore> FranchiseStores { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryTransaction> InventoryTransactions { get; set; }

    public virtual DbSet<InventoryType> InventoryTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductBatch> ProductBatches { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<ProductionPlanQuangNd> ProductionPlanQuangNds { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeItem> RecipeItems { get; set; }

    public virtual DbSet<StoreOrder> StoreOrders { get; set; }

    public virtual DbSet<StoreOrderItemQuangNd> StoreOrderItemQuangNds { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SystemUserAccount> SystemUserAccounts { get; set; }

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CentralKitchen>(entity =>
        {
            entity.HasKey(e => e.KitchenId).HasName("PK__CentralK__8A856D2147514A5D");

            entity.ToTable("CentralKitchen");

            entity.Property(e => e.KitchenId).HasColumnName("KitchenID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContactPerson).HasMaxLength(100);
            entity.Property(e => e.KitchenName).HasMaxLength(100);
            entity.Property(e => e.OperatingStatus).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<DeliveryIssue>(entity =>
        {
            entity.HasKey(e => e.IssueId).HasName("PK__Delivery__6C8616242726BA44");

            entity.ToTable("DeliveryIssue");

            entity.Property(e => e.IssueId).HasColumnName("IssueID");
            entity.Property(e => e.IssueType).HasMaxLength(100);
            entity.Property(e => e.ReportedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

            entity.HasOne(d => d.Schedule).WithMany(p => p.DeliveryIssues)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DeliveryI__Sched__73BA3083");
        });

        modelBuilder.Entity<DeliverySchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Delivery__9C8A5B691D272703");

            entity.ToTable("DeliverySchedule");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.ActualArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DeliveryStatus).HasMaxLength(50);
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.DriverName).HasMaxLength(100);
            entity.Property(e => e.EndLocation).HasMaxLength(255);
            entity.Property(e => e.EstimatedArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProofOfDelivery).HasMaxLength(255);
            entity.Property(e => e.StartLocation).HasMaxLength(255);
            entity.Property(e => e.VehiclePlate).HasMaxLength(20);

            entity.HasOne(d => d.Order).WithMany(p => p.DeliverySchedules)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DeliveryS__Order__6FE99F9F");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF634D50D6D");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.FeedbackDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__OrderI__06CD04F7");

            entity.HasOne(d => d.Store).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__StoreI__05D8E0BE");
        });

        modelBuilder.Entity<FranchiseStore>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Franchis__3B82F0E1683BD1F3");

            entity.ToTable("FranchiseStore");

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContractStatus).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.OwnerName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.StoreName).HasMaxLength(100);
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__BEAEB27A70CEDA70");

            entity.ToTable("Ingredient");

            entity.HasIndex(e => e.Sku, "UQ__Ingredie__CA1ECF0D8EFADED2").IsUnique();

            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
            entity.Property(e => e.CurrentPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IngredientName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .HasColumnName("SKU");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.UnitOfMeasure).HasMaxLength(20);

            entity.HasOne(d => d.Supplier).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ingredien__Suppl__4222D4EF");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6D36422222C");

            entity.ToTable("Inventory");

            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.BatchNumber).HasMaxLength(50);
            entity.Property(e => e.CurrentQuantity).HasDefaultValue(0.0);
            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
            entity.Property(e => e.KitchenId).HasColumnName("KitchenID");
            entity.Property(e => e.LastReversalReference).HasMaxLength(100);
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.WarehouseLocation).HasMaxLength(50);

            entity.HasOne(d => d.Ingredient).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__Ingre__4BAC3F29");

            entity.HasOne(d => d.Kitchen).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.KitchenId)
                .HasConstraintName("FK__Inventory__Kitch__49C3F6B7");

            entity.HasOne(d => d.Store).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Inventory__Store__4AB81AF0");

            entity.HasOne(d => d.Type).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__TypeI__4CA06362");
        });

        modelBuilder.Entity<InventoryTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Inventor__55433A4B74DBA4D6");

            entity.ToTable("InventoryTransaction");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.OriginalTransactionId).HasColumnName("OriginalTransactionID");
            entity.Property(e => e.PerformedByAccountId).HasColumnName("PerformedByAccountID");
            entity.Property(e => e.Reason).HasMaxLength(400);
            entity.Property(e => e.ReferenceNumber).HasMaxLength(100);
            entity.Property(e => e.ReversalDate).HasColumnType("datetime");
            entity.Property(e => e.ReversalReason).HasMaxLength(255);
            entity.Property(e => e.ReversedByAccountId).HasColumnName("ReversedByAccountID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionType).HasMaxLength(50);

            entity.HasOne(d => d.Inventory).WithMany(p => p.InventoryTransactions)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__Inven__5165187F");
        });

        modelBuilder.Entity<InventoryType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Inventor__516F03954CC5ADB4");

            entity.ToTable("InventoryType");

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6EDED0B621B");

            entity.ToTable("Product");

            entity.HasIndex(e => e.ProductCode, "UQ__Product__2F4E024FFE4F1A64").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsBatchTracked).HasDefaultValue(false);
            entity.Property(e => e.ItemCategory).HasMaxLength(20);
            entity.Property(e => e.MinOrderQty).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductCode).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitOfMeasure).HasMaxLength(20);

            entity.HasOne(d => d.Inventory).WithMany(p => p.Products)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Invento__59FA5E80");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Product__59063A47");
        });

        modelBuilder.Entity<ProductBatch>(entity =>
        {
            entity.HasKey(e => e.BatchId).HasName("PK__ProductB__5D55CE3810C98A0E");

            entity.ToTable("ProductBatch");

            entity.Property(e => e.BatchId).HasColumnName("BatchID");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Qcstatus)
                .HasMaxLength(50)
                .HasColumnName("QCStatus");

            entity.HasOne(d => d.Plan).WithMany(p => p.ProductBatches)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductBa__PlanI__00200768");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductBatches)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductBa__Produ__01142BA1");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ProductTypeId).HasName("PK__ProductT__A1312F4E1F14D185");

            entity.ToTable("ProductType");

            entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductionPlanQuangNd>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Producti__755C22D797FB545C");

            entity.ToTable("ProductionPlanQuangND");

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.KitchenId).HasColumnName("KitchenID");
            entity.Property(e => e.PlanStatus).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.StoreOrderItemId).HasColumnName("StoreOrderItemID");

            entity.HasOne(d => d.Kitchen).WithMany(p => p.ProductionPlanQuangNds)
                .HasForeignKey(d => d.KitchenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productio__Kitch__7B5B524B");

            entity.HasOne(d => d.StoreOrderItem).WithMany(p => p.ProductionPlanQuangNds)
                .HasForeignKey(d => d.StoreOrderItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productio__Store__7C4F7684");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipe__FDD988D03891F577");

            entity.ToTable("Recipe");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDefault).HasDefaultValue(true);
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.RecipeName).HasMaxLength(100);
            entity.Property(e => e.StandardYield).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Version).HasMaxLength(20);
            entity.Property(e => e.YieldUnit).HasMaxLength(20);

            entity.HasOne(d => d.Product).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipe__ProductI__5FB337D6");
        });

        modelBuilder.Entity<RecipeItem>(entity =>
        {
            entity.HasKey(e => e.RecipeItemId).HasName("PK__RecipeIt__5C2588CCD04A2996");

            entity.ToTable("RecipeItem");

            entity.Property(e => e.RecipeItemId).HasColumnName("RecipeItemID");
            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
            entity.Property(e => e.QuantityRequired).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.UnitOfMeasure).HasMaxLength(20);
            entity.Property(e => e.WastePercentage)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeItems)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RecipeIte__Recip__6383C8BA");
        });

        modelBuilder.Entity<StoreOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__StoreOrd__C3905BAFE6EFFB1E");

            entity.ToTable("StoreOrder");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ContactPhone).HasMaxLength(20);
            entity.Property(e => e.KitchenId).HasColumnName("KitchenID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.RequiredDeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.ShippingAddress).HasMaxLength(255);
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Kitchen).WithMany(p => p.StoreOrders)
                .HasForeignKey(d => d.KitchenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoreOrde__Kitch__693CA210");

            entity.HasOne(d => d.Store).WithMany(p => p.StoreOrders)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoreOrde__Store__68487DD7");
        });

        modelBuilder.Entity<StoreOrderItemQuangNd>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__StoreOrd__57ED06A18FF28B40");

            entity.ToTable("StoreOrderItemQuangND");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.StoreOrderItemQuangNds)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoreOrde__Order__6C190EBB");

            entity.HasOne(d => d.Product).WithMany(p => p.StoreOrderItemQuangNds)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoreOrde__Produ__6D0D32F4");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666948F64BC5C");

            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.SupplierName).HasMaxLength(100);
            entity.Property(e => e.TaxCode).HasMaxLength(20);
        });

        modelBuilder.Entity<SystemUserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId);

            entity.ToTable("System.UserAccount");

            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");
            entity.Property(e => e.ApplicationCode).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RequestCode).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
