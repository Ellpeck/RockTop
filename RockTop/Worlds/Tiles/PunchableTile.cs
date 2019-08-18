using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;
using MLEM.Textures;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using RockTop.Items;
using RockTop.Worlds.Entities;

namespace RockTop.Worlds.Tiles {
    public class PunchableTile : Tile {

        protected static readonly TextureRegion[] CrackTextures = {
            new TextureRegion(Texture, 0, 16, 8, 8), new TextureRegion(Texture, 8, 16, 8, 8), new TextureRegion(Texture, 16, 16, 8, 8),
            new TextureRegion(Texture, 24, 16, 8, 8), new TextureRegion(Texture, 32, 16, 8, 8)
        };

        protected readonly int Durability;
        protected readonly ItemStack Drop;
        protected readonly int DropAmount;
        protected readonly TileCreator ReplacementTile;
        protected int CurrDurability;

        public PunchableTile(string name, int textureX, int textureY, int durability, ItemStack drop, int amount, TileCreator replacementTile) : base(name, textureX, textureY) {
            this.Durability = durability;
            this.CurrDurability = durability;
            this.Drop = drop;
            this.DropAmount = amount;
            this.ReplacementTile = replacementTile;
        }

        public override void Draw(SpriteBatch batch, World world, int x, int y) {
            base.Draw(batch, world, x, y);
            this.DrawCracks(batch, x, y);
        }

        protected void DrawCracks(SpriteBatch batch, int x, int y) {
            var crackIndex = ((1 - this.CurrDurability / (float) this.Durability) * CrackTextures.Length).Ceil();
            if (crackIndex > 0)
                batch.Draw(CrackTextures[crackIndex - 1], new Rectangle(x, y, 1, 1), Color.White);
        }

        public override bool OnAttacked(World world, int x, int y, Player player) {
            this.CurrDurability--;
            if (this.CurrDurability <= 0) {
                world[x, y] = this.ReplacementTile();
                for (var i = 0; i < this.DropAmount; i++)
                    world.Entities.Add(new DroppedItem(world, this.Drop) {
                        Position = new Vector2(x + 0.5F, y + 0.5F),
                        Motion = new Vector2(world.Random.NextSingle(-0.1F, 0.1F), world.Random.NextSingle(-0.1F, 0.1F))
                    });
            }
            return true;
        }

    }
}