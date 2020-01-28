namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groceryListWithRecipeId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroceryItems", "RecipeId", c => c.Int());
            AddColumn("dbo.GroceryItems", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroceryItems", "Discriminator");
            DropColumn("dbo.GroceryItems", "RecipeId");
        }
    }
}
