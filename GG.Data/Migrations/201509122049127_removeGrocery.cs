namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeGrocery : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroceryCategories", "GroceryList_Id", "dbo.GroceryLists");
            DropIndex("dbo.GroceryCategories", new[] { "GroceryList_Id" });
            DropTable("dbo.GroceryCategories");
            DropTable("dbo.GroceryLists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GroceryLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroceryCategoryId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroceryCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Name = c.String(),
                        GroceryList_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.GroceryCategories", "GroceryList_Id");
            AddForeignKey("dbo.GroceryCategories", "GroceryList_Id", "dbo.GroceryLists", "Id");
        }
    }
}
