using HarmonyLib;
using SFS.Parts;
using SFS.Parts.Modules;
using System;
using System.Linq;
using UnityEngine;

namespace PartMenuAPI.Patches
{
    [HarmonyPatch(typeof (Part), "DrawPartStats")]
    public static class DrawPartStats
    {
        [HarmonyPostfix]
        public static void Postfix(Part part, Part[] allParts, StatsMenu drawer, PartDrawSettings settings)
        {
            try
            {
                IPartMenuAllParts[] modules = part.GetComponents<IPartMenuAllParts>();
                if (modules.Length == 0)
                    return;

                foreach (var module in modules)
                    module.Draw(allParts[0], allParts, drawer, settings);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[PartMenuAPI] DrawPartStats postfix failed: {ex.Message}");
            }
        }
    }
}
