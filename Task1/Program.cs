using System;
using System.Data.Common;

public class MyMatrix
{
    private int m; //строки
    private int n; //столбцы
    private int[,] Matrix;
    private int d1;
    private int d2;
    private Random rand;
    public MyMatrix(int M, int N, int D1, int D2) //конструктор
    {
        m = M;      
        n = N;
        Matrix = new int[M, N]; //создание матрицы m*n
        rand = new Random();

        for(int i = 0; i<M; i++) //заполнение матрицы случайными значениями от D1 до D2
        {
            for(int j = 0; j<N; j++)
            {
                Matrix[i, j] = rand.Next(D1, D2);
            }
        }
    }

    public int this[int m, int n] //индексатор
    {
        get => Matrix[m, n];    
        set => Matrix[m, n] = value;
    }

    //оператор сложения матриц
    public static MyMatrix operator +(MyMatrix a, MyMatrix b)
    {
        if(a.m != b.m || a.n != b.n)
        {
            throw new ArgumentException("Матрицы должны быть одинаковыми по размеру.");
        }
        MyMatrix result = new MyMatrix(a.m, a.n, 0, 0);
        for(int i = 0; i<a.m; ++i)
        {
            for(int j = 0; j<a.n; ++j)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }
        return result;
    }

    //оператор вычитания матриц
    public static MyMatrix operator -(MyMatrix a, MyMatrix b)
    {
        if (a.m != b.m || a.n != b.n)
        {
            throw new ArgumentException("Матрицы должны быть одинаковыми по размеру.");
        }
        MyMatrix result = new MyMatrix(a.m, a.n, 0, 0);
        for (int i = 0; i < a.m; ++i)
        {
            for (int j = 0; j < a.n; ++j)
            {
                result[i, j] = a[i, j] - b[i, j];
            }
        }
        return result;
    }

    //оператор умножения матриц
    public static MyMatrix operator *(MyMatrix a, MyMatrix b)
    {
        if(a.n != b.m)
        {
            throw new ArgumentException("Матрица а должна иметь столько же столбцов, что и матрица b - строк.");
        }
        MyMatrix result = new MyMatrix(a.m, b.n, 0, 0);
        for(int i = 0; i<a.n; ++i)
        {
            for(int j = 0; j < b.m; ++j)
            {
                result[i, j] += a[i, j] * b[i, j];
            }
        }
        return result;
    }

    //оператор умножения матрицы на число
    public static MyMatrix operator *(MyMatrix a, int scalar)
    {
        MyMatrix result = new MyMatrix(a.m, a.n, 0, 0);
        for(int i = 0; i<a.m; ++i)
        {
            for(int j = 0; j<a.n; ++j)
            {
                result[i, j] = a[i, j] * scalar;
            }
        }
        return result;
    }

    //оператор умножения числа на матрицу
    public static MyMatrix operator *(int scalar, MyMatrix a)
    {
        return a * scalar;
    }

    //оператор деления матрицы на число
    public static MyMatrix operator /(MyMatrix a, int scalar)
    {
        if(scalar == 0)
        {
            throw new ArgumentException("Нельзя делить на 0");
        }

        MyMatrix result = new MyMatrix(a.m, a.n, 0, 0);
        for(int i = 0; i<a.m; ++i)
        {
            for(int j = 0; j<a.n; ++j)
            {
                result[i, j] = a[i, j] / scalar;
            }
        }
        return result;
    }

    //печать матрицы
    public void Print()
    {
        for(int i = 0; i<m; ++i)
        {
            for(int j = 0; j<n; ++j)
            {
                Console.Write(Matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите количество строк матрицы:");
        int m = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество столбцов матрицы:");
        int n = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите минимальное значение случайного числа:");
        int D1 = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите максимальное значение случайного числа:");
        int D2 = int.Parse(Console.ReadLine());

        MyMatrix matrix = new MyMatrix(m, n, D1, D2);
        Console.WriteLine("Сгенерированная матрица:");
        matrix.Print();
    }
}