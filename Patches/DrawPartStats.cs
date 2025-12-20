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
                // collect implementors attached to this instance
                IPartMenuAllParts[] modules = part.GetComponents<Component>().OfType<IPartMenuAllParts>().ToArray();
                if (modules.Length == 0)
                    return;

                // check whether there is at least one implementor across allParts
                if (allParts == null || Array.IndexOf(allParts, part) == -1)
                    Debug.LogWarning("[PartMenuAPI] Unexpected: allParts does not contain the current part instance.");

                // call Draw on the modules attached to the current instance.
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
