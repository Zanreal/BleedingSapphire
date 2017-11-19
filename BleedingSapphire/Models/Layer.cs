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

        public int[,] Tiles { get; private set; }

        public string Name { get; set; }

        public Layer(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            Tiles = new int[width, height];
        }
    }
}
