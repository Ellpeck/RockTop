using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using RockTop.Items;
using RockTop.Worlds.Tiles;

namespace RockTop.Worlds.Entities {
    public class Workbench : PunchableEntity {

        public Workbench(World world, Vector2 pos) : base(world, false, 5, new ItemStack(Item.Workbench), 1) {
            this.Position = pos.Floor() + new Vector2(0, 0.5F);
            this.Bounds = new RectangleF(-1, -0.5F, 2, 0.5F);
            this.VisualBounds = new RectangleF(-1, -0.75F, 2, 1.25F);
        }

        public override void Draw(GameTime time, SpriteBatch batch) {
            var pos = this.Position - new Vector2(1, 0.5F);
            batch.Draw(Shadow, pos + new Vector2(0, -1) / Tile.Size, Color.White, 0, Vector2.Zero, new Vector2(2, 1) / Tile.Size, SpriteEffects.None, this.GetRenderDepth(-0.01F));
            batch.Draw(Texture, pos + new Vector2(0, -2) / Tile.Size, new Rectangle(Tile.Size * 3, 0, Tile.Size * 2, Tile.Size), Color.White, 0, Vector2.Zero, Vector2.One / Tile.Size, SpriteEffects.None, this.GetRenderDepth());
        }

    }
}