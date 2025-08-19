namespace StatusEffects {

    public interface IEffectBehaviour {
        public StatusEffectsType GetType();

        public void Enable();
        public void Disable();
        public void Tick();
    }

}