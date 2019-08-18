using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Misc;
using MLEM.Startup;
using MonoGame.Extended.TextureAtlases;
using RockTop.Items;
using RockTop.Worlds.Entities;

namespace RockTop.Worlds.Tiles {
    public class Tile {

        public static readonly Texture2D Texture = MlemGame.LoadContent<Texture2D>("Textures/Tiles");
        public const int Size = 8;

        public static readonly TileCreator Grass = Static(new Tile("Grass", 0, 0) {CanWalkOn = true, IsAutoTile = true});
        public static readonly TileCreator Water = Static(new Tile("Water", 5, 0));
        public static readonly TileCreator Rock = Static(new Tile("Rock", 5, 1) {CanWalkOn = true});
        public static readonly TileCreator RockWall = () => new PunchableTile("RockWall", 0, 1, 10, new ItemStack(Item.Rock), 1, Rock) {IsAutoTile = true};

        public readonly string Name;
        protected readonly TextureRegion2D TextureRegion;
        public bool CanWalkOn;
        public bool IsAutoTile;
        
        public Tile(string name, int textureX, int textureY) {
            this.Name = name;
            this.TextureRegion = new TextureRegion2D(Texture, textureX * Size, textureY * Size, Size, Size);
        }

        public virtual void Draw(SpriteBatch batch, World world, int x, int y) {
            if (this.IsAutoTile) {
                AutoTiling.DrawAutoTile(batch, new Vector2(x, y), Texture, this.TextureRegion.Bounds,
                    (offX, offY) => world.IsOutOfBounds(x + offX, y + offY) || world[x + offX, y + offY] == this, Color.White, scale: Vector2.One / Size);
            } else {
                batch.Draw(this.TextureRegion, new Rectangle(x, y, 1, 1), Color.White);
            }
        }

        public virtual bool OnAttacked(World world, int x, int y, Player player) {
            return false;
        }

        public static bool operator ==(Tile t1, Tile t2) {
            return Equals(t1, t2);
        }

        public static bool operator !=(Tile t1, Tile t2) {
            return !Equals(t1, t2);
        }

        public override bool Equals(object obj) {
            return ReferenceEquals(obj, this) || obj is Tile other && other.Name == this.Name;
        }

        public override int GetHashCode() {
            return this.Name.GetHashCode();
        }

        public delegate Tile TileCreator();

        public static TileCreator Static(Tile tile) {
            return () => tile;
        }

    }
}