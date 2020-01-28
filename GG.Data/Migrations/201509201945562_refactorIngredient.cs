namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactorIngredient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomIngredients", "Description", c => c.String());
            AlterColumn("dbo.RecipeIngredients", "Count", c => c.String());
            DropColumn("dbo.Ingredients", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredients", "Type", c => c.String());
            AlterColumn("dbo.RecipeIngredients", "Count", c => c.Double(nullable: false));
            DropColumn("dbo.CustomIngredients", "Description");
        }
    }
}
