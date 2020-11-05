using System;

namespace Code.Scripts.Environment
{
    [Serializable]
    public struct GameTime
    {
        public int hour;
        public int minute;
        public int second;

        public GameTime(int hour = 0, int minute = 0, int second = 0)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }
    }
}