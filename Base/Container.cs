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
        private Vector2 position = new Vector2(0, 0);

        public ObservableCollection<Region> Items = new ObservableCollection<Region>();
        //public List<Region> Items { get; private set; } = new List<Region>();

        public override int Width
        {
            get
            {
                if (this.Items.Count == 0)
                    return 1;

                float x1 = 100000000;
                float x2 = 0;
                foreach (var item in this.Items)
                {
                    float px = item.Position.X;
                    float pw = item.Width;

                    x1 = (px >= 0) && (px < x1) ? px : x1;
                    x2 = (pw > 0) && ((pw + px) > x2) ? pw + px : x2;
                }

                return (int)(x2 - x1);
            }
            protected set => base.Width = value;
        }
        
        public override int Height
        {
            get
            {
                if (this.Items.Count == 0)
                    return 1;

                float y1 = 100000000;
                float y2 = 0;

                foreach (var item in this.Items)
                {
                    float py = item.Position.Y;
                    float ph = item.Height;

                    y1 = (py >= 0) && (py < y1) ? py : y1;
                    y2 = (ph > 0) && ((ph + py) > y2) ? ph + py : y2;
                }

                return (int)(y2 - y1);
            }
            protected set => base.Height = value;
        }

        public override Vector2 Position
        {
            get => this.position;
            set
            {
                if (value != null)
                {
                    this.position = value;
                    foreach (var item in this.Items)
                    {
                        float x = this.position.X + item.Position.X;
                        float y = this.position.Y + item.Position.Y;
                        item.Position = new Vector2(x, y);
                    }
                }
            }
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
