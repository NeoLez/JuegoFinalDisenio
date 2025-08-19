using Facts.Updaters;

namespace Facts {
    public class Facts {
        public static readonly Fact<int> TOTAL_JUMPS = new (0);
        private static readonly FactUpdater<int, Unit> TOTAL_JUMPS_UPDATER =
            new TriggerCounterInt(TOTAL_JUMPS, Events.ON_PLAYER_JUMPED);
        
        public static readonly Fact<int> TOTAL_DASH_USES_SELF = new(0);
        private static readonly FactUpdater<int, Unit> TOTAL_DASH_USES_SELF_UPDATER =
            new TriggerCounterInt(TOTAL_DASH_USES_SELF, Events.ON_PLAYER_USE_DASH_SELF);
        
        public static readonly Fact<float> TOTAL_WALK_TIME = new(0);
        private static readonly FactUpdater<float, float> TOTAL_WALK_TIME_UPDATER =
            new AdderCounterFloat(Facts.TOTAL_WALK_TIME, Events.ON_PLAYER_WALKED);
        
        
        
        public static readonly Fact<int> TOTAL_DASH_USES = new(0);
        private static readonly FactUpdater<int, Unit> TOTAL_DASH_USES_UPDATER =
            new TriggerCounterInt(TOTAL_DASH_USES, Events.ON_PLAYER_USE_DASH);
        
        public static readonly Fact<int> TOTAL_FREEZE_USES = new(0);
        private static readonly FactUpdater<int, Unit> TOTAL_FREEZE_USES_UPDATER =
            new TriggerCounterInt(TOTAL_FREEZE_USES, Events.ON_PLAYER_USE_FREEZE);
        
        public static readonly Fact<int> TOTAL_FIRE_USES = new(0);
        private static readonly FactUpdater<int, Unit> TOTAL_FIRE_USES_UPDATER =
            new TriggerCounterInt(TOTAL_FIRE_USES, Events.ON_PLAYER_USE_FIRE);
        
        public static readonly Fact<int> TOTAL_DEATHS = new(0);
        private static readonly FactUpdater<int, Unit> TOTAL_DEATHS_UPDATER =
            new TriggerCounterInt(TOTAL_DEATHS, Events.ON_PLAYER_DIE);
        
        public static readonly Fact<int> TOTAL_GAME_COMPLETIONS = new(0);
        private static readonly FactUpdater<int, Unit> TOTAL_GAME_COMPLETIONS_UPDATER =
            new TriggerCounterInt(TOTAL_GAME_COMPLETIONS, Events.ON_PLAYER_COMPLETED_GAME);
    }
}