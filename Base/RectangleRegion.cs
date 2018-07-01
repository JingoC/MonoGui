using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGuiFramework.Base
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class RectangleRegion : Region
    {
        Texture2D fillRectangle;
        Texture2D borderRectangle;

        public RectangleRegion() : this(null)
        {

        }

        public RectangleRegion(Region parent, int width = 1, int height = 1) : base(parent)
        {
            this.Width = width;
            this.Height = height;
        }
        
        public override void Designer()
        {
            this.SetBounds((int)this.Position.Relative.X, (int)this.Position.Relative.Y, this.Width, this.Height);

            base.Designer();
        }

        public override Color BorderColor
        {
            get => base.BorderColor;
            set { base.BorderColor = value; this.IsRequireRendering = true; }
        }


        public override Color FillColor
        {
            get => base.FillColor;
            set { base.FillColor = value; this.IsRequireRendering = true; }
        }

        public void UpdateDraw()
        {
            if (this.fillRectangle != null)
                this.fillRectangle.Dispose();

            int w = this.Width - this.BorderSize * 2;
            int h = this.Height - this.BorderSize * 2;
            int fillWidth = w > 0 ? w : 1;
            int fillHeight = h > 0 ? h : 1;

            this.fillRectangle = new Texture2D(this.graphics.GraphicsDevice, fillWidth, fillHeight);
            if (this.BorderColor != Color.Transparent)
            {
                Color[] data = new Color[fillWidth * fillHeight];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = this.FillColor;
                }
                this.fillRectangle.SetData(data);
            }

            if (this.borderRectangle != null)
                this.borderRectangle.Dispose();

            w = this.Width;
            h = this.Height;
            int borderWidth = w > 0 ? w : 1;
            int borderHeight = h > 0 ? h : 1;
            this.borderRectangle = new Texture2D(this.graphics.GraphicsDevice, borderWidth, borderHeight);
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
                }

                this.borderRectangle.SetData(data);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.Render();

            if ((this.Visible) && 
                (!((this.FillColor == Color.Transparent) && (this.BorderColor == Color.Transparent))) &&
                (this.Width > 0) && (this.Height > 0))
            {
                int x = (int)this.Position.Absolute.X;
                int y = (int)this.Position.Absolute.Y;
                int w = this.Width;
                int h = this.Height;
                
                if (this.TextureManager.Textures.Current != null)
                {
                    if (this.TextureScale == ScaleMode.Wrap)
                    {
                        w = (int)((this.TextureManager.Textures.Current.Width + 1) * this.Scale);
                        h = (int)((this.TextureManager.Textures.Current.Height + 1) * this.Scale);
                    }
                }

                if (this.borderRectangle != null)
                    this.spriteBatch.Draw(this.borderRectangle, new Rectangle(x, y, w, h), this.BorderColor);

                if (this.fillRectangle != null)
                {
                    this.spriteBatch.Draw(this.fillRectangle,
                        new Rectangle(x + this.BorderSize, y + this.BorderSize, w - this.BorderSize * 2, h - this.BorderSize * 2),
                        this.FillColor);
                }
                    
            }

            base.Draw(gameTime);
        }

        protected override void Render()
        {
            if (this.IsRequireRendering)
            {
                this.logger.Write($"{Environment.NewLine}Render: {this.ToString()}");
                this.UpdateDraw();
                base.Render();
            }
        }

        public override bool IsEntry(float x, float y)
        {
            float x1 = this.Position.Absolute.X;
            float y1 = this.Position.Absolute.Y;
            float x2 = x1 + this.Width;
            float y2 = y1 + this.Height;

            return ((x > x1) && (x < x2) && (y > y1) && (y < y2));
        }
    }
}
