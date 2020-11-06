using System.Collections.Generic;

namespace Code.Scripts.Environment
{
    public class TickComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x < y)
            {
                return -1;
            }

            return 1;
        }
    }
}