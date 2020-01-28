namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCustomIng : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: false)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: false)
                .Index(t => t.RecipeId)
                .Index(t => t.IngredientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomIngredients", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.CustomIngredients", "IngredientId", "dbo.Ingredients");
            DropIndex("dbo.CustomIngredients", new[] { "IngredientId" });
            DropIndex("dbo.CustomIngredients", new[] { "RecipeId" });
            DropTable("dbo.CustomIngredients");
        }
    }
}
