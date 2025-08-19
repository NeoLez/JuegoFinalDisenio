namespace Facts {
    public abstract class FactUpdater<T, U> {
        protected readonly Fact<T> Fact;

        public FactUpdater(Fact<T> fact, GameEvent<U> gameEvent) {
            Fact = fact;
            gameEvent.Subscribe(ProcessEvent);
        }

        protected abstract void ProcessEvent(U val);
    }
}