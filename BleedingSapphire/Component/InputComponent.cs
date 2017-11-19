using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BleedingSapphire
{
	public class InputComponent : GameComponent
	{
        private readonly Game1 game;

        public Vector2 Movement
        {
            get;
            private set;
        }

        public InputComponent(Game1 game) : base(game)
		{
			this.game = game;
		}

		public override void Update(GameTime gameTime)
		{
            Vector2 movement = Vector2.Zero;


            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
            movement += gamePad.ThumbSticks.Left;

            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Left))
                movement += new Vector2(-1f, 0f);
            if (keystate.IsKeyDown(Keys.Right))
                movement += new Vector2(1f, 0f);
            if (keystate.IsKeyDown(Keys.Up))
                movement += new Vector2(0f, -1f);
            if (keystate.IsKeyDown(Keys.Down))
                movement += new Vector2(0f, 1f);

            if (movement.Length() > 1f)
                movement.Normalize();

            Movement = movement;

			base.Update(gameTime);
		}
	}
}
