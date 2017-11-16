using System;
using System.Collections.Generic;

namespace BleedingSapphire
{
	public class Area
	{
		public List<Tile> tiles
		{
			get;
			set;
		}

		public Area()
		{
			tiles = new List<Tile>();
		}
	}
}
