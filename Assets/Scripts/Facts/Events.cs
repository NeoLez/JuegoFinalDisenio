namespace Facts {
    public static class Events {
        public static readonly GameEvent<Unit> ON_PLAYER_JUMPED = new();
        public static readonly GameEvent<Unit> ON_PLAYER_USE_DASH_SELF = new();
        public static readonly GameEvent<float> ON_PLAYER_WALKED = new();

        public static readonly GameEvent<Unit> ON_PLAYER_USE_DASH = new();
        public static readonly GameEvent<Unit> ON_PLAYER_USE_FREEZE = new();
        public static readonly GameEvent<Unit> ON_PLAYER_USE_FIRE = new();
        public static readonly GameEvent<Unit> ON_PLAYER_DIE = new();
        public static readonly GameEvent<Unit> ON_PLAYER_COMPLETED_GAME = new();
    }
}