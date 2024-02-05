public class Student
{
    public string Name { get; set; }
    public string F_name { get; set; }
    public int Age { get; set; }
    public Student(string name, string f_name, int age)
    {
        Name = name;
        F_name = f_name;
        Age = age;
    }

    public override string ToString()
    {
       return $"Имя: {Name}, Фамилия: {F_name}, Возраст: {Age}";
    }

}

public class School : Student
{
    public int Number_class { get; set; }
    public double Sr_b {  get; set; }
    public School (string name, string f_name, int age, int number_class, double sr_b) : base (name, f_name, age)
    {
        Number_class = number_class;
        Sr_b = sr_b;
    }

    public string Good()
    {
        if (Sr_b > 4.5)
        {
            return "Отличник";
        }
        if (Sr_b <4.5 && Sr_b > 3.5)
        {
            return "Хорошист";
        }
        else
        {
            return "Отстающий";
        }
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Класс: {Number_class}, Средний балл: {Sr_b}";
    }
}

public class Univercity : Student
{
    public int Years_of_study { get; set; }

    public int Number_credit { get; set; }
    public Univercity(string name, string f_name, int age, int years_of_study, int number_credit) : base (name, f_name, age)
    {
        Years_of_study = years_of_study;
        Number_credit = number_credit;
    }

    public string Degree_of_education()
    {
        if (Years_of_study <= 4)
        {
            return "Бакалавриат";
        }
        if (Years_of_study > 4 && Years_of_study <= 6)
        {
            return "Магистратура";
        }
        else
        {
            return "Аспирантура";
        }
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Годы обучения в университете: {Years_of_study}, Номер зачетной книжки: {Number_credit}";
    }

}

class Conference
{
    private List<Student> students = new List<Student>();

    public void Add(Student student)
    {
        students.Add(student);
    }

    public int Count_School()
    {
        return students.Count(s => s is School);
    }

    public int Count_Univercity()
    {
        return students.Count(s => s is Univercity);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Conference conference = new Conference();

        Student Alex = new Student("Алексей", "Степанов", 15);
        Student Ivan = new Student("Иван", "Годзилыч", 22);
        Student Oleg = new Student("Олег", "Бобруйский", 12);

        School Inocentiy = new School("Инокентий", "Висицкий", 17, 10, 4.95);
        School Mihail = new School("Михаил", "Балдеев", 14, 8, 2.6);

        Univercity Leopold = new Univercity("Леопольд", "Котик", 22, 4, 1234);
        Univercity Igor = new Univercity("Игорь", "Крутой", 24, 6, 5555);
        Univercity Egor = new Univercity("Егор", "Обалденный", 26, 8, 1212);

        Console.WriteLine(Alex.ToString());
        Console.WriteLine(Ivan.ToString());
        Console.WriteLine(Oleg.ToString());
        Console.WriteLine();
        Console.WriteLine(Inocentiy.ToString());
        Console.WriteLine(Inocentiy.Good());
        Console.WriteLine(Mihail.ToString());
        Console.WriteLine(Mihail.Good());
        Console.WriteLine();
        Console.WriteLine(Leopold.ToString());
        Console.WriteLine(Leopold.Degree_of_education());
        Console.WriteLine(Igor.ToString());
        Console.WriteLine(Igor.Degree_of_education());
        Console.WriteLine(Egor.ToString());
        Console.WriteLine(Egor.Degree_of_education());
        Console.WriteLine();

        conference.Add(Alex);
        conference.Add(Ivan);
        conference.Add(Oleg);
        conference.Add(Inocentiy);
        conference.Add(Mihail);
        conference.Add(Leopold);
        conference.Add(Igor);
        conference.Add(Egor);

        Console.WriteLine("Количество школьников в конференции: " + conference.Count_School());
        Console.WriteLine("Количество студентов в конференции: " + conference.Count_Univercity());
    }
}