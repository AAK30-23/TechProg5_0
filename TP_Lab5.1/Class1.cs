using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//n и m - целочисленные свойства, представляет количество строк и столбцов матрицы соответственно.
// p - статическое свойство, представляет двумерный массив строк, который хранит значения элементов матрицы.
//q - двумерный массив строк для инициализации матрицы.
//класс Mat принимает параметры, инициализирует матрицу, создает новый двумерный массив строк a размером nxm и копирует значения из массива q в созданный массив p


class MatX
{
    protected int n { get; set; }
    public static double[] p { get; private set; }
    protected double[] q { get; set; }

    public MatX(int n, double[] b)
    {
        this.n = n;
        p = new double[n];
        this.q = new double[n];
        for (int i = 0; i < n; i++)
        {
            p[i] = b[i];
        }
    }
}


class MatY
{
    protected int n { get; set; }
    public static double[] p { get; private set; }
    protected double[] q { get; set; }

    public MatY(int n, double[] b)
    {
        this.n = n;
        p = new double[n];
        this.q = new double[n];
        for (int i = 0; i < n; i++)
        {
            p[i] = b[i];
        }
    }
}
class Mat
{
    protected int n { get; set; }
    protected int m { get; set; }
    public static string[,] p { get; private set; }
    protected string[,] q { get; set; }

    public Mat(int n, int m, string[,] b)
    {
        this.n = n;
        this.m = m;
        p = new string[n, m];
        this.q = new string[n, m];
        for (int i = 0; i < n; i++)
        {
            for (int w = 0; w < m; w++)
            {
                p[i, w] = b[i, w];
            }
        }

    }
}

class Mat_
{
    protected int n { get; set; }
    protected int m { get; set; }
    public static double[,] p { get; private set; }
    protected double[,] q { get; set; }

    public Mat_(int n, int m, double[,] b)
    {
        this.n = n;
        this.m = m;
        p = new double[n, m];
        this.q = new double[n, m];
        for (int i = 0; i < n; i++)
        {
            for (int w = 0; w < m; w++)
            {
                p[i, w] = b[i, w];
            }
        }

    }
}
    

