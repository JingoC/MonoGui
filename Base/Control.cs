using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGuiFramework.Base
{
    public class Control : RectangleRegion
    {
        private TextRegion textRegion;

        public override int Width
        {
            get
            {
                var texture = this.TextureManager.Textures.Current;
                if ((texture != null) && (this.TextureScale == ScaleMode.Wrap))
                    return (int)(texture.Width * this.Scale);
                else
                    return base.Width;
            }
            protected set
            {
                base.Width = value;
            }
        }

        public override int Height
        {
            get
            {
                var texture = this.TextureManager.Textures.Current;
                if ((texture != null) && (this.TextureScale == ScaleMode.Wrap))
                    return (int)(texture.Height * this.Scale);
                else
                    return base.Height;
            }
            protected set
            {
                base.Height = value;
            }
        }

        public Control(Region parent = null) : base(parent)
        {
            this.textRegion = new TextRegion(this);
        }

        public override void Designer()
        {
            base.Designer();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            this.textRegion.Draw(gameTime);
        }

        public override bool IsEntry(float x, float y)
        {
            float x1 = this.Position.Absolute.X;
            float y1 = this.Position.Absolute.Y;
            float x2 = x1 + this.TextureManager.Textures.Current.Width * this.Scale;
            float y2 = y1 + this.TextureManager.Textures.Current.Height * this.Scale;

            if (this.TextureScale == ScaleMode.Strech)
            {
                x2 = x1 + this.Width;
                y2 = y1 + this.Height;
            }
            
            return ((x > x1) && (x < x2) && (y > y1) && (y < y2));
        }
    }
}
