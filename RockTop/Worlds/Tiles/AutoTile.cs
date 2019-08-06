using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Misc;

namespace RockTop.Worlds.Tiles {
    public class AutoTile : Tile {

        public AutoTile(int textureX, int textureY, bool canWalkOn = false) : base(textureX, textureY, canWalkOn) {
        }

        public override void Draw(SpriteBatch batch, World world, int x, int y) {
            AutoTiling.DrawAutoTile(batch, new Vector2(x, y), Texture, this.TextureRegion.Bounds,
                (offX, offY) => world.IsOutOfBounds(x + offX, y + offY) || world[x + offX, y + offY] == this, Color.White, scale: Vector2.One / Size);
        }

    }
}