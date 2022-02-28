using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using SystemLocalStore;
using Util;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            object[] wheres = new object[] {
                new object[]{ G.And( new {Id=7867, Age=new int[] { 23,56,90,56,23} }), G.Or(new {Id=976,Height=new int[] {100,344,888,1 } }),G.Or(new object[] { new {Week=9,Month=12 } }) },
                new string[]{ "Jane Doe"},
                new{Id=23,Name="Jane Doe"},
            };
            var pi = new string(new char[] { 'S', 'E' });
           // foreach (var w in pi.GetType().GetMethods()) Console.WriteLine(w.Name.ToString());
            Console.WriteLine(pi.HasMethod("IsInterned"));
            Console.WriteLine("text".GetType().Namespace);
            Console.WriteLine(new { }.GetType().Namespace);
            Console.WriteLine(typeof(PropertyInfo).Namespace);
            foreach (var w in wheres)
            {
                var abs = new WhereClause(w);
                Console.WriteLine(abs.AsString);
            }
        }
    }
}
