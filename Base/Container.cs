using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGuiFramework.Base
{
    public class Container : Region
    {
        public List<Region> Items = new List<Region>();

        public bool Scrollable { get; set; } = false;

        public override int MaxHeight { get => this.TextureScale == ScaleMode.None ? this.Height : base.MaxHeight; set => base.MaxHeight = value; }
        public override int MaxWidth { get => this.TextureScale == ScaleMode.None ? this.Width : base.MaxWidth; set => base.MaxWidth = value; }

        public override int Width
        {
            get
            {
                if (this.TextureScale == ScaleMode.None)
                {
                    return base.Width;
                }
                else
                {
                    if (this.Items.Count == 0)
                        return 10;

                    float x1 = 100000000;
                    float x2 = 0;
                    foreach (var item in this.Items)
                    {
                        float px = item.Position.Absolute.X;
                        float pw = item.Width;

                        x1 = (px >= 0) && (px < x1) ? px : x1;
                        x2 = (pw > 0) && ((pw + px) > x2) ? pw + px : x2;
                    }

                    var pos = this.Position.Absolute;
                    x1 = x1 > pos.X ? pos.X : x1;

                    return (int)(x2 - x1);
                }
            }
            protected set
            {
                base.Width = value;
                if (this.TextureScale != ScaleMode.None)
                    this.IsRequireRendering = false;
            }
        }

        public override int Height
        {
            get
            {
                if (this.TextureScale == ScaleMode.None)
                {
                    return base.Height;
                }
                else
                {
                    if (this.Items.Count == 0)
                        return 10;

                    float y1 = 100000000;
                    float y2 = 0;

                    foreach (var item in this.Items)
                    {
                        float py = item.Position.Absolute.Y;
                        float ph = item.Height;

                        y1 = (py >= 0) && (py < y1) ? py : y1;
                        y2 = (ph > 0) && ((ph + py) > y2) ? ph + py : y2;
                    }

                    var pos = this.Position.Absolute;
                    y1 = y1 > pos.Y ? pos.Y : y1;
                    return (int)(y2 - y1);
                }
            }
            protected set
            {
                base.Height = value;
                if (this.TextureScale == ScaleMode.None)
                {
                    this.IsRequireRendering = true;
                }
            }
        }

        public Container(Region parent = null) : base(parent)
        {

        }

        public virtual void UpdateBounds()
        {
            foreach (var item in this.Items)
            {
                float x = 0;
                float y = 0;

                if (item.IsAlign(AlignmentType.Left)) { x = this.Position.Absolute.X + item.Position.Relative.X; }
                else if (item.IsAlign(AlignmentType.Center)) { x = (this.Position.Absolute.X + this.MaxWidth / 2) + (item.Position.Relative.X - item.Width / 2); }
                else if (item.IsAlign(AlignmentType.Right)) { x = this.Position.Absolute.X + this.MaxWidth - item.Width - item.Position.Relative.X; }
                else { x = this.Position.Absolute.X + item.Position.Relative.X; }

                if (item.IsAlign(AlignmentType.Top)) { y = this.Position.Absolute.Y + item.Position.Relative.Y; }
                else if (item.IsAlign(AlignmentType.Middle)) { y = (this.Position.Absolute.Y + this.MaxHeight / 2) + (item.Position.Relative.Y - item.Height / 2); }
                else if (item.IsAlign(AlignmentType.Bottom)) { y = this.Position.Absolute.Y + this.MaxHeight - item.Height - item.Position.Relative.Y; }
                else { y = this.Position.Absolute.Y + item.Position.Relative.Y; }

                item.SetAbsolute((int)x, (int)y);

                if (item is Container)
                    (item as Container).UpdateBounds();
            }
        }

        public override float Scale { get => base.Scale; set { base.Scale = value; this.Items.ForEach(x => x.Scale = value); } }

        public override void SetBounds(int x, int y, int width, int height)
        {
            base.SetBounds(x, y, width, height);
            this.UpdateBounds();
        }
        
        public override void Designer()
        {
            base.Designer();

            foreach(var item in this.Items) item.Designer();
        }

        private List<Region> GetItemsByOrder(bool descending)
        {
            return descending ? this.Items.OfType<Region>().OrderByDescending(x => x.DrawOrder).ToList<Region>() :
                this.Items.OfType<Region>().OrderBy(x => x.DrawOrder).ToList<Region>();
        }

        protected override void Render()
        {
            base.Render();

            if (this.Items.Any(x => x.IsRequireRendering))
                this.UpdateBounds();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (this.Visible)
            {
                foreach (var item in this.GetItemsByOrder(false))
                {
                    item.Draw(gameTime);
                }
            }
        }
        
        public override bool CheckEntry(float x, float y)
        {
            if (this.Visible)
            {
                foreach (var item in this.GetItemsByOrder(true))
                {
                    if (item.CheckEntry(x, y))
                        return true;
                }
            }

            return false;
        }
    }
}
