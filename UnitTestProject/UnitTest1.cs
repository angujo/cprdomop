using DatabaseProcessor.postgres;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using SystemLocalStore;
using SystemLocalStore.models;
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
        [TestMethod]
        public void TestSecure()
        {
            var txtx = new string[] { "MyFirstString", "My Name", "Ch..Ch..Ch..Ch....", "Slim Shady" };
            var encr = new string[txtx.Length];
            Console.WriteLine("Encryptions...");
            for (var i = 0; i < txtx.Length; i++)
            {
                string en;
                Console.WriteLine("{0}\tEncryption:[{1}]", txtx[i].ToString(), en = Secure.Encrypt(txtx[i].ToString()));
                encr[i] = en;
            }
            Console.WriteLine("Decryptions...");
            for (var i = 0; i < txtx.Length; i++)
            {
                Console.WriteLine("{0}\tDecryption:[{1}]", txtx[i].ToString(), Secure.Decrypt(encr[i]));
            }
        }

        [TestMethod]
        public void TestDB()
        {
            var loads = SysDB<ChunkTimer>.Column<int>("ChunkId", "Where WorkLoadId = @WorkLoadId AND Touched = @Touched AND Status <> @Status ORDER BY ChunkId ASC", new { WorkLoadId = 1, Touched = false, Status = Status.COMPLETED });
            foreach (var load in loads) Console.WriteLine(load.ToString());
        }

        [TestMethod]
        public void TestCopy()
        {
            var dvdrental = new DBSchema
            {
                DBName = "dvdrental",
                Password = "postgres",
                Port = 5432,
                SchemaName = "public",
                Server = "localhost",
                Username = "postgres"
            };
            var test = new DBSchema
            {
                DBName = "omopapp",
                Password = "postgres",
                Port = 5432,
                SchemaName = "test",
                Server = "localhost",
                Username = "postgres"
            };

            string from = "COPY (select first_name||' '||last_name AS fname, last_update FROM public.actor) TO STDOUT (FORMAT BINARY)";
            string to = "COPY test.actor(full_name, last_update) FROM STDIN (FORMAT BINARY)";

            PostgreSql.BinaryCopy(dvdrental, test, from, to);
        }
    }
}
