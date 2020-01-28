namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetForeignKeyId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recipes", "Menu_Id", "dbo.Menus");
            DropForeignKey("dbo.Ingredients", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.Ingredients", new[] { "Recipe_Id" });
            DropIndex("dbo.Recipes", new[] { "Menu_Id" });
            RenameColumn(table: "dbo.Recipes", name: "Menu_Id", newName: "MenuId");
            RenameColumn(table: "dbo.Ingredients", name: "Recipe_Id", newName: "RecipeId");
            AlterColumn("dbo.Ingredients", "RecipeId", c => c.Int(nullable: true));
            AlterColumn("dbo.Recipes", "MenuId", c => c.Int(nullable: true));
            CreateIndex("dbo.Ingredients", "RecipeId");
            CreateIndex("dbo.Recipes", "MenuId");
            AddForeignKey("dbo.Recipes", "MenuId", "dbo.Menus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Ingredients", "RecipeId", "dbo.Recipes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredients", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "MenuId", "dbo.Menus");
            DropIndex("dbo.Recipes", new[] { "MenuId" });
            DropIndex("dbo.Ingredients", new[] { "RecipeId" });
            AlterColumn("dbo.Recipes", "MenuId", c => c.Int());
            AlterColumn("dbo.Ingredients", "RecipeId", c => c.Int());
            RenameColumn(table: "dbo.Ingredients", name: "RecipeId", newName: "Recipe_Id");
            RenameColumn(table: "dbo.Recipes", name: "MenuId", newName: "Menu_Id");
            CreateIndex("dbo.Recipes", "Menu_Id");
            CreateIndex("dbo.Ingredients", "Recipe_Id");
            AddForeignKey("dbo.Ingredients", "Recipe_Id", "dbo.Recipes", "Id");
            AddForeignKey("dbo.Recipes", "Menu_Id", "dbo.Menus", "Id");
        }
    }
}
