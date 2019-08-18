using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using RockTop.Items;

namespace RockTop.Worlds.Entities {
    public class PunchableEntity : Entity {

        protected int Durability;
        protected readonly ItemStack Drop;
        protected readonly int DropAmount;

        public PunchableEntity(World world, bool canUpdate, int durability, ItemStack drop, int amount) : base(world, canUpdate) {
            this.Durability = durability;
            this.Drop = drop;
            this.DropAmount = amount;
        }

        public override bool OnAttacked(Player player) {
            this.Durability--;
            if (this.Durability <= 0) {
                this.World.Entities.Remove(this);
                for (var i = 0; i < this.DropAmount; i++)
                    this.World.Entities.Add(new DroppedItem(this.World, this.Drop) {
                        Position = this.Position,
                        Motion = new Vector2(this.World.Random.NextSingle(-0.1F, 0.1F), this.World.Random.NextSingle(-0.1F, 0.1F))
                    });
            }
            return true;
        }

    }
}