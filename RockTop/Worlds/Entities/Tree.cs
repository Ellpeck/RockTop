using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using RockTop.Items;
using RockTop.Worlds.Tiles;

namespace RockTop.Worlds.Entities {
    public class Tree : Entity {

        private int durability = 3;

        public Tree(World world, Point point) : base(world, false) {
            this.Position = point.ToVector2() + Vector2.One / 2;
            this.Bounds = new RectangleF(-0.35F, -0.25F, 0.7F, 0.5F);
            this.VisualBounds = new RectangleF(-0.5F, -0.75F, 1, 1.25F);
        }

        public override bool OnInteractedWith(Player player) {
            this.durability--;
            if (this.durability <= 0) {
                this.World.Entities.Remove(this);
                for (var i = 0; i < 3; i++)
                    this.World.Entities.Add(new DroppedItem(this.World, new ItemStack(Item.Wood)) {
                        Position = this.Position,
                        Motion = new Vector2(this.World.Random.NextSingle(-0.1F, 0.1F), this.World.Random.NextSingle(-0.1F, 0.1F))
                    });
            }
            return true;
        }

        public override void Draw(GameTime time, SpriteBatch batch) {
            var pos = this.Position - Vector2.One / 2;
            batch.Draw(Shadow, pos + new Vector2(0, 0) / Tile.Size, Color.White, 0, Vector2.Zero, Vector2.One / Tile.Size, SpriteEffects.None, this.GetRenderDepth(-0.01F));
            batch.Draw(Texture, pos + new Vector2(0, -2) / Tile.Size, new Rectangle(Tile.Size, 0, Tile.Size, Tile.Size), Color.White, 0, Vector2.Zero, Vector2.One / Tile.Size, SpriteEffects.None, this.GetRenderDepth());
        }

    }
}