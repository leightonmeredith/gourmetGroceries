namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remCustomIng : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ingredients", "CustomIngredient_Id", "dbo.CustomIngredients");
            DropForeignKey("dbo.Recipes", "CustomIngredient_Id", "dbo.CustomIngredients");
            DropIndex("dbo.Ingredients", new[] { "CustomIngredient_Id" });
            DropIndex("dbo.Recipes", new[] { "CustomIngredient_Id" });
            DropColumn("dbo.Ingredients", "CustomIngredient_Id");
            DropColumn("dbo.Recipes", "CustomIngredient_Id");
            DropTable("dbo.CustomIngredients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustomIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Recipes", "CustomIngredient_Id", c => c.Int());
            AddColumn("dbo.Ingredients", "CustomIngredient_Id", c => c.Int());
            CreateIndex("dbo.Recipes", "CustomIngredient_Id");
            CreateIndex("dbo.Ingredients", "CustomIngredient_Id");
            AddForeignKey("dbo.Recipes", "CustomIngredient_Id", "dbo.CustomIngredients", "Id");
            AddForeignKey("dbo.Ingredients", "CustomIngredient_Id", "dbo.CustomIngredients", "Id");
        }
    }
}
