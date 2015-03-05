namespace StravaApiTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityMetas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ExternalId = c.String(),
                        Distance = c.Single(),
                        MovingTime = c.Int(),
                        ElapsedTime = c.Int(),
                        ElevationGain = c.Single(),
                        HasKudoed = c.Boolean(),
                        AverageHeartrate = c.Single(),
                        MaxHeartrate = c.Single(),
                        Truncated = c.Int(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        GearId = c.String(),
                        AverageSpeed = c.Single(),
                        MaxSpeed = c.Single(),
                        AverageCadence = c.Single(),
                        AverageTemperature = c.Single(),
                        AveragePower = c.Single(),
                        Kilojoules = c.Single(),
                        IsTrainer = c.Boolean(),
                        IsCommute = c.Boolean(),
                        IsManual = c.Boolean(),
                        IsPrivate = c.Boolean(),
                        IsFlagged = c.Boolean(),
                        AchievementCount = c.Int(),
                        KudosCount = c.Int(),
                        CommentCount = c.Int(),
                        AthleteCount = c.Int(),
                        PhotoCount = c.Int(),
                        StartDate = c.String(),
                        StartDateLocal = c.String(),
                        TimeZone = c.String(),
                        WeightedAverageWatts = c.Int(),
                        HasPowerMeter = c.Boolean(),
                        Calories = c.Single(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Gear_Id = c.String(maxLength: 128),
                        Athlete_Id = c.Long(),
                        Map_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GearSummaries", t => t.Gear_Id)
                .ForeignKey("dbo.AthleteMetas", t => t.Athlete_Id)
                .ForeignKey("dbo.Maps", t => t.Map_Id)
                .Index(t => t.Gear_Id)
                .Index(t => t.Athlete_Id)
                .Index(t => t.Map_Id);
            
            CreateTable(
                "dbo.AthleteMetas",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ResourceState = c.Int(nullable: false),
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
                        IsPremium = c.Boolean(),
                        CreatedAt = c.String(),
                        UpdatedAt = c.String(),
                        ApproveFollowers = c.Boolean(),
                        FollowerCount = c.Int(),
                        FriendCount = c.Int(),
                        MutualFriendCount = c.Int(),
                        DatePreference = c.String(),
                        MeasurementPreference = c.String(),
                        Email = c.String(),
                        Ftp = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.GearSummaries",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsPrimary = c.Boolean(nullable: false),
                        Name = c.String(),
                        Distance = c.Single(nullable: false),
                        ResourceState = c.Int(nullable: false),
                        Brand = c.String(),
                        Model = c.String(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Athlete_Id = c.Long(),
                        Athlete_Id1 = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AthleteMetas", t => t.Athlete_Id)
                .ForeignKey("dbo.AthleteMetas", t => t.Athlete_Id1)
                .Index(t => t.Athlete_Id)
                .Index(t => t.Athlete_Id1);
            
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
                        Athlete_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AthleteMetas", t => t.Athlete_Id)
                .Index(t => t.Athlete_Id);
            
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
                "dbo.SegmentEfforts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AverageCadence = c.Single(nullable: false),
                        AveragePower = c.Single(nullable: false),
                        AverageHeartrate = c.Single(nullable: false),
                        MaxHeartrate = c.Single(nullable: false),
                        ResourceState = c.Int(nullable: false),
                        Name = c.String(),
                        KingOfMountainRank = c.Int(),
                        PersonalRecordRank = c.Int(),
                        MovingTime = c.Int(nullable: false),
                        ElapsedTime = c.Int(nullable: false),
                        StartDate = c.String(),
                        Distance = c.Single(nullable: false),
                        StartIndex = c.Int(nullable: false),
                        EndIndex = c.Int(nullable: false),
                        Activity_Id = c.Long(),
                        Athlete_Id = c.Long(),
                        Segment_Id = c.Int(),
                        Activity_Id1 = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActivityMetas", t => t.Activity_Id)
                .ForeignKey("dbo.AthleteMetas", t => t.Athlete_Id)
                .ForeignKey("dbo.Segments", t => t.Segment_Id)
                .ForeignKey("dbo.ActivityMetas", t => t.Activity_Id1)
                .Index(t => t.Activity_Id)
                .Index(t => t.Athlete_Id)
                .Index(t => t.Segment_Id)
                .Index(t => t.Activity_Id1);
            
            CreateTable(
                "dbo.Segments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.String(),
                        UpdatedAt = c.String(),
                        TotalElevationGain = c.Single(nullable: false),
                        EffortCount = c.Int(nullable: false),
                        AthleteCount = c.Int(nullable: false),
                        IsHazardous = c.Boolean(nullable: false),
                        PersonalRecordTime = c.Int(nullable: false),
                        PersonalRecordDistance = c.Single(nullable: false),
                        StarCount = c.Int(nullable: false),
                        ResourceState = c.Int(nullable: false),
                        Name = c.String(),
                        ActivityType = c.String(),
                        Distance = c.Single(nullable: false),
                        AverageGrade = c.Single(nullable: false),
                        MaxGrade = c.Single(nullable: false),
                        MaxElevation = c.Single(nullable: false),
                        MinElevation = c.Single(nullable: false),
                        Category = c.Int(nullable: false),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        IsPrivate = c.Boolean(nullable: false),
                        IsStarred = c.Boolean(nullable: false),
                        Map_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Maps", t => t.Map_Id)
                .Index(t => t.Map_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        LastName = c.String(),
                        FirstName = c.String(),
                        StravaAccessToken = c.String(maxLength: 60),
                        Email = c.String(),
                        LastSyncDate = c.DateTime(),
                        AthleteId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.StravaAccessToken, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AthleteMetas", "Id", "dbo.Users");
            DropForeignKey("dbo.SegmentEfforts", "Activity_Id1", "dbo.ActivityMetas");
            DropForeignKey("dbo.SegmentEfforts", "Segment_Id", "dbo.Segments");
            DropForeignKey("dbo.Segments", "Map_Id", "dbo.Maps");
            DropForeignKey("dbo.SegmentEfforts", "Athlete_Id", "dbo.AthleteMetas");
            DropForeignKey("dbo.SegmentEfforts", "Activity_Id", "dbo.ActivityMetas");
            DropForeignKey("dbo.ActivityMetas", "Map_Id", "dbo.Maps");
            DropForeignKey("dbo.ActivityMetas", "Athlete_Id", "dbo.AthleteMetas");
            DropForeignKey("dbo.ActivityMetas", "Gear_Id", "dbo.GearSummaries");
            DropForeignKey("dbo.GearSummaries", "Athlete_Id1", "dbo.AthleteMetas");
            DropForeignKey("dbo.Clubs", "Athlete_Id", "dbo.AthleteMetas");
            DropForeignKey("dbo.GearSummaries", "Athlete_Id", "dbo.AthleteMetas");
            DropIndex("dbo.Users", new[] { "StravaAccessToken" });
            DropIndex("dbo.Segments", new[] { "Map_Id" });
            DropIndex("dbo.SegmentEfforts", new[] { "Activity_Id1" });
            DropIndex("dbo.SegmentEfforts", new[] { "Segment_Id" });
            DropIndex("dbo.SegmentEfforts", new[] { "Athlete_Id" });
            DropIndex("dbo.SegmentEfforts", new[] { "Activity_Id" });
            DropIndex("dbo.Clubs", new[] { "Athlete_Id" });
            DropIndex("dbo.GearSummaries", new[] { "Athlete_Id1" });
            DropIndex("dbo.GearSummaries", new[] { "Athlete_Id" });
            DropIndex("dbo.AthleteMetas", new[] { "Id" });
            DropIndex("dbo.ActivityMetas", new[] { "Map_Id" });
            DropIndex("dbo.ActivityMetas", new[] { "Athlete_Id" });
            DropIndex("dbo.ActivityMetas", new[] { "Gear_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Segments");
            DropTable("dbo.SegmentEfforts");
            DropTable("dbo.Maps");
            DropTable("dbo.Clubs");
            DropTable("dbo.GearSummaries");
            DropTable("dbo.AthleteMetas");
            DropTable("dbo.ActivityMetas");
        }
    }
}
