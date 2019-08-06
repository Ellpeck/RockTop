using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MLEM.Extensions;
using MLEM.Startup;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using RockTop.Worlds.Tiles;

namespace RockTop.Worlds.Entities {
    public class Player : Entity {

        public Player(World world) : base(world) {
            this.Bounds = new RectangleF(-0.5F, -0.5F, 1, 1);
        }

        public override void Update(GameTime time) {
            var move = new Vector2();
            var keyboard = MlemGame.Keyboard;
            if (keyboard.IsKeyDown(Keys.Left))
                move.X--;
            if (keyboard.IsKeyDown(Keys.Right))
                move.X++;
            if (keyboard.IsKeyDown(Keys.Up))
                move.Y--;
            if (keyboard.IsKeyDown(Keys.Down))
                move.Y++;
            move *= 0.07F;

            var pos = this.Position;
            if (!this.IsSomethingInTheWay(pos + new Vector2(move.X, 0)))
                pos.X += move.X;
            if (!this.IsSomethingInTheWay(pos + new Vector2(0, move.Y)))
                pos.Y += move.Y;
            this.Position = pos;
        }

        public override void Draw(GameTime time, SpriteBatch batch) {
            var pos = this.Position - Vector2.One / 2;
            batch.Draw(Shadow, pos + new Vector2(0.5F, 2) / Tile.Size, Color.White, 0, Vector2.Zero, Vector2.One / Tile.Size, SpriteEffects.None, 0);
            batch.Draw(Texture, pos, new Rectangle(0, 0, Tile.Size, Tile.Size), Color.White, 0, Vector2.Zero, Vector2.One / Tile.Size, SpriteEffects.None, 0);
        }

    }
}