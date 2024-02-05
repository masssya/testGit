using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2.classes
{
    class TaskSection // Класс с разными разделами для задач
    {
        public string Title { get; set; }
        public List<Task> Tasks { get; set; }

        public TaskSection(string title)
        {
            Title = title;
            Tasks = new List<Task>();
        }
    }
}
