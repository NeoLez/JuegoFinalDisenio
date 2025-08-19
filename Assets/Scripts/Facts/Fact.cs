using System;

namespace Facts {
    public class Fact<T> {
        private Action _listeners;
        private T _value;
        public T Value {
            get => _value;
            set {
                _value = value;
                _listeners?.Invoke();
            }
        }

        public Fact(T initialValue) {
            Value = initialValue;
        }

        public void Subscribe(Action listener) {
            _listeners += listener;
        }

        public void Unsubscribe(Action listener) {
            _listeners -= listener;
        }

        public override string ToString() {
            return _value.ToString();
        }
    }
}
