using System;
using System.Collections.Generic;
using Conditions;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Achievements {
    public static class AchievementManager {
        private static readonly float SecondsBetweenChecks = 1.5f;
        private static float _lastTimeChecked = Single.NegativeInfinity;
        private static List<Achievement> _achievements = new();
        public static void CheckAchievements() {
            if (Time.time - _lastTimeChecked < SecondsBetweenChecks) return;
            foreach (var achievement in _achievements) {
                _lastTimeChecked = Time.time;
                
                if (achievement.Evaluate()) {
                    Debug.Log(achievement.Name);
                }
            }
        }

        public static readonly Achievement USE_FREEZE_SPELL = new (
            "Use Freeze Spell", 
            new LeafConditionGeneric<int>(Facts.Facts.TOTAL_FREEZE_USES, num => num >= 1)
            );
        public static readonly Achievement USE_FIRE_SPELL = new (
            "Use Fire Spell", 
            new LeafConditionGeneric<int>(Facts.Facts.TOTAL_FIRE_USES, num => num >= 1)
        );
        public static readonly Achievement USE_DASH_SPELL = new (
            "Use Dash Spell", 
            new LeafConditionGeneric<int>(Facts.Facts.TOTAL_DASH_USES, num => num >= 1)
        );
        public static readonly Achievement DIE_ONCE = new (
            "Die Once", 
            new LeafConditionGeneric<int>(Facts.Facts.TOTAL_DEATHS, num => num >= 1)
        );
        public static readonly Achievement COMPLETE_GAME = new (
            "Complete Game", 
            new LeafConditionGeneric<int>(Facts.Facts.TOTAL_GAME_COMPLETIONS, num => num >= 1)
        );
        
        
        public static readonly Achievement WALKED_3_SECONDS_AND_JUMPED_5_TIMES = new (
            "Walked 3 seconds and jumped 5 times",
            new OrCondition(
                new AndCondition(
                    new LeafConditionGeneric<int>(Facts.Facts.TOTAL_JUMPS, val => val >= 5), 
                    new LeafConditionGeneric<int>(Facts.Facts.TOTAL_DASH_USES_SELF, val => val >= 3)
                ),
                new LeafConditionGeneric<float>(Facts.Facts.TOTAL_WALK_TIME, val => val >= 3)
            )
        );
        
        static AchievementManager() {
            _achievements.Add(USE_FREEZE_SPELL);
            _achievements.Add(USE_DASH_SPELL);
            _achievements.Add(USE_FIRE_SPELL);
            _achievements.Add(DIE_ONCE);
            _achievements.Add(COMPLETE_GAME);
            
            _achievements.Add(WALKED_3_SECONDS_AND_JUMPED_5_TIMES);
	    }
    }
}
