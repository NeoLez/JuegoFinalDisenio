using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NonMonobehaviorUpdates;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;
using Achievements;

namespace PlayerLoopCleaner {
    public static class PlayerLoopCleaner {
        //List of subSystems to remove
        private static readonly Type[] TypesToRemove = {
            typeof(Initialization.XREarlyUpdate),
            typeof(EarlyUpdate.AnalyticsCoreStatsUpdate),
            typeof(EarlyUpdate.XRUpdate),
            typeof(EarlyUpdate.UpdateKinect),
            typeof(EarlyUpdate.ARCoreUpdate),
            typeof(EarlyUpdate.ARCoreUpdate),
            typeof(FixedUpdate.XRFixedUpdate),
            typeof(PostLateUpdate.XRPostPresent),
            typeof(PostLateUpdate.XRPostLateUpdate),
            typeof(PostLateUpdate.XRPreEndFrame),
        };

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void RegisterNewSystems() {
            PlayerLoopInterface.InsertSystemBefore(typeof(UpdatesManager), UpdatesManager.TickUpdate, typeof(Update.ScriptRunBehaviourUpdate));
            PlayerLoopInterface.InsertSystemBefore(typeof(UpdatesManager), UpdatesManager.TickFixedUpdate, typeof(FixedUpdate.ScriptRunBehaviourFixedUpdate));
            
            PlayerLoopInterface.InsertSystemAfter(typeof(AchievementManager), AchievementManager.CheckAchievements, typeof(FixedUpdate.ScriptRunBehaviourFixedUpdate));
        }
        
        //Removes all instances of the systems specified in TypesToRemove
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void RunChanges() {
            
            var systems = PlayerLoop.GetCurrentPlayerLoop();
            DeleteSubsystems(ref systems, TypesToRemove);
            PlayerLoop.SetPlayerLoop(systems);
        }
        
        
        private static void DeleteSubsystems(ref PlayerLoopSystem system, Type[] typesToDelete) {
            if (system.subSystemList == null)
                return;
            List<PlayerLoopSystem> subSystems = system.subSystemList.ToList();
            subSystems.RemoveAll(loopSystem => typesToDelete.Contains(loopSystem.type));
            system.subSystemList = subSystems.ToArray();

            for (int i = 0; i < system.subSystemList.Length; i++) {
                DeleteSubsystems(ref system.subSystemList[i], typesToDelete);
            }
        }

        
        /// <summary>
        /// Adds the subSystem as the first entry below the specified type (if said type is found).
        /// </summary>
        /// <param name="subSystem"></param>
        /// <param name="system"></param>
        /// <param name="type"></param>
        private static void AddSubsystemUnderType(ref PlayerLoopSystem subSystem, ref PlayerLoopSystem system, Type type) {
            if (system.type == type) {
                var subSystems = system.subSystemList.ToList();
                subSystems.Insert(0, subSystem);
                system.subSystemList = subSystems.ToArray();
            }
            else if (system.subSystemList != null) {
                for (int i = 0; i < system.subSystemList.Length; i++) {
                    AddSubsystemUnderType(ref subSystem, ref system.subSystemList[i], type);
                }
            }
        }
        
        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void PrintCurrentPlayerLoop() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Unity Player Loop");
            foreach (PlayerLoopSystem subSystem in PlayerLoop.GetCurrentPlayerLoop().subSystemList) {
                PrintSubsystem(subSystem, sb, 0);
            }
            Debug.Log(sb.ToString());
        }

        private static void PrintSubsystem(PlayerLoopSystem system, StringBuilder sb, int level) {
            sb.Append(' ', level * 2).AppendLine(system.type.ToString());
            if (system.subSystemList == null || system.subSystemList.Length == 0) return;

            foreach (PlayerLoopSystem subSystem in system.subSystemList) {
                PrintSubsystem(subSystem, sb, level + 1);
            }
        }
    }
}