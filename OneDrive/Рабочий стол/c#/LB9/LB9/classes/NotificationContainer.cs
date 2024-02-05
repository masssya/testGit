namespace LB9.classes
{
    public class NotificationContainer<T> where T : Notification, IComparable<T>
    {
        private List<T> Notifications = new List<T>();

        public NotificationContainer()
        {
            Notifications = new List<T>();
        }
        public void AddNotifications(T notification)
        {
            Notifications.Add(notification);
        }

        public void ElementsFromContainer()
        {
            foreach (var elem in Notifications)
            {
                elem.TextOfMessage();
            }
        }

        public bool IsThis(T notification)
        {
            return Notifications.Contains(notification);
        }

        public void SortContainer()
        {
            Notifications.Sort();
        }
    }
}
