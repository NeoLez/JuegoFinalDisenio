namespace Conditions {
    public class NotCondition : ICondition {
        private ICondition _condition;

        public NotCondition(ICondition condition) {
            _condition = condition;
            _condition.SetParent(this);
        }
        
        protected override bool Recalculate() {
            return !_condition.Evaluate();
        }
    }
}