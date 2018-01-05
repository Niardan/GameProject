using System;
using UnityEngine;

namespace Assets.Scripts.Mining.Controllers.GamePole
{
    public delegate void Tick();

    public delegate void Elapsed();
    public class GameTimer
    {
        float timeLeft = 30.0f;
        private float _tickTime;
        private long _elapsedTime;


        //public void StartTimer(float tickTime,  elapsedTime)
        //{
        //    _tickTime = tickTime;
        //    _elapsedTime = DateTime.Now.Ticks;
        //}

        void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                
            }
        }
    }
}