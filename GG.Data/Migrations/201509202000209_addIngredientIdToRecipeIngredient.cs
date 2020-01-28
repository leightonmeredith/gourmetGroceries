namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIngredientIdToRecipeIngredient : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecipeIngredients", "Ingredient_Id", "dbo.Ingredients");
            DropIndex("dbo.RecipeIngredients", new[] { "Ingredient_Id" });
            RenameColumn(table: "dbo.RecipeIngredients", name: "Ingredient_Id", newName: "IngredientId");
            AlterColumn("dbo.RecipeIngredients", "IngredientId", c => c.Int(nullable: false));
            CreateIndex("dbo.RecipeIngredients", "IngredientId");
            AddForeignKey("dbo.RecipeIngredients", "IngredientId", "dbo.Ingredients", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecipeIngredients", "IngredientId", "dbo.Ingredients");
            DropIndex("dbo.RecipeIngredients", new[] { "IngredientId" });
            AlterColumn("dbo.RecipeIngredients", "IngredientId", c => c.Int());
            RenameColumn(table: "dbo.RecipeIngredients", name: "IngredientId", newName: "Ingredient_Id");
            CreateIndex("dbo.RecipeIngredients", "Ingredient_Id");
            AddForeignKey("dbo.RecipeIngredients", "Ingredient_Id", "dbo.Ingredients", "Id");
        }
    }
}
