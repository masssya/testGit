//1.Как происходит преобразование типов в языке c#?
//Какие способы преобразования можно применять, в чем различие между нисходящим и восходящим преобразовании?
//Укажите особенности преобразования типов в языке c#. Поясните свой ответ примерами.
//2.Как происходит поиск блока catch при обработке исключений?

class Animal // создание системы классов
{
    public string Poroda { get; set; }
    public void Vid()
    {
        Console.WriteLine($"Вид животного: {Poroda}");
    }

    public Animal (string poroda)
    {
        Poroda = poroda;
    }
}

class Vid : Animal
{
    public double Ves { get; set; }
    public void Ves_pokaz()
    {
        Console.WriteLine($"Вес животного: {Ves}");
    }

    public Vid (string poroda, double ves) : base (poroda)
    {
        Ves = ves;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int a = 20; // неявное
        double a_double = a;

        double b = 20.64; // явное
        int b_int = (int)b;

        Vid vid = new Vid("Кошка", 9); // восходящее преобразование
        Animal animal = vid;
        Console.WriteLine(animal.Poroda);
        animal.Vid();
        Console.WriteLine();

        Vid vid4 = new Vid("Птичка", 0.5);
        Animal animal4 = vid4;

        Vid vid5 = (Vid)animal4;


        Animal animal2 = new Vid("Собака", 16); //использование ключевого слова as
        Vid? vid2 = animal2 as Vid;
        if (vid2 != null)
        {
            vid2.Ves_pokaz();
            Console.WriteLine(vid2.Poroda);
        }
        else
        {
            Console.WriteLine("Неудачное преобразование");
        }
        Console.WriteLine();

        Animal animal3 = new Vid("Слон", 444);
        if (animal3 is Vid) vid3) // использование слова is
        {
            vid3.Ves_pokaz();
            Console.WriteLine(vid3.Poroda);
        }
        else
        {
            Console.WriteLine("Неудачное преобразование");
        }
    }
}

try
{
    
}
catch()
{

}
catch ()
{
    
}
catch ()
{
    
}