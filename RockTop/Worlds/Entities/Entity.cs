using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;
using MLEM.Startup;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using RockTop.Worlds.Tiles;

namespace RockTop.Worlds.Entities {
    public abstract class Entity {

        public static readonly Texture2D Texture = MlemGame.LoadContent<Texture2D>("Textures/Entities");
        public static readonly TextureRegion2D Shadow = new TextureRegion2D(Texture, 0, 0, Tile.Size, Tile.Size);

        public readonly World World;
        public readonly bool CanUpdate;
        public Vector2 Position;
        public RectangleF Bounds;
        public RectangleF VisualBounds;
        public bool Dead;

        public Entity(World world, bool canUpdate) {
            this.World = world;
            this.CanUpdate = canUpdate;
        }

        public virtual void Update(GameTime time) {
        }

        public virtual void Draw(GameTime time, SpriteBatch batch) {
        }

        public RectangleF GetCurrBounds(Vector2 position, bool visual = false) {
            var bounds = visual && !this.VisualBounds.IsEmpty ? this.VisualBounds : this.Bounds;
            bounds.Offset(position);
            return bounds;
        }

        public float GetRenderDepth(float offset = 0) {
            return (this.Position.Y + offset) / this.World.Height;
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