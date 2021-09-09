using System;

namespace Дискриминантная_функция
{
    class Program
    {
        public static double AverageVal(double[,] arr, int column)
        {
            uint counter = 0;
            double Sum = 0;

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = column; j == column; j++)
                {
                    Sum += arr[i, j];
                    counter++;
                }
            }
            return Sum / counter;
        }
        public static double GetMixedCovarValue(double[,] LearnData, double Avg1, double Avg2)
        {
            double Sum = 0;
            int counter = 0;
            double[] AvgArr = { Avg1, Avg2 };
            double[,] sepArray = new double[LearnData.GetLength(0), LearnData.GetLength(1)];

            for (int i = 0; i < LearnData.GetLength(0); i++)
            {
                for (int j = 0; j < LearnData.GetLength(1); j++)
                {
                    sepArray[i, j] = AvgArr[j] - LearnData[i, j];
                }
            }

            for (int i = 0; i < LearnData.GetLength(0); i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    Sum += sepArray[i, j] * sepArray[i, j + 1];
                    counter++;
                }
            }

            return Sum/counter;
        }
        public static double GetPureCovarValue(double[,] LearnData, double Avg12, int column)
        {
            double Sum = 0;
            int counter = 0;
            double[,] sepArray = new double[LearnData.GetLength(0), LearnData.GetLength(1)];

            for (int i = 0; i < LearnData.GetLength(0); i++)
            {
                for (int j = column; j == column; j++)
                {
                    Sum += Math.Pow((Avg12 - LearnData[i, j]), 2);
                    counter++;
                }
            }

            return Sum / counter;
        }
        public static double DiscriminantFunction(double x, double y, double[,] b_Vect, double[,] p_Val)
        {
            double result = b_Vect[0, 0] * x + b_Vect[1, 0] * y + p_Val[0, 0];
            return result;
        }
        static void Main(string[] args)
        {
            string path = @"C:\Users\ADM\Desktop\Проекты C# Visual Studio 2019\Дискриминантная функция\ExcelFile.xlsx";
            Excel excel = new Excel(path, 1);

            double[,] LearningDataA = excel.ReadRange(2, 1, 26, 2);
            double[,] LearningDataB = excel.ReadRange(27, 1, 51, 2);
            double[,] FullLearningData = excel.ReadRange(2, 1, 51, 2);

            double[,] AvgA = { { AverageVal(LearningDataA, 0), AverageVal(LearningDataA, 1) } };
            double[,] AvgB = { { AverageVal(LearningDataB, 0), AverageVal(LearningDataB, 1) } };
            double[,] AvgMinusAvg = { { AvgA[0, 0] - AvgB[0, 0], AvgA[0, 1] - AvgB[0, 1] } };
            double[,] AvgPlusAvg = { { AvgA[0, 0] + AvgB[0, 0], AvgA[0, 1] + AvgB[0, 1] } };

            double AvgTotal1 = AverageVal(FullLearningData, 0);
            double AvgTotal2 = AverageVal(FullLearningData, 1);

            double[,] CovarMatrix = new double[FullLearningData.GetLength(1), FullLearningData.GetLength(1)];

            CovarMatrix[0, 0] = GetPureCovarValue(FullLearningData, AvgTotal1, 0);
            CovarMatrix[0, 1] = GetMixedCovarValue(FullLearningData, AvgTotal1, AvgTotal2);
            CovarMatrix[1, 0] = CovarMatrix[0, 1];
            CovarMatrix[1, 1] = GetPureCovarValue(FullLearningData, AvgTotal2, 1);

            excel.WriteRange(3, 6, 4, 7, CovarMatrix);
            excel.Save();

            double[,] InverseCovarMatrix = excel.ReadRange(3, 9, 4, 10);

            double[,] b_Vector = Matrix.Multiply(AvgMinusAvg, InverseCovarMatrix);
            b_Vector = Matrix.Transpose(b_Vector);

            double[,] p_Value = Matrix.MultiplyByValue(Matrix.Multiply(AvgPlusAvg, b_Vector), -0.5);

            excel.Close();

            int counter = 0;
            double x;
            double y;
            while (true)
            {
                if (counter == 3)
                {
                    Console.Clear();
                    counter = 0;
                }

                Console.WriteLine();
                Console.WriteLine("Укажите координаты:");
                Console.Write("X: ");
                x = Convert.ToDouble(Console.ReadLine());
                Console.Write("Y: ");
                y = Convert.ToDouble(Console.ReadLine());

                if (DiscriminantFunction(x, y, b_Vector, p_Value) > 0)
                {
                    Console.WriteLine($"Данная точка принадлежит образу А. Её значение функции: {DiscriminantFunction(x, y, b_Vector, p_Value)} (F > 0)");
                }
                else Console.WriteLine($"Данная точка принадлежит образу Б. Её значение функции: {DiscriminantFunction(x, y, b_Vector, p_Value)} (F < 0)");
            }
        }
    }
}
