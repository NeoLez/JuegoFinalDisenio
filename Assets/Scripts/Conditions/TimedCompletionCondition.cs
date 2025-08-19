using System;
using UnityEngine;

namespace Conditions {
    public class TimedCompletionCondition : ICondition{
        private ICondition _condition;
        private float _lastTimeCompleted = Single.NegativeInfinity;
        private readonly float _timeFrame;
        private bool _timeScaled;

        public TimedCompletionCondition(ICondition condition, float timeFrame, bool timeScaled = true) {
            _condition = condition;
            _condition.SetParent(this);
            
            _timeScaled = timeScaled;
            _timeFrame = timeFrame;
        }


        public override bool Evaluate() {
            LastResult = IsDirty() ? Recalculate() : LastResult;
            return LastResult;
        }

        protected override bool Recalculate() {
            if (LastResult) {
                float currentTime = _timeScaled ? Time.time : Time.unscaledTime;
                
                if (currentTime - _lastTimeCompleted < _timeFrame) return true;
            }
            
            bool isCompleted = _condition.Evaluate();
            if (isCompleted) {
                _lastTimeCompleted = (_timeScaled ? Time.time : Time.unscaledTime) + _timeFrame;
            }

            Dirty = false;
            
            return isCompleted;
        }
    }
}