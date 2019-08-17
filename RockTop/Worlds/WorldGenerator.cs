using System;
using Microsoft.Xna.Framework;
using MLEM.Noise;
using MonoGame.Extended;
using RockTop.Worlds.Entities;
using RockTop.Worlds.Tiles;

namespace RockTop.Worlds {
    public static class WorldGenerator {

        public static World GenerateOverworld(Random random, int width, int height, int seed) {
            var world = new World(width, height, random);

            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    var noise = Perlin.Generate(x / 50D, y / 50D, seed) / 2;
                    noise += Perlin.Generate(x / 10D, y / 10D, seed) / 4;
                    noise += Perlin.Generate(x / 30D, y / 30D, seed);

                    if (noise >= 0.8) {
                        world[x, y] = Tile.Water();
                    } else {
                        world[x, y] = Tile.Grass();

                        if (random.NextSingle() >= 0.75F)
                            world.Entities.Add(new Tree(world, new Point(x, y)));
                        else if (random.NextSingle() >= 0.5F)
                            world.Entities.Add(new GrassTuft(world, new Point(x, y)));
                    }
                }
            }

            return world;
        }

        public static World GenerateCaves(Random random, int width, int height, int seed) {
            var world = new World(width, height, random);

            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    var noise = Perlin.Generate(x / 30D, y / 30D, seed) / 2;
                    noise += Perlin.Generate(x / 5D, y / 5D, seed) / 4;
                    noise += Perlin.Generate(x / 15D, y / 15D, seed);

                    if (noise >= 0.725) {
                        world[x, y] = Tile.RockWall();
                    } else {
                        world[x, y] = Tile.Rock();
                    }
                }
            }

            return world;
        }

    }
}