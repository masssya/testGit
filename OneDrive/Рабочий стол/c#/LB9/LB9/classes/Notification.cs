namespace LB9.classes
{
    abstract public class Notification : IComparable<Notification>
    {
        public string Message { get; set; } // поле сообщение - общее для всех уведомлений

        public Notification(string message)
        {
            Message = message;
        }
        public int LenMassage() // функция для вывода длины сообщения
        {
            return Message.Length;
        }

        public int CompareTo(Notification? notification)
        {
            return Message.CompareTo(notification.Message);
        }
        public abstract void TextOfMessage();

    }
}
