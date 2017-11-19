using System;
using Microsoft.Xna.Framework;

namespace BleedingSapphire.Models
{
    public class Item : ICollidable
	{
		public Vector2 Position
		{
			get;
			set;
		}

        public float Radius
        {
            get;
            set;
        }

        public float Mass
        {
            get;
            set;
        }

        public bool Fixed
        {
            get;
            set;
        }

        public Vector2 move = Vector2.Zero;

        public Item()
		{
            Fixed = false;
            Mass = 1f;
		}
	}
}
