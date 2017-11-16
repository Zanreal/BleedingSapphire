using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BleedingSapphire
{
	public class InputComponent : GameComponent
	{
		public KeyboardState kState
		{
			get;
			set;
		}

		public Game1 game
		{
			get;
			set;
		}

		public InputComponent(Game1 game) : base(game)
		{
			this.game = game;
		}

		public override void Update(GameTime gameTime)
		{
			if (kState.IsKeyDown(Keys.Escape))
			{
				game.Exit();
			}

			base.Update(gameTime);
		}
	}
}
