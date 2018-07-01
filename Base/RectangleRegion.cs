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

    public partial class RectangleRegion
    {
        static public Rectangle Intersect(Region r1, Region r2)
        {
            return Rectangle.Intersect(r1.GetRectangle(), r2.GetRectangle());
        }
    }

    public partial class RectangleRegion : Region
    {
        public RectangleRegion(Region parent = null, int width = 1, int height = 1) : base(parent)
        {  
            this.Width = width;
            this.Height = height;
        }

        public override bool IsEntry(float x, float y)
        {
            float x1 = this.Position.Absolute.X;
            float y1 = this.Position.Absolute.Y;
            float x2 = x1 + this.Width;
            float y2 = y1 + this.Height;

            return ((x > x1) && (x < x2) && (y > y1) && (y < y2));
        }

        public override void Designer()
        {
            this.SetBounds((int)this.Position.Relative.X, (int)this.Position.Relative.Y, this.Width, this.Height);
            base.Designer();
        }

        #region Rendering
        
        Texture2D regionRender;
        Texture2D textureRender;
        
        Rectangle regionRectangle;
        Rectangle textureRectangle;
        
        private void RenderRegion()
        {
            //this.regionRectangle = (this.Parent != null) ? RectangleRegion.Intersect(this.Parent, this) : this.GetRectangle();
            this.regionRectangle = this.GetRectangle();
            // Render Border Rectangle texture
            if (this.regionRender != null)
                this.regionRender.Dispose();
            /*
            int difW = (int)(this.Position.Absolute.X - this.regionRectangle.X);
            int difH = (int)(this.Position.Absolute.Y - this.regionRectangle.Y);
            int startWidth = (int)Math.Abs(difW);
            int startHeight = (int)Math.Abs(difH);
            int offsetIndex = 0;// startHeight * startWidth;
            */

            int w = this.Width;
            int h = this.Height;
            int borderWidth = w > 0 ? w : 1;
            int borderHeight = h > 0 ? h : 1;
            this.regionRender = new Texture2D(this.graphics.GraphicsDevice, borderWidth, borderHeight);
            if (this.BorderColor != Color.Transparent)
            {
                Color[] data = new Color[borderWidth * borderHeight];
                
                for (int i = 0; i < data.Length; i++)
                {
                    int iy = i / borderWidth;
                    int ix = i % borderWidth;
                    /*
                    int dx = (int)(ix + this.Position.Absolute.X);
                    int dy = (int)(iy + this.Position.Absolute.Y);
                    if (!((dx >= this.regionRectangle.X) && (dx < (this.regionRectangle.X + this.regionRectangle.Width)) &&
                        (dy >= this.regionRectangle.Y) && (dy < (this.regionRectangle.Y + this.regionRectangle.Height))))
                        continue;
                    */
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
        
        private void RenderTexture()
        {
            if (this.TextureManager.Textures.Current != null)
            {

            }
        }

        protected override void Render()
        {
            if (this.IsRequireRendering)
            {
                this.logger.Write($"{Environment.NewLine}Render[{this.Name}]: {this.ToString()}");
                this.RenderRegion();
                this.RenderTexture();
                base.Render();
            }
        }

        #endregion

        #region Drawing

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

                if ((this.regionRender != null) && (this.regionRectangle != null))
                {
                    this.spriteBatch.Draw(this.regionRender, 
                        new Rectangle(x, y, w, h),
                        Color.White);
                }
                /*
                if (this.borderRectangle != null)
                {
                    int bw = this.borderRectangle.Width;
                    int bh = this.borderRectangle.Height;
                    this.spriteBatch.Draw(this.borderRectangle, new Rectangle(x, y, bw, bh), this.BorderColor);
                }

                if (this.fillRectangle != null)
                {
                    int fw = this.fillRectangle.Width - this.BorderSize * 2;
                    int fh = this.fillRectangle.Height - this.BorderSize * 2;
                    this.spriteBatch.Draw(this.fillRectangle,
                        new Rectangle(x + this.BorderSize, y + this.BorderSize, fw, fh),
                        this.FillColor);
                }
                */
            }

            base.Draw(gameTime);
        }

        #endregion
    }
}
