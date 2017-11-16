using System;
using System.Collections.Generic;

namespace BleedingSapphire
{
	public class World
	{
		public List<Area> areas
		{
			get;
			set;
		}

		public List<Item> Items
		{
			get;
			set;
		}

		public World()
		{
			areas = new List<Area>();
			Items = new List<Item>();
		}
	}
}
