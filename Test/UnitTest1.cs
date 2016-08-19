using System;
using System.Collections.Generic;
using System.IO;
using EliminateGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        private static int[] GetMap(string filePath,out int width,out int height)
        {
            width = 0;
            height = 0;
            int[] map = null;
            try
            {
                if (File.Exists(filePath))
                {
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        using (var sr = new StreamReader(fs))
                        {
                            var line = sr.ReadLine();
                            var headLine = line.Split(',');
                            width = int.Parse(headLine[0]);
                            height = int.Parse(headLine[1]);
                            map = new int[(width*height)];

                            int index = 0;
                            while (!string.IsNullOrEmpty(line))
                            {
                                line = sr.ReadLine();
                                for (int i = 0; i < line.Length; i++)
                                {
                                    map[width*index + i] = int.Parse(line[i].ToString());
                                }
                                index++;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }
            return map;
        }

        private static void ArraryEqual(List<int> a,List<int> b)
        {
            Assert.AreEqual(a.Count,b.Count);
            for (int i = 0; i < a.Count; i++)
            {
                Assert.AreEqual(a[i],b[i]);
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"TestMap", "1.txt");
            int width = 0;
            int height = 0;
            var map = GetMap(filePath, out width, out height);

           var result= RegionAggregationHelper.GatherGrids(map, height, width);

            Assert.AreEqual(result.Count,4);

            ArraryEqual(result[0],new List<int>() {0,1,2});
            ArraryEqual(result[1],new List<int>() {3,7,6,5,4});
            ArraryEqual(result[2],new List<int>() {8,9,10,11});
            ArraryEqual(result[3],new List<int>() {12,13,14,15});
        }

        /// <summary>
        /// 边缘测试
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestMap", "2.txt");
            int width = 0;
            int height = 0;
            var map = GetMap(filePath, out width, out height);

            var result = RegionAggregationHelper.GatherGrids(map, height, width);

            Assert.AreEqual(result.Count, 2);
            ArraryEqual(result[0],new List<int>(){0,5,10,15,16,17,18,19,14,9,4,3,2,1});
            ArraryEqual(result[1],new List<int>(){6,11,12,7,8,13});
        }

        /// <summary>
        /// 长宽不一致
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestMap", "3.txt");
            int width = 0;
            int height = 0;
            var map = GetMap(filePath, out width, out height);

            var result = RegionAggregationHelper.GatherGrids(map, height, width);

            Assert.AreEqual(result.Count, 2);
            ArraryEqual(result[0], new List<int>() {0});
            ArraryEqual(result[1], new List<int>() {1});
        }

        /// <summary>
        /// 田字
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestMap", "4.txt");
            int width = 0;
            int height = 0;
            var map = GetMap(filePath, out width, out height);

            var result = RegionAggregationHelper.GatherGrids(map, height, width);

            Assert.AreEqual(result.Count, 3);
            ArraryEqual(result[0], new List<int>() {0,5,10,11,12,7,2,3,4,9,14,13,1});
            ArraryEqual(result[1],new List<int>() {6});
            ArraryEqual(result[2],new List<int>() {8});
        }
    }
}
