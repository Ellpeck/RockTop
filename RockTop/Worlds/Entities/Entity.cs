using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;
using MLEM.Startup;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using RockTop.Worlds.Tiles;

namespace RockTop.Worlds.Entities {
    public class Entity {

        public static readonly Texture2D Texture = MlemGame.LoadContent<Texture2D>("Textures/Entities");
        public static readonly TextureRegion2D Shadow = new TextureRegion2D(Texture, Tile.Size, 0, Tile.Size, Tile.Size);

        public readonly World World;
        public Vector2 Position;
        public RectangleF Bounds;
        public bool Dead;

        public Entity(World world) {
            this.World = world;
        }

        public virtual void Update(GameTime time) {
        }

        public virtual void Draw(GameTime time, SpriteBatch batch) {
        }

        public RectangleF GetCurrBounds(Vector2 position) {
            var bounds = this.Bounds;
            bounds.Offset(position);
            return bounds;
        }

        public bool IsSomethingInTheWay(Vector2 position) {
            var bounds = this.GetCurrBounds(position);
            for (var x = bounds.Left.Floor(); x < bounds.Right.Ceil(); x++) {
                for (var y = bounds.Top.Floor(); y < bounds.Bottom.Ceil(); y++) {
                    if (this.World.IsOutOfBounds(x, y))
                        return true;
                    if (!this.World[x, y].CanWalkOn)
                        return true;
                }
            }

            return this.World.GetEntities(bounds, this).Any();
        }

    }
}