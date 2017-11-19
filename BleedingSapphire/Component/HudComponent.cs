using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BleedingSapphire
{
    public class HudComponent : DrawableGameComponent
    {
        private Game1 game;

        private SpriteBatch spritebatch;

        private SpriteFont font;

        public HudComponent(Game1 game) : base(game)
        {
            this.game = game;
        }

        protected override void LoadContent()
        {
            spritebatch = new SpriteBatch(GraphicsDevice);

            font = game.Content.Load<SpriteFont>("HudFont");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spritebatch.Begin();
            spritebatch.DrawString(font, "Development Version", new Vector2(20, 20), Color.White); 
            spritebatch.End();

            base.Draw(gameTime);
        }
    }
}
