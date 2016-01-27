namespace AnApiOfIceAndFire.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AnApiOfIceAndFireContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AnApiOfIceAndFireContext context)
        {
            
        }
    }
}
