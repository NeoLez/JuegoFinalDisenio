namespace Conditions {
    public class OrCondition : ICondition{
        private ICondition[] _conditions;

        public OrCondition(params ICondition[] conditions) {
            _conditions = conditions;
            foreach (var condition in _conditions) {
                condition.SetParent(this);
            }
        }

        protected override bool Recalculate() {
            foreach (var condition in _conditions) {
                if (condition.Evaluate()) {
                    return true;
                }
            }

            return false;
        }
    }
}