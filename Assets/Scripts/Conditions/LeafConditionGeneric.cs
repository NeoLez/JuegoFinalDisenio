using System;
using Facts;

namespace Conditions {
    public class LeafConditionGeneric<T> : LeafCondition {
        private Func<T, bool> _conditionToEvaluate;
        private Fact<T> _fact;

        public LeafConditionGeneric(Fact<T> fact, Func<T, bool> functionToEvaluate) {
            _conditionToEvaluate = functionToEvaluate;
            _fact = fact;
            _fact.Subscribe(PropagateDirty);
        }

        protected override bool Recalculate() {
            return _conditionToEvaluate.Invoke(_fact.Value);
        }
    }
}