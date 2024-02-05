using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2.classes
{
    class Admin : RegularUser // У админа будет на пару опций больше, чем у обычного пользователя
    {
        public void RemoveTaskSection(int i, int g) // Метод для удаления задачи из раздела
        {

            if (i > 0 && i <= Sections.Count)
            {
                var section = Sections[i - 1];

                if (g > 0 && g <= section.Tasks.Count)
                {
                    section.Tasks.RemoveAt(g - 1);
                    Console.WriteLine($"Задача успешно удалена из раздела.");
                }
                else
                {
                    Console.WriteLine($"Неверный номер задачи в разделе.");
                }
            }
            else
            {
                Console.WriteLine($"Неверный номер раздела.");
            }
        }

        public void RemoveUnderTaskSection(int i, int g, string underTaskTitle) // Метод для удаления подзадач из раздела
        {
            if (i > 0 && i <= Sections.Count)
            {
                var section = Sections[i - 1];

                if (g > 0 && g <= section.Tasks.Count)
                {
                    var task = section.Tasks[g - 1];

                    var underTaskToRemove = task.UnderTask.Find(ut => ut.Title == underTaskTitle);

                    if (underTaskToRemove != null)
                    {
                        task.UnderTask.Remove(underTaskToRemove);
                        Console.WriteLine($"Подзадача '{underTaskTitle}' успешно удалена из задачи в разделе.");
                    }
                    else
                    {
                        Console.WriteLine($"Подзадача '{underTaskTitle}' не найдена в указанной задаче в разделе.");
                    }
                }
                else
                {
                    Console.WriteLine($"Неверный номер задачи в разделе.");
                }
            }
            else
            {
                Console.WriteLine($"Неверный номер раздела.");
            }
        }
    }
}
