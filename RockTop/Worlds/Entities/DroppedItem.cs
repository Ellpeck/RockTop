using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Textures;
using MonoGame.Extended;
using RockTop.Items;
using RockTop.Worlds.Tiles;

namespace RockTop.Worlds.Entities {
    public class DroppedItem : Entity {

        public ItemStack Stack;
        public Vector2 Motion;

        public DroppedItem(World world, ItemStack stack) : base(world, true) {
            this.Stack = stack;
            this.Bounds = new RectangleF(-0.25F, -0.25F, 0.5F, 0.5F);
        }

        public override bool CollidesWith(Entity other) {
            return !(other is Player) && !(other is DroppedItem);
        }

        public override void Update(GameTime time) {
            base.Update(time);
            if (!this.IsSomethingInTheWay(this.Position + this.Motion))
                this.Position += this.Motion;
            this.Motion *= 0.8F;
        }

        public override void Draw(GameTime time, SpriteBatch batch) {
            batch.Draw(this.Stack.Item.TextureRegion, this.Position - Vector2.One / 4, Color.White, 0, Vector2.Zero, Vector2.One / 2 / Tile.Size, SpriteEffects.None, this.GetRenderDepth(-1));
        }

    }
}