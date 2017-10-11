using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Bachelor.Utilities
{
    public abstract class SOBaseList<T> : ScriptableObject, IEnumerable where T : SODetails
    {
        [SerializeField]
        private List<T> m_DetailsList;

        public List<T> DetailsList
        {
            get
            {
                return m_DetailsList;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index > Count - 1 && index > 0)
                {
                    return null;
                }
                else
                {
                    return m_DetailsList[index];
                }                
            }
        }

        public int Count
        {
            get
            {
                return m_DetailsList.Count;
            }
        }

        public IEnumerator GetEnumerator()
        {
            for(int i = 0; i < Count; i++)
            {
                yield return m_DetailsList[i];
            }
        }
    }
}