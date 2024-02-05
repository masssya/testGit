namespace LB9.classes
{
    public class PushNotification : Notification, IComparable<PushNotification>
    {
        public string App { get; set; }
        public string HideMessage { get; set; }

        public PushNotification(string message, string app) : base(message)
        {
            App = app;
        }

        public override void TextOfMessage()
        {
            Console.WriteLine($"Вам пришло сообщение от приложения {App}: {Message}.");
        }

        public void TextOfMessageHide()
        {
            Console.WriteLine($"Вам пришло сообщение от приложения {App}: {HideMessage}.");
        }

        public int CompareTo(PushNotification notification)
        {
            return Message.CompareTo(notification.Message);
        }
        public void HideAll()
        {
            Console.WriteLine("Хотите скрыть сообщения на главном экране? Текст не будет виден на заблокированном экране. Y - да, N - нет");
            string IsHide = Console.ReadLine();
            if (IsHide == "Y" || IsHide == "y")
            {
                Console.WriteLine("Данное сообщение будет скрыто");
                HideMessage = "Сообщение скрыто, разблокируйте устройство.";
            }
            else if (IsHide == "N" || IsHide == "n")
            {
                Console.WriteLine("Данное сообщение останется открытым");
                HideMessage = Message;
            }
            else
            {
                Console.WriteLine("Команда введена неверно");
            }
        }
    }
}
