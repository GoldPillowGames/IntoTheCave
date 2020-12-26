using System.Collections.Generic;
using System.Linq;

namespace GoldPillowGames.Enemy
{
    public class AnimationComboSelector
    {
        private readonly string[] _boolParameters;
        private int _nextBool;
        public AnimationComboSelector(IEnumerable<string> boolParameters)
        {
            _boolParameters = boolParameters.ToArray();
            _nextBool = 0;
        }

        public string GetNextAttackComboBoolParameter()
        {
            var boolParameter = _boolParameters[_nextBool];
            IncrementNextBool();
            return boolParameter;
        }

        public void ResetCombo()
        {
            _nextBool = 0;
        }
        
        private void IncrementNextBool()
        {
            _nextBool++;
            if (_nextBool >= _boolParameters.Length)
            {
                _nextBool = 0;
            }
            //_nextBool -= (_nextBool % _boolParameters.Length) * _boolParameters.Length;
        }
    }
}