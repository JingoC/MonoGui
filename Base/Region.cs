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

    public enum ScaleMode
    {
        None = 0,
        Wrap = 1,
        Strech = 2
    }

    public class Region : IDisposable, IDrawable
    {
        protected Graphics graphics = GraphicsSingleton.GetInstance();
        protected SpriteBatch spriteBatch = GraphicsSingleton.GetInstance().GetSpriteBatch();
        
        private int drawOrder = 0;
        private bool visible = true;
        private float scale = 1f;
        protected int width = 1;
        protected int height = 1;

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

        public string Name { get; set; } = String.Empty;
        public TextureContainer TextureManager { get; set; } = new TextureContainer();
        public int BorderSize { get; set; } = 2;

        public Region Parent { get; private set; } = null;
        public virtual Color BorderColor { get; set; } = Color.Transparent;
        public virtual Color FillColor { get; set; } = Color.Transparent;

        public virtual Vector2 Position { get; set; } = new Vector2(0, 0);
        public float Scale { get => this.ScaleEnable ? this.scale : 1f; set => this.scale = value; }
        public bool ScaleEnable { get; set; } = true;
        public virtual int Width
        {
            get {return (int)(this.width * this.Scale);}
            protected set => this.width = value;
        }

        public virtual int Height
        {
            get => (int)(this.height * this.Scale);
            protected set => this.height = value;
        }

        public ScaleMode TextureScale { get; set; } = ScaleMode.None;

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        protected event EventHandler<EventArgs> BoundsChanged;

        public virtual void Dispose()
        {
            
        }

        public virtual void Draw(GameTime gameTime)
        {

        }
        
        protected virtual bool IsEntry(float x, float y)
        {
            return false;
        }

        public virtual bool CheckEntry(float x, float y)
        {
            return this.IsEntry(x, y);
        }

        public virtual void Designer()
        {
            if (this.TextureManager.Fonts.Count() > 0)
                this.TextureManager.Fonts.Add(Resources.GetResource("defaultControlFont") as SpriteFont);
        }

        public virtual void SetBounds(int x, int y, int width, int height)
        {
            this.Position = new Vector2(x, y);
            this.Width = width;
            this.Height = height;

            if (this.BoundsChanged != null)
                this.BoundsChanged(this, EventArgs.Empty);
        }

        public Region() : this(null)
        {
            
        }

        public Region(Region parent)
        {
            this.Parent = parent;
        }
    }
}
