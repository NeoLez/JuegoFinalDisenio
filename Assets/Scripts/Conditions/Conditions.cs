namespace Conditions {
    public static class Conditions {
        public static readonly ICondition JUMP_10_TIMES = new LeafConditionGeneric<int>(Facts.Facts.TOTAL_JUMPS, a => a >= 10);
    }
}