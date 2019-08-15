using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Startup;
using MLEM.Textures;
using RockTop.Worlds.Tiles;

namespace RockTop.Items {
    public class Item {

        public static readonly Texture2D Texture = MlemGame.LoadContent<Texture2D>("Textures/Items");

        public static readonly Item Wood = new Item("Wood", new Point(0, 0), 5);
        public static readonly Item Twig = new Item("Twig", new Point(8, 0), 20);

        public readonly string Name;
        public readonly TextureRegion TextureRegion;
        public readonly int MaxAmount;

        public Item(string name, Point textureCoord, int maxAmount = 10) {
            this.Name = name;
            this.TextureRegion = new TextureRegion(Texture, new Rectangle(textureCoord, new Point(Tile.Size)));
            this.MaxAmount = maxAmount;
        }

    }

    public struct ItemStack {

        public readonly Item Item;
        public int Amount;

        public ItemStack(Item item, int amount = 1) {
            this.Item = item;
            this.Amount = amount;
        }

        public bool IsEmpty() {
            return this.Item == null || this.Amount <= 0;
        }

    }
}