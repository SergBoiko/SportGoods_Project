namespace SportGoods.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        Product_Id = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "Product_Id", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Orders", new[] { "Product_Id" });
            DropTable("dbo.Orders");
        }
    }
}
