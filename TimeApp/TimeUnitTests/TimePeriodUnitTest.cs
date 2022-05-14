using TimeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TimeUnitTests
{
    [TestClass]
    public class TimePeriodUnitTest
    {
        #region >>> TimePeriod Constructors <<<

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)10, (byte)30, (byte)30, (byte)10, (byte)30, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void TimePeriodConstructor_3Arguments(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(new TimePeriod(h, m, s), new TimePeriod(expectedH, expectedM, expectedS));
        }

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)10, (byte)30, (byte)10, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)23, (byte)59)]
        public void TimePeriodConstructor_2Arguments(byte h, byte m, byte expectedH, byte expectedM)
        {
            Assert.AreEqual(new TimePeriod(h, m), new TimePeriod(expectedH, expectedM));
        }

        [TestMethod]
        [DataRow((byte)0, (byte)0)]
        [DataRow((byte)10, (byte)10)]
        [DataRow((byte)23, (byte)23)]
        public void TimePeriodConstructor_1Argument(byte h, byte expectedH)
        {
            Assert.AreEqual(new TimePeriod(h), new TimePeriod(expectedH));
        }

        [TestMethod]
        public void TimePeriodConstructor_0Arguments()
        {
            Assert.AreEqual(new TimePeriod(0, 0, 0), new TimePeriod());
        }

        [TestMethod]
        [DataRow((byte)30, (byte)60, (byte)60)]
        [DataRow((byte)128, (byte)128, (byte)128)]
        [DataRow((byte)255, (byte)255, (byte)255)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TimePeriodConstructor_ArgumentOutOfRangeException(byte h, byte m, byte s)
        {
            new TimePeriod(h, m, s);
        }

        [TestMethod]
        [DataRow("0:00:00", (byte)0, (byte)0, (byte)0)]
        [DataRow("10:30:30", (byte)10, (byte)30, (byte)30)]
        [DataRow("23:59:59", (byte)23, (byte)59, (byte)59)]
        public void TimePeriodConstructor_StringArgument(string str, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(new TimePeriod(str), new TimePeriod(expectedH, expectedM, expectedS));
        }

        [TestMethod]
        [DataRow("0:00_00")]
        [DataRow("0:00a:00")]
        [DataRow(" 0:00:00:00")]
        [ExpectedException(typeof(ArgumentException))]
        public void TimePeriodConstructor_StringArgument_ArgumentException(string str)
        {
            new TimePeriod(str);
        }

        [TestMethod]
        [DataRow(128, (byte)0, (byte)2, (byte)8)]
        [DataRow(1024, (byte)0, (byte)17, (byte)4)]
        [DataRow(16384, (byte)4, (byte)33, (byte)4)]
        public void TimePeriodConstructor_TimeLengthArgument(long t, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(new TimePeriod(t), new TimePeriod(expectedH, expectedM, expectedS));
        }

        #endregion

        #region >>> TimePeriod ToString <<<

        [TestMethod]
        [DataRow("0:00:00", (byte)0, (byte)0, (byte)0)]
        [DataRow("10:30:30", (byte)10, (byte)30, (byte)30)]
        [DataRow("23:59:59", (byte)23, (byte)59, (byte)59)]
        public void TimePeriod_ToString(string str, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(str, new TimePeriod(expectedH, expectedM, expectedS).ToString());
        }

        #endregion

        #region >>> TimePeriod Equals <<<

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)10, (byte)30, (byte)30, (byte)10, (byte)30, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void TimePeriod_Equal(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new TimePeriod(h, m, s);
            var t2 = new TimePeriod(expectedH, expectedM, expectedS);
            Assert.AreEqual(true, t1 == t2);
        }

        [TestMethod]
        [DataRow((byte)1, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)1, (byte)2, (byte)3, (byte)10, (byte)30, (byte)30)]
        [DataRow((byte)0, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void TimePeriod_NotEqual(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new TimePeriod(h, m, s);
            var t2 = new TimePeriod(expectedH, expectedM, expectedS);
            Assert.AreEqual(true, t1 != t2);
        }

        #endregion

        #region >>> TimePeriod Operators <<<

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)1, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)11, (byte)30, (byte)30, (byte)10, (byte)30, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)0)]
        public void TimePeriod_Larger(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new TimePeriod(h, m, s);
            var t2 = new TimePeriod(expectedH, expectedM, expectedS);
            Assert.AreEqual(true, t1 > t2);
        }

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)11, (byte)30, (byte)30, (byte)10, (byte)30, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void TimePeriod_LargerOrEqual(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new TimePeriod(h, m, s);
            var t2 = new TimePeriod(expectedH, expectedM, expectedS);
            Assert.AreEqual(true, t1 >= t2);
        }

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)10, (byte)30, (byte)30, (byte)11, (byte)30, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)0, (byte)23, (byte)59, (byte)59)]
        public void TimePeriod_Smaller(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new TimePeriod(h, m, s);
            var t2 = new TimePeriod(expectedH, expectedM, expectedS);
            Assert.AreEqual(true, t1 < t2);
        }

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)10, (byte)30, (byte)30, (byte)11, (byte)30, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void TimePeriod_SmallerOrEqual(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new TimePeriod(h, m, s);
            var t2 = new TimePeriod(expectedH, expectedM, expectedS);
            Assert.AreEqual(true, t1 <= t2);
        }

        #endregion

        #region >>> Time Arithmetic <<<

        [TestMethod]
        [DataRow("00:00:00", "00:00:00", "00:00:00")]
        [DataRow("01:00:00", "00:00:01", "01:00:01")]
        [DataRow("23:50:59", "00:08:01", "23:59:00")]
        public void TimePeriod_Plus(string str1, string str2, string expectedStr)
        {
            Assert.AreEqual(new TimePeriod(expectedStr), new TimePeriod(str1) + new TimePeriod(str2));
        }

        [TestMethod]
        [DataRow("00:00:00", "00:00:00", "00:00:00")]
        [DataRow("01:00:00", "00:00:01", "00:59:59")]
        [DataRow("23:50:59", "00:08:01", "23:42:58")]
        public void TimePeriod_Minus(string str1, string str2, string expectedStr)
        {
            Assert.AreEqual(new TimePeriod(expectedStr), new TimePeriod(str1) - new TimePeriod(str2));
        }

        #endregion

    }
}
