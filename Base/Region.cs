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

    public enum AlignmentType
    {
        None = 0,
        Left = 1,
        Center = 2,
        Right = 4,
        Top = 8,
        Middle = 16,
        Bottom = 32
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

    /// <summary>
    /// Partial "Draw"
    /// </summary>
    public partial class Region
    {
        private bool visible = true;
        private int drawOrder = 0;
        private Color fillColor = Color.Transparent;
        private Color borderColor = Color.Transparent;

        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;

        public bool Visible { get => this.visible; set { if (this.visible != value) { this.visible = value; ExecHandler(this, this.VisibleChanged); } } }
        public int DrawOrder { get => this.drawOrder; set { if (this.drawOrder != value) { this.drawOrder = value; ExecHandler(this, this.DrawOrderChanged); } } }

        public Texture2D Texture { get => this.TextureManager.Textures.Current; }
        
        public TextureContainer TextureManager { get; set; } = new TextureContainer();

        public Color BorderColor { get => this.borderColor; set { this.borderColor = value; this.IsRequireRendering = true; } }
        public Color FillColor { get => this.fillColor; set { this.fillColor = value; this.IsRequireRendering = true; } }
        
        public virtual void Designer()
        {
            if (this.TextureManager.Fonts.Count() == 0)
                this.TextureManager.Fonts.Add(Resources.GetResource("defaultControlFont") as SpriteFont);

            this.IsRequireRendering = true;
        }

        protected bool IsTransparent() => (this.FillColor == Color.Transparent) && (this.BorderColor == Color.Transparent);

        public virtual void Draw(GameTime gameTime)
        {
            this.Render();
            
            if ((this.Visible) &&
                (!(this.IsTransparent() && (this.Texture == null))) &&
                (this.Width > 0) && (this.Height > 0))
            {
                var rect = this.GetRectangle();
                
                if ((this.regionRender != null) && (this.regionRectangle != null))
                    SpriteBatch.Draw(this.regionRender, rect, Color.White);

                if (this.Texture != null)
                    SpriteBatch.Draw(this.Texture, rect, Color.White);
            }
        }
    }

    /// <summary>
    /// Partial 'Rendering'
    /// </summary>
    public partial class Region
    {
        private Texture2D regionRender;
        private Rectangle regionRectangle;

        public Rectangle GetRectangle() { return new Rectangle((int)this.Position.Absolute.X, (int)this.Position.Absolute.Y, this.Width, this.Height); }
        public bool IsRequireRendering { get; protected set; } = true;

        private void RenderRegion()
        {
            this.regionRectangle = this.GetRectangle();

            if (this.regionRender != null)
                this.regionRender.Dispose();

            int w = this.Width;
            int h = this.Height;
            int borderWidth = w > 0 ? w : 1;
            int borderHeight = h > 0 ? h : 1;
            this.regionRender = new Texture2D(Graphics.GraphicsDevice, borderWidth, borderHeight);
            if (this.BorderColor != Color.Transparent)
            {
                Color[] data = new Color[borderWidth * borderHeight];

                for (int i = 0; i < data.Length; i++)
                {
                    int iy = i / borderWidth;
                    int ix = i % borderWidth;

                    bool isLeftBorder = ix < this.BorderSize;
                    bool isRightBorder = ix >= (borderWidth - this.BorderSize);
                    bool isTopBorder = iy < this.BorderSize;
                    bool isBottomBorder = iy > (borderHeight - this.BorderSize);

                    bool isBorder = isLeftBorder || isRightBorder || isTopBorder || isBottomBorder;

                    if (isBorder)
                        data[i] = this.BorderColor;
                    else
                        data[i] = this.FillColor;
                }

                this.regionRender.SetData(data);
            }
        }

        protected virtual void Render()
        {
            if (this.IsRequireRendering)
            {
                //this.logger.Write($"{Environment.NewLine}Render[{this.Name}]: {this.ToString()}");
                this.RenderRegion();
                this.IsRequireRendering = false;
            }
        }
    }

    /// <summary>
    /// Partial "Bounds"
    /// </summary>
    public partial class Region
    {
        private AlignmentType alignment;
        private float scale = 1f;

        private int maxWidth = 1;
        private int maxHeight = 1;
        private int width = 1;
        private int height = 1;
        
        public virtual int MaxWidth { get => this.maxWidth; set { this.maxWidth = value; this.IsRequireRendering = true; } }
        public virtual int MaxHeight { get => this.maxHeight; set { this.maxHeight = value; this.IsRequireRendering = true; } }

        public bool ScaleEnable { get; set; } = true;
        public float Scale { get => this.ScaleEnable ? this.scale : 1f; set { this.scale = value; this.IsRequireRendering = true; } }

        public int BorderSize { get; set; } = 2;
        public virtual Position Position { get; protected set; } = new Position();
        
        public AlignmentType Align { get => this.alignment; set { this.alignment = value; this.IsRequireRendering = true; } }
        public ScaleMode TextureScale { get; set; } = ScaleMode.Wrap;
        
        public virtual int Width
        {
            get
            {
                int w = 0;
                if ((this.Texture != null) && (this.TextureScale == ScaleMode.Wrap))
                    w = (int)(this.Texture.Width * this.Scale);
                else
                    w = (int)(this.width * this.Scale);

                return w > this.maxWidth ? this.maxWidth : w;
            }
            protected set { this.width = value; this.IsRequireRendering = true; }
        }
        public virtual int Height
        {
            get
            {
                int h = 0;
                if ((this.Texture != null) && (this.TextureScale == ScaleMode.Wrap))
                    h = (int)(this.Texture.Height * this.Scale);
                else
                    h = (int)(this.height * this.Scale);
                return h > this.maxHeight ? this.maxHeight : h;
            }

            protected set { this.height = value; this.IsRequireRendering = true; } }

        public event EventHandler<EventArgs> BoundsChanged;
        
        public virtual void SetBounds(int x, int y, int width, int height)
        {
            this.Position = new Position(x, y);
            this.Width = width;
            this.Height = height;

            this.IsRequireRendering = true;
            this.BoundsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetAbsolute(int x, int y) => this.Position.Absolute = new Vector2(x, y);
        public bool IsAlign(AlignmentType align) => (this.Align & align) == align;
    }

    /// <summary>
    /// Partial "Clickalable"
    /// </summary>
    public partial class Region
    {
        public virtual bool IsEntry(float x, float y)
        {
            float x1 = this.Position.Absolute.X;
            float y1 = this.Position.Absolute.Y;
            float x2 = x1 + this.Width;
            float y2 = y1 + this.Height;

            return ((x > x1) && (x < x2) && (y > y1) && (y < y2));
        }

        public virtual bool CheckEntry(float x, float y) => this.IsEntry(x, y);
    }

    /// <summary>
    /// Partial "Main"
    /// </summary>
    public partial class Region : IDisposable, IDrawable
    {
        private static Graphics graphics;
        protected static Graphics Graphics { get => graphics == null ? graphics = GraphicsSingleton.GetInstance() : graphics; }

        private static SpriteBatch spriteBatch;
        protected static SpriteBatch SpriteBatch { get => spriteBatch == null ? spriteBatch = Graphics.GetSpriteBatch() : spriteBatch; }
        
        private Logger logger;
        protected Logger Logger { get => logger == null ? logger = LoggerSingleton.GetInstance() : logger; }

        protected static void ExecHandler(Object context, EventHandler<EventArgs> ev) { ev?.Invoke(context, EventArgs.Empty); }
        
        public string Name { get; set; } = String.Empty;
        public Region Parent { get; private set; } = null;

        public Region(Region parent = null)
        {
            this.Parent = parent;

            this.MaxWidth = Graphics.Width;
            this.MaxHeight = Graphics.Height;
        }
        
        public virtual void Dispose()
        {
            if (this.regionRender != null)
                this.regionRender.Dispose();
        }
    }
}
