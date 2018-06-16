using System;
using BlocksLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlocksTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestingBlockRender()
        {
            var b = new Block();

            b.Render().Save("Block.bmp");
        }
    }
}
