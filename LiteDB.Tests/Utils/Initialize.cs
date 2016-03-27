using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteDB.Tests
{
    [TestClass]
    public class Initialize
    {
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // wait all threads close FileDB
            Thread.Sleep(2000);

            DB.DeleteFiles();
        }
    }
}