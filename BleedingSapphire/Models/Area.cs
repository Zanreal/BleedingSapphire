using System;
using System.Collections.Generic;
using BleedingSapphire.Models;
using Microsoft.Xna.Framework;

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

        public Color Background
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public Dictionary<int, Tile> Tiles { get; set; }

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

            Tiles = new Dictionary<int, Tile>();
            //Tiles.Add(1, new Tile() { Blocked = false, SourceRectangle = new Rectangle(448, 128, 32, 32)});
            //Tiles.Add(2, new Tile() { Blocked = true, SourceRectangle = new Rectangle(384, 384, 32, 32)});
        }

        public bool IsCellBlocked(int x, int y)
        {
            // Sonderfall auserhalb des Spielfeldes
            if (x < 0 || y < 0 || x > Width - 1 || y > Height - 1)
                return true;

            for (int l = 0; l < Layers.Length; l++)
            {
                int tileId = Layers[l].Tiles[x, y];
                if (tileId == 0)
                    continue;

                Tile tile = Tiles[tileId];

                if (tile.Blocked)
                    return true;
            }
            return false;
        }
	}
}
