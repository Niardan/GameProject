using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Unils
{

    public delegate void TickHandler(TickTimer timer);

    public delegate void ElapsedHandler(TickTimer timer);
    public class TickTimer
    {
        private float _milisecondDuration;
        private readonly MonoBehaviour _monoBehaviour;
        private WaitForSeconds _waiter;
        private float _elapsedTs;
        private float _currentTime;
        private Coroutine _system;

        public event TickHandler Tick;
        public event ElapsedHandler Elapsed;
        
        public TickTimer(double elapsedTs, float elapsedTs1, MonoBehaviour monoBehaviour, float milisecondDuration = 1000)
        {
            _elapsedTs = elapsedTs1;
            _monoBehaviour = monoBehaviour;
            _milisecondDuration = milisecondDuration;
            _waiter = new WaitForSeconds(milisecondDuration / 1000);
           
        }

        public void Start()
        {
            _system = _monoBehaviour.StartCoroutine(TickSystem());
        }

        public void Stop()
        {
            _monoBehaviour.StopCoroutine(_system);
        }

        public void Reset()
        {
            _currentTime = 0;
        }

        private IEnumerator TickSystem()
        {
            while (_currentTime <= _elapsedTs)
            {
                yield return _waiter;
                _currentTime += Time.deltaTime;
                CallTick();
            }
            CallElapsed();
        }

        public void CallTick()
        {
            if (Tick != null)
            {
                Tick(this);
            }
        }

        public void CallElapsed()
        {
            if (Elapsed != null)
            {
                Elapsed(this);
            }
        }
    }
}
