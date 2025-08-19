namespace Conditions {
    public class AndCondition : ICondition{
        private ICondition[] _conditions;
        
        public AndCondition(params ICondition[] conditions) {
            _conditions = conditions;
            foreach (ICondition condition in _conditions) {
                condition.SetParent(this);
            }
        }

        protected override bool Recalculate() {
            foreach (var condition in _conditions) {
                if (!condition.Evaluate()) {
                    return false;
                }
            }

            return true;
        }
    }
}