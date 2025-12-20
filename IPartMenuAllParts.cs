using SFS.Parts;
using SFS.Parts.Modules;

namespace PartMenuAPI
{
    public interface IPartMenuAllParts
    {
        void Draw(Part[] allParts, StatsMenu drawer, PartDrawSettings settings);
    }
}
