using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnApiOfIceAndFire.Tests.IntegrationTests
{
    [TestClass]
    public class TestCleanup
    {
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Database.Delete(DbIntegrationTestHarness.NameOrConnectionString);
        }
    }
}