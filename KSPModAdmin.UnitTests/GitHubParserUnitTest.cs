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
            string url = "https://github.com/MuMech/MechJeb2/releases";
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
        public void GetVersion_Success()
        {
            var version = GitHubParser.GetVersion(GitHubHtmlDocument);

            Assert.IsTrue(new Version(version) > new Version(2, 6, 0));
        }

        [TestMethod]
        public void GetChangeDate_Success()
        {
            var changeDate = GitHubParser.GetChangeDate(GitHubHtmlDocument);

            Assert.IsTrue(changeDate != DateTime.MinValue);
        }
    }
}
