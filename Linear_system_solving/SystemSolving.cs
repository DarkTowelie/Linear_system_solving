namespace Linear_system_solving
{
    class SystemSolving
    {
        private double[,] left;//Основная матрицы системы
        private double[] right;//Вектор свободных компонент
        //------------------------------------------------------------------------------------------------------
        public SystemSolving(double[,] left, double[] right)
        {
            this.left = left;
            this.right = right;
        }
        //------------------------------------------------------------------------------------------------------
        public double[] gauss()
        {
            double[,] copyLeft = (double[,])left.Clone();
            double[] copyRight = (double[])right.Clone();
            int rows = copyLeft.GetLength(0);
            int cols = copyLeft.GetLength(1);
            int replaceInd;
            double buf;

            //Прямой пробег
            for (int rInd = 0; rInd < rows; rInd++)
            {
                if (copyLeft[rInd, rInd] == 0)
                {
                    replaceInd = rInd;
                    for (int j = rInd + 1; j < rows; j++)
                    {
                        if (copyLeft[j, rInd] != 0)
                        {
                            replaceInd = j;
                            break;
                        }
                    }

                    if (replaceInd != rInd)
                    {
                        for (int cInd = 0; cInd < cols; cInd++)
                        {
                            buf = copyLeft[rInd, cInd];
                            copyLeft[rInd, cInd] = copyLeft[replaceInd, cInd];
                            copyLeft[replaceInd, cInd] = buf;
                        }

                        buf = copyRight[rInd];
                        copyRight[rInd] = copyRight[replaceInd];
                        copyRight[replaceInd] = buf;
                    }
                }

                if (copyLeft[rInd, rInd] != 0)
                {
                    buf = copyLeft[rInd, rInd];
                    for (int cIndex = 0; cIndex < cols; cIndex++)
                    {
                        copyLeft[rInd, cIndex] /= buf;
                    }
                    copyRight[rInd] /= buf;


                    for (int j = rInd + 1; j < rows; j++)
                    {
                        buf = copyLeft[j, rInd];
                        for (int cIndex = 0; cIndex < cols; cIndex++)
                        {
                            copyLeft[j, cIndex] -= copyLeft[rInd, cIndex] * buf;
                        }
                        copyRight[j] -= copyRight[rInd] * buf;
                    }
                }
            }

            //Обратный пробег
            for (int rInd = rows - 1; rInd > 0; rInd--)
            {
                for (int j = rInd - 1; j >= 0; j--)
                {
                    if (copyLeft[j, rInd] != 0)
                    {
                        buf = copyLeft[j, rInd];

                        for (int cInd = 0; cInd < cols; cInd++)
                        {
                            copyLeft[j, cInd] -= copyLeft[rInd, cInd] * buf;
                        }
                        copyRight[j] -= copyRight[rInd] * buf;
                    }
                }
            }
            return copyRight;
        }
    }
}
