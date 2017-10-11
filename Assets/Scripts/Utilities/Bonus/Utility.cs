using System;
using UnityEngine;

namespace Bachelor.Utilities
{
    public class Utility 
    {
        public static string ConvertTimeToString(float seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2}", (int)time.TotalHours, time.Minutes, time.Seconds);
        }

        public static T FindComponentByObjectName<T>(string objectName)
        {
            return FindComponent<T>(GameObject.Find(objectName));
        }

        public static T FindComponentByObjectTag<T>(string objectTag)
        {
            return FindComponent<T>(GameObject.FindWithTag(objectTag));
        }

        public static T FindComponent<T>(GameObject gameobject)
        {
            T searchingComponent = default(T);
            if (gameobject != null)
            {
                searchingComponent = gameobject.GetComponent<T>();
            }
            if (searchingComponent == null)
            {

                Debug.Log("Cannot find '" + typeof(T).Name + "' script");
            }
            return (T)Convert.ChangeType(searchingComponent, typeof(T));
        }
    }
}