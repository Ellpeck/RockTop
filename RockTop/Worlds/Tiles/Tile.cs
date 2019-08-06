using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Startup;
using MonoGame.Extended.TextureAtlases;

namespace RockTop.Worlds.Tiles {
    public class Tile {

        public static readonly Texture2D Texture = MlemGame.LoadContent<Texture2D>("Textures/Tiles");
        public const int Size = 8;

        protected readonly TextureRegion2D TextureRegion;
        public readonly bool CanWalkOn;

        public Tile(int textureX, int textureY, bool canWalkOn = false) {
            this.TextureRegion = new TextureRegion2D(Texture, textureX * Size, textureY * Size, Size, Size);
            this.CanWalkOn = canWalkOn;
        }

        public virtual void Draw(SpriteBatch batch, World world, int x, int y) {
            batch.Draw(this.TextureRegion, new Rectangle(x, y, 1, 1), Color.White);
        }

    }
}