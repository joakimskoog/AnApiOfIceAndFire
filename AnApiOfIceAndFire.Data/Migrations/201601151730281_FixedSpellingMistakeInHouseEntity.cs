namespace AnApiOfIceAndFire.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedSpellingMistakeInHouseEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HouseEntities", "Founded", c => c.String());
            DropColumn("dbo.HouseEntities", "Fouded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HouseEntities", "Fouded", c => c.String());
            DropColumn("dbo.HouseEntities", "Founded");
        }
    }
}
