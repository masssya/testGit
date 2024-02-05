using System;
using System.Text.RegularExpressions;

public abstract class Animal
{
    public abstract string Type { get; set; } //тип животного
    public abstract double Weight { get; set; } //вес
    public abstract string Breed { get; set; } //порода

    public abstract string Information();
    public abstract string Comparison_with_human();
}

public class Tiger:Animal
{
    public override string Type { get; set; }
    public override double Weight { get; set;}
    public override string Breed { get; set;}

    public int Len_of_claws { get; set;} //длина когтей
    public int Speed { get; set;}

    public Tiger(string type, double weight, string breed, int len_of_claws, int speed)
    {
        Type = type;
        Weight = weight;
        Breed = breed;
        Len_of_claws = len_of_claws;
        Speed = speed;
    }

    public override string Information()
    {
        return $"Тип: {Type}, Вес: {Weight}, Порода: {Breed}, Длина когтей: {Len_of_claws}, Скорость: {Speed} км/ч";
    }
    public override string Comparison_with_human()
    {
        double sr = Math.Round(Weight / 70, 2);
        return $"{Breed} тигр больше человека в {sr} раз!";
    }

    public string Danger() //отдельный метод для класса
    {
        if (Len_of_claws > 3)
        {
            return $"Тигр с такими когтями опасен, если вы его встретите, вам поможет только чудо";
        }
        else
        {
            return $"Все тигры опасны, но тут вам повезло, просто обойдите такого красавчика стороной, ничего страшного не произойдет";
        }
    }

}

public class Cat : Animal
{
    public override string Type { get; set; }
    public override double Weight { get; set; }
    public override string Breed { get; set; }

    public string Wool { get; set; } //шерсть
    public double Jump_height { get; set; } //высота прыжка

    public Cat(string type, double weight, string breed, string wool, double jump_height)
    {
        Type = type;
        Weight = weight;
        Breed = breed;
        Wool = wool;
        Jump_height = jump_height;
    }

    public override string Information()
    {
        return $"Тип: {Type}, Вес: {Weight} кг, Порода: {Breed}, Тип шерсти: {Wool}, Высота прыжка: {Jump_height} метра";
    }
    public override string Comparison_with_human()
    {
        double sr = Math.Round(70 / Weight, 2);
        return $"{Breed} кошка меньше человека в {sr} раз";
    }

    public string Recommendations()
    {
        if (Weight >= 5)
        {
            double g = Weight - 5;
            return $"Ваша кошка весит {Weight} килограммов, это больше 5, ей пора сбросить хотя бы {g} килограмм";
        }
        if (Weight < 5 && Weight >= 2)
        {
            return $"С вашей кошкой все хорошо, ее вес в норме";
        }
        else
        {
            double l = 2 - Weight;
            return $"Ваша кошка сильно походула, она весит меньше 2 кг, советуем набрать ей {l} килограмм";
        }
    }

}

public class Bear : Animal
{
    public override string Type { get; set; }
    public override double Weight { get; set; }
    public override string Breed { get; set; }

    public double Height { get; set; } //высота на задних лапах
    public double Impact_force { get; set; } //сила удара

    public Bear(string type, double weight, string breed, double height, double impact_force)
    {
        Type = type;
        Weight = weight;
        Breed = breed;
        Height = height;
        Impact_force = impact_force;
    }

    public override string Information()
    {
        return $"Тип: {Type}, Вес: {Weight} кг, Порода: {Breed}, Высота на задних лапах: {Height} см, Сила удара: {Impact_force} кг";
    }
    public override string Comparison_with_human()
    {
        double sr = Math.Round(Weight / 70, 2);
        return $"{Breed} медведь больше человека в {sr} раз";
    }

    public string Good_bye()
    {
        return $"Сегодня вы узнали информацию о медведе породы {Breed}, оставайтесь с нами и узнавайте множество всего интересного!";
    }

}

class Program
{
    static void Function_for_abstract(Animal animal)
    {
        Console.WriteLine(animal.Information());
        Console.WriteLine(animal.Comparison_with_human());

        Console.WriteLine("Сейчас вы просматриваете информацию о:" + animal.Type + " " + animal.Breed);

        if (animal is Tiger tiger) 
        {
            Console.WriteLine(tiger.Danger());
            Console.WriteLine();
        }
        if (animal is Cat cat)
        {
            Console.WriteLine(cat.Recommendations());
            Console.WriteLine();
        }
        if (animal is Bear bear)
        {
            Console.WriteLine(bear.Good_bye());
            Console.WriteLine();
        }
    }

    static void Main(string[] args)
    {
        Tiger tiger = new Tiger("Тигр", 300, "Амурский", 8, 80);
        Cat cat = new Cat("Кошка", 6, "Шотландская вислоухая", "Короткошерстная", 1.25);
        Bear bear = new Bear("Медведь", 270, "Бурый", 240, 1200);

        Function_for_abstract(tiger);
        Function_for_abstract(cat);
        Function_for_abstract(bear);
    }
}