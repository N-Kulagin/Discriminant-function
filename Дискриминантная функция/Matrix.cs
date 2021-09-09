using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Дискриминантная_функция
{
    static class Matrix
    {
        public static double[,] Sum(double[,] M1, double[,] M2)
        {
            if (M1.GetLength(0) != M2.GetLength(0) || M1.GetLength(1) != M2.GetLength(1) || M1.GetLength(0) != M1.GetLength(1))
            {
                return new double[1, 1];
            }

            double[,] returnArr = new double[M1.GetLength(0), M1.GetLength(0)];

            for (int i = 0; i < M1.GetLength(0); i++)
            {
                for (int j = 0; j < M1.GetLength(0); j++)
                {
                    returnArr[i, j] = M1[i, j] + M2[i, j];
                }
            }
            return returnArr;
        }
        public static double[,] Subtract(double[,] M1, double[,] M2)
        {
            if (M1.GetLength(0) != M2.GetLength(0) || M1.GetLength(1) != M2.GetLength(1) || M1.GetLength(0) != M1.GetLength(1))
            {
                return new double[1, 1];
            }

            double[,] returnArr = new double[M1.GetLength(0), M1.GetLength(0)];

            for (int i = 0; i < M1.GetLength(0); i++)
            {
                for (int j = 0; j < M1.GetLength(0); j++)
                {
                    returnArr[i, j] = M1[i, j] - M2[i, j];
                }
            }
            return returnArr;
        }
        public static double[,] Multiply(double[,] M1, double[,] M2)
        {
            if (M1.GetLength(1) != M2.GetLength(0))
            {
                return new double[1, 1];
            }

            double[,] returnArr = new double[M1.GetLength(0), M2.GetLength(1)];

            for (int i = 0; i < M1.GetLength(0); i++)
            {
                for (int j = 0; j < M2.GetLength(1); j++)
                {
                    for (int k = 0; k < M1.GetLength(1); k++)
                    {
                        returnArr[i, j] += M1[i, k] * M2[k, j];
                    }
                }
            }

            return returnArr;
        }
        public static double[,] Transpose(double[,] arr)
        {
            double[,] myArr = new double[arr.GetLength(1), arr.GetLength(0)];

            for (int i = 0; i < myArr.GetLength(0); i++)
            {
                for (int j = 0; j < myArr.GetLength(1); j++)
                {
                    myArr[i, j] = arr[j, i];
                }
            }
            return myArr;
        }
        public static double[,] MultiplyByValue(double[,] arr, double val)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] *= val;
                }
            }
            return arr;
        }
    }
}
