using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CS2.classes
{
    class RegularUser : BaseUser
    {
        public static string fileName = Path.Combine(Environment.CurrentDirectory, "users.txt"); //функция сохранения перенесена в класс Regular_user для удобства работы

        public static void SaveUsers(List<RegularUser> users)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var user in users)
                {
                    writer.WriteLine($"{user.Login},{user.Password},{user.IsAdmin}");
                }
            }
        }

        public static List<RegularUser> LoadUsers()
        {
            List<RegularUser> users = new List<RegularUser>();

            if (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 3)
                        {
                            RegularUser user = new RegularUser
                            {
                                Login = parts[0],
                                Password = parts[1],
                                IsAdmin = int.Parse(parts[2])
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }


        public List<TaskSection> Sections { get; set; }
        public RegularUser()
        {
            Sections = new List<TaskSection>();
        }

        public void CreateSection(string sectionTitle) // Создание новой секции для пользователя
        {
            Sections.Add(new TaskSection(sectionTitle));
        }
        public void AddTaskSection(int i, Task task) // Для 1 функции
        {
            foreach (var section in Sections)
            {
                if (Sections.Count >= i)
                {
                    if (section == Sections[i - 1] && i <= Sections.Count())
                    {
                        section.Tasks.Add(task);
                    }
                    else
                    {
                        Console.WriteLine($"Раздел не найден.");
                    }
                }
                else
                {
                    Console.WriteLine($"Неверно введен номер раздела");
                    Thread.Sleep(2000);
                }

            }
        }


        public void AddTaskToSection(string sectionTitle, Task task)
        { 
            var section = Sections.Find(s => s.Title == sectionTitle);
            if (section != null)
            {
                section.Tasks.Add(task);
            }
            else
            {
                Console.WriteLine($"Раздел '{sectionTitle}' не найден.");
            }
        }

        public void SaveSections() // метод для сохранения всех задач и разделов, для каждого пользователя все сохраняется в отдельный файл
        {
            string userFileName = $"{Login}_tasks.txt";
            using StreamWriter writer = new StreamWriter(userFileName);

            if (Sections.Count == 0)
            {
                writer.WriteLine($"[Основное]");
            }
            else
            {
                foreach (var section in Sections)
                {
                    writer.WriteLine($"[{section.Title}]");
                    foreach (var task in section.Tasks)
                    {
                        writer.WriteLine($"{task.Title}*{task.IsCompleted}*{task.Date}");
                        foreach (var underTask in task.UnderTask)
                        {
                            writer.WriteLine($"<{underTask.Title}*{underTask.IsCompleted}*{underTask.Date}>");
                        }
                    }
                }
            }
        }

        public void LoadSections()
        {
            string userFileName = $"{Login}_tasks.txt";

            if (File.Exists(userFileName))
            {
                string[] lines = File.ReadAllLines(userFileName);

                string currentSection = null;
                Task currentTask = null;

                foreach (var line in lines)
                {
                    if (line.StartsWith("[") && line.EndsWith("]")) // проверка, является ли строка разделом
                    {
                        currentSection = line.Substring(1, line.Length - 2);
                        CreateSection(currentSection);
                    }
                    else if (currentSection != null)
                    {
                        if (line.StartsWith("<") && line.EndsWith(">")) // проверка, является ли строка подзадачей
                        {
                            string underTask = line.Substring(1, line.Length - 2);
                            string[] parts = underTask.Split('*');
                            if (currentTask != null)
                            {
                                if (parts.Length == 3)
                                {
                                    Task cur_task = new Task(parts[0], bool.Parse(parts[1]), DateTime.Parse(parts[2]));
                                    currentTask.UnderTask.Add(cur_task);
                                }
                            }
                        }
                        else
                        {
                            string[] parts = line.Split('*');
                            if (parts.Length == 3)
                            {
                                Task task = new Task(parts[0], bool.Parse(parts[1]), DateTime.Parse(parts[2]));
                                AddTaskToSection(currentSection, task);
                                currentTask = task;
                            }
                        }
                    }
                }
            }
        }
        public void AddUnderTaskToTask(int i, int g, string underTaskTitle)
        {
            if (Sections.Count >= i)
            {
                foreach (var section in Sections)
                {
                    if (section.Tasks.Count >= g)
                    {
                        foreach (var task in section.Tasks)
                        {
                            if (section == Sections[i - 1] && task == section.Tasks[g - 1])
                            {
                                task.UnderTask.Add(new Task(underTaskTitle));
                                Console.WriteLine("Подзадача добавлена успешно!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверно введен номер задачи");
                        Thread.Sleep(2000);
                    }

                }
            }
            else
            {
                Console.WriteLine("Неверно введен номер раздела");
                Thread.Sleep(2000);
            }
            
        }

        public void DoingTask(int number_section, int num_task)
        {
            if (Sections.Count >= number_section)
            {
                foreach (var task in Sections[number_section - 1].Tasks)
                {
                    if (Sections[number_section - 1].Tasks.Count >= num_task)
                    {
                        if (task == Sections[number_section - 1].Tasks[num_task - 1])
                        {
                            task.IsCompleted = true;
                            Console.WriteLine($"Задача {Sections[number_section - 1].Tasks[num_task - 1].Title} выполена!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверно введен номер задачи");
                        Thread.Sleep(2000);
                    }
                }
            }
            else
            {
                Console.WriteLine("Неверно введен номер раздела");
                Thread.Sleep(2000);
            }
            
        }

        public void DoingUnderTask(string Under_task_title) //Метод для отметки сделанных задач
        {
            foreach (var section in Sections)
            {
                foreach (var task in section.Tasks)
                {
                    foreach (var under_tasks in task.UnderTask)
                    {
                        if (under_tasks.Title == Under_task_title)
                        {
                            under_tasks.IsCompleted = true;
                        }
                    }
                }
            }
        }
        public void NewDate(int number_section, int dedlayn_number, string Date)
        {
            if (Sections.Count >= number_section)
            {
                foreach (var section in Sections)
                {
                    foreach (var task in section.Tasks)
                    {
                        if (section.Tasks.Count >= dedlayn_number)
                        {
                            if (task == Sections[number_section - 1].Tasks[dedlayn_number - 1])
                            {
                                if (DateTime.TryParse(Date, out DateTime dueDate) && dueDate >= DateTime.Today.AddDays(-1))
                                {
                                    task.Date = dueDate;
                                    Console.WriteLine($"Дедлайн к задаче успешно установлен");
                                }
                                else
                                {
                                    Console.WriteLine("Нельзя ввести прошедшую дату.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверно введен номер задачи");
                            Thread.Sleep(2000);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Неверно введен номер раздела");
                Thread.Sleep(2000);
            }
        }

        public void DisplayTodayTasks(RegularUser user)
        {
            foreach (var section in user.Sections)
            {
                foreach (var task in section.Tasks)
                {
                    if (task.Date.Date == DateTime.Today)
                    {
                        Console.WriteLine("Вот задачи, которые вам необходимо выполнить сегодня!");
                        Console.WriteLine($"{section.Title} - {task.Title}");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
