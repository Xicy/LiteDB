using System;

namespace LiteDB.Shell
{
    internal class Helper
    {
        public static bool Try(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}