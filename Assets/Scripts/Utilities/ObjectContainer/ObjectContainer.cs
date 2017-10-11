namespace Bachelor.Utilities
{
    public abstract class ObjectContainer<T> where T : ObjectData
    {
        protected T m_ObjectData;

        public T ObjectData
        {
            get
            {
                return m_ObjectData;
            }
        }

        public ObjectContainer(T objectData)
        {
            m_ObjectData = objectData;
        }
    }
}