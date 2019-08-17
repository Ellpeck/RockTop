using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extended.Extensions;
using MLEM.Extensions;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using RockTop.Worlds.Entities;
using RockTop.Worlds.Tiles;
using Camera = MLEM.Cameras.Camera;

namespace RockTop.Worlds {
    public class World {

        public readonly int Width;
        public readonly int Height;
        public readonly Random Random;
        private readonly Tile[,] tiles;

        public readonly ObservableCollection<Entity> Entities = new ObservableCollection<Entity>();
        private readonly List<Entity> updatingEntities = new List<Entity>();

        public Tile this[int x, int y] {
            get => this.IsOutOfBounds(x, y) ? null : this.tiles[x, y];
            set => this.tiles[x, y] = value;
        }

        public World(int width, int height, Random random) {
            this.tiles = new Tile[width, height];
            this.Width = width;
            this.Height = height;
            this.Random = random;

            for (var x = 0; x < this.Width; x++) {
                for (var y = 0; y < this.Height; y++) {
                    this[x, y] = Tile.Grass();
                }
            }

            this.Entities.ItemAdded += (sender, args) => {
                if (args.Item.CanUpdate)
                    this.updatingEntities.Add(args.Item);
            };
            this.Entities.ItemRemoved += (sender, args) => {
                if (args.Item.CanUpdate)
                    this.updatingEntities.Remove(args.Item);
            };
        }

        public void Update(GameTime time) {
            for (var i = this.updatingEntities.Count - 1; i >= 0; i--) {
                var entity = this.updatingEntities[i];
                entity.Update(time);
                if (entity.Dead)
                    this.Entities.Remove(entity);
            }
        }

        public void Draw(GameTime time, SpriteBatch batch, Camera camera) {
            var frustum = camera.GetVisibleRectangle();
            var minX = Math.Max(0, frustum.Left).Floor();
            var minY = Math.Max(0, frustum.Top).Floor();
            var maxX = Math.Min(this.Width, frustum.Right).Ceil();
            var maxY = Math.Min(this.Height, frustum.Bottom).Ceil();

            batch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, transformMatrix: camera.ViewMatrix);
            for (var x = minX; x < maxX; x++) {
                for (var y = minY; y < maxY; y++) {
                    this[x, y].Draw(batch, this, x, y);
                }
            }
            batch.End();
            batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, transformMatrix: camera.ViewMatrix);
            foreach (var entity in this.Entities)
                if (frustum.Intersects(entity.GetCurrBounds(entity.Position, true)))
                    entity.Draw(time, batch);
            batch.End();
        }

        public IEnumerable<Entity> GetEntities(RectangleF rectangle, params Entity[] excluded) {
            foreach (var entity in this.Entities) {
                if (entity.GetCurrBounds(entity.Position).Intersects(rectangle) && !excluded.Contains(entity))
                    yield return entity;
            }
        }

        public bool IsOutOfBounds(int x, int y) {
            return x < 0 || y < 0 || x >= this.Width || y >= this.Height;
        }

    }
}