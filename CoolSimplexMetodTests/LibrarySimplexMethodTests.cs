using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoolSimplexMetodTests
{
    [TestClass]
    public class LibrarySimplexMethodTests
    {
        [TestMethod]
        public void TestMatrixA1_punkt2()
        {
            double[,] a1 = new double[,]
            {
                {2.5,1,0,1},
                {6,0,1,1},
                
            };
            char[] sign1 = new char[] 
            {
                '=',
                '<',
                '<',
                '>'
            };
            double[,] extected = new double[,]
            {
                {2.5,1,0,1 },
                {6,0,1,1},
                {0,1,0,0},
                {0,0,1,0 },
                {0,0,0,-1}
            };
            LibrarySimplexMethod.Simplex simplex = new LibrarySimplexMethod.Simplex(2, 4, a1,
                new double[] { 1, 2, 3 },new double[] { 1,2,3},sign1);
            simplex.TranslationMatrixA();

            for (int i = 0; i < extected.GetLength(0); i++)
            {
                for (int j = 0; j < extected.GetLength(1); j++)
                {
                    Assert.AreEqual(extected[i, j], simplex.A[i, j]);
                    Console.Write(simplex.A[i,j]+" ");
                }
                Console.WriteLine();
            }
        }
        [TestMethod]
        public void TestMatrixA2_punkt2()
        {
            double[,] a1 = new double[,]
            {
                {2.5,1,0,1},
                {6,0,1,1},

            };
            char[] sign1 = new char[]
            {
                '=',
                '<',
                '<',
                '='
            };
            double[,] extected = new double[,]
            {
                {2.5,1,0,1 },
                {6,0,1,1},
                {0,1,0,0},
                {0,0,1,0 },
            };
            LibrarySimplexMethod.Simplex simplex = new LibrarySimplexMethod.Simplex(2, 4, a1,
                new double[] { 1, 2, 3 }, new double[] { 1, 2, 3 }, sign1);
            simplex.TranslationMatrixA();

            for (int i = 0; i < extected.GetLength(0); i++)
            {
                for (int j = 0; j < extected.GetLength(1); j++)
                {
                    Assert.AreEqual(extected[i, j], simplex.A[i, j]);
                    Console.Write(simplex.A[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void TestIndexStartBasisInMatrixA()
        {
            double[,] a1 = new double[,]
            {
                {2.5,1,0,1},
                {6,0,1,1},

            };
            char[] sign1 = new char[]
            {
                '=',
                '<',
                '<',
                '>'
            };
            List<int> extected=new List<int>();
            extected.Add(2);
            extected.Add(3);
            LibrarySimplexMethod.Simplex simplex = new LibrarySimplexMethod.Simplex(2, 4, a1,
                new double[] { 1, 2, 3 }, new double[] { 1, 2, 3 }, sign1);
            simplex.TranslationMatrixA();
            simplex.SearchBasis();

            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(extected[i],simplex.IndexBasis[i]);
                Console.Write(simplex.IndexBasis[i]+" ");
            }
        }

        [TestMethod]
        public void TestSimpleSimplexMethod()
        {
            double[,] a = new double[,]
            {
                {3,2},
                {4,5}
            };
            double[] b = new double[] {1700,1600};
            char[] znak = new char[] { '<', '<' };
            double expected = -1400;


            LibrarySimplexMethod.Simplex simplex = new LibrarySimplexMethod.Simplex(n: 2, m: 2,a,b,new double[] {-2,-4},znak);
            simplex.TranslationMatrixA();
            double Z = simplex.SimpleSimplexMethod(0);

            Assert.AreEqual(expected, Z);
        }
    }
}
