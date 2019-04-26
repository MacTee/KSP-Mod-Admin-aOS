using System;
using HtmlAgilityPack;
using KSPModAdmin.Core.Utils.SiteHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KSPModAdmin.UnitTests
{
    [TestClass]
    public class GitHubParserUnitTest
    {
        private static HtmlDocument GitHubHtmlDocument { get; set; }

        [ClassInitialize]
        public static void InitTests(TestContext testContext)
        {
            string url = "https://github.com/KSP-RO/RealismOverhaul/releases"; // https://github.com/RemoteTechnologiesGroup/RemoteTech/releases
            HtmlWeb web = new HtmlWeb();
            GitHubHtmlDocument = web.Load(url);
        }

        [TestMethod]
        public void GetDownloadInfos_Success()
        {
            var downloadInfos = GitHubParser.GetDownloadInfos(GitHubHtmlDocument);

            Assert.IsTrue(downloadInfos.Count > 0);
        }

        [TestMethod]
        public void GetUrlParts_Success()
        {
            var parts = GitHubHandler.GetUrlParts("https://github.com/KSP-RO/RealismOverhaul/releases");
            var parts2 = GitHubHandler.GetUrlParts("https://github.com/krpc/krpc/releases");

            Assert.IsTrue(parts.Count >= 4);
            Assert.IsTrue(parts2.Count >= 4);
        }
    }
}
