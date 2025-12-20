using HarmonyLib;
using ModLoader;
using UnityEngine;

namespace PartMenuAPI
{
    public class Main : Mod
    {
        public override string ModNameID => "partmenuapi";
        public override string DisplayName => "PartMenuAPI";
        public override string Author => "Koja Mori";
        public override string MinimumGameVersionNecessary => "1.5.10";
        public override string ModVersion => "1.0.0";
        public override string Description => "An API for creating custom part menu modules.";
        public override void Early_Load()
        { 
            patcher.PatchAll();
            Debug.Log($"[{ModNameID}] Early_Load completed, patches applied");
        }

        private readonly Harmony patcher = new Harmony("com.mori.partmenuapi");

    }
}
