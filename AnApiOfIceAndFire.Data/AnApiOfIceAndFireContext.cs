using EntityFrameworkRepository;

namespace AnApiOfIceAndFire.Data
{
    public class AnApiOfIceAndFireContext : EntityDbContext
    {
        public AnApiOfIceAndFireContext() : base("nameOrConnectionString")
        {
        }
    }
}