using System;
using System.Collections.Generic;
using System.IO;

class TaskManager
{
    static List<TaskItem> tasks = new List<TaskItem>();

    static void Main()
    {
        Console.WriteLine("Добро пожаловать в консольный менеджер задач!");

        LoadTasks(); // Загрузка задач из файла

        while (true)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Просмотреть задачи");
            Console.WriteLine("2. Добавить задачу");
            Console.WriteLine("3. Отметить задачу как выполненную");
            Console.WriteLine("4. Редактировать задачу");
            Console.WriteLine("5. Установить срок выполнения для задачи");
            Console.WriteLine("6. Добавить подзадачу");
            Console.WriteLine("7. Сохранить задачи в файл");
            Console.WriteLine("8. Выйти");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Введите корректное число.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    DisplayTasks();
                    break;
                case 2:
                    AddTask();
                    break;
                case 3:
                    MarkTaskAsCompleted();
                    break;
                case 4:
                    EditTask();
                    break;
                case 5:
                    SetDueDate();
                    break;
                case 6:
                    AddSubtask();
                    break;
                case 7:
                    SaveTasks(); // Сохранение задач в файл
                    break;
                case 8:
                    Console.WriteLine("До свидания!");
                    return;
                default:
                    Console.WriteLine("Введите число от 1 до 8.");
                    break;
            }
        }
    }

    static void DisplayTasks()
    {
        Console.WriteLine("\nСписок задач:");

        if (tasks.Count == 0)
        {
            Console.WriteLine("Нет задач.");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                TaskItem task = tasks[i];
                string status = task.IsCompleted ? " [X]" : " [ ]";
                Console.WriteLine($"{i + 1}. {task.Description}{status} (Оставшееся время: {GetRemainingTime(task)})");

                if (task.Subtasks.Count > 0)
                {
                    Console.WriteLine("   Подзадачи:");
                    for (int j = 0; j < task.Subtasks.Count; j++)
                    {
                        TaskItem subtask = task.Subtasks[j];
                        string subtaskStatus = subtask.IsCompleted ? " [X]" : " [ ]";
                        Console.WriteLine($"   - {subtask.Description}{subtaskStatus} (Оставшееся время: {GetRemainingTime(subtask)})");
                    }
                }
            }
        }
    }

    static void AddTask()
    {
        Console.WriteLine("\nВведите новую задачу:");
        string newTask = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(newTask))
        {
            tasks.Add(new TaskItem(newTask));
            Console.WriteLine("Задача добавлена успешно!");
        }
        else
        {
            Console.WriteLine("Пустая задача не может быть добавлена.");
        }
    }

    static void AddSubtask()
    {
        DisplayTasks();

        if (tasks.Count == 0)
        {
            Console.WriteLine("Нет задач для добавления подзадач.");
            return;
        }

        Console.WriteLine("\nВведите номер задачи, для которой нужно добавить подзадачу:");
        if (!int.TryParse(Console.ReadLine(), out int taskNumber) || taskNumber < 1 || taskNumber > tasks.Count)
        {
            Console.WriteLine("Введите корректный номер задачи.");
            return;
        }

        Console.WriteLine("Введите новую подзадачу:");
        string newSubtask = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(newSubtask))
        {
            tasks[taskNumber - 1].Subtasks.Add(new TaskItem(newSubtask));
            Console.WriteLine("Подзадача добавлена успешно!");
        }
        else
        {
            Console.WriteLine("Пустая подзадача не может быть добавлена.");
        }
    }

    static void MarkTaskAsCompleted()
    {
        DisplayTasks();

        if (tasks.Count == 0)
        {
            Console.WriteLine("Нет задач для отметки выполненными.");
            return;
        }

        Console.WriteLine("\nВведите номер задачи, которую вы хотите отметить как выполненную:");
        if (!int.TryParse(Console.ReadLine(), out int taskNumber) || taskNumber < 1 || taskNumber > tasks.Count)
        {
            Console.WriteLine("Введите корректный номер задачи.");
            return;
        }

        TaskItem completedTask = tasks[taskNumber - 1];
        completedTask.IsCompleted = true;

        Console.WriteLine($"Задача \"{completedTask}\" отмечена как выполненная.");
    }

    static void EditTask()
    {
        DisplayTasks();

        if (tasks.Count == 0)
        {
            Console.WriteLine("Нет задач для редактирования.");
            return;
        }

        Console.WriteLine("\nВведите номер задачи, которую вы хотите отредактировать:");
        if (!int.TryParse(Console.ReadLine(), out int taskNumber) || taskNumber < 1 || taskNumber > tasks.Count)
        {
            Console.WriteLine("Введите корректный номер задачи.");
            return;
        }

        Console.WriteLine("Введите новое описание задачи:");
        string newDescription = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(newDescription))
        {
            tasks[taskNumber - 1].Description = newDescription;
            Console.WriteLine("Задача успешно отредактирована.");
        }
        else
        {
            Console.WriteLine("Пустое описание задачи не допускается.");
        }
    }

    static void SetDueDate()
    {
        DisplayTasks();

        if (tasks.Count == 0)
        {
            Console.WriteLine("Нет задач для установки срока выполнения.");
            return;
        }

        Console.WriteLine("\nВведите номер задачи, для которой нужно установить срок выполнения:");
        if (!int.TryParse(Console.ReadLine(), out int taskNumber) || taskNumber < 1 || taskNumber > tasks.Count)
        {
            Console.WriteLine("Введите корректный номер задачи.");
            return;
        }

        Console.WriteLine("Введите срок выполнения (в формате ГГГГ-ММ-ДД):");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
        {
            tasks[taskNumber - 1].DueDate = dueDate;
            Console.WriteLine($"Срок выполнения для задачи \"{tasks[taskNumber - 1]}\" успешно установлен.");
        }
        else
        {
            Console.WriteLine("Некорректный формат даты.");
        }
    }

    static void SaveTasks()
    {
        Console.WriteLine("Введите имя файла для сохранения задач:");
        string fileName = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var task in tasks)
                {
                    SaveTask(writer, task);
                }
            }

            Console.WriteLine($"Задачи успешно сохранены в файл {fileName}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении задач: {ex.Message}");
        }
    }

    static void SaveTask(StreamWriter writer, TaskItem task)
    {
        writer.WriteLine($"{task.Description},{task.IsCompleted},{task.DueDate}");

        foreach (var subtask in task.Subtasks)
        {
            SaveTask(writer, subtask);
        }
    }

    static void LoadTasks()
    {
        Console.WriteLine("Введите имя файла для загрузки задач:");
        string fileName = Console.ReadLine();

        if (File.Exists(fileName))
        {
            try
            {
                tasks.Clear();

                using (StreamReader reader = new StreamReader(fileName))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] taskData = reader.ReadLine().Split(',');

                        if (taskData.Length == 3 &&
                            !string.IsNullOrWhiteSpace(taskData[0]) &&
                            bool.TryParse(taskData[1], out bool isCompleted) &&
                            DateTime.TryParse(taskData[2], out DateTime dueDate))
                        {
                            TaskItem task = new TaskItem(taskData[0], isCompleted, dueDate);
                            LoadSubtasks(reader, task);
                            tasks.Add(task);
                        }
                    }
                }

                Console.WriteLine($"Задачи успешно загружены из файла {fileName}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке задач: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Файл не существует. Создан новый список задач.");
        }
    }

    static void LoadSubtasks(StreamReader reader, TaskItem parentTask)
    {
        while (!reader.EndOfStream)
        {
            string[] taskData = reader.ReadLine().Split(',');

            if (taskData.Length == 3 &&
                !string.IsNullOrWhiteSpace(taskData[0]) &&
                bool.TryParse(taskData[1], out bool isCompleted) &&
                DateTime.TryParse(taskData[2], out DateTime dueDate))
            {
                TaskItem subtask = new TaskItem(taskData[0], isCompleted, dueDate);
                LoadSubtasks(reader, subtask);
                parentTask.Subtasks.Add(subtask);
            }
            else
            {
                break;
            }
        }
    }

    static string GetRemainingTime(TaskItem task)
    {
        if (task.DueDate != DateTime.MinValue)
        {
            TimeSpan remainingTime = task.DueDate - DateTime.Now;
            return remainingTime.ToString(@"dd\.hh\:mm\:ss");
        }

        return "N/A";
    }
}

class TaskItem
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }
    public List<TaskItem> Subtasks { get; set; }

    public TaskItem(string description)
    {
        Description = description;
        IsCompleted = false;
        DueDate = DateTime.MinValue;
        Subtasks = new List<TaskItem>();
    }

    public TaskItem(string description, bool isCompleted, DateTime dueDate)
    {
        Description = description;
        IsCompleted = isCompleted;
        DueDate = dueDate;
        Subtasks = new List<TaskItem>();
    }

    public override string ToString()
    {
        string status = IsCompleted ? "[X]" : "[ ]";
        string dueDateStr = (DueDate != DateTime.MinValue) ? DueDate.ToString("yyyy-MM-dd") : "N/A";

        return $"{status} {Description}{(IsCompleted ? " ❤" : "")} (Due: {dueDateStr})";
    }
}
