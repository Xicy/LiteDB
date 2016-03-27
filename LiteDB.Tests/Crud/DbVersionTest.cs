using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteDB.Tests
{
    public class VerDatabase : LiteDatabase
    {
        public VerDatabase(Stream s, ushort version)
            : base(s, version)
        {
            Log.Level = Logger.FULL;
            Log.Logging += m => Debug.Print(m);
        }

        protected override void OnVersionUpdate(int newVersion)
        {
            if (newVersion == 1)
                Run("db.col1.insert {_id:1}");

            if (newVersion == 2)
                Run("db.col2.insert {_id:2}");

            if (newVersion == 3)
                Run("db.col3.insert {_id:3}");
        }
    }

    [TestClass]
    public class DbVersionTest
    {
        [TestMethod]
        public void DbVerion_Test()
        {
            var m = new MemoryStream();

            using (var db = new VerDatabase(m, 1))
            {
                Assert.AreEqual(true, db.CollectionExists("col1"));
                Assert.AreEqual(false, db.CollectionExists("col2"));
                Assert.AreEqual(false, db.CollectionExists("col3"));
            }

            using (var db = new VerDatabase(m, 2))
            {
                Assert.AreEqual(true, db.CollectionExists("col1"));
                Assert.AreEqual(true, db.CollectionExists("col2"));
                Assert.AreEqual(false, db.CollectionExists("col3"));
            }

            using (var db = new VerDatabase(m, 3))
            {
                Assert.AreEqual(true, db.CollectionExists("col1"));
                Assert.AreEqual(true, db.CollectionExists("col2"));
                Assert.AreEqual(true, db.CollectionExists("col3"));
            }
        }
    }
}