namespace Facts.Updaters {
    public class AdderCounterFloat : FactUpdater<float, float> {
        public AdderCounterFloat(Fact<float> fact, GameEvent<float> gameEvent) : base(fact, gameEvent) {
        }

        protected override void ProcessEvent(float val) {
            Fact.Value += val;
        }
    }
}