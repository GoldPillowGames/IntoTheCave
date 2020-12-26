using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GoldPillowGames.Core
{
    public class RandomStringSelector
    {
        private readonly List<string> _posibleStrings;
        
        public RandomStringSelector()
        {
            _posibleStrings = new List<string>();
        }
        
        public RandomStringSelector(IEnumerable<string> newStrings)
        {
            _posibleStrings = newStrings.ToList();
        }
        
        public RandomStringSelector(string newString)
        {
            _posibleStrings = new List<string> {newString};
        }

        public void Add(string newString)
        {
            _posibleStrings.Add(newString);
        }

        public void Add(IEnumerable<string> newStrings)
        {
            _posibleStrings.AddRange(newStrings.ToList());
        }
        
        public string ChooseRandom()
        {
            var randomId = Random.Range(0, _posibleStrings.Count - 1);
            return _posibleStrings[randomId];
        }
    }
}