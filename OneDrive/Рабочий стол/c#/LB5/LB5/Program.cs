//using System;
//using System.Globalization;
//using System.Net;
//using System.Reflection.Metadata.Ecma335;
//using System.Xml.Linq;
using DataAccess;

class Student : Human
{
    private double sr_b;
    private int number_book;
    public Student(string name, string f_name, string patronymic, string adress, int age, double sr_b, int number_book) : base(name, f_name, patronymic, adress, age)
    {
        Sr_b = sr_b;
        Number_book = number_book;
    }
    public double Sr_b
    {
        get { return sr_b; }
        set { sr_b = value; }
    }
    public int Number_book
    {
        get { return number_book; }
        set { number_book = value; }
    }
    //////////////////////////////////// ПЕРЕДЕЛАТЬ!!!
    public override string ToString()
    {
        return $"{Name} {F_name}, Возраст: {Age}, Средний балл: {Sr_b}";
    }

    //public string ToString()
    //{
    //    return $"{Name} {F_name} {Patronymic}, Возраст: {Age}, Адрес: {Address}, Средний балл: {Sr_b}, Номер зачетной книжки: {number_book}";
    //}
}

class University
{
    private List<Student> students = new List<Student>();


    public void AddStudent(Student student)
    {
        if (student != null)
        {
            students.Add(student);
            Console.WriteLine($"Студент {student.Name} {student.F_name} принят в университет!");
        }
        else
        {
            Console.WriteLine("Данные введены некорректно, студент не может быть зачислен.");
        }
    }

    public void RemoveStudent(Student student)
    {
        if (students.Contains(student))
        {
            students.Remove(student);
            Console.WriteLine($"Студент {student.Name} {student.F_name} отчислен)");
        }
        else
        {
            Console.WriteLine("Такой студент не найден.");
        }
    }

    public Student FindStudent(string firstName, string lastName)
    {
        return students.Find(student => student.Name == firstName && student.F_name == lastName);
    }

    public List<Student> GetStudents()
    {
        return students;
    }
}

class Human
{
    private string name;
    private string f_name;
    private string patronymic;
    private string date_of_birth;
    private string address;
    private int age;
    static private int human_id = 0;

    //private static int ID = 0;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string F_name
    {
        get { return f_name; }
        set { f_name = value; }
    }
    public string Patronymic
    {
        get { return patronymic; }
        set { patronymic = value; }
    }
    public string DateOfBirth
    {
        get { return date_of_birth; }
        set { date_of_birth = value; }
    }
    public string Address
    {
        get { return address; }
        set { address = value; }
    }
    public int Age
    {
        get { return age; }
        set
        {
            if (value <= 0)
            {
                age = value;
            }
            else
            {
                Console.WriteLine("Возраст не может быть отрицательным");
                age = 0;
            }
        }
    }
    public int Human_id
    {
        get { return human_id; }
        set { human_id = value; }
    }

    public Human(string NAME, string F_NAME, string PATRONYMIC, string ADDRESS, int AGE)
    {
        name = NAME;
        f_name = F_NAME;
        patronymic = PATRONYMIC;
        address = ADDRESS;
        age = AGE;
        //human_id = ID;

        human_id += 1;
        Saved_human.AddHuman(this);
    }

    //public void
}

static class Saved_human
{
    public static List<Human> humans = new List<Human>(); // список для хранения данных всех людей

    public static void AddHuman(Human human)
    {
        humans.Add(human);
    }

    public static Human Show_data_human(int Human_id) // показать данные человека по ID
    {
        return humans.Find(humans => humans.Human_id == Human_id);
    }

    public static void PPP(Human human)
    {
        Console.WriteLine($"Имя: {human.Name}");
    }
}

namespace DataAccess
{
    class StudentsRepository
    {
        private const string FilePath = "students.txt";

        public static void SaveStudents(List<Student> students)
        {
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.Name},{student.F_name},{student.Patronymic},{student.Address},{student.Age},{student.Sr_b},{student.Number_book}");
                }
            }
        }

        public static List<Student> LoadStudents()
        {
            List<Student> students = new List<Student>();
            if (File.Exists(FilePath))
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 7)
                        {
                            string name = parts[0];
                            string f_name = parts[1];
                            string patronymic = parts[2];
                            string adress = parts[3];
                            int age = int.Parse(parts[4]);
                            double sr_b = Double.Parse(parts[5]);
                            int number_book = int.Parse(parts[6]);

                            students.Add(new Student(name, f_name, patronymic, adress, age, sr_b, number_book));
                            //if (int.TryParse(parts[2], out int age) && double.TryParse(parts[3], out double averageGrade))
                            //{
                            //    students.Add(new Student(name, f_name, patronymic, adress, age, sr_b, number_book));
                            //}
                        }
                    }
                }
            }
            return students;
        }

        public static void PrintStudents(List<Student> students)
        {
            Console.WriteLine("Обучающиеся студенты:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }

        //public Human Show_data_human(int Human_id) // показать данные человека по ID
        //{
        //    return Saved_human.humans.Find(humans => humans.Human_id == Human_id); 
        //}
    }
}
class Program
{
    static void Main(string[] args)
    {
        University university = new University();

        List<Student> students = StudentsRepository.LoadStudents();
        university.GetStudents().AddRange(students);

        int choice = 0;
        while (choice != 4)
        {
            Console.WriteLine("Выберите операцию:");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Исключить студента");
            Console.WriteLine("3. Вывести список обучающихся");
            Console.WriteLine("4. Выход из прграммы");
            Console.WriteLine("5. Показать данные человека по ID");
            Console.WriteLine("6. Ввести данные человека:");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Введите имя студента: ");
                        string name = Console.ReadLine();
                        Console.Write("Введите фамилию студента: ");
                        string f_name = Console.ReadLine();
                        Console.Write("Введите отчество студента: ");
                        string patronymic = Console.ReadLine();
                        Console.Write("Введите адрес студента: ");
                        string adress = Console.ReadLine();
                        Console.Write("Введите возраст: ");
                        if (int.TryParse(Console.ReadLine(), out int age))
                        {
                            Console.Write("Введите средний балл студента: ");
                            if (double.TryParse(Console.ReadLine(), out double sr_b))
                            {
                                Console.Write("Введите номер зачетной книжки студента: ");
                                int number_book = int.Parse(Console.ReadLine());
                                university.AddStudent(new Student(name, f_name, patronymic, adress, age, sr_b, number_book));
                                StudentsRepository.SaveStudents(university.GetStudents());
                            }
                            else
                            {
                                Console.WriteLine("Некорректно введен средний балл, попробуйте еще раз");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверно введен возраст, студент не добавлен");
                        }
                        break;
                    case 2:
                        Console.Write("Введите имя отчисляемого студента: ");
                        string removeFirstName = Console.ReadLine();
                        Console.Write("Введите фамилию отчисляемого студента: ");
                        string removeLastName = Console.ReadLine();
                        Student studentToRemove = university.FindStudent(removeFirstName, removeLastName);
                        if (studentToRemove != null)
                        {
                            university.RemoveStudent(studentToRemove);
                            StudentsRepository.SaveStudents(university.GetStudents());
                        }
                        else
                        {
                            Console.WriteLine("Такой студент не найден.");
                        }
                        break;
                    case 3:
                        StudentsRepository.PrintStudents(university.GetStudents());
                        break;
                    case 4:
                        Console.WriteLine("Выход из программы...");
                        break;
                    case 5:
                        Console.WriteLine("Введите ID человека:");
                        int ID = int.Parse(Console.ReadLine());
                        Saved_human.PPP(Saved_human.Show_data_human(ID));
                        break;
                    //case 6:
                    //    Console.Write("Введите имя человека: ");
                    //    string name = Console.ReadLine();
                    //    Console.Write("Введите фамилию человека: ");
                    //    string f_name = Console.ReadLine();
                    //    Console.Write("Введите отчество человека: ");
                    //    string patronymic = Console.ReadLine();
                    //    Console.Write("Введите дату рождения человека: ");
                    //    string date_of_birth = Console.ReadLine();
                    //    Console.Write("Введите адрес человека: ");
                    //    string address = Console.ReadLine();
                    //    Console.Write("Введите возраст человека: ");
                    //    int Age = int.Parse(Console.ReadLine());

                    //    Human human = new Human (name, f_name, patronymic, address, Age);
                    //    break;
                    default:
                        Console.WriteLine("Такой операции нет в нашем списке");
                        break;
                }
            }
        }

        StudentsRepository.SaveStudents(university.GetStudents());
    }
}