using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BleedingSapphire
{
	public class SceneComponent : DrawableGameComponent
	{
		public GraphicsDeviceManager gDevice
		{
			get;
			set;
		}

		public SpriteBatch spriteBatch
		{
			get;
			set;
		}

		public Game1 game
		{
			get;
			set;
		}

		public SceneComponent(Game1 game) : base(game)
		{
			this.game = game;
			gDevice = new GraphicsDeviceManager(game);
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(gDevice.GraphicsDevice);
			base.LoadContent();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			gDevice.GraphicsDevice.Clear(Color.CornflowerBlue);

			base.Draw(gameTime);
		}
	}
}
