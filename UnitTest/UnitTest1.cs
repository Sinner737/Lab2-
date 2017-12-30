using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        #region Операторы перегрузок
        [TestMethod]
        public void TestOperators()
        {
            var str1 = new MyString.MyString(new char[] { 'a', 'b', 'c', 'd' });
            var str2 = new MyString.MyString(new char[] { 'c', 'd' });
            var str3 = new MyString.MyString(new char[] { 'a', 'b', 'c', 'd','c','d' });
            Assert.AreEqual(str3, str1 + str2);
            Assert.AreEqual(false, str1 > str2);
            Assert.AreEqual(false, str2 < str3);
            Assert.AreEqual(true, str3 >= str1);
            Assert.AreEqual(false, str2 <= str1);
            Assert.AreEqual(true, str1 != str3);
            Assert.AreEqual(false, str2 == str3);
        }
        #endregion

        #region Методы
        [TestMethod]
        public void TestMethods()
        {
            var str1 = new MyString.MyString(new char[] { 'a', 'b', 'c', 'd' });
            var str2 = new MyString.MyString(new char[] { 'c', 'd' });

            Assert.AreEqual(new MyString.MyString(new char[] { 'b', 'c', 'd' }), str1.SubString(1));
            Assert.AreEqual(new MyString.MyString(new char[] { 'c','d' }), str2.SubString(0, 1));

            Assert.AreEqual(2, str1.IndexOf(str2));
            Assert.AreEqual(2, str1.IndexOf(str2, 1));

            Assert.AreEqual(new MyString.MyString(new char[] { 'a', 'd' }), str2.Replace('c', 'a'));
            Assert.AreEqual(new MyString.MyString(new char[] { 'a', 'b', 'a', 'b' }), str1.Replace(str2, new MyString.MyString(new char[] { 'a', 'b' })));

            Assert.AreEqual(new MyString.MyString(new char[] { 'c', 'a', 'd' }), str2.Insert(1, new MyString.MyString(new char[] { 'a' })));

            Assert.AreEqual(new MyString.MyString(new char[] { 'c' }), str2.Remove(1));
            Assert.AreEqual(new MyString.MyString(new char[] { 'a', 'd' }), str1.Remove(1, 2));
        }

#endregion

        #region Преобразования
        [TestMethod]
        public void TestTypeCast()
        {
            try
            {
                var str1 = new MyString.MyString(new char[] { 'a', 'b', 'c', 'd' });
                var str2 = new MyString.MyString(new char[] { 'c', 'd' });
                var str3 = new MyString.MyString(new char[] { '1', '2' });
                var str4 = new MyString.MyString();
                string strForMyString = "abcd";
                str4 = strForMyString;
                string s1 = str1;
                char[] s2 = str2;
                int n3 = (int)str3;

                Assert.AreEqual("abcd", s1);
                Assert.AreEqual(2, s2.Length);
                Assert.AreEqual('c', s2[0]);
                Assert.AreEqual('d', s2[1]);
                Assert.AreEqual(12, n3);
                Assert.AreEqual(true, str4 == strForMyString);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
#endregion 
    }
}
