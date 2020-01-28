namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableRecipeId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ingredients", "RecipeId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ingredients", "RecipeId", c => c.Int(nullable: false));
        }
    }
}
