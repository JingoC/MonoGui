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
        private int drawOrder = 0;
        private Color fillColor = Color.Transparent;
        private Color borderColor = Color.Transparent;

        protected bool IsRequireRendering { get; set; } = true;
        protected virtual void Render() { this.IsRequireRendering = false; }

        public TextureContainer TextureManager { get; set; } = new TextureContainer();

        public virtual Color BorderColor { get => this.borderColor; set { this.borderColor = value; this.IsRequireRendering = true; } }
        public virtual Color FillColor { get => this.fillColor; set { this.fillColor = value; this.IsRequireRendering = true; } }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public int DrawOrder
        {
            get => this.drawOrder;
            set { if (this.drawOrder != value) this.DrawOrderChanged?.Invoke(this, EventArgs.Empty); this.drawOrder = value; }
        }

        public virtual void Designer()
        {
            if (this.TextureManager.Fonts.Count() == 0)
                this.TextureManager.Fonts.Add(Resources.GetResource("defaultControlFont") as SpriteFont);

            this.IsRequireRendering = true;
        }
        
        public virtual void Draw(GameTime gameTime) { }
    }

    /// <summary>
    /// Partial "Bounds"
    /// </summary>
    public partial class Region
    {
        private AlignmentType alignment;
        private float scale = 1f;

        protected int maxWidth = 1;
        protected int maxHeight = 1;
        protected int width = 1;
        protected int height = 1;

        public bool IsBoundChanged { get; protected set; } = true;
        public virtual int MaxWidth { get => this.maxWidth; set { this.maxWidth = value; this.IsBoundChanged = true; } }
        public virtual int MaxHeight { get => this.maxHeight; set { this.maxHeight = value; this.IsBoundChanged = true; } }

        public bool ScaleEnable { get; set; } = true;
        public float Scale { get => this.ScaleEnable ? this.scale : 1f; set { this.scale = value; this.IsBoundChanged = true; } }

        public int BorderSize { get; set; } = 2;
        public virtual Position Position { get; set; } = new Position();
        
        public AlignmentType Align { get => this.alignment; set { this.alignment = value; this.IsBoundChanged = true; } }
        public ScaleMode TextureScale { get; set; } = ScaleMode.Wrap;
        
        public virtual int Width { get => (int)(this.width * this.Scale); protected set { this.width = value; this.IsBoundChanged = true; } }
        public virtual int Height { get => (int)(this.height * this.Scale); protected set { this.height = value; this.IsBoundChanged = true; } }

        public event EventHandler<EventArgs> BoundsChanged;

        public virtual void SetBounds(int x, int y, int width, int height)
        {
            this.Position = new Position(x, y);
            this.Width = width;
            this.Height = height;

            this.IsRequireRendering = true;
            this.BoundsChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool IsAlign(AlignmentType align) { return (this.Align & align) == align; }
        public void ResetBoundsChanged() { this.IsBoundChanged = false; }

        public Rectangle GetRectangle() { return new Rectangle((int)this.Position.Absolute.X, (int)this.Position.Absolute.Y, this.Width, this.Height); }
    }

    /// <summary>
    /// Parial "Clickalable"
    /// </summary>
    public partial class Region
    {
        public virtual bool IsEntry(float x, float y) { return false; }
        public virtual bool CheckEntry(float x, float y) { return this.IsEntry(x, y); }
    }

    public partial class Region : IDisposable, IDrawable
    {
        private bool visible = true;

        protected Graphics graphics;
        protected SpriteBatch spriteBatch;
        protected Logger logger;

        public SpriteBatch SpriteBatch { get => this.spriteBatch == null ? this.spriteBatch = this.graphics.GetSpriteBatch() : this.spriteBatch; }

        public event EventHandler<EventArgs> VisibleChanged;

        public string Name { get; set; } = String.Empty;
        public Region Parent { get; private set; } = null;

        public Region(Region parent = null)
        {
            this.Parent = parent;
            this.graphics = GraphicsSingleton.GetInstance();
            this.spriteBatch = this.graphics.GetSpriteBatch();
            this.logger = LoggerSingleton.GetInstance();

            this.MaxWidth = this.graphics.Width;
            this.MaxHeight = this.graphics.Height;
        }
        
        public bool Visible { get => this.visible; set { if (this.visible != value) this.VisibleChanged?.Invoke(this, EventArgs.Empty); this.visible = value; } }
        
        public virtual void Dispose() {}
    }
}
