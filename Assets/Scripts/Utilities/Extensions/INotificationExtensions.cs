using Object = System.Object;
using Handler = System.Action<System.Object, System.Object>;
using Bachelor.Managers;

namespace Bachelor.MyExtensions.Managers
{
    public static class INotificationExtensions
    {
        public static void AddObserver(this INotification obj, Handler handler, string notificationName, Object sender)
        {
            NotificationCenter.Instance.AddObserver(handler, notificationName, sender);
        }

        public static void AddObserver(this INotification obj, Handler handler, string notificationName)
        {
            NotificationCenter.Instance.AddObserver(handler, notificationName);
        }

        public static void RemoveObserver(this INotification obj, Handler handler, string notificationName, Object sender)
        {
            NotificationCenter.Instance.RemoveObserver(handler, notificationName, sender);
        }

        public static void RemoveObserver(this INotification obj, Handler handler, string notificationName)
        {
            NotificationCenter.Instance.RemoveObserver(handler, notificationName);
        }

        public static void PostNotification(this INotification obj, string notificationName, Object target)
        {
            NotificationCenter.Instance.PostNotification(notificationName, obj, target);
        }

        public static void PostNotification(this INotification obj, string notificationName)
        {
            NotificationCenter.Instance.PostNotification(notificationName, obj);
        }
    }
}