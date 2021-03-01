using System;
using System.Text;
using System.IO;

namespace Linear_system_solving
{
    class Tests
    {
        static void Main(string[] args)
        {
            SystemSolving systemSolving = null;
            //------------------------------------------------------------------------------------------------------
            bool testSuccess = true;
            double[] testRes = new double[6];
            double[] testRight = new double[6];
            double[,] testLeft = new double[6, 6];

            //Тест 1 - классическая система-------------------------------------------------------------------------
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string testDataPath = startupPath + "\\Test\\TEST_gauss_system_solving1.dat";
            string inputDataPath = startupPath + "\\Test\\RESULT_gauss_system_solving1.dat";
            
            readTestFile(ref testLeft, ref testRight, testDataPath);
            readResultFile(ref testRes, inputDataPath);

            systemSolving = new SystemSolving(testLeft, testRight);
            double[] res = systemSolving.gauss();

            testSuccess = true;
            for (int i = 0; i < 6; i++)
            {
                if (Math.Abs(res[i] - testRes[i]) > Math.Abs(0.01))
                {
                    testSuccess= false;
                    Console.Write("TEST1 FAIL \n");
                    break;
                } 
            }

            if (testSuccess == true)
                Console.Write("TEST1 SUCCESS \n");

            //Тест 2 - нулевой элемент на главной диагонали----------------------------------------------------------
            testDataPath = startupPath + "\\Test\\TEST_gauss_system_solving2.dat";
            inputDataPath = startupPath + "\\Test\\RESULT_gauss_system_solving2.dat";

            readTestFile(ref testLeft, ref testRight, testDataPath);
            readResultFile(ref testRes, inputDataPath);

            systemSolving = new SystemSolving(testLeft, testRight);
            res = systemSolving.gauss();

            testSuccess = true;
            for (int i = 0; i < 6; i++)
            {
                if (Math.Abs(res[i] - testRes[i]) > Math.Abs(0.01))
                {
                    testSuccess = false;
                    Console.Write("TEST2 FAIL \n");
                    break;
                }
            }

            if (testSuccess == true)
                Console.Write("TEST2 SUCCESS \n");

            //Тест 3 - случайный нулевой элемент--------------------------------------------------------------------
            testDataPath = startupPath + "\\Test\\TEST_gauss_system_solving3.dat";
            inputDataPath = startupPath + "\\Test\\RESULT_gauss_system_solving3.dat";

            readTestFile(ref testLeft, ref testRight, testDataPath);
            readResultFile(ref testRes, inputDataPath);

            systemSolving = new SystemSolving(testLeft, testRight);
            res = systemSolving.gauss();

            testSuccess = true;
            for (int i = 0; i < 6; i++)
            {
                if (Math.Abs(res[i] - testRes[i]) > Math.Abs(0.01))
                {
                    testSuccess = false;
                    Console.Write("TEST3 FAIL \n");
                    break;
                }
            }

            if (testSuccess == true)
                Console.Write("TEST3 SUCCESS \n");

            //Тест 4 - почти нулевые стобцы-----------------------------------------------------------------------
            testDataPath = startupPath + "\\Test\\TEST_gauss_system_solving4.dat";
            inputDataPath = startupPath + "\\Test\\RESULT_gauss_system_solving4.dat";

            readTestFile(ref testLeft, ref testRight, testDataPath);
            readResultFile(ref testRes, inputDataPath);

            systemSolving = new SystemSolving(testLeft, testRight);
            res = systemSolving.gauss();
            testSuccess = true;
            for (int i = 0; i < 6; i++)
            {
                if (Math.Abs(res[i] - testRes[i]) > 0.01)
                {
                    testSuccess = false;
                    Console.Write("TEST4 FAIL \n");
                    break;
                }
            }

            if (testSuccess == true)
                Console.Write("TEST4 SUCCESS \n");

            //Тест 5 - нулевая нижняя часть-------------------------------------------------------------------------

            testDataPath = startupPath + "\\Test\\TEST_gauss_system_solving5.dat";
            inputDataPath = startupPath + "\\Test\\RESULT_gauss_system_solving5.dat";

            readTestFile(ref testLeft, ref testRight, testDataPath);
            readResultFile(ref testRes, inputDataPath);

            systemSolving = new SystemSolving(testLeft, testRight);
            res = systemSolving.gauss();
            testSuccess = true;
            for (int i = 0; i < 6; i++)
            {
                if (Math.Abs(res[i] - testRes[i]) > 0.01)
                {
                    testSuccess = false;
                    Console.Write("TEST5 FAIL \n");
                    break;
                }
            }

            if (testSuccess == true)
                Console.Write("TEST5 SUCCESS \n");
            //------------------------------------------------------------------------------------------------------
            Console.ReadKey(); 
        }
        public static void readTestFile(ref double[,] A, ref double[] b, string path)
        {
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);

            int i = 0;
            foreach (string line in lines)
            {
                string sep = "\t";
                string[] words = line.Split(sep.ToCharArray());

                for (int j = 0; j < A.GetLength(0); j++)
                {
                    try
                    {
                        A[i, j] = Convert.ToDouble(words[j]);
                    }
                    catch
                    {
                        words[j] = words[j].Replace('.', ',');
                        A[i, j] = Convert.ToDouble(words[j]);
                    }
                    
                }
                try
                {
                    b[i] = Convert.ToDouble(words[b.GetLength(0)]);
                }
                catch
                {
                    words[b.GetLength(0)] = words[b.GetLength(0)].Replace('.', ',');
                    b[i] = Convert.ToDouble(words[b.GetLength(0)]);
                }
                
                i++;
            }
        }
        public static void readResultFile(ref double[] testRes, string path)
        {
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);

            int i = 0;
            foreach (string line in lines)
            {
                string sep = "\t";
                string[] words = line.Split(sep.ToCharArray());

                try
                {
                    testRes[i] = Convert.ToDouble(words[0]);
                }
                catch
                {
                    words[0] = words[0].Replace('.', ',');
                    testRes[i] = Convert.ToDouble(words[0]);
                }
                i++;
            }
        }
    }
}