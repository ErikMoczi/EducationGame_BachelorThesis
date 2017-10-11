using System.Collections.Generic;

namespace Bachelor.Game.Base
{
    public class SharedObject
    {
        private static List<StringItem> m_StringCollections = new List<StringItem>();

        public static void SetString(string key, string value)
        {
            foreach (StringItem stringItem in m_StringCollections)
            {
                if (stringItem.Key == key)
                {
                    m_StringCollections.Remove(stringItem);
                    break;
                }
            }

            m_StringCollections.Add(new StringItem(key, value));
        }

        public static string GetString(string key)
        {
            string value = null;

            foreach (StringItem stringItem in m_StringCollections)
            {
                if (stringItem.Key == key)
                {
                    value = stringItem.Value;
                    break;
                }
            }

            return value;
        }

        private class ObjectItem
        {
            private string m_Key;

            public ObjectItem(string key)
            {
                m_Key = key;
            }

            public string Key
            {
                get
                {
                    return m_Key;
                }
            }
        }

        private class StringItem : ObjectItem
        {
            private string m_Value;

            public StringItem(string key, string value) : base(key)
            {
                m_Value = value;
            }

            public string Value
            {
                get
                {
                    return m_Value;
                }
            }
        }
    }
}