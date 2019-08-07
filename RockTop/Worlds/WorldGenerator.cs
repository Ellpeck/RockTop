using System;
using Microsoft.Xna.Framework;
using MLEM.Noise;
using MonoGame.Extended;
using RockTop.Worlds.Entities;
using RockTop.Worlds.Tiles;

namespace RockTop.Worlds {
    public static class WorldGenerator {

        public static readonly Tile Grass = new AutoTile(0, 0, true);
        public static readonly Tile Water = new Tile(5, 0);

        public static World Generate(Random random, int width, int height, int seed) {
            var world = new World(width, height, random);

            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    var noise = Perlin.Generate(x / 50D, y / 50D, seed) / 2;
                    noise += Perlin.Generate(x / 10D, y / 10D, seed) / 4;
                    noise += Perlin.Generate(x / 30D, y / 30D, seed);

                    if (noise >= 0.8) {
                        world[x, y] = Water;
                    } else {
                        world[x, y] = Grass;

                        if (world.Random.NextSingle() >= 0.75F)
                            world.Entities.Add(new Tree(world, new Point(x, y)));
                    }
                }
            }

            return world;
        }

    }
}