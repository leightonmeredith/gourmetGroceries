namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeSomeIdsNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.CustomIngredients", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.CustomIngredients", new[] { "RecipeId" });
            DropIndex("dbo.CustomIngredients", new[] { "IngredientId" });
            AlterColumn("dbo.CustomIngredients", "RecipeId", c => c.Int());
            AlterColumn("dbo.CustomIngredients", "IngredientId", c => c.Int());
            CreateIndex("dbo.CustomIngredients", "RecipeId");
            CreateIndex("dbo.CustomIngredients", "IngredientId");
            AddForeignKey("dbo.CustomIngredients", "IngredientId", "dbo.Ingredients", "Id");
            AddForeignKey("dbo.CustomIngredients", "RecipeId", "dbo.Recipes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomIngredients", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.CustomIngredients", "IngredientId", "dbo.Ingredients");
            DropIndex("dbo.CustomIngredients", new[] { "IngredientId" });
            DropIndex("dbo.CustomIngredients", new[] { "RecipeId" });
            AlterColumn("dbo.CustomIngredients", "IngredientId", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomIngredients", "RecipeId", c => c.Int(nullable: false));
            CreateIndex("dbo.CustomIngredients", "IngredientId");
            CreateIndex("dbo.CustomIngredients", "RecipeId");
            AddForeignKey("dbo.CustomIngredients", "RecipeId", "dbo.Recipes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomIngredients", "IngredientId", "dbo.Ingredients", "Id", cascadeDelete: true);
        }
    }
}
