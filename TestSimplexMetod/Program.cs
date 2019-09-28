using System;
using System.Collections.Generic;
using Simpex = LibrarySimplexMethod;

namespace TestSimplexMetod
{
    class Program
    {
        public static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j!=matrix.GetLength(1)-1)
                    {
                        Console.Write(matrix[i, j] + " ");
                    }
                    else
                    {
                        Console.Write(matrix[i, j] + " ");
                        Console.WriteLine();
                    }
                }
                
            }
        }
        static void PrintListInt(List<int> list)
        {
            foreach (var item in list)
            {
                Console.Write(item+" ");
            }
            Console.WriteLine();
        }
        static void PrintVectorDouble(double[] list)
        {
            foreach (var item in list)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            
            
        }
    }
}
