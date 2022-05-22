using TimeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TimeUnitTests
{
    [TestClass]
    public class TimeUnitTest
    {
        #region >>> Time Constructors <<<

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)10, (byte)30, (byte)30, (byte)10, (byte)30, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void TimeConstructor_3Arguments(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(new Time(h, m, s), new Time(expectedH, expectedM, expectedS));
        }

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)10, (byte)30, (byte)10, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)23, (byte)59)]
        public void TimeConstructor_2Arguments(byte h, byte m, byte expectedH, byte expectedM)
        {
            Assert.AreEqual(new Time(h, m), new Time(expectedH, expectedM));
        }

        [TestMethod]
        [DataRow((byte)0, (byte)0)]
        [DataRow((byte)10, (byte)10)]
        [DataRow((byte)23, (byte)23)]
        public void TimeConstructor_1Argument(byte h, byte expectedH)
        {
            Assert.AreEqual(new Time(h), new Time(expectedH));
        }

        [TestMethod]
        public void TimeConstructor_0Arguments()
        {
            Assert.AreEqual(new Time(0, 0, 0), new Time());
        }

        [TestMethod]
        [DataRow((byte)30, (byte)60, (byte)60)]
        [DataRow((byte)128, (byte)128, (byte)128)]
        [DataRow((byte)255, (byte)255, (byte)255)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TimeConstructor_ArgumentOutOfRangeException(byte h, byte m, byte s)
        {
            new Time(h, m, s);
        }

        [TestMethod]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0)]
        [DataRow("10:30:30", (byte)10, (byte)30, (byte)30)]
        [DataRow("23:59:59", (byte)23, (byte)59, (byte)59)]
        public void TimeConstructor_StringArgument(string str, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(new Time(str), new Time(expectedH, expectedM, expectedS));
        }

        [TestMethod]
        [DataRow("0:00_00")]
        [DataRow("0:00a:00")]
        [DataRow(" 0:00:00:00:00")]
        [ExpectedException(typeof(ArgumentException))]
        public void TimeConstructor_StringArgument_ArgumentException(string str)
        {
            new Time(str);
        }

        [TestMethod]
        [DataRow(128000, (byte)0, (byte)2, (byte)8)]
        [DataRow(1024000, (byte)0, (byte)17, (byte)4)]
        [DataRow(16384000, (byte)4, (byte)33, (byte)4)]
        public void TimeConstructor_TimeLengthArgument(long t, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(new Time(t), new Time(expectedH, expectedM, expectedS));
        }

        #endregion

        #region >>> Time ToString <<<

        [TestMethod]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0)]
        [DataRow("10:30:30", (byte)10, (byte)30, (byte)30)]
        [DataRow("23:59:59", (byte)23, (byte)59, (byte)59)]
        public void Time_ToString(string str, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(str, new Time(expectedH, expectedM, expectedS).ToString());
        }

        #endregion

        #region >>> Time Equals <<<

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)10, (byte)30, (byte)30, (byte)10, (byte)30, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Time_Equal(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new Time(h, m, s);
            var t2 = new Time(expectedH, expectedM, expectedS);
            Assert.AreEqual(true, t1 == t2);
        }

        [TestMethod]
        [DataRow((byte)1, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)1, (byte)2, (byte)3, (byte)10, (byte)30, (byte)30)]
        [DataRow((byte)0, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Time_NotEqual(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new Time(h, m, s);
            var t2 = new Time(expectedH, expectedM, expectedS);
            Assert.AreEqual(true, t1 != t2);
        }

        #endregion

        #region >>> Time Operators <<<

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)1, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)11, (byte)30, (byte)30, (byte)10, (byte)30, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)0)]
        public void Time_Larger(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new Time(h, m, s);
            var t2 = new Time(expectedH, expectedM, expectedS);
            Assert.AreEqual(true, t1 > t2);
        }

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)11, (byte)30, (byte)30, (byte)10, (byte)30, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Time_LargerOrEqual(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new Time(h, m, s);
            var t2 = new Time(expectedH, expectedM, expectedS);
            Assert.AreEqual(true, t1 >= t2);
        }

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)10, (byte)30, (byte)30, (byte)11, (byte)30, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)0, (byte)23, (byte)59, (byte)59)]
        public void Time_Smaller(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new Time(h, m, s);
            var t2 = new Time(expectedH, expectedM, expectedS);
            Assert.AreEqual(true, t1 < t2);
        }

        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)10, (byte)30, (byte)30, (byte)11, (byte)30, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Time_SmallerOrEqual(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new Time(h, m, s);
            var t2 = new Time(expectedH, expectedM, expectedS);
            Assert.AreEqual(true, t1 <= t2);
        }

        #endregion

        #region >>> Time Arithmetic <<<

        [TestMethod]
        [DataRow("00:00:00", "00:00:00", "00:00:00")]
        [DataRow("01:00:00", "00:00:01", "01:00:01")]
        [DataRow("23:50:59", "00:08:01", "23:59:00")]
        public void Time_Plus(string str1, string str2, string expectedStr)
        {
            Assert.AreEqual(new Time(expectedStr), new Time(str1) + new TimePeriod(str2));
        }

        [TestMethod]
        [DataRow("11:11:11", "10:10:10", "01:01:01")]
        [DataRow("01:00:00", "00:00:01", "00:59:59")]
        [DataRow("23:50:59", "00:08:01", "23:42:58")]
        public void Time_Minus(string str1, string str2, string expectedStr)
        {
            Assert.AreEqual(new Time(expectedStr), new Time(str1) - new TimePeriod(str2));
        }

        #endregion
    }
}
