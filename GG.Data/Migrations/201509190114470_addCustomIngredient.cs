namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCustomIngredient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Ingredients", "CustomIngredient_Id", c => c.Int());
            AddColumn("dbo.Recipes", "CustomIngredient_Id", c => c.Int());
            CreateIndex("dbo.Ingredients", "CustomIngredient_Id");
            CreateIndex("dbo.Recipes", "CustomIngredient_Id");
            AddForeignKey("dbo.Ingredients", "CustomIngredient_Id", "dbo.CustomIngredients", "Id");
            AddForeignKey("dbo.Recipes", "CustomIngredient_Id", "dbo.CustomIngredients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "CustomIngredient_Id", "dbo.CustomIngredients");
            DropForeignKey("dbo.Ingredients", "CustomIngredient_Id", "dbo.CustomIngredients");
            DropIndex("dbo.Recipes", new[] { "CustomIngredient_Id" });
            DropIndex("dbo.Ingredients", new[] { "CustomIngredient_Id" });
            DropColumn("dbo.Recipes", "CustomIngredient_Id");
            DropColumn("dbo.Ingredients", "CustomIngredient_Id");
            DropTable("dbo.CustomIngredients");
        }
    }
}
