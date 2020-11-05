using System.Collections.Generic;

namespace Code.Scripts.Environment
{
    public class TickComparer : IComparer<uint>
    {
        public int Compare(uint x, uint y)
        {
            if (x < y)
            {
                return -1;
            }

            return 1;
        }
    }
}