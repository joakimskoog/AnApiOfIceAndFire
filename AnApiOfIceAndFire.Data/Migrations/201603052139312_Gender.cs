namespace AnApiOfIceAndFire.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Gender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CharacterEntities", "IsFemale", c => c.Boolean(nullable:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CharacterEntities", "IsFemale");
        }
    }
}
