using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.Base
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using MonoGuiFramework.System;

    public enum TextureScale
    {
        Zoom = 1,
        Scratch = 2
    }

    public class Region : IDisposable, IDrawable
    {
        protected Graphics graphics = GraphicsSingleton.GetInstance();
        protected SpriteBatch spriteBatch = GraphicsSingleton.GetInstance().GetSpriteBatch();

        private int drawOrder = 0;
        private bool visible = true;

        public int DrawOrder
        {
            get => this.drawOrder;
            set
            {
                if (this.drawOrder != value)
                {
                    this.drawOrder = value;
                    if (this.DrawOrderChanged != null)
                        this.DrawOrderChanged(this, EventArgs.Empty);
                }
            }
        }

        public bool Visible
        {
            get => this.visible;
            set
            {
                if (this.visible != value)
                {
                    this.visible = value;
                    if (this.VisibleChanged != null)
                        this.VisibleChanged(this, EventArgs.Empty);
                }
            }
        }

        public Color BorderColor { get; set; } = Color.Transparent;
        public Color FillColor { get; set; } = Color.Transparent;

        public virtual Vector2 Position { get; set; }
        public virtual int Width { get; protected set; } = 1;
        public virtual int Height { get; protected set; } = 1;

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        protected event EventHandler<EventArgs> BoundsChanged;

        public TextureScale TextureScale { get; set; }

        public virtual void Dispose()
        {
            
        }

        public virtual void Draw(GameTime gameTime)
        {
            
        }

        public virtual void Designer()
        {

        }

        public virtual void SetBounds(int x, int y, int width, int height)
        {
            this.Position = new Vector2(x, y);
            this.Width = width;
            this.Height = height;

            if (this.BoundsChanged != null)
                this.BoundsChanged(this, EventArgs.Empty);
        }
    }
}
