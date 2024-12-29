using System;
using System.Collections.Generic;

public class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public int MaxSpeed { get; set; }

    public Car(string name, int productionYear, int maxSpeed) //конструктор
    {
        Name = name;
        ProductionYear = productionYear;
        MaxSpeed = maxSpeed;
    }

    public override string ToString()
    {
        return $"{Name}: {ProductionYear} года выпуска, максимальная скорость - {MaxSpeed}";
    }
}

public enum SortCriteria //возможные критерии сортировки
{
    Name,
    ProductionYear,
    MaxSpeed
}

public class CarComparer: IComparer<Car> //сравнение объектов типа Car
{
    private readonly SortCriteria sortCriteria; //критерий сравнения
    public CarComparer(SortCriteria criteria) //конструктор
    {
        sortCriteria = criteria;
    }
    public int Compare(Car a, Car b) //сравнение двух машин
    {
        if(a == null || b == null)
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

        Console.WriteLine("Сортировка по названию");
        Array.Sort(cars, new CarComparer(SortCriteria.Name));
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }

        Console.WriteLine("\nСортировка по году выпуска:");
        Array.Sort(cars, new CarComparer(SortCriteria.ProductionYear));
        foreach(var car in cars)
        {
            Console.WriteLine(car);
        }

        Console.WriteLine("\nСортировка по максимальной скорости:");
        Array.Sort(cars, new CarComparer(SortCriteria.MaxSpeed));
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }
    }
}