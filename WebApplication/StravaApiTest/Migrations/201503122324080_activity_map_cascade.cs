namespace StravaApiTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activity_map_cascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActivityEntities", "Map_Id", "dbo.Maps");
            AddForeignKey("dbo.ActivityEntities", "Map_Id", "dbo.Maps", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActivityEntities", "Map_Id", "dbo.Maps");
            AddForeignKey("dbo.ActivityEntities", "Map_Id", "dbo.Maps", "Id");
        }
    }
}
