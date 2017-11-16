using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BleedingSapphire
{
	public class Game1 : Game
	{
		public SceneComponent scene
		{
			get;
			set;
		}

		public InputComponent input
		{
			get;
			set;
		}

		public SimulationComponent simulation
		{
			get;
			set;
		}

		public Game1()
		{
			Content.RootDirectory = "Content";

			input = new InputComponent(this);
			input.UpdateOrder = 0;
			Components.Add(input);

			simulation = new SimulationComponent(this);
			simulation.UpdateOrder = 1;
			Components.Add(simulation);

			scene = new SceneComponent(this);
			scene.UpdateOrder = 2;
			Components.Add(scene);
		}
	}
}
