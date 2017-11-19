using System;
using System.IO;
using System.Collections.Generic;
using BleedingSapphire.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BleedingSapphire
{
	public class SceneComponent : DrawableGameComponent
	{
        private readonly Game1 game;
        private SpriteBatch spriteBatch;
        private Texture2D pixel;
        //private Texture2D landscape;

        public Dictionary<string, Texture2D> textures;

		public SceneComponent(Game1 game) : base(game)
		{
			this.game = game;
            textures = new Dictionary<string, Texture2D>();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new [] {Color.White});

            List<string> requiredTextures = new List<string>();
            foreach (var area in game.Simulation.World.Areas)
            {
                foreach (var tile in area.Tiles.Values)
                {
                    if (!requiredTextures.Contains(tile.Texture))
                        requiredTextures.Add(tile.Texture);
                }
            }

            string mapPath = Path.Combine(Environment.CurrentDirectory, "Maps");
            foreach (var textureName in requiredTextures)
            {
                using (Stream stream = File.OpenRead(mapPath + "//" + textureName))
                {
                    Texture2D texture = Texture2D.FromStream(GraphicsDevice, stream);
                    textures.Add(textureName,texture);
                }
            }

			base.LoadContent();
		}
        	
		public override void Draw(GameTime gameTime)
		{
            Area area = game.Simulation.World.Areas[0];

            GraphicsDevice.Clear(area.Background);

            float scaleX = (GraphicsDevice.Viewport.Width - 20) / area.Width;
            float scaleY = (GraphicsDevice.Viewport.Height - 20) / area.Height;

            spriteBatch.Begin();

            for (int i = 0; i < area.Layers.Length; i++)
            {
                RenderLayer(area, area.Layers[i],scaleX,scaleY);
                if (i == 4) RenderItems(area, scaleX, scaleY);
            }

            spriteBatch.End();

            base.Draw(gameTime);
		}

        private void RenderLayer(Area area, Layer layer, float scaleX, float scaleY)
        {
            for (int x = 0; x < area.Width; x++)
            {
                for (int y = 0; y < area.Height; y++)
                {
                    int tileId = layer.Tiles[x, y];
                    if (tileId == 0)
                        continue;

                    Tile tile = area.Tiles[tileId];
                    Texture2D texture = textures[tile.Texture];

                    int offsetX = (int)(x * scaleX) + 10;
                    int offsetY = (int)(y * scaleY) + 10;

                    spriteBatch.Draw(texture, new Rectangle(offsetX, offsetY, (int)scaleX, (int)scaleY), tile.SourceRectangle, Color.White);
                }
            }
        }

        private void RenderItems(Area area, float scaleX, float scaleY)
        {
            foreach (var item in area.Items)
            {
                Color color = Color.Yellow;
                if (item is Player)
                    color = Color.Red;
                
                int posX = (int)((item.Position.X - item.Radius) * scaleX) + 10;
                int posY = (int)((item.Position.Y - item.Radius) * scaleY) + 10;
                int size = (int)((item.Radius * 2) * scaleX);

                spriteBatch.Draw(pixel, new Rectangle(posX, posY, size, size), color);
            }
        }
	}
}
