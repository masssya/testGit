using LB9.classes;
class Program
{
    static void Main(string[] args)
    {
        var Email1 = new EmailNotification("anastasiya@mail.ru", "Консультация переносится на пятницу");
        var Email2 = new EmailNotification("trening@gmail.com", "Ждем вас в субботу на семинаре!");

        var SMS1 = new SMSNotification("+79183456787", "Запиши уже наконец мой номер...");
        var SMS2 = new SMSNotification("+79281113456", "Скидка последние 3 дня на абонемент в PowerHouse");

        var Push1 = new PushNotification("Вам пришло 3 новых сообщения", "VK");
        var Push2 = new PushNotification("Клан ждет тебя, возвращайся в битву!", "Clash Of Clans");

        var EmailContainer = new NotificationContainer<EmailNotification>();
        var SMSContainer = new NotificationContainer<SMSNotification>();
        var PushContainer = new NotificationContainer<PushNotification>();

        EmailContainer.AddNotifications(Email1);
        EmailContainer.AddNotifications(Email2);
        SMSContainer.AddNotifications(SMS1);
        SMSContainer.AddNotifications(SMS2);
        PushContainer.AddNotifications(Push1);
        PushContainer.AddNotifications(Push2);

        Console.WriteLine("Элементы Email контейнера:");
        EmailContainer.ElementsFromContainer();
        Console.WriteLine("Элементы SMS контейнера:");
        SMSContainer.ElementsFromContainer();
        Console.WriteLine("Элементы Push контейнера:");
        PushContainer.ElementsFromContainer();

        SMS2.TextOfMessage();
        SMS2.Spam();

        Push1.TextOfMessage();
        Push1.HideAll();
        Push1.TextOfMessageHide();

        Console.WriteLine("Сортировка:");
        SMSContainer.SortContainer();

        Email2.TextOfMessage();
        Email2.Region();

        Console.WriteLine("CompareTo:");
        Console.WriteLine(SMS2.CompareTo(SMS1));

        var NewContainer = new NotificationContainer<Notification>();
        NewContainer.AddNotifications(SMS1);
        NewContainer.AddNotifications(Push1);
        NewContainer.AddNotifications(Email1);
        NewContainer.ElementsFromContainer();
    }
}