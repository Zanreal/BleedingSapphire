using System;
using System.Collections.Generic;

namespace BleedingSapphire.Models
{
    public interface IAttacker
    {
        ICollection<Item> AttackableItems { get; }

        float AttackRange { get; }

        int AttackValue { get; }
    }
}
