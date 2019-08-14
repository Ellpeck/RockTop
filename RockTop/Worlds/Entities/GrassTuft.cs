using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using RockTop.Items;
using RockTop.Worlds.Tiles;

namespace RockTop.Worlds.Entities {
    public class GrassTuft : PunchableEntity {

        public GrassTuft(World world, Point point) : base(world, false, 2, new ItemStack(Item.Twig), 1) {
            this.Position = point.ToVector2() + Vector2.One / 2;
            this.Bounds = new RectangleF(-0.35F, -0.25F, 0.7F, 0.5F);
            this.VisualBounds = new RectangleF(-0.5F, -0.75F, 1, 1.25F);
        }

        public override bool CollidesWith(Entity other) {
            return false;
        }

        public override void Draw(GameTime time, SpriteBatch batch) {
            batch.Draw(Texture, this.Position - Vector2.One / 2 + new Vector2(0, -2) / Tile.Size, new Rectangle(2 * Tile.Size, 0, Tile.Size, Tile.Size), Color.White, 0, Vector2.Zero, Vector2.One / Tile.Size, SpriteEffects.None, this.GetRenderDepth(-1));
        }

    }
}