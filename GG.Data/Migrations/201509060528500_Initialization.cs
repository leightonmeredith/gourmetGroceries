namespace GG.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Double(nullable: false),
                        Measurement = c.String(),
                        Brand = c.String(),
                        Name = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        Recipe_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id)
                .Index(t => t.Recipe_Id);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Menu_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.Menu_Id)
                .Index(t => t.Menu_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "Menu_Id", "dbo.Menus");
            DropForeignKey("dbo.Ingredients", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.Recipes", new[] { "Menu_Id" });
            DropIndex("dbo.Ingredients", new[] { "Recipe_Id" });
            DropTable("dbo.Recipes");
            DropTable("dbo.Menus");
            DropTable("dbo.Ingredients");
        }
    }
}
