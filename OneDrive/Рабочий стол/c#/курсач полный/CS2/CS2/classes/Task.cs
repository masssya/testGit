using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2.classes
{
    class Task // класс для задач, он же главный для подзадач
    {
        public string Title { get; set; } // Название задачи (оно же описание, от нее будет наследоваться подзадача). 
        public bool IsCompleted { get; set; } // 0 or 1 - выполнена или нет
        public DateTime Date { get; set; }
        public List<Task> UnderTask { get; set; }

        public Task(string title)
        {
            Title = title;
            IsCompleted = false;
            Date = DateTime.MinValue;
            UnderTask = new List<Task>();
        }

        public Task(string title, bool is_completed, DateTime date)
        {
            Title = title;
            IsCompleted = is_completed;
            Date = date;
            UnderTask = new List<Task>();
        }
    }
}
