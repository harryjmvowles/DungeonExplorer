using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public interface IDamageable
    {
        void TakeDamage(int amount);
    }
    public interface ICollectible
    {
        void Collect(Player player);
    }
    

}
