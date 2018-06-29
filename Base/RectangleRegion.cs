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
        Texture2D rectangle;

        public RectangleRegion() : this(1, 1)
        {

        }

        public RectangleRegion(int width, int height) : base()
        {
            this.Width = width;
            this.Height = height;
        }
        
        public override void Designer()
        {
            if (this.rectangle != null)
                this.rectangle.Dispose();

            this.rectangle = new Texture2D(this.graphics.GraphicsDevice, this.Width, this.Height);
            Color[] data = new Color[this.Width * this.Height];
            for (int i = 0; i < data.Length; i++) data[i] = this.FillColor;
                this.rectangle.SetData(data);

            base.Designer();
        }

        public override void SetBounds(int x, int y, int width, int height)
        {
            if (this.rectangle != null)
                this.rectangle.Dispose();

            this.rectangle = new Texture2D(this.graphics.GraphicsDevice, width, height);
            base.SetBounds(x, y, width, height);
        }

        public override void Draw(GameTime gameTime)
        {
            this.spriteBatch.Draw(this.rectangle, this.Position, this.FillColor);

            base.Draw(gameTime);
        }
    }
}
