using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MLEM.Animations;
using MLEM.Startup;
using MLEM.Textures;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using RockTop.Worlds.Tiles;

namespace RockTop.Worlds.Entities {
    public class Player : Entity {

        private readonly SpriteAnimationGroup animations;
        public bool IsMoving;
        public int Direction;

        public Player(World world) : base(world, true) {
            this.Bounds = new RectangleF(-0.375F, -0.2F, 0.75F, 0.75F);

            this.animations = new SpriteAnimationGroup();
            // standing
            this.animations.Add(new SpriteAnimation(1, Texture, new Rectangle(0, 8, 8, 8)), () => !this.IsMoving && this.Direction == 0);
            this.animations.Add(new SpriteAnimation(1, Texture, new Rectangle(8, 8, 8, 8)), () => !this.IsMoving && this.Direction == 1);
            this.animations.Add(new SpriteAnimation(1, Texture, new Rectangle(16, 8, 8, 8)), () => !this.IsMoving && this.Direction == 2);
            this.animations.Add(new SpriteAnimation(1, Texture, new Rectangle(24, 8, 8, 8)), () => !this.IsMoving && this.Direction == 3);
            // moving
            const float speed = 0.15F;
            this.animations.Add(new SpriteAnimation(speed, Texture, new Rectangle(0, 8, 8, 8), new Rectangle(0, 16, 8, 8), new Rectangle(0, 24, 8, 8), new Rectangle(0, 32, 8, 8)), () => this.IsMoving && this.Direction == 0);
            this.animations.Add(new SpriteAnimation(speed, Texture, new Rectangle(8, 8, 8, 8), new Rectangle(8, 16, 8, 8), new Rectangle(8, 24, 8, 8), new Rectangle(8, 32, 8, 8)), () => this.IsMoving && this.Direction == 1);
            this.animations.Add(new SpriteAnimation(speed, Texture, new Rectangle(16, 8, 8, 8), new Rectangle(16, 16, 8, 8), new Rectangle(16, 24, 8, 8), new Rectangle(16, 32, 8, 8)), () => this.IsMoving && this.Direction == 2);
            this.animations.Add(new SpriteAnimation(speed, Texture, new Rectangle(24, 8, 8, 8), new Rectangle(24, 16, 8, 8), new Rectangle(24, 24, 8, 8), new Rectangle(24, 32, 8, 8)), () => this.IsMoving && this.Direction == 3);
        }

        public override void Update(GameTime time) {
            var move = new Vector2();
            var input = MlemGame.Input;
            if (input.IsKeyDown(Keys.Left)) {
                move.X--;
                this.Direction = 2;
            }
            if (input.IsKeyDown(Keys.Right)) {
                move.X++;
                this.Direction = 3;
            }
            if (input.IsKeyDown(Keys.Up)) {
                move.Y--;
                this.Direction = 1;
            }
            if (input.IsKeyDown(Keys.Down)) {
                move.Y++;
                this.Direction = 0;
            }
            this.IsMoving = move.X != 0 || move.Y != 0;
            move *= 0.07F;

            var pos = this.Position;
            if (!this.IsSomethingInTheWay(pos + new Vector2(move.X, 0)))
                pos.X += move.X;
            if (!this.IsSomethingInTheWay(pos + new Vector2(0, move.Y)))
                pos.Y += move.Y;
            this.Position = pos;

            this.animations.Update(time);
        }

        public override void Draw(GameTime time, SpriteBatch batch) {
            var pos = this.Position - Vector2.One / 2;
            batch.Draw(Shadow, pos + new Vector2(0.5F, 2) / Tile.Size, Color.White, 0, Vector2.Zero, Vector2.One / Tile.Size, SpriteEffects.None, this.GetRenderDepth(-0.01F));
            batch.Draw(this.animations.CurrentRegion, pos, Color.White, 0, Vector2.Zero, Vector2.One / Tile.Size, SpriteEffects.None, this.GetRenderDepth());
        }

        public void Spawn() {
            var radius = 3;
            var center = new Point(this.World.Width / 2, this.World.Height / 2);
            Point spawn;
            do {
                spawn = center + new Point(this.World.Random.Next(-radius, radius), this.World.Random.Next(-radius, radius));
                if (radius < Math.Min(this.World.Width, this.World.Height) / 2)
                    radius += 2;
            } while (this.IsSomethingInTheWay(spawn.ToVector2()));
            this.Position = spawn.ToVector2();
        }

    }
}