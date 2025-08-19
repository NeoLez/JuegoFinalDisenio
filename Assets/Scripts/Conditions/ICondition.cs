using UnityEngine;

namespace Conditions {
    public abstract class ICondition {
        protected bool Dirty = true;
        protected bool LastResult;
        protected ICondition Parent;

        public virtual bool Evaluate() {
            LastResult = IsDirty() ? Recalculate() : LastResult;
            Dirty = false;
            return LastResult;
        }
        protected abstract bool Recalculate();

        internal void SetParent(ICondition parent) {
            Parent = parent;
        }
        public virtual void PropagateDirty() {
            Dirty = true;
            Parent?.PropagateDirty();
        }
        public bool IsDirty() {
            return Dirty;
        }
        //public abstract void MarkDirty();
    }
}
