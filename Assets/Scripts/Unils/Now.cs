 using System;

namespace Assets.Scripts.Unils
{
    public class Now 
    {
        private readonly DateTime _start = new DateTime(1970, 1, 1);

        public double Get
        {
            get { return DateTime.UtcNow.Subtract(_start).TotalMilliseconds; }
        }
    }
}