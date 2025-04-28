using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// Interfaces for various game components

    //damageable interface
    public interface IDamageable
    {
        void TakeDamage(int amount);
    }

    //collectible interface
    public interface ICollectible
    {
        void Collect(Player player);
    }
    

}
