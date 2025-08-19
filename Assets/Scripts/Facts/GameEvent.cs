using System;

namespace Facts {
    public class GameEvent<T> {
        private Action<T> listeners;

        public void Subscribe(Action<T> listener) {
            listeners += listener;
        }

        public void Unsubscribe(Action<T> listener) {
            listeners -= listener;
        }

        public void Raise(T payload) {
            listeners?.Invoke(payload);
        }
    }
}