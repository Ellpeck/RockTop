using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Cameras;
using MLEM.Font;
using MLEM.Startup;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;
using RockTop.Ui;
using RockTop.Worlds;
using RockTop.Worlds.Entities;

namespace RockTop {
    public class GameImpl : MlemGame {

        public static GameImpl Instance { get; private set; }
        public static SpriteFont Font;

        public World World;
        public Player Player;
        public Camera Camera;

        public GameImpl() {
            Instance = this;
            this.IsMouseVisible = true;
        }

        protected override void LoadContent() {
            base.LoadContent();

            var rand = new Random();
            this.World = WorldGenerator.Generate(rand, 100, 100, rand.Next());
            this.Player = new Player(this.World);
            this.Player.Spawn();
            this.World.Entities.Add(this.Player);

            this.Camera = new Camera(this.GraphicsDevice) {
                Scale = 80
            };

            Font = LoadContent<SpriteFont>("Fonts/Font");
            this.UiSystem.GlobalScale = 5;
            this.UiSystem.Style = new UntexturedStyle(this.SpriteBatch) {
                TextScale = 0.125F,
                Font = new GenericSpriteFont(Font)
            };

            var hotbar = new Group(Anchor.BottomCenter, new Vector2(this.Player.Inventory.Length * 15, 15), false) {
                PositionOffset = new Vector2(0, 5),
                IgnoresMouse = false
            };
            for (var i = 0; i < this.Player.Inventory.Length; i++) {
                hotbar.AddChild(new ItemSlot(Anchor.AutoInline, new Vector2(15, 15), this.Player.Inventory, i));
            }
            this.UiSystem.Add("Hotbar", hotbar);
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
            this.World.Update(gameTime);
        }

        protected override void DoDraw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.Black);

            this.Camera.LookingPosition = Vector2.Lerp(this.Camera.LookingPosition, this.Player.Position, 0.1F);
            this.Camera.ConstrainWorldBounds(Vector2.Zero, new Vector2(this.World.Width, this.World.Height));
            this.World.Draw(gameTime, this.SpriteBatch, this.Camera);

            base.DoDraw(gameTime);
        }

    }
}