using CS2.classes;
class Program
{
    public static string RootPass = "root";


    static void Main(string[] args)
    {
        EnsureFileExists();
        List<RegularUser> users = RegularUser.LoadUsers();

        Console.WriteLine("Добрый день, это приложение заметки.");
        Console.WriteLine("Если хотите войти, нажмите Enter, если зарегистрироваться - введите любое слово:");

        //SlowWrite("Добрый день, это приложение заметки.");
        //SlowWrite("Если хотите войти, нажмите Enter, если зарегистрироваться - введите любое слово:");

        string input = Console.ReadLine();

        Input(users, input); // вызов функции для регистрации


    }

    public static void Input(List<RegularUser> users, string input) //функция для регистрации пользователя
    {
        bool output = true;
        while (output)
        {
            if (input == "")
            {
                Console.Clear();
                Console.WriteLine("Введите ник пользователя:");
                string name = Console.ReadLine();
                Console.WriteLine("Введите пароль пользователя:");
                string pass = Console.ReadLine();

                bool loginSuccess = false;
                foreach (var user0 in users) //проверка для админов 
                {
                    if (user0.Login == name && user0.Password == pass)
                    {
                        loginSuccess = true;
                        Console.WriteLine("Вход успешен!");
                        if (user0.IsAdmin == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Вы вошли как администратор.");
                            Admin user1 = new Admin()
                            {
                                Login = user0.Login,
                                Password = pass
                            };
                            RecurringFunctionAdmin(user1); 
                            output = false;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Вы вошли как обычный пользователь.");
                            RecurringFunction(user0); 
                            output = false;
                        }
                        
                        break;
                    }
                }

                if (!loginSuccess)
                {
                    Console.Clear();
                    Console.WriteLine("Ошибка входа. Попробуйте еще раз.");
                    
                }
            }
            else
            {
                Console.WriteLine("Придумайте ник пользователя:");
                string name = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Придумайте пароль пользователя:");
                string pass = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Вы хотите зарегистрироваться с правами администратора? Y - да, N - нет.");
                string admin = Console.ReadLine();
                Console.Clear();
                if (admin == "Y")
                {
                    Console.WriteLine("Введите общий пароль для администраторов:");
                    string admin_pass = Console.ReadLine();
                    Console.Clear();
                    if (admin_pass == RootPass)
                    {
                        Admin users1 = new Admin
                        {
                            Login = name,
                            Password = pass,
                            IsAdmin = 1
                        };
                        
                        users.Add(users1);
                        Admin.SaveUsers(users);
                        Admin user = new Admin
                        {
                            Login = name
                        };
                        user.SaveSections();
                        user.LoadSections();
                        Console.WriteLine("Вы успешно зарегистрированы в системе с правами админа!");
                        RecurringFunctionAdmin(users1); 
                        output = false;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Отказано в доступе, хотите зарегистрироваться как обычный пользователь? Y - да, N - нет.");
                        string y_n = Console.ReadLine();
                        if (y_n == "Y")
                        {
                            Admin users1 = new Admin
                            {
                                Login = name,
                                Password = pass,
                                IsAdmin = 0
                            };

                            users.Add(users1);
                            Admin.SaveUsers(users);
                            Console.Clear();
                            Admin user = new Admin();
                            user.SaveSections();
                            Console.WriteLine("Вы успешно зарегистрированы в системе!");
                            RecurringFunctionAdmin(users1); 
                            output = false;
                        }
                        else
                        {
                            Console.WriteLine("Введите пароль для администраторов заново:");
                            string new_root_pass = Console.ReadLine();
                            Console.Clear();
                            if (new_root_pass == RootPass)
                            {
                                Admin users1 = new Admin
                                {
                                    Login = name,
                                    Password = pass,
                                    IsAdmin = 1
                                };

                                users.Add(users1);
                                Admin.SaveUsers(users);
                                Console.Clear();
                                Admin user = new Admin();
                                user.SaveSections();
                                Console.WriteLine("Вы успешно зарегистрированы в системе с правами админа!");
                                RecurringFunctionAdmin(users1); 
                                output = false;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Попробуйте еще раз.");
                            }
                        }
                    }
                }
                else if (admin == "N")
                {
                    RegularUser users1 = new RegularUser
                    {
                        Login = name,
                        Password = pass,
                        IsAdmin = 0
                    };

                    users.Add(users1);
                    RegularUser.SaveUsers(users);
                    RegularUser user = new RegularUser
                    {
                        Login = name
                    };
                    user.SaveSections();
                    user.LoadSections();
                    Console.WriteLine("Вы успешно зарегистрированы в системе с правами обычного пользователя!");
                    RecurringFunction(users1); 
                    output = false;
                }
            }
        }
    }

    public static void RecurringFunction(RegularUser user) // основная функция для обычных пользователей
    {
        user.LoadSections();
        EnsureFileExists();
        int number_section = 1;
        int number = 0;

        while (true)
        {
            Console.WriteLine("Вот задачи, которые вам необходимо выполнить сегодня!");
            user.DisplayTodayTasks(user);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Текущий раздел: {user.Sections[number_section - 1].Title}"); // Сначала будет выводиться первый раздел, потом переменная будет обновляться

            DisplayTasks(user, number_section);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Добавить подзадачу");
            Console.WriteLine("3. Создать новый раздел");
            Console.WriteLine("4. Выполнить задачу");
            Console.WriteLine("5. Выполнить подзадачу");
            Console.WriteLine("6. Ввести или изменить время выполенения задачи");
            Console.WriteLine("7. Сменить раздел и показать задачи");
            Console.WriteLine("8. Вывести все задачи на сегодняшний день");
            Console.WriteLine("9. Выйти из программы");

            number = int.Parse(Console.ReadLine()); // выбранный номер действия

            switch (number)
            {
                case 1:
                    Console.Clear();
                    DisplaySections(user);
                    Console.WriteLine("Введите номер раздела, в который хотите добавить задачу.");
                    int i = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите название задачи.");
                    string title = Console.ReadLine(); 
                    user.AddTaskSection(i, new CS2.classes.Task(title));
                    user.SaveSections();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    DisplaySections(user);
                    Console.WriteLine("Введите номер раздела:"); 
                    int Big_title = int.Parse(Console.ReadLine());
                    DisplayTasks(user, Big_title);
                    Console.WriteLine("Введите номер задачи, к которой хотите добавить подзадачу:"); 
                    int parentTaskTitle = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите название подзадачи:");
                    string underTaskTitle = Console.ReadLine();
                    user.AddUnderTaskToTask(Big_title, parentTaskTitle, underTaskTitle);
                    user.SaveSections();
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Введите название нового раздела:");
                    string sectionTitle = Console.ReadLine();
                    user.CreateSection(sectionTitle);
                    Console.WriteLine($"Раздел {sectionTitle} добавлен.");
                    Thread.Sleep(50);
                    user.SaveSections();
                    Console.Clear();
                    break;
                case 4:
                    Console.Clear();
                    DisplayTasks(user, number_section);
                    Console.WriteLine("Введите номер выполненной задачи:");
                    int sectionTitle1 = int.Parse(Console.ReadLine()); // название задачи
                    user.DoingTask(number_section, sectionTitle1);
                    Thread.Sleep(50);
                    user.SaveSections();
                    Console.Clear();
                    break;
                case 5:
                    Console.WriteLine("Введите название подзадачи"); 
                    string Under_task_name = Console.ReadLine();
                    user.DoingUnderTask(Under_task_name);
                    Console.WriteLine($"Подзадача {Under_task_name} успешно выполнена");
                    user.SaveSections();
                    break;
                case 6:
                    Console.Clear();
                    DisplayTasks(user, number_section);
                    Console.WriteLine("Введите номер задачи, для которой хотите установить дедлайн:");
                    int dedlayn_number = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите время в формате дд.мм.гггг:");
                    string date = Console.ReadLine();
                    user.NewDate(number_section, dedlayn_number, date);
                    user.SaveSections();
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
                case 7:
                    Console.Clear();
                    DisplaySections(user);
                    Console.WriteLine("Введите номер раздела:");
                    int number_sec = number_section;
                    number_section = Int32.Parse(Console.ReadLine());
                    if (user.Sections.Count < number_section)
                    {
                        number_section = number_sec;
                        Console.WriteLine("Неверно введен номер раздела");
                        Thread.Sleep(2000);
                    }
                    break;
                case 8:
                    Console.Clear();
                    Console.WriteLine("Вот список задач, которые необходимо выполнить сегодня"); 
                    user.DisplayTodayTasks(user);
                    break;
                case 9:
                    Console.WriteLine("До свидания!");
                    user.SaveSections();
                    return;
                default:
                    Console.WriteLine("Введите цифру от 1 до 11");
                    break;
            }

        }
        static void DisplaySections(RegularUser user) // Функция для отображения доступных разделов
        {
            int n = 1;
            Console.WriteLine("Доступные разделы:");
            foreach (var section in user.Sections)
            {
                Console.WriteLine($"{n}. {section.Title}"); // Вывод названия раздела с номером
                n += 1;
            }
        }

        static void DisplayTasks(RegularUser user, int number_section) // Отображение задач для текущего пользователя
        {
            int count = 1;


            foreach (var section in user.Sections)
            {
                if (section == user.Sections[number_section - 1])
                {
                    foreach (var task in section.Tasks)
                    {
                        if (task.Date != DateTime.MinValue)
                        {
                            Console.WriteLine($"{count}. {task.Title} Дедлайн для задачи: {task.Date.ToString("dd.MM.yyyy")} {(task.IsCompleted ? "[X]" : "[ ]")}");
                        }
                        else
                        {
                            Console.WriteLine($"{count}. {task.Title} {(task.IsCompleted ? "[X]" : "[ ]")}");
                        }
                        
                        count += 1;
                        foreach (var underTask in task.UnderTask)
                        {
                            Console.WriteLine($"   └─ {underTask.Title} {(underTask.IsCompleted ? "[X]" : "[ ]")}");
                        }
                    }
                }
            }
        }

    }

    public static void RecurringFunctionAdmin(Admin user) // основная функция для админов
    {
        user.LoadSections();
        EnsureFileExists_admin();
        int number_section = 1;
        int number = 0;

        while (true)
        {
            //Console.WriteLine("Вот задачи, которые вам необходимо выполнить сегодня!");
            user.DisplayTodayTasks(user);

            Console.WriteLine($"Текущий раздел: {user.Sections[number_section - 1].Title}"); // Сначала будет выводиться первый раздел, потом переменная будет обновляться

            DisplayTasks(user, number_section);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Добавить подзадачу");
            Console.WriteLine("3. Создать новый раздел");
            Console.WriteLine("4. Выполнить задачу");
            Console.WriteLine("5. Выполнить подзадачу");
            Console.WriteLine("6. Ввести или изменить время выполенения задачи");
            Console.WriteLine("7. Удалить задачу"); 
            Console.WriteLine("8. Удалить подзадачу"); 
            Console.WriteLine("9. Сменить раздел и показать задачи");
            Console.WriteLine("10. Вывести все задачи на сегодняшний день");
            Console.WriteLine("11. Выйти из программы");

            number = int.Parse(Console.ReadLine()); // выбранный номер действия

            switch (number)
            {
                case 1:
                    Console.Clear();
                    DisplaySections(user);
                    Console.WriteLine("Введите номер раздела, в который хотите добавить задачу.");
                    //int i = int.Parse(Console.ReadLine());
                    string i1 = Console.ReadLine();
                    Console.WriteLine("Введите название задачи.");
                    string title = Console.ReadLine();
                    if (int.TryParse(i1, out int result))
                    {
                        user.AddTaskSection(result, new CS2.classes.Task(title));
                        user.SaveSections();
                    }
                    else
                    {
                        Console.WriteLine("Введенное значение должно быть числом");
                    }
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    DisplaySections(user);
                    Console.WriteLine("Введите номер раздела:"); 
                    string Big_title = Console.ReadLine();
                    if (int.TryParse(Big_title, out int result2))
                    {
                        DisplayTasks(user, result2);
                    }
                    Console.WriteLine("Введите номер задачи, к которой хотите добавить подзадачу:"); 
                    string parentTaskTitle = Console.ReadLine();
                    Console.WriteLine("Введите название подзадачи:");
                    string underTaskTitle = Console.ReadLine();
                    if (int.TryParse(parentTaskTitle, out int result3))
                    {
                        user.AddUnderTaskToTask(result2, result3, underTaskTitle);
                        user.SaveSections();
                    }
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Введите название нового раздела:");
                    string sectionTitle = Console.ReadLine();
                    user.CreateSection(sectionTitle);
                    Console.WriteLine($"Раздел {sectionTitle} добавлен.");
                    Thread.Sleep(50);
                    user.SaveSections();
                    Console.Clear();
                    break;
                case 4:
                    Console.Clear();
                    DisplayTasks(user, number_section);
                    Console.WriteLine("Введите номер выполненной задачи:");
                    string sectionTitle1 = Console.ReadLine(); // название задачи
                    if (int.TryParse(sectionTitle1, out int result4))
                    {
                        user.DoingTask(number_section, result4);
                        user.SaveSections();
                    }
                    
                    Console.Clear();
                    break;
                case 5:
                    Console.WriteLine("Введите название подзадачи");
                    string Under_task_name = Console.ReadLine();
                    user.DoingUnderTask(Under_task_name);
                    Console.WriteLine($"Подзадача {Under_task_name} успешно выполнена");
                    user.SaveSections();
                    break;
                case 6:
                    Console.Clear();
                    DisplayTasks(user, number_section);
                    Console.WriteLine("Введите номер задачи, для которой хотите установить дедлайн:");
                    string dedlayn_number = Console.ReadLine();
                    string date = "";
                    if (int.TryParse(dedlayn_number, out int result5))
                    {
                        Console.WriteLine("Введите время в формате дд.мм.гггг:");
                        date = Console.ReadLine();
                        user.NewDate(number_section, result5, date);
                        user.SaveSections();
                    }
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
                case 7:
                    Console.Clear();
                    DisplaySections(user);
                    Console.WriteLine("Введите номер раздела, из которого хотите удалить задачу:");
                    string big_title = Console.ReadLine();
                    if (int.TryParse(big_title, out int result6))
                    {
                        DisplayTasks(user, result6);
                        Console.WriteLine("Введите номер задачи, которую хотите удалить:");
                        string taskTitle = Console.ReadLine();
                        if (int.TryParse(taskTitle, out int result10))
                        {
                            user.RemoveTaskSection(result6, result10);
                        }
                    }
                    break;
                case 8:
                    Console.Clear();
                    DisplaySections(user);
                    Console.WriteLine("Введите номер раздела, из которого хотите удалить задачу:"); 
                    string biig_title = Console.ReadLine();
                    if (int.TryParse(biig_title, out int result7))
                    {
                        DisplayTasks(user, result7);
                        Console.WriteLine("Введите номер задачи, которую хотите удалить:");
                        string taaaskTitle = Console.ReadLine();
                        if (int.TryParse(biig_title, out int result8))
                        {
                            Console.WriteLine("Введите название подзадачи:");
                            string taaask_under_Title = Console.ReadLine();
                            user.RemoveUnderTaskSection(result7, result8, taaask_under_Title);
                        }
                    }
                    break;
                case 9:
                    Console.Clear();
                    DisplaySections(user);
                    Console.WriteLine("Введите номер раздела:");
                    int number_sec = number_section;
                    string number_section1 = Console.ReadLine();
                    number_section = Int32.Parse(number_section1);
                    if (int.TryParse(number_section1, out int result9) && user.Sections.Count < number_section)
                    {
                        number_section = number_sec;
                        Console.WriteLine("Неверно введен номер раздела");
                        Thread.Sleep(2000);
                    }
                    break;
                case 10:
                    Console.Clear();
                    Console.WriteLine("Вот список задач, которые необходимо выполнить сегодня"); 
                    user.DisplayTodayTasks(user);
                    break;
                case 11:
                    Console.WriteLine("До свидания!");
                    user.SaveSections();
                    return;
                default:
                    Console.WriteLine("Введите цифру от 1 до 11");
                    break;
            }

        }
        static void DisplaySections(Admin user) // Функция для отображения доступных разделов для пользователя с уровнем доступа 1 - администратор
        {
            int n = 1;
            Console.WriteLine("Доступные разделы:");
            foreach (var section in user.Sections)
            {
                Console.WriteLine($"{n}. {section.Title}"); // Вывод названия раздела с номером
                n += 1;
            }
        }

        static void DisplayTasks(Admin user, int number_section) // Отображение задач для текущего пользователя - админа
        {
            int count = 1;

            if (user.Sections.Count >= number_section)
            {
                foreach (var section in user.Sections)
                {
                    if (section == user.Sections[number_section - 1])
                    {
                        foreach (var task in section.Tasks)
                        {
                            if (task.Date != DateTime.MinValue)
                            {
                                Console.WriteLine($"{count}. {task.Title} Дедлайн для задачи: {task.Date.ToString("dd.MM.yyyy")} {(task.IsCompleted ? "[X]" : "[ ]")}");
                            }
                            else
                            {
                                Console.WriteLine($"{count}. {task.Title} {(task.IsCompleted ? "[X]" : "[ ]")}");
                            }

                            count += 1;
                            foreach (var underTask in task.UnderTask)
                            {
                                Console.WriteLine($"   └─ {underTask.Title} {(underTask.IsCompleted ? "[X]" : "[ ]")}");
                            }
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

    }

    public static void EnsureFileExists() //создание файла, если его нет для уровня 0 - обычный пользователь
    {
        if (!File.Exists(RegularUser.fileName))
        {
            File.Create(RegularUser.fileName).Close();
        }
    }

    public static void EnsureFileExists_admin() //создание файла, если его нет для уровня 1 - админ
    {
        if (!File.Exists(Admin.fileName))
        {
            File.Create(Admin.fileName).Close();
        }
    }


    public static void SlowWrite(string text)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(50);
        }
        Console.WriteLine();
    }
}
