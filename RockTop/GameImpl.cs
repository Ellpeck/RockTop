using System;
using Microsoft.Xna.Framework;
using MLEM.Startup;
using RockTop.Worlds;
using RockTop.Worlds.Entities;
using MLEM.Cameras;

namespace RockTop {
    public class GameImpl : MlemGame {

        public World World;
        public Player Player;
        private Camera camera;

        protected override void LoadContent() {
            base.LoadContent();

            var rand = new Random();
            this.World = WorldGenerator.Generate(rand, 100, 100, rand.Next());
            this.Player = new Player(this.World);
            this.Player.Spawn();
            this.World.Entities.Add(this.Player);

            this.camera = new Camera(this.GraphicsDevice) {
                Scale = 80
            };
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
            this.World.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            this.GraphicsDevice.Clear(Color.Black);

            this.camera.LookAt(Vector2.Lerp(this.camera.LookingPosition, this.Player.Position, 0.1F));
            this.World.Draw(gameTime, this.SpriteBatch, this.camera);
        }

    }
}