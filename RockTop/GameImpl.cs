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

            this.World = WorldGenerator.Generate(50, 50);
            this.Player = new Player(this.World);
            this.Player.Position = new Vector2(25, 25);
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

            this.camera.LookAt(Vector2.Lerp(this.camera.LookingPosition, this.Player.Position, 0.15F));
            this.World.Draw(gameTime, this.SpriteBatch, this.camera);
        }

    }
}