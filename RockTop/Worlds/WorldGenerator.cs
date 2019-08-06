using System;
using MLEM.Noise;
using RockTop.Worlds.Tiles;

namespace RockTop.Worlds {
    public static class WorldGenerator {

        public static readonly Tile Grass = new AutoTile(0, 0, true);
        public static readonly Tile Water = new Tile(5, 0);

        public static World Generate(int width, int height) {
            var world = new World(width, height);

            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    var noise = Perlin.Generate(x / 20D, y / 20D, 0);
                    world[x, y] = noise <= 0.5F ? Water : Grass;
                }
            }

            return world;
        }

    }
}