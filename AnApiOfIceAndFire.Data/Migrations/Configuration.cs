namespace AnApiOfIceAndFire.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AnApiOfIceAndFire.Data.AnApiOfIceAndFireContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AnApiOfIceAndFire.Data.AnApiOfIceAndFireContext context)
        {
            
        }
    }
}
