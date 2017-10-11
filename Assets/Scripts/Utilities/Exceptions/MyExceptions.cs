using System;

namespace Bachelor.Utilities
{
    public class MyException : Exception
    {
        public MyException(string message, string hint) 
            : base(hint != null ? (message + "\n" + hint) : message) { }
    }

    public class NotPrivateConstructor : MyException
    {
        public NotPrivateConstructor(string objectName) 
            : base(
                  "A private or protected constructor is missing for " + objectName,
                  "You should implement this feature"
                  ) { }
    }
}