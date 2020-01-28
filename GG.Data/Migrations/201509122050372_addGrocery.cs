namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGrocery : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroceryCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroceryItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroceryCategoryId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GroceryCategories", t => t.GroceryCategoryId, cascadeDelete: true)
                .Index(t => t.GroceryCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroceryItems", "GroceryCategoryId", "dbo.GroceryCategories");
            DropIndex("dbo.GroceryItems", new[] { "GroceryCategoryId" });
            DropTable("dbo.GroceryItems");
            DropTable("dbo.GroceryCategories");
        }
    }
}
