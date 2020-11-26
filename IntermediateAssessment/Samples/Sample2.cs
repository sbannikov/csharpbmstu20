using System;
using System.ServiceModel;

namespace IntermediateAssessment.Samples
{
    public class Matrix
    {
        private double[][] data;
        private int n;
        private int m;

        public Matrix() { }

        // Инициализация матрицы заданного размера
        public Matrix(int n, int m)
        {
            this.n = n;
            this.m = m;
            data = new double[n][];
            for (int i = 0; i < n; i++)
            {
                data[i] = new double[m];
                for (int j = 0; i < m; j++)
                {
                    data[i][j] = double.NaN;
                }
            }
        }

        // Создание матрицы с размером как у заданной матрицы
        public Matrix(Matrix m)
            : this(m.n, m.m)
        {
        }
    }

    [ServiceContract()]
    public interface IExercise2
    {
        // Обратная матрица
        [OperationContract()]
        Matrix Inversion(Matrix m);

        // Определитель матрицы
        [OperationContract()]
        double Determinant(Matrix m);
    }

    [ServiceBehavior
     (InstanceContextMode = InstanceContextMode.PerCall,
      ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Exercise2 : IExercise2
    {
        public double Determinant(Matrix m)
        {
            double result = double.NaN;
            // Код вычисления определителя матрицы для простоты опущен
            return result;
        }

        public Matrix Inversion(Matrix m)
        {
            Matrix result = new Matrix(m);
            // Код вычисления обратной матрицы для простоты опущен
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host;
            host = new ServiceHost(typeof(Exercise2));

            host.Open();
            Console.WriteLine("Сервис запущен");
            Console.ReadLine();
        }
    }
}
