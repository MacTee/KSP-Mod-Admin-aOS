using System;
using System.Globalization;
using HtmlAgilityPack;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.SiteHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KSPModAdmin.UnitTests
{
    [TestClass]
    public class CorseForgeParserUnitTest
    {
        private static HtmlDocument CurseForgeHtmlDocument { get; set; }

        [ClassInitialize]
        public static void InitTests(TestContext testContext)
        {
            string url = "https://www.curseforge.com/kerbal/ksp-mods/mechjeb";
            HtmlWeb web = new HtmlWeb();
            CurseForgeHtmlDocument = web.Load(url);
        }

        [TestMethod]
        public void GetCurseUrl_Success()
        {
            string url = "https://kerbal.curseforge.com/projects/mechjeb";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            var curseUrl = CurseForgeParser.GetCurseUrl(doc);

            Assert.AreEqual("https://www.curseforge.com/kerbal/ksp-mods/mechjeb", curseUrl);
        }

        [TestMethod]
        public void GetCurseForgeUrl_Success()
        {
            var curseUrl = CurseForgeParser.GetCurseForgeUrl(CurseForgeHtmlDocument);

            Assert.AreEqual("https://kerbal.curseforge.com/projects/mechjeb", curseUrl);
        }

        [TestMethod]
        public void GetModName_Success()
        {
            var modName = CurseForgeParser.GetModName(CurseForgeHtmlDocument);

            Assert.AreEqual("MechJeb", modName);
        }

        [TestMethod]
        public void GetModId_Success()
        {
            var modId = CurseForgeParser.GetModId(CurseForgeHtmlDocument);

            Assert.AreEqual("220221", modId);
        }

        [TestMethod]
        public void GetModCreationDate_Success()
        {
            var modCreationDate = CurseForgeParser.GetModCreationDate(CurseForgeHtmlDocument);

            Assert.AreEqual(new DateTime(2014, 5, 6, 18, 15, 45), modCreationDate);
        }

        [TestMethod]
        public void GetChangeDate_Success()
        {
            var modUpdateDate = CurseForgeParser.GetChangeDate(CurseForgeHtmlDocument);

            Assert.IsTrue(modUpdateDate > DateTime.MinValue);
        }

        [TestMethod]
        public void GetModDownloadCount_Success()
        {
            var count = CurseForgeParser.GetModDownloadCount(CurseForgeHtmlDocument);

            Assert.IsTrue(int.Parse(count.Replace(",", string.Empty).Replace(".", string.Empty)) > 2246587);
        }

        [TestMethod]
        public void GetModAuthor_Success()
        {
            var modAuthor = CurseForgeParser.GetModAuthor(CurseForgeHtmlDocument);

            Assert.AreEqual("sarbian", modAuthor);
        }

        [TestMethod]
        public void GetModGameVersion_Success()
        {
            var version = CurseForgeParser.GetModGameVersion(CurseForgeHtmlDocument);

            Assert.IsTrue(new Version(version) > new Version(1, 4, 0));
        }
    }
}
