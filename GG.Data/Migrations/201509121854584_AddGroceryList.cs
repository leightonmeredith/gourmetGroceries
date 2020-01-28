namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroceryList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroceryCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Name = c.String(),
                        GroceryList_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GroceryLists", t => t.GroceryList_Id)
                .Index(t => t.GroceryList_Id);
            
            CreateTable(
                "dbo.GroceryLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroceryCategoryId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroceryCategories", "GroceryList_Id", "dbo.GroceryLists");
            DropIndex("dbo.GroceryCategories", new[] { "GroceryList_Id" });
            DropTable("dbo.GroceryLists");
            DropTable("dbo.GroceryCategories");
        }
    }
}
