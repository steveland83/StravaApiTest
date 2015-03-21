namespace StravaApiTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityEntities",
                c => new
                    {
                        DatabaseId = c.Int(nullable: false, identity: true),
                        AthleteId = c.Long(nullable: false),
                        ActivityDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        ExternalId = c.String(),
                        Distance = c.Single(nullable: false),
                        MovingTime = c.Int(nullable: false),
                        ElapsedTime = c.Int(nullable: false),
                        ElevationGain = c.Single(nullable: false),
                        HasKudoed = c.Boolean(nullable: false),
                        AverageHeartrate = c.Single(nullable: false),
                        MaxHeartrate = c.Single(nullable: false),
                        Truncated = c.Int(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        GearId = c.String(),
                        AverageSpeed = c.Single(nullable: false),
                        MaxSpeed = c.Single(nullable: false),
                        AverageCadence = c.Single(nullable: false),
                        AverageTemperature = c.Single(nullable: false),
                        AveragePower = c.Single(nullable: false),
                        Kilojoules = c.Single(nullable: false),
                        IsTrainer = c.Boolean(nullable: false),
                        IsCommute = c.Boolean(nullable: false),
                        IsManual = c.Boolean(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        IsFlagged = c.Boolean(nullable: false),
                        AchievementCount = c.Int(nullable: false),
                        KudosCount = c.Int(nullable: false),
                        CommentCount = c.Int(nullable: false),
                        AthleteCount = c.Int(nullable: false),
                        PhotoCount = c.Int(nullable: false),
                        StartDate = c.String(),
                        StartDateLocal = c.String(),
                        TimeZone = c.String(),
                        WeightedAverageWatts = c.Int(nullable: false),
                        HasPowerMeter = c.Boolean(nullable: false),
                        Id = c.Long(nullable: false),
                        Map_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DatabaseId)
                .ForeignKey("dbo.Maps", t => t.Map_Id)
                .Index(t => t.Map_Id);
            
            CreateTable(
                "dbo.Maps",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Polyline = c.String(),
                        SummaryPolyline = c.String(),
                        ResourceState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StreamEntities",
                c => new
                    {
                        AthleteId = c.Long(nullable: false),
                        ActivityId = c.Long(nullable: false),
                        StreamType = c.Int(nullable: false),
                        Data_SerialisedData = c.String(),
                        SeriesType = c.String(),
                        OriginalSize = c.Int(nullable: false),
                        Resolution = c.String(),
                    })
                .PrimaryKey(t => new { t.AthleteId, t.ActivityId, t.StreamType });
            
            CreateTable(
                "dbo.Bikes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Brand = c.String(),
                        Model = c.String(),
                        Description = c.String(),
                        IsPrimary = c.Boolean(nullable: false),
                        Name = c.String(),
                        Distance = c.Single(nullable: false),
                        ResourceState = c.Int(nullable: false),
                        UserEntity_DatabaseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntities", t => t.UserEntity_DatabaseId)
                .Index(t => t.UserEntity_DatabaseId);
            
            CreateTable(
                "dbo.Shoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsPrimary = c.Boolean(nullable: false),
                        Name = c.String(),
                        Distance = c.Single(nullable: false),
                        ResourceState = c.Int(nullable: false),
                        UserEntity_DatabaseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntities", t => t.UserEntity_DatabaseId)
                .Index(t => t.UserEntity_DatabaseId);
            
            CreateTable(
                "dbo.UserEntities",
                c => new
                    {
                        DatabaseId = c.Int(nullable: false, identity: true),
                        AccessToken = c.String(),
                        LastSyncDate = c.DateTime(),
                        FollowerCount = c.Int(nullable: false),
                        FriendCount = c.Int(nullable: false),
                        MutualFriendCount = c.Int(nullable: false),
                        DatePreference = c.String(),
                        MeasurementPreference = c.String(),
                        Email = c.String(),
                        Ftp = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ProfileMedium = c.String(),
                        Profile = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        Sex = c.String(),
                        Friend = c.String(),
                        Follower = c.String(),
                        IsPremium = c.Boolean(nullable: false),
                        CreatedAt = c.String(),
                        UpdatedAt = c.String(),
                        ApproveFollowers = c.Boolean(nullable: false),
                        Id = c.Long(nullable: false),
                        ResourceState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DatabaseId);
            
            CreateTable(
                "dbo.Clubs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        IsPrivate = c.Boolean(nullable: false),
                        MemberCount = c.Int(nullable: false),
                        ResourceState = c.Int(nullable: false),
                        Name = c.String(),
                        ProfileMedium = c.String(),
                        Profile = c.String(),
                        UserEntity_DatabaseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntities", t => t.UserEntity_DatabaseId)
                .Index(t => t.UserEntity_DatabaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shoes", "UserEntity_DatabaseId", "dbo.UserEntities");
            DropForeignKey("dbo.Clubs", "UserEntity_DatabaseId", "dbo.UserEntities");
            DropForeignKey("dbo.Bikes", "UserEntity_DatabaseId", "dbo.UserEntities");
            DropForeignKey("dbo.ActivityEntities", "Map_Id", "dbo.Maps");
            DropIndex("dbo.Clubs", new[] { "UserEntity_DatabaseId" });
            DropIndex("dbo.Shoes", new[] { "UserEntity_DatabaseId" });
            DropIndex("dbo.Bikes", new[] { "UserEntity_DatabaseId" });
            DropIndex("dbo.ActivityEntities", new[] { "Map_Id" });
            DropTable("dbo.Clubs");
            DropTable("dbo.UserEntities");
            DropTable("dbo.Shoes");
            DropTable("dbo.Bikes");
            DropTable("dbo.StreamEntities");
            DropTable("dbo.Maps");
            DropTable("dbo.ActivityEntities");
        }
    }
}
