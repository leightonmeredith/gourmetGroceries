namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeMenuIdFromRecipe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recipes", "MenuId", "dbo.Menus");
            DropIndex("dbo.Recipes", new[] { "MenuId" });
            DropColumn("dbo.Recipes", "MenuId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recipes", "MenuId", c => c.Int(nullable: false));
            CreateIndex("dbo.Recipes", "MenuId");
            AddForeignKey("dbo.Recipes", "MenuId", "dbo.Menus", "Id", cascadeDelete: true);
        }
    }
}
