using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Lab5._1
{

    class Matrix
    {
        protected int n { get; set; }
        protected int m { get; set; }
        public static string[,] a { get; private set; }
        protected string[,] b { get; set; }

        public Matrix(int n, int m, string[,] b)
        {
            this.n = n;
            this.m = m;
            a = new string[n, m];
            this.b = new string[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int w = 0; w < m; w++)
                {
                    a[i, w] = b[i, w];
                }
            }

/*            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
                for (int w = 0; w < m; w++)
                {
                    Console.Write(a[i, w] + "\t");
                }
            }*/
        }
    }

    class Matrix1
    {
        protected int n { get; set; }
        protected int m { get; set; }
        public static double[,] a { get; private set; }
        protected double[,] b { get; set; }

        public Matrix1(int n, int m, double[,] b)
        {
            this.n = n;
            this.m = m;
            a = new double[n, m];
            this.b = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int w = 0; w < m; w++)
                {
                    a[i, w] = b[i, w];
                }
            }
/*            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
                for (int w = 0; w < m; w++)
                {
                    Console.Write(a[i, w] + "\t");
                }
            }*/
        }
    }
    class MatrixX
    {
        protected int n { get; set; }
        public static double[] a { get; private set; }
        protected double[] b { get; set; }

        public MatrixX(int n, double[] b)
        {
            this.n = n;
            a = new double[n];
            this.b = new double[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = b[i];
            }
        }
    }


    class MatrixY
    {
        protected int n { get; set; }
        public static double[] a { get; private set; }
        protected double[] b { get; set; }

        public MatrixY(int n, double[] b)
        {
            this.n = n;
            a = new double[n];
            this.b = new double[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = b[i];
            }
        }
    }
/*    public static class Extensions
    {
        public static T[,] ToRectangularArray<T>(this IReadOnlyList<T[]> arrays)
        {
            var ret = new T[arrays.Count, arrays[0].Length];
            for (var i = 0; i < arrays.Count; i++)
                for (var j = 0; j < arrays[0].Length; j++)
                    ret[i, j] = arrays[i][j];
            return ret;
        }
    }*/
}
