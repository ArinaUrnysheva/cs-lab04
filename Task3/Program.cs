using System;
using System.Collections;
using System.Collections.Generic;

public class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public int MaxSpeed { get; set; }

    public Car(string name, int productionYear, int maxSpeed)
    {
        Name = name;
        ProductionYear = productionYear;
        MaxSpeed = maxSpeed;
    }

    public override string ToString()
    {
        return $"{Name}: год выпуска - {ProductionYear}, максимальная скорость - {MaxSpeed}";
    }
}

public enum SortCriteria
{
    Name,
    ProductionYear,
    MaxSpeed
}

public class CarComparer : IComparer<Car> //сравнение объектов типа Car
{
    private readonly SortCriteria sortCriteria; //критерий сравнения
    public CarComparer(SortCriteria criteria) //конструктор
    {
        sortCriteria = criteria;
    }
    public int Compare(Car a, Car b) //сравнение двух машин
    {
        if (a == null || b == null)
        {
            return 0;
        }

        return sortCriteria switch //сравнение по трем критериям
        {
            SortCriteria.Name => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase),
            SortCriteria.ProductionYear => a.ProductionYear.CompareTo(b.ProductionYear),
            SortCriteria.MaxSpeed => a.MaxSpeed.CompareTo(b.MaxSpeed),
            _ => 0 //по умолчанию
        };
    }
}

public class CarCatalog : IEnumerable<Car>
{
    private Car[] cars; //массив элементов типа Car
    public CarCatalog(Car[] carArray) //конструктор
    {
        cars = carArray;
    }

    //прямой проход с первого элемента до последнего
    public IEnumerator<Car> GetEnumerator()
    {
        foreach(var car in cars)
        {
            yield return car;
        }
    }

    //прямой проход для IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    //oбратный проход от последнего к первому
    public IEnumerable<Car> GetReverseEnumerator()
    {
        for (int i = cars.Length - 1; i >= 0; i--)
        {
            yield return cars[i];
        }
    }

    //проход с фильтром по году выпуска
    public IEnumerable<Car> GetCarsByProductionYear(int year)
    {
        foreach (var car in cars)
        {
            if (car.ProductionYear == year)
            {
                yield return car;
            }
        }
    }

    //проход с фильтром по максимальной скорости
    public IEnumerable<Car> GetCarsByMaxSpeed(int speed)
    {
        foreach (var car in cars)
        {
            if (car.MaxSpeed >= speed)
            {
                yield return car;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Car[] cars = new Car[]
        {
            new Car("Mercedes", 2010, 260),
            new Car("Kia", 2000, 200),
            new Car("BMW", 2020, 300)
        };

        CarCatalog catalog = new CarCatalog(cars);

        Console.WriteLine("Прямой проход по элементам:");
        foreach (var car in catalog)
        {
            Console.WriteLine(car);
        }

        Console.WriteLine("\nОбратный проход по элементам:");
        foreach (var car in catalog.GetReverseEnumerator())
        {
            Console.WriteLine(car);
        }


        Console.WriteLine("\nФильтр по году выпуска (2020):");
        foreach (var car in catalog.GetCarsByProductionYear(2020))
        {
            Console.WriteLine(car);
        }

        Console.WriteLine("\nФильтр по максимальной скорости (250 км/ч):");
        foreach (var car in catalog.GetCarsByMaxSpeed(250))
        {
            Console.WriteLine(car);
        }
    }
}