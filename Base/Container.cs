using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGuiFramework.Base
{
    public class Container : RectangleRegion
    {
        private Position position = new Position();

        public ObservableCollection<Region> Items = new ObservableCollection<Region>();

        public override int Width
        {
            get
            {
                if (this.TextureScale == ScaleMode.None)
                {
                    return base.width;
                }
                else
                {
                    if (this.Items.Count == 0)
                        return 1;

                    float x1 = 100000000;
                    float x2 = 0;
                    foreach (var item in this.Items)
                    {
                        float px = item.Position.Absolute.X;
                        float pw = item.Width;

                        x1 = (px >= 0) && (px < x1) ? px : x1;
                        x2 = (pw > 0) && ((pw + px) > x2) ? pw + px : x2;
                    }

                    return (int)(x2 - x1);
                }
            }
            protected set
            {
                base.Width = value;
                if (this.TextureScale == ScaleMode.None)
                    this.IsRequireRendering = true;
            }
        }
        
        public override int Height
        {
            get
            {
                if (this.TextureScale == ScaleMode.None)
                {
                    return base.height;
                }
                else
                {
                    if (this.Items.Count == 0)
                        return 1;

                    float y1 = 100000000;
                    float y2 = 0;

                    foreach (var item in this.Items)
                    {
                        float py = item.Position.Absolute.Y;
                        float ph = item.Height;

                        y1 = (py >= 0) && (py < y1) ? py : y1;
                        y2 = (ph > 0) && ((ph + py) > y2) ? ph + py : y2;
                    }

                    return (int)(y2 - y1);
                }
            }
            protected set
            {
                base.Height = value;
                if (this.TextureScale == ScaleMode.None)
                    this.IsRequireRendering = true;
            }
        }

        protected virtual void UpdateBounds()
        {
            foreach (var item in this.Items)
            {
                float x = this.position.Absolute.X + item.Position.Relative.X;
                float y = this.position.Absolute.Y + item.Position.Relative.Y;
                item.Position.Absolute = new Vector2(x, y);
            }
        }

        public override void SetBounds(int x, int y, int width, int height)
        {
            base.SetBounds(x, y, width, height);
            this.UpdateBounds();
        }

        public Container(Region parent = null) : base(parent)
        {

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
            bool isEntry = false;

            if (this.Visible)
            {
                foreach (var item in this.GetItemsByOrder(true))
                {
                    isEntry = item.CheckEntry(x, y);
                    if (isEntry)
                        return true;
                }
            }

            return isEntry;
        }
    }
}
