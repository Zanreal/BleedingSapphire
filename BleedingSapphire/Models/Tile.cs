using System;
using Microsoft.Xna.Framework;

namespace BleedingSapphire.Models
{
    public class Tile
	{
        public bool Blocked{ get; set; }

        public Rectangle SourceRectangle{ get; set; }

        public string Texture { get; set; }

        public Tile()
		{
		}
	}
}
