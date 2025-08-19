using System;
using Conditions;

namespace Achievements {
    public class Achievement {
        private ICondition _condition;
        public event Action OnCompleted;
        private bool wasCompleted = false;
        public string Name { get; private set; }

        public Achievement(string name, ICondition condition) {
            _condition = condition;
            Name = name;
        }

        public bool Evaluate() {
            if (wasCompleted) return true;
            
            bool result = _condition.Evaluate();
            if(result) OnCompleted?.Invoke();
            return result;
        }
    }
}