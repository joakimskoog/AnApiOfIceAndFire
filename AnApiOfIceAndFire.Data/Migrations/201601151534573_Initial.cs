namespace AnApiOfIceAndFire.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ISBN = c.String(),
                        AuthorsRaw = c.String(),
                        NumberOfPages = c.Int(nullable: false),
                        Publisher = c.String(),
                        MediaType = c.Int(nullable: false),
                        Country = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CharacterEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Culture = c.String(),
                        Born = c.String(),
                        Died = c.String(),
                        AliasesRaw = c.String(),
                        TitlesRaw = c.String(),
                        TvSeriesRaw = c.String(),
                        PlayedByRaw = c.String(),
                        FatherId = c.Int(),
                        MotherId = c.Int(),
                        SpouseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CharacterEntities", t => t.FatherId)
                .ForeignKey("dbo.CharacterEntities", t => t.MotherId)
                .ForeignKey("dbo.CharacterEntities", t => t.SpouseId)
                .Index(t => t.FatherId)
                .Index(t => t.MotherId)
                .Index(t => t.SpouseId);
            
            CreateTable(
                "dbo.HouseEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CoatOfArms = c.String(),
                        Words = c.String(),
                        Region = c.String(),
                        Fouded = c.String(),
                        DiedOut = c.String(),
                        SeatsRaw = c.String(),
                        TitlesRaw = c.String(),
                        AncestralWeaponsRaw = c.String(),
                        FounderId = c.Int(),
                        CurrentLordId = c.Int(),
                        HeirId = c.Int(),
                        OverlordId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CharacterEntities", t => t.CurrentLordId)
                .ForeignKey("dbo.CharacterEntities", t => t.FounderId)
                .ForeignKey("dbo.CharacterEntities", t => t.HeirId)
                .ForeignKey("dbo.HouseEntities", t => t.OverlordId)
                .Index(t => t.FounderId)
                .Index(t => t.CurrentLordId)
                .Index(t => t.HeirId)
                .Index(t => t.OverlordId);
            
            CreateTable(
                "dbo.HouseCadetBranches",
                c => new
                    {
                        HouseId1 = c.Int(nullable: false),
                        HouseId2 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HouseId1, t.HouseId2 })
                .ForeignKey("dbo.HouseEntities", t => t.HouseId1)
                .ForeignKey("dbo.HouseEntities", t => t.HouseId2)
                .Index(t => t.HouseId1)
                .Index(t => t.HouseId2);
            
            CreateTable(
                "dbo.CharacterHouseAllegiances",
                c => new
                    {
                        CharacterId = c.Int(nullable: false),
                        HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CharacterId, t.HouseId })
                .ForeignKey("dbo.CharacterEntities", t => t.CharacterId, cascadeDelete: true)
                .ForeignKey("dbo.HouseEntities", t => t.HouseId, cascadeDelete: true)
                .Index(t => t.CharacterId)
                .Index(t => t.HouseId);
            
            CreateTable(
                "dbo.BookCharacters",
                c => new
                    {
                        CharacterId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CharacterId, t.BookId })
                .ForeignKey("dbo.BookEntities", t => t.CharacterId, cascadeDelete: true)
                .ForeignKey("dbo.CharacterEntities", t => t.BookId, cascadeDelete: true)
                .Index(t => t.CharacterId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.PovBookCharacters",
                c => new
                    {
                        CharacterId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CharacterId, t.BookId })
                .ForeignKey("dbo.BookEntities", t => t.CharacterId, cascadeDelete: true)
                .ForeignKey("dbo.CharacterEntities", t => t.BookId, cascadeDelete: true)
                .Index(t => t.CharacterId)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PovBookCharacters", "BookId", "dbo.CharacterEntities");
            DropForeignKey("dbo.PovBookCharacters", "CharacterId", "dbo.BookEntities");
            DropForeignKey("dbo.BookCharacters", "BookId", "dbo.CharacterEntities");
            DropForeignKey("dbo.BookCharacters", "CharacterId", "dbo.BookEntities");
            DropForeignKey("dbo.CharacterEntities", "SpouseId", "dbo.CharacterEntities");
            DropForeignKey("dbo.CharacterEntities", "MotherId", "dbo.CharacterEntities");
            DropForeignKey("dbo.CharacterEntities", "FatherId", "dbo.CharacterEntities");
            DropForeignKey("dbo.CharacterHouseAllegiances", "HouseId", "dbo.HouseEntities");
            DropForeignKey("dbo.CharacterHouseAllegiances", "CharacterId", "dbo.CharacterEntities");
            DropForeignKey("dbo.HouseEntities", "OverlordId", "dbo.HouseEntities");
            DropForeignKey("dbo.HouseEntities", "HeirId", "dbo.CharacterEntities");
            DropForeignKey("dbo.HouseEntities", "FounderId", "dbo.CharacterEntities");
            DropForeignKey("dbo.HouseEntities", "CurrentLordId", "dbo.CharacterEntities");
            DropForeignKey("dbo.HouseCadetBranches", "HouseId2", "dbo.HouseEntities");
            DropForeignKey("dbo.HouseCadetBranches", "HouseId1", "dbo.HouseEntities");
            DropIndex("dbo.PovBookCharacters", new[] { "BookId" });
            DropIndex("dbo.PovBookCharacters", new[] { "CharacterId" });
            DropIndex("dbo.BookCharacters", new[] { "BookId" });
            DropIndex("dbo.BookCharacters", new[] { "CharacterId" });
            DropIndex("dbo.CharacterHouseAllegiances", new[] { "HouseId" });
            DropIndex("dbo.CharacterHouseAllegiances", new[] { "CharacterId" });
            DropIndex("dbo.HouseCadetBranches", new[] { "HouseId2" });
            DropIndex("dbo.HouseCadetBranches", new[] { "HouseId1" });
            DropIndex("dbo.HouseEntities", new[] { "OverlordId" });
            DropIndex("dbo.HouseEntities", new[] { "HeirId" });
            DropIndex("dbo.HouseEntities", new[] { "CurrentLordId" });
            DropIndex("dbo.HouseEntities", new[] { "FounderId" });
            DropIndex("dbo.CharacterEntities", new[] { "SpouseId" });
            DropIndex("dbo.CharacterEntities", new[] { "MotherId" });
            DropIndex("dbo.CharacterEntities", new[] { "FatherId" });
            DropTable("dbo.PovBookCharacters");
            DropTable("dbo.BookCharacters");
            DropTable("dbo.CharacterHouseAllegiances");
            DropTable("dbo.HouseCadetBranches");
            DropTable("dbo.HouseEntities");
            DropTable("dbo.CharacterEntities");
            DropTable("dbo.BookEntities");
        }
    }
}
