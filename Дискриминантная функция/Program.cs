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
        static void Main(string[] args)
        {
            string path = @"C:\Users\ADM\Desktop\Проекты C# Visual Studio 2019\Дискриминантная функция\ExcelFile.xlsx";
            Excel excel = new Excel(path, 1);

            double[,] LearningDataA = excel.ReadRange(2, 1, 26, 2);
            double[,] LearningDataB = excel.ReadRange(27, 1, 51, 2);
            double[,] FullLearningData = excel.ReadRange(2, 1, 51, 2);

            //double AvgA1 = AverageVal(LearningDataA, 0);
            //double AvgA2 = AverageVal(LearningDataA, 1);
            //double AvgB1 = AverageVal(LearningDataB, 0);
            //double AvgB2 = AverageVal(LearningDataB, 1);

            double AvgTotal1 = AverageVal(FullLearningData, 0);
            double AvgTotal2 = AverageVal(FullLearningData, 1);

            double[,] CovarMatrix = new double[FullLearningData.GetLength(1), FullLearningData.GetLength(1)];

            CovarMatrix[0, 0] = GetPureCovarValue(FullLearningData, AvgTotal1, 0);
            CovarMatrix[0, 1] = GetMixedCovarValue(FullLearningData, AvgTotal1, AvgTotal2);
            CovarMatrix[1, 0] = CovarMatrix[0, 1];
            CovarMatrix[1, 1] = GetPureCovarValue(FullLearningData, AvgTotal2, 1);

            excel.WriteRange(3, 6, 4, 7, CovarMatrix);
            excel.Save();


            excel.Close();
        }
    }
}
