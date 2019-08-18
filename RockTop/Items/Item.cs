using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Startup;
using MLEM.Textures;
using RockTop.Worlds.Entities;
using RockTop.Worlds.Tiles;

namespace RockTop.Items {
    public class Item {

        public static readonly Texture2D Texture = MlemGame.LoadContent<Texture2D>("Textures/Items");

        public static readonly Item Wood = new Item("Wood", new Point(0, 0), 10);
        public static readonly Item Twig = new Item("Twig", new Point(8, 0), 30);
        public static readonly Item Rock = new Item("Rock", new Point(16, 0), 20);
        public static readonly Item Workbench = new PlaceableItem("Workbench", new Point(24, 0), 1, (world, position) => new Workbench(world, position));

        public readonly string Name;
        public readonly TextureRegion TextureRegion;
        public readonly int MaxAmount;

        public Item(string name, Point textureCoord, int maxAmount) {
            this.Name = name;
            this.TextureRegion = new TextureRegion(Texture, new Rectangle(textureCoord, new Point(Tile.Size)));
            this.MaxAmount = maxAmount;
        }

        public virtual void OnInteractWith(Player player, ref ItemStack stack) {
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