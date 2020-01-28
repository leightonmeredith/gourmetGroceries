namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeGroceryListAndItem : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GroceryCategories", newName: "IngredientCategories");
            DropForeignKey("dbo.GroceryItems", "GroceryCategoryId", "dbo.GroceryCategories");
            DropIndex("dbo.GroceryItems", new[] { "GroceryCategoryId" });
            AddColumn("dbo.Ingredients", "IngredientCategoryId", c => c.Int(nullable: true));
            CreateIndex("dbo.Ingredients", "IngredientCategoryId");
            AddForeignKey("dbo.Ingredients", "IngredientCategoryId", "dbo.IngredientCategories", "Id", cascadeDelete: true);
            DropTable("dbo.GroceryItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GroceryItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroceryCategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        RecipeId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Ingredients", "IngredientCategoryId", "dbo.IngredientCategories");
            DropIndex("dbo.Ingredients", new[] { "IngredientCategoryId" });
            DropColumn("dbo.Ingredients", "IngredientCategoryId");
            CreateIndex("dbo.GroceryItems", "GroceryCategoryId");
            AddForeignKey("dbo.GroceryItems", "GroceryCategoryId", "dbo.GroceryCategories", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.IngredientCategories", newName: "GroceryCategories");
        }
    }
}
