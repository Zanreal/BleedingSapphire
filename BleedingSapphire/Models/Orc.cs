using System;
using System.Collections.Generic;

namespace BleedingSapphire.Models
{
    internal class Orc : Character, IAttackable, IAttacker
    {
        public int MaxHitPoints
        {
            get;
            set;
        }

        public int HitPoints
        {
            get;
            set;
        }

        public ICollection<Item> AttackableItems { get; private set; }

        public float AttackRange { get; }

        public int AttackValue { get; }

        public Orc()
        {
            AttackableItems = new List<Item>();
        }
    }
}
