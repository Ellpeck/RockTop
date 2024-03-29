using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;
using MLEM.Font;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;
using MonoGame.Extended;
using RockTop.Items;

namespace RockTop.Ui {
    public class ItemSlot : Element {

        public readonly ItemStack[] Inventory;
        public readonly int Index;
        private IGenericFont font;
        private float textScale;

        public ItemSlot(Anchor anchor, Vector2 size, ItemStack[] inventory, int index) : base(anchor, size) {
            this.Inventory = inventory;
            this.Index = index;
            this.Padding = new Point(3);

            var tooltip = new Tooltip(100, "") {Padding = new Point(2, 0)};
            this.OnMouseEnter += element => {
                var item = this.Inventory[this.Index];
                if (!item.IsEmpty()) {
                    tooltip.Text = item.Item.Name;
                    this.System.Add("ItemSlotTooltip", tooltip);
                }
            };
            this.OnMouseExit += element => this.System.Remove("ItemSlotTooltip");
        }

        public override void Draw(GameTime time, SpriteBatch batch, float alpha, Point offset) {
            var item = this.Inventory[this.Index];
            if (!item.IsEmpty()) {
                batch.Draw(item.Item.TextureRegion, this.DisplayArea.OffsetCopy(offset), Color.White * alpha);
                this.font.DrawCenteredString(batch, item.Amount.ToString(),
                    (this.DisplayArea.Location + offset + this.DisplayArea.Size).ToVector2(),
                    this.textScale * this.Scale, Color.White * alpha, true, true);

                if (this.IsSelected) {
                    batch.DrawRectangle(this.Area, Color.Red, this.Scale);
                }
            }
            base.Draw(time, batch, alpha, offset);
        }

        protected override void InitStyle(UiStyle style) {
            base.InitStyle(style);
            this.font = style.Font;
            this.textScale = style.TextScale;
        }

    }
}