namespace Code.Scripts.Environment.TimeSystem
{
    public struct TimeSlot
    {
        public readonly int ID;
        public readonly int Ticks;

        public TimeSlot(int id, int ticks)
        {
            ID = id;
            Ticks = ticks;
        }
        
        public override string ToString()
        {
            return $"ID: {ID}, Ticks: {Ticks}";
        }
    }
}