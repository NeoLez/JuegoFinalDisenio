namespace Conditions {
    public class PermanentCompletionCondition : ICondition {
        private ICondition _condition;

        public PermanentCompletionCondition(ICondition condition) {
            _condition = condition;
            _condition.SetParent(this);
        }
        
        protected override bool Recalculate() {
            if (LastResult) return true;

            return _condition.Evaluate();
        }

        public override void PropagateDirty() {
            if(!LastResult)
                base.PropagateDirty();
        }
    }
}