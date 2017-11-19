using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BleedingSapphire.Models
{
    public class Player : Character, IAttackable, IAttacker
	{
 
        public int MaxHitPoints{ get; set; }

        public int HitPoints{ get; set; }

        public ICollection<Item> AttackableItems { get; private set; }

        public float AttackRange { get; }

        public int AttackValue { get; }

        public Player()
		{
            AttackableItems = new List<Item>();
		}
	}
}
