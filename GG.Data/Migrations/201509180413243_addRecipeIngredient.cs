namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRecipeIngredient : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ingredients", new[] { "RecipeId" });
            CreateTable(
                "dbo.RecipeIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Double(nullable: false),
                        RecipeId = c.Int(nullable: false),
                        Measurement = c.String(),
                        Description = c.String(),
                        Ingredient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_Id)
                .Index(t => t.RecipeId)
                .Index(t => t.Ingredient_Id);
            
            DropColumn("dbo.Ingredients", "Count");
            DropColumn("dbo.Ingredients", "Measurement");
            DropColumn("dbo.Ingredients", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredients", "Description", c => c.String());
            AddColumn("dbo.Ingredients", "Measurement", c => c.String());
            AddColumn("dbo.Ingredients", "Count", c => c.Double(nullable: false));
            DropForeignKey("dbo.RecipeIngredients", "Ingredient_Id", "dbo.Ingredients");
            DropIndex("dbo.RecipeIngredients", new[] { "Ingredient_Id" });
            DropIndex("dbo.RecipeIngredients", new[] { "RecipeId" });
            DropTable("dbo.RecipeIngredients");
            CreateIndex("dbo.Ingredients", "RecipeId");
        }
    }
}
