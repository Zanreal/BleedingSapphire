using System;
using System.Collections.Generic;
using BleedingSapphire.Models;

namespace BleedingSapphire
{
    public class Area
    {
        public int Width
        {
            get;
            private set;
        }

        public int Height
        {
            get;
            private set;
        }

        public List<Item> Items
        {
            get;
            private set;
        }

        public Layer[] Layers
        {
            get;
            private set;
        }

		public Area(int layers, int width, int height)
		{
            this.Width = width;
            this.Height = height;

            Layers = new Layer[layers];
            for (int i = 0; i < layers; i++)
            {
                Layers[i] = new Layer(width, height);
            }

            Items = new List<Item>();
        }

        public bool IsCellBlocked(int x, int y)
        {
            // Sonderfall auserhalb des Spielfeldes
            if (x < 0 || y < 0 || x > Width - 1 || y > Height - 1)
                return true;

            for (int l = 0; l < Layers.Length; l++)
            {
                if (Layers[l].Tiles[x, y].Blocked)
                    return true;
            }
            return false;
        }
	}
}
