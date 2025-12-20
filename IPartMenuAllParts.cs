using SFS.Parts;
using SFS.Parts.Modules;

namespace PartMenuAPI
{
    public interface IPartMenuAllParts
    {
        /// <summary>
        /// Draws the specified part and related information to the provided stats menu using the given drawing
        /// settings.
        /// </summary>
        /// <param name="representative">The part whose stats are to be represented on behalf of all selected parts in the stats menu. It is allParts[0].</param>
        /// <param name="allParts">An array containing all parts to be considered during the drawing operation. Cannot be null.</param>
        /// <param name="drawer">The stats menu instance used as the drawing surface. Cannot be null.</param>
        /// <param name="settings">The drawing settings that control the conditions and settings of stats rendering.</param>
        void Draw(Part representative, Part[] allParts, StatsMenu drawer, PartDrawSettings settings);
    }
}
