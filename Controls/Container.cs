using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGuiFramework.System;

namespace MonoGuiFramework.Controls
{
    public class Container : IControl
    {
        Vector2 position;

        public string Name { get; set; }
        public TextureContainer TextureManager { get; set; }
        public List<IControl> Items { get; set; } = new List<IControl>();
        public Color Background { get; set; } = Color.Transparent;
        public int ZIndex { get; set; } = 0;

        public virtual int Width
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

                return (int) (x2 - x1);
            }
            set { }
        }

        public virtual int Height
        {
            get
            {
                float y1 = 100000000;
                float y2 = 0;
                
                foreach(var item in this.Items)
                {
                    float py = item.Position.Y;
                    float ph = item.Height;

                    y1 = (py >= 0) && (py < y1) ? py : y1;
                    y2 = (ph > 0) && ((ph + py) > y2) ? ph + py : y2;
                }

                return (int) (y2 - y1);
            }
            set { }
        }

        public Vector2 Position
        {
            get => this.position;
            set
            {
                if (value != null)
                {
                    this.position = value;
                    foreach(var item in this.Items)
                    {
                        float x = this.position.X + item.Position.X;
                        float y = this.position.Y + item.Position.Y;
                        item.Position = new Vector2(x, y);
                    }
                }
            }
        }

        public int DrawOrder { get; set; }

        public bool Visible { get; set; } = true;

        public event EventHandler OnClick;
        public event EventHandler OnPressed;
        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public Container()
        {
            this.TextureManager = new TextureContainer();
        }

        public virtual void Designer()
        {
            this.Items.ForEach(x => x.Designer());
        }

        public virtual void Draw(GameTime gameTime)
        {
            if (this.Visible)
            {
                List<int> zIndex = new List<int>();
                foreach(var item in this.Items)
                {
                    if (!zIndex.Any(x => x == item.ZIndex))
                        zIndex.Add(item.ZIndex);
                }

                foreach (var z in zIndex.OrderBy(x => x))
                {
                    foreach(var item in this.Items)
                    {
                        if (z == item.ZIndex)
                            item.Draw(gameTime);
                    }
                }
            }
        }

        public virtual void CheckEntry(float x, float y)
        {
            if (this.Visible)
                this.Items.ForEach((v) => v.CheckEntry(x, y));
        }

        public virtual void CheckEntryPressed(float x, float y)
        {
            if (this.Visible)
                this.Items.ForEach((v) => v.CheckEntryPressed(x, y));
        }
    }
}
