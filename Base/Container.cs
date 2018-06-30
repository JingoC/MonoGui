using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGuiFramework.Base
{
    public class Container : Region
    {
        private Vector2 position = new Vector2(0, 0);

        public List<Region> Items { get; private set; } = new List<Region>();

        public override int Width
        {
            get
            {
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
            protected set { }
        }

        public override int Height
        {
            get
            {
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
            protected set { }
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

            this.Items.ForEach(x => x.Designer());
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (this.Visible)
            {
                List<int> orders = new List<int>();
                foreach (var item in this.Items)
                {
                    if (!orders.Any(x => x == item.DrawOrder))
                        orders.Add(item.DrawOrder);
                }

                foreach (var z in orders.OrderBy(x => x))
                {
                    foreach (var item in this.Items)
                    {
                        if (z == item.DrawOrder)
                            item.Draw(gameTime);
                    }
                }
            }
        }
    }
}
