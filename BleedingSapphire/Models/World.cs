using System;
using System.Collections.Generic;

namespace BleedingSapphire.Models
{
    public class World
	{
		public List<Area> Areas
		{
			get;
			set;
		}

		public List<IAttackable> Items
		{
			get;
			set;
		}

		public World()
		{
			Areas = new List<Area>();
			Items = new List<IAttackable>();
		}
	}
}
