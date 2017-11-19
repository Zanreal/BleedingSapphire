using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BleedingSapphire.Models;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace BleedingSapphire
{
	public class SimulationComponent : GameComponent
	{
        private float gap = 0.00001f;

        private readonly Game1 game;

        public World World
        {
            get;
            private set;
        }

        public Player Player
        {
            get;
            private set;
        }

		public SimulationComponent(Game1 game) : base(game)
 		{
            this.game = game;
            NewGame();
		}

        private void NewGame()
        {
            World = new World();

            //Area area = new Area(2,30,30);
            Area area = LoadFromJson("town");
            World.Areas.Add(area);

            //for (int x = 0; x < area.Width; x++)
            //{
            //    for (int y = 0; y < area.Height; y++)
            //    {
            //        area.Layers[0].Tiles[x, y] = 1;
            //        area.Layers[1].Tiles[x, y] = 0;

            //        if (x == 0 || y == 0 || x == area.Width - 1 || y == area.Height - 1)
            //            area.Layers[1].Tiles[x, y] = 2;
            //    }
            //}

            Player = new Player() { Position = new Vector2(15, 10), Radius = 0.25f };
            area.Items.Add(Player);

            Diamand diamand = new Diamand { Position = new Vector2(10, 10), Radius = 0.25f };
            area.Items.Add(diamand);
        }

		public override void Update(GameTime gameTime)
		{
            #region Player Input

            Player.Velocity = game.Input.Movement * 10f;

            #endregion

            #region Character Movement

            foreach (var area in World.Areas)            
            {
                foreach (var character in area.Items.OfType<Character>())
                {
                    character.move += character.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    IAttacker attacker = null;
                    if(character is IAttacker)
                    {
                        attacker = (IAttacker)character;
                        attacker.AttackableItems.Clear();
                    }

                    foreach (var item in area.Items)
                    {
                        if (item == character) continue;

                        Vector2 distance = (item.Position + item.move) - (character.Position + character.move);

                        if (item is IAttackable && distance.Length() - attacker.AttackRange - item.Radius < 0)
                            attacker.AttackableItems.Add(item);

                        float overlap = item.Radius + character.Radius - distance.Length();

                        if (overlap > 0f)
                        {
                            Vector2 resolution = distance * (overlap / distance.Length());
                            if (item.Fixed && !character.Fixed)
                            {
                                character.move -= resolution;    
                            }
                            else if(!item.Fixed && character.Fixed)
                            {
                                item.move += resolution;
                            }
                            else if(!item.Fixed && !character.Fixed)
                            {
                                float totalMass = item.Mass + character.Mass;
                                character.move -= resolution * (item.Mass / totalMass);
                                item.move += resolution * (character.Mass / totalMass);
                            }
                        }

                    }
                }

                foreach (var item in area.Items.ToArray())
                {
                    bool collision = false;
                    int loops = 0;

                    do
                    {
                        Vector2 position = item.Position + item.move;
                        int minCellX = (int)(position.X - item.Radius);
                        int maxCellX = (int)(position.X + item.Radius);
                        int minCellY = (int)(position.Y - item.Radius);
                        int maxCellY = (int)(position.Y + item.Radius);

                        collision = false;
                        float minImpact = 2f;
                        int minAxis = 0;

                        for (int x = minCellX; x <= maxCellX; x++)
                        {
                            for (int y = minCellY; y <= maxCellY; y++)
                            {
                                if (!area.IsCellBlocked(x, y)) continue;

                                if (position.X - item.Radius > x + 1 ||
                                    position.X + item.Radius < x ||
                                    position.Y - item.Radius > y + 1 ||
                                    position.Y + item.Radius < y)
                                    continue;

                                collision = true;

                                float diffX = float.MaxValue;
                                if (item.move.X > 0) diffX = position.X + item.Radius - x + gap;
                                if (item.move.X < 0) diffX = position.X - item.Radius - (x + 1) - gap;
                                float impactX = 1f - (diffX / item.move.X);

                                float diffY = float.MaxValue;
                                if (item.move.Y > 0) diffY = position.Y + item.Radius - y + gap;
                                if (item.move.Y < 0) diffY = position.Y - item.Radius - (y + 1) - gap;
                                float impactY = 1f - (diffY / item.move.Y);

                                int axis = 0;
                                float impact = 0;

                                if (impactX > impactY)
                                {
                                    axis = 1;
                                    impact = impactX;
                                }
                                else
                                {
                                    axis = 2;
                                    impact = impactY;
                                }

                                //
                                if (impact < minImpact)
                                {
                                    minImpact = impact;
                                    minAxis = axis;
                                }
                            }
                        }

                        if (collision)
                        {
                            if (minAxis == 1) item.move *= new Vector2(minImpact, 1f);
                            if (minAxis == 2) item.move *= new Vector2(1f, minImpact);
                        }
                        loops++;
                    } while (collision && loops < 2);

                        item.Position += item.move;
                        item.move = Vector2.Zero;
                }
            }

            #endregion

            base.Update(gameTime);
		}

        private Area LoadFromJson(string name)
        {
            string rootPath = Path.Combine(Environment.CurrentDirectory, "Maps");
            using (Stream stream = File.OpenRead(rootPath + "//" + name + ".json"))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    string json = sr.ReadToEnd();

                    FileArea result = JsonConvert.DeserializeObject<FileArea>(json);

                    Area area = new Area(result.layers.Length, result.width, result.height);
                    area.Name = name;

                    area.Background = new Color(128, 128, 128);
                    if(!string.IsNullOrEmpty(result.backgroundcolor))
                    {
                        area.Background = new Color(
                            Convert.ToInt32(result.backgroundcolor.Substring(1, 2), 16),
                            Convert.ToInt32(result.backgroundcolor.Substring(3, 2), 16),
                            Convert.ToInt32(result.backgroundcolor.Substring(5, 2), 16)
                        );
                    }

                    for (int i = 0; i < result.tilesets.Length; i++)
                    {
                        FileTileset tileset = result.tilesets[i];

                        int start = tileset.firstgid;
                        int perRow = tileset.imagewidth / tileset.tilewidth;
                        int width = tileset.tilewidth;

                        for (int j = 0; j < tileset.tilecount; j++)
                        {
                            int x = j % perRow;
                            int y = j / perRow;

                            bool block = false;
                            if(tileset.tileproperties != null)
                            {
                                FileTileProperty property;
                                if (tileset.tileproperties.TryGetValue(j, out property))
                                    block = property.Block;
                            }

                            Tile tile = new Tile()
                            {
                                Texture = tileset.image,
                                SourceRectangle = new Rectangle(x * width, y * width, width, width),
                                Blocked = block
                            };

                            area.Tiles.Add(start + j, tile);
                        }
                    }

                    for (int l = 0; l < result.layers.Length; l++)
                    {
                        FileLayer layer = result.layers[l];

                        area.Layers[l].Name = layer.name;

                        for (int i = 0; i < layer.data.Length; i++)
                        {
                            int x = i % area.Width;
                            int y = i / area.Height;
                            area.Layers[l].Tiles[x, y] = layer.data[i];
                        }
                    }

                    return area;
                }
            }
        }

        private class FileArea
        {
            public string backgroundcolor
            {
                get;
                set;
            }

            public int width
            {
                get;
                set;
            }

            public int height
            {
                get;
                set;
            }

            public FileLayer[] layers
            {
                get;
                set;
            }

            public FileTileset[] tilesets
            {
                get;
                set;
            }
        }

        private class FileLayer
        {
            public int[] data { get; set; }
            public string name { get; set; }
        }

        private class FileTileset
        {
            public int firstgid { get; set; }

            public string image { get; set; }

            public int tilewidth { get; set; }

            public int imagewidth { get; set; }

			public int tilecount { get; set; }

            public Dictionary<int, FileTileProperty> tileproperties { get; set; }
        }

        private class FileTileProperty
        {
            public bool Block { get; set; }
        }
    }
}

