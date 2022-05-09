using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HCB.UI
{
    public static class HCBPanelList
    {
        public static string JoysticPanel = "JoysticPanel";
        public static string GameStartPanel = "GameStartPanel";
        public static string InGamePanel = "InGamePanel";
        public static string TutorialPanel = "TutorialPanel";
        public static string EffectPanel = "EffectPanel";
        public static string LevelLoadingPanel = "LevelLoadingPanel";
        public static string BlockInputPanel = "BlockInputPanel";
        public static string DebugPanel = "DebugPanel";
        public static string InitializePanel = "InitializePanel";

        public static Dictionary<string, HCBPanel> HCBPanels = new Dictionary<string, HCBPanel>();

        private static string[] panelIDs = new string[]
        {
            "None",
            JoysticPanel,
            GameStartPanel,
            InGamePanel,
            TutorialPanel,
            EffectPanel,
            LevelLoadingPanel,
            BlockInputPanel,
            DebugPanel,
            InitializePanel
        };
        public static List<string> PanelIDs { get { return panelIDs.ToList(); } }
    }
}
