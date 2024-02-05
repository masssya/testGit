namespace LB9.classes
{
    public class EmailNotification : Notification, IComparable<EmailNotification>
    {
        private string emailadres;
        public string EmailAdres
        {
            get { return emailadres; }
            set
            {
                if (value.Length != 0)
                {
                    emailadres = value;
                }
                else
                {
                    Console.WriteLine("Введите почту, поле не может оставаться пустым");
                    emailadres = "example@mail.ru";
                }
            }
        }

        public string Region() // функция для определения региона
        {
            if (EmailAdres.Substring(EmailAdres.Length - 3) == "com")
            {
                return "Это американский адрес";
            }
            if (EmailAdres.Substring(EmailAdres.Length - 3) == "org")
            {
                return "Это адрес организации";
            }
            if (EmailAdres.Substring(EmailAdres.Length - 2) == "ru")
            {
                return "Это русский адрес";
            }
            else
            {
                return "Не удалось опеределить регион";
            }
        }

        public EmailNotification(string emailadres, string message) : base(message)
        {
            EmailAdres = emailadres;
        }

        public int CompareTo(EmailNotification notification)
        {
            return Message.CompareTo(notification.Message);
        }
        public override void TextOfMessage()
        {
            Console.WriteLine($"Вам пришло сообщение с адреса {EmailAdres}: {Message}.");
        }
    }
}
