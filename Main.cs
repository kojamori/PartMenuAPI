using HarmonyLib;
using ModLoader;
using System.Collections.Generic;
using UnityEngine;

namespace PartMenuAPI
{
    public class Main : Mod
    {
        public static Main Instance { get; private set; }
        public Main()
        {
            Instance = this;
        }
        public override string ModNameID => "partmenuapi";
        public override string DisplayName => "PartMenuAPI";
        public override string Author => "Koja Mori";
        public override string MinimumGameVersionNecessary => "1.5.10";
        public override string ModVersion => "1.0.0";
        public override string Description => "An API for creating custom part menu modules.";
        public override Dictionary<string, string> Dependencies => null;
        public override void Early_Load()
        { 
            patcher.PatchAll();
            Debug.Log($"[{DisplayName}] Early_Load completed, patches applied");
        }

        public override void Load()
        {
            // ...
        }

        private readonly Harmony patcher = new Harmony("com.mori.partmenuapi");

    }
}
