
class First
{
    public string Name { get; set; } = string.Empty; //вот так мы можем подавить предупрждение компилятора, иницализировав как пустую строку.
    public int Age { get; set; }
    public double Score { get; set; }
    public string? Description { get; set; }
    public int? Rating { get; set; }
    public double? Double {  get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        First first = new First()
        {
            Name = "Алекс",
            Age = 10,
            Score = 13.5,
            Description = null,
            Rating = 1,
            Double = 1,
        };

        First second = new First()
        {
            Name = "Джони Пека",
            Age = 34,
            Score = 223.2,
            Description = "скромен, не женат",
            Rating = 12,
            Double = null,
        };
        second.Name = null; //строка является ссылчным типом данных, поэтому она может принимать значение null.
        //first.Age = null!; //тип данных int не может принимать значение типа null, поставив восклицательный знак после null мы указываем, 
                           //что значение типа null допустимо присвоить int или double (или другим подобным типам данных). Однако это доступно
                           //начиная с c# 8 версии.
        int lengthOfString = first.Name!.Length; //здесь при помощи восклицательного знака мы указываем, что уверены в том, что значение не null
        Info(first);
        Info(second);

    }
    static void Info(First first)
    {
        Console.WriteLine("Имя: " + first.Name);
        Console.WriteLine("Возраст: " + first.Age);
        Console.WriteLine("Счет: " + first.Score);
        Console.WriteLine("Описание: " + (first.Description ?? "Данные отсутсвуют"));
        Console.WriteLine("Рейтниг: " + (first.Rating.HasValue ? first.Rating : "Данные отсутсвуют"));
        Console.WriteLine("Double значение: " + (first.Double.HasValue ? first.Double : "Данные отсутсвуют"));
    }

    static void Assignment_znach(First first)
    {
        first.Description ??= "Новобранец";
        first.Rating ??= 10;
        first.Double ??= 10; 
    }

    static int NoNull(First? first)
    {
        return first?.Rating ?? 10;
    }

    static double NoNoNull(First first)
    {
        return first.Rating ?? 10.5;
    }
}
