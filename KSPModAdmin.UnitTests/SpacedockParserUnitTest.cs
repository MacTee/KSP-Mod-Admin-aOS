using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.SiteHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KSPModAdmin.UnitTests
{
    [TestClass]
    public class SpacedockUnitTest
    {
        private static string content { get; set; }

        [ClassInitialize]
        public static void InitTests(TestContext testContext)
        {
            var url = "http://spacedock.info/mod/69/kRPC";
            content = Www.Load(url);
        }

        [TestMethod]
        public void GetDownloadInfos_Success()
        {
            var additionalUrl = SpacedockParser.GetAdditionalURL(content);

            Assert.IsTrue(!string.IsNullOrEmpty(additionalUrl));
        }

        [TestMethod]
        public void GetProductID_Success()
        {
            var productID = SpacedockParser.GetProductID(content);

            Assert.IsTrue(!string.IsNullOrEmpty(productID));
        }

        [TestMethod]
        public void GetModName_Success()
        {
            var modName = SpacedockParser.GetModName(content);

            Assert.IsTrue(!string.IsNullOrEmpty(modName));
        }

        [TestMethod]
        public void GetDownloadCount_Success()
        {
            var count = SpacedockParser.GetDownloadCount(content);

            Assert.IsTrue(!string.IsNullOrEmpty(count));
        }

        [TestMethod]
        public void GetAuthor_Success()
        {
            var author = SpacedockParser.GetAuthor(content);

            Assert.IsTrue(!string.IsNullOrEmpty(author));
        }

        [TestMethod]
        public void GetVersion_Success()
        {
            var version = SpacedockParser.GetVersion(content);

            Assert.IsTrue(!string.IsNullOrEmpty(version));
        }

        [TestMethod]
        public void GetKSPVersion_Success()
        {
            var kspPVersion = SpacedockParser.GetKSPVersion(content);

            Assert.IsTrue(!string.IsNullOrEmpty(kspPVersion));
        }

        [TestMethod]
        public void GetNote_Success()
        {
            var note = SpacedockParser.GetNote(content);

            Assert.IsTrue(!string.IsNullOrEmpty(note));
        }
    }
}
