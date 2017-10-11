using UnityEngine;
using Bachelor.Utilities;
using System.Collections.Generic;
using Object = System.Object;
using Handler = System.Action<System.Object, System.Object>;
using SenderTable = System.Collections.Generic.Dictionary<
    System.Object,
    System.Collections.Generic.List<System.Action<
        System.Object,
        System.Object>
    >
>;

namespace Bachelor.Managers
{
    public class NotificationCenter : SingletonMonoBehaviourPersistent<NotificationCenter>
    {
        private Dictionary<string, SenderTable> m_NotificationTable = new Dictionary<string, SenderTable>();
        private HashSet<List<Handler>> m_Invoking = new HashSet<List<Handler>>();

        private NotificationCenter() { }

        public void AddObserver(Handler handler, string notificationName, Object sender)
        {
            if (handler == null)
            {
                Debug.LogError("Can't add a null event handler for notification, " + notificationName);
                return;
            }

            if (string.IsNullOrEmpty(notificationName))
            {
                Debug.LogError("Can't observe an unnamed notification");
                return;
            }

            if (!m_NotificationTable.ContainsKey(notificationName))
            {
                m_NotificationTable.Add(notificationName, new SenderTable());
            }

            SenderTable subTable = m_NotificationTable[notificationName];
            Object key = sender ?? (this);

            if (!subTable.ContainsKey(key))
            {
                subTable.Add(key, new List<Handler>());
            }

            List<Handler> list = subTable[key];
            if (!list.Contains(handler))
            {
                if (m_Invoking.Contains(list))
                {
                    subTable[key] = list = new List<Handler>(list);
                }

                list.Add(handler);
            }
        }

        public void AddObserver(Handler handler, string notificationName)
        {
            AddObserver(handler, notificationName, null);
        }

        public void RemoveObserver(Handler handler, string notificationName, Object sender)
        {
            if (handler == null)
            {
                Debug.LogError("Can't remove a null event handler for notification, " + notificationName);
                return;
            }

            if (string.IsNullOrEmpty(notificationName))
            {
                Debug.LogError("A notification name is required to stop observation");
                return;
            }

            if (!m_NotificationTable.ContainsKey(notificationName))
                return;

            SenderTable subTable = m_NotificationTable[notificationName];
            Object key = sender ?? (this);

            if (!subTable.ContainsKey(key))
            {
                return;
            }

            List<Handler> list = subTable[key];
            int index = list.IndexOf(handler);
            if (index != -1)
            {
                if (m_Invoking.Contains(list))
                {
                    subTable[key] = list = new List<Handler>(list);
                }

                list.RemoveAt(index);
            }
        }

        public void RemoveObserver(Handler handler, string notificationName)
        {
            RemoveObserver(handler, notificationName, null);
        }

        public void PostNotification(string notificationName, Object sender, Object target)
        {
            if (string.IsNullOrEmpty(notificationName))
            {
                Debug.LogError("A notification name is required");
                return;
            }

            if (!m_NotificationTable.ContainsKey(notificationName))
            {
                return;
            }

            SenderTable subTable = m_NotificationTable[notificationName];
            if (sender != null && subTable.ContainsKey(sender))
            {
                List<Handler> handlers = subTable[sender];
                m_Invoking.Add(handlers);
                for (int i = 0; i < handlers.Count; ++i)
                {
                    handlers[i](sender, target);
                }

                m_Invoking.Remove(handlers);
            }

            if (subTable.ContainsKey(this))
            {
                List<Handler> handlers = subTable[this];
                m_Invoking.Add(handlers);
                for (int i = 0; i < handlers.Count; ++i)
                {
                    handlers[i](sender, target);
                }

                m_Invoking.Remove(handlers);
            }
        }

        public void PostNotification(string notificationName, Object sender)
        {
            PostNotification(notificationName, sender, null);
        }

        public void PostNotification(string notificationName)
        {
            PostNotification(notificationName, null);
        }

        public void Clean()
        {
            string[] notKeys = new string[m_NotificationTable.Keys.Count];
            m_NotificationTable.Keys.CopyTo(notKeys, 0);
            for (int i = notKeys.Length - 1; i >= 0; --i)
            {
                string notificationName = notKeys[i];
                SenderTable senderTable = m_NotificationTable[notificationName];
                Object[] senKeys = new Object[senderTable.Keys.Count];
                senderTable.Keys.CopyTo(senKeys, 0);
                for (int j = senKeys.Length - 1; j >= 0; --j)
                {
                    Object sender = senKeys[j];
                    List<Handler> handlers = senderTable[sender];
                    if (handlers.Count == 0)
                    {
                        senderTable.Remove(sender);
                    }
                }

                if (senderTable.Count == 0)
                {
                    m_NotificationTable.Remove(notificationName);
                }
            }
        }
    }
}