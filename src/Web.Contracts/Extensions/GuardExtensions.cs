namespace Web.Contracts.Extensions
{
    using System;

    public static class GuardExtentions
    {
        public static void Guard(this object obj, string message = null, string name = null)
        {
            if (obj != null) return;

            if (message != null)
            {
                if (name != null) throw new ArgumentNullException(name, message);

                throw new ArgumentNullException(string.Empty, message);
            }

            if (name != null) throw new ArgumentNullException(name);

            throw new ArgumentNullException();
        }

        public static void Guard(this object obj, Exception exception)
        {
            if (obj == null) throw exception;
        }

        public static void Guard(this string str, string message, string name = null)
        {
            if (!string.IsNullOrEmpty(str)) return;

            if (name != null) throw new ArgumentNullException(name, message);

            throw new ArgumentNullException(message);
        }

        public static void Guard(this string str, Exception exception)
        {
            if (string.IsNullOrEmpty(str)) throw exception;
        }
    }
}