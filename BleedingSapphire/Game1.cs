using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace BleedingSapphire
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

		public SceneComponent Scene{get;set;}

		public InputComponent Input{get;set;}

		public SimulationComponent Simulation{ get; set; }

        public HudComponent Hud { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Input = new InputComponent(this);
			Input.UpdateOrder = 0;
            Components.Add(Input);

			Simulation = new SimulationComponent(this);
			Simulation.UpdateOrder = 1;
			Components.Add(Simulation);

			Scene = new SceneComponent(this);
			Scene.UpdateOrder = 2;
            Scene.DrawOrder = 0;
			Components.Add(Scene);

            Hud = new HudComponent(this);
            Hud.UpdateOrder = 3;
            Hud.DrawOrder = 1;
            Components.Add(Hud);
		}
	}
}
