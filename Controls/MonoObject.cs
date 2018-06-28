using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.Controls
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;

    using MonoGuiFramework.System;

    public enum TextAlignHorizontal
    {
        Left = 1,
        Center = 2,
        Right = 3
    }

    public enum TextAlignVertical
    {
        Top = 1,
        Center = 2,
        Botton = 3
    }

    public class MonoObject : IControl
    {
        bool visible = true;
        int drawOrder = 0;

        public float Scale { get; set; } = 1.5f;
        public TextureContainer TextureManager { get; set; } = new TextureContainer();
        
        public event EventHandler OnClick;
        public event EventHandler OnPressed;
        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public virtual string Text { get; set; } = String.Empty;
        public Color ForeColor { get; set; } = Color.Black;
        
        virtual public Vector2 Position { get; set; }
        virtual protected Vector2 TextPosition { get; set; }
        public Color Color { get; set; } = Color.White;
        public string Name { get; set; }

        public Texture2D Texture { get => this.TextureManager.Textures.Current; }
        public SpriteFont Font { get => this.TextureManager.Fonts.Current; }
        virtual public int Width { get => this.Texture != null ? (int) (this.Texture.Width * this.Scale) : 0; }
        virtual public int Height { get => this.Texture != null ? (int) (this.Texture.Height * this.Scale) : 0; }
        public int ZIndex { get; set; } = 0;

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
        
        public MonoObject()
        {

        }

        public virtual bool IsEntry(float x, float y)
        {
            float x1 = this.Position.X;
            float y1 = this.Position.Y;
            float x2 = x1 + this.Width;
            float y2 = y1 + this.Height;

            return ((x > x1) && (x < x2) && (y > y1) && (y < y2));
        }

        public virtual void CheckEntry(float x, float y)
        {
            if (this.Visible)
            {
                if (this.IsEntry(x, y) && (this.OnClick != null))
                {
                    this.OnClick(this, EventArgs.Empty);
                }
            }
        }

        public virtual void Designer()
        {
            if (this.Font == null)
            {
                this.TextureManager.Fonts.Add(Resources.GetResource("defaultFont") as SpriteFont);
            }
        }

        public virtual void CheckEntryPressed(float x, float y)
        {
            if (this.Visible)
            {
                if (this.IsEntry(x, y) && (this.OnPressed != null))
                    this.OnPressed(this, EventArgs.Empty);
            }
        }
        
        virtual public void Draw(GameTime gameTime)
        {
            if (this.Visible)
            {
                if (this.Texture != null)
                {
                    SpriteBatch sb = GraphicsSingleton.GetInstance().GetSpriteBatch();
                    int x = (int) this.Position.X;
                    int y = (int) this.Position.Y;
                    int w = this.Width;
                    int h = this.Height;

                    //sb.Draw(this.Texture, this.Position, this.Color);
                    sb.Draw(this.Texture, new Rectangle(x, y, w, h), this.Color);
                    //sb.Draw(this.Texture, position: this.Position, color: this.Color, scale: new Vector2((float)2.0));
                }
                
                if ((this.Font != null) && !this.Text.Equals(String.Empty))
                {
                    GraphicsSingleton.GetInstance().GetSpriteBatch().DrawString(this.Font, 
                        this.Text, this.TextPosition, this.ForeColor);
                }
            }
        }
    }
}
