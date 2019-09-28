using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySimplexMethod
{
    /// <summary>
    /// Вспомогательный класс
    /// </summary>
    public static class Bin
    {
        /// <summary>
        /// Записывает в новую матрицу значения другой матрицы
        /// </summary>
        /// <param name="oldMatrix">Матрица, из которой нужно достать данные</param>
        /// <param name="newMatrix">Матрица, в которую нужно записать данные</param>
        /// <returns></returns>
        public static void OldValueInNewMatrix( double[,] oldMatrix,ref double[,] newMatrix)
        {
            for (int i = 0; i < oldMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < oldMatrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = oldMatrix[i, j];
                }
            }
        }

        public static void OldValueInNewMatrix(double[] oldMatrix, ref double[] newMatrix)
        {
            for (int i = 0; i < oldMatrix.Length; i++)
            {
                newMatrix[i] = oldMatrix[i];
            }
        }
    }
}
