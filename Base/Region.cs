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

    public class Position
    {
        private Vector2 absolute;

        public bool IsModified { get; private set; }
        public Vector2 Relative { get; private set; }
        public Vector2 Absolute { get => this.absolute; set { this.absolute = value; this.IsModified = true; } }

        public Position(int x = 0, int y = 0)
        {
            this.Relative = new Vector2(x, y);
            this.Absolute = new Vector2(x, y);
        }
    }

    public class Region : IDisposable, IDrawable
    {
        protected Graphics graphics;
        protected SpriteBatch spriteBatch;
        protected Logger logger;

        public SpriteBatch SpriteBatch { get => this.spriteBatch == null ? this.spriteBatch = this.graphics.GetSpriteBatch() : this.spriteBatch; }

        private int drawOrder = 0;
        private bool visible = true;
        private float scale = 1f;
        private Color fillColor;
        private Color borderColor;
        protected int width = 1;
        protected int height = 1;

        public Region(Region parent = null)
        {
            this.Parent = parent;
            this.graphics = GraphicsSingleton.GetInstance();
            this.spriteBatch = this.graphics.GetSpriteBatch();
            this.logger = LoggerSingleton.GetInstance();
        }

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

        protected bool IsRequireRendering { get; set; } = true;

        public string Name { get; set; } = String.Empty;
        public TextureContainer TextureManager { get; set; } = new TextureContainer();
        public int BorderSize { get; set; } = 2;

        public Region Parent { get; private set; } = null;
        public virtual Color BorderColor
        {
            get => this.borderColor;
            set { this.borderColor = value; this.IsRequireRendering = true; }
        }


        public virtual Color FillColor
        {
            get => this.fillColor;
            set { this.fillColor = value; this.IsRequireRendering = true; }
        }


        public virtual Position Position { get; set; } = new Position();
        public virtual float Scale { get => this.ScaleEnable ? this.scale : 1f; set => this.scale = value; }
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

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)this.Position.Absolute.X, (int)this.Position.Absolute.Y, this.Width, this.Height);
        }

        public ScaleMode TextureScale { get; set; } = ScaleMode.Wrap;

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> BoundsChanged;

        public virtual void Dispose()
        {
            
        }

        public virtual void Draw(GameTime gameTime)
        {

        }
        
        public virtual bool IsEntry(float x, float y)
        {
            return false;
        }

        public virtual bool CheckEntry(float x, float y)
        {
            return this.IsEntry(x, y);
        }

        public virtual void Designer()
        {
            if (this.TextureManager.Fonts.Count() == 0)
                this.TextureManager.Fonts.Add(Resources.GetResource("defaultControlFont") as SpriteFont);

            this.IsRequireRendering = true;
        }

        public virtual void SetBounds(int x, int y, int width, int height)
        {
            this.Position = new Position(x, y);
            this.Width = width;
            this.Height = height;

            this.IsRequireRendering = true;
            if (this.BoundsChanged != null)
                this.BoundsChanged(this, EventArgs.Empty);
        }
        
        protected virtual void Render()
        {
            this.IsRequireRendering = false;
        }
    }
}
