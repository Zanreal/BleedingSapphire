using System;
namespace BleedingSapphire.Models
{
    public class Layer
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

        public Tile[,] Tiles { get; private set; }

        public Layer(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            Tiles = new Tile[width, height];
        }
    }
}
