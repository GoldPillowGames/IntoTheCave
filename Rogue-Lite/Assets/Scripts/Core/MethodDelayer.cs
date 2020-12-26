using System;

namespace GoldPillowGames.Core
{
    public class MethodDelayer
    {
        private readonly Action _methodCallback;
        private float _methodDelay;

        public MethodDelayer(Action methodCallback)
        {
            _methodCallback = methodCallback;
            _methodDelay = -1;
        }

        public void Update(float deltaTime)
        {
            if (_methodDelay <= 0)
                return;
            
            _methodDelay -= deltaTime;
            
            if (_methodDelay <= 0)
            {
                _methodCallback?.Invoke();
            }
        }

        public void SetNewDelay(float newMethodDelay)
        {
            _methodDelay = newMethodDelay;
        }
    }
}