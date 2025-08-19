namespace Facts.Updaters {
    public class TriggerCounterInt : FactUpdater<int, Unit> {
        public TriggerCounterInt(Fact<int> fact, GameEvent<Unit> gameEvent) : base(fact, gameEvent) {
        }

        protected override void ProcessEvent(Unit val) {
            Fact.Value++;
        }
    }
}