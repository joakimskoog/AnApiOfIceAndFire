using System.Data.Entity;
using System.Transactions;
using AnApiOfIceAndFire.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnApiOfIceAndFire.Tests.IntegrationTests
{
    public class DbIntegrationTestHarness
    {
        public static string NameOrConnectionString = "AnApiOfIceAndFire_IntegrationTest_Db";


        protected AnApiOfIceAndFireContext DbContext;
        protected DbContextTransaction Transaction;

        [TestInitialize]
        public void Setup()
        {
            DbContext = new AnApiOfIceAndFireContext(NameOrConnectionString);
            DbContext.Database.CreateIfNotExists();
            Transaction = DbContext.Database.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Transaction.Rollback();
            Transaction.Dispose();
            DbContext.Dispose();
        }
    }
}