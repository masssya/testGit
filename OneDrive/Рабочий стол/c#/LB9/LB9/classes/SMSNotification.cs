namespace LB9.classes
{
    public class SMSNotification : Notification, IComparable<SMSNotification>
    {
        private string number;

        public bool SpamMessage = false;

        public string Number
        {
            get { return number; }
            set
            {
                if (value.Length >= 11)
                {
                    number = value;
                }
                else
                {
                    Console.WriteLine("Номер введен некорректно");
                    number = "";
                }
            }
        }

        public SMSNotification(string number, string message) : base(message)
        {
            Number = number;
        }

        public override void TextOfMessage()
        {
            Console.WriteLine($"Вам пришло сообщение с номера {Number}: {Message}.");
        }


        public int CompareTo(SMSNotification notification)
        {
            return Message.CompareTo(notification.Message);
        }
        public void Spam()
        {
            Console.WriteLine("Хотите добавить сообщение в раздел спам? Введите Y или N");
            string IsSpam = Console.ReadLine();
            if (IsSpam == "Y" || IsSpam == "y")
            {
                SpamMessage = true;
                Console.WriteLine("Сообщение занесено в спам.");
            }
            else if (IsSpam == "N" || IsSpam == "n")
            {
                Console.WriteLine("Сообщение остается в безопасном разделе.");
            }
            else
            {
                Console.WriteLine("Команда введена неверно.");
            }
        }

    }
}
