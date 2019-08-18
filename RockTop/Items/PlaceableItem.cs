using Microsoft.Xna.Framework;
using RockTop.Worlds;
using RockTop.Worlds.Entities;

namespace RockTop.Items {
    public class PlaceableItem : Item {

        private readonly EntityCreator creator;

        public PlaceableItem(string name, Point textureCoord, int maxAmount, EntityCreator creator) : base(name, textureCoord, maxAmount) {
            this.creator = creator;
        }

        public override void OnInteractWith(Player player, ref ItemStack stack) {
            var moused = player.GetMousedPosition();
            if (player.CanReach(moused)) {
                var entity = this.creator(player.World, moused);
                if (entity != null && !entity.IsSomethingInTheWay(entity.Position)) {
                    player.World.Entities.Add(entity);
                    stack.Amount--;
                }
            }
        }

        public delegate Entity EntityCreator(World world, Vector2 position);

    }
}