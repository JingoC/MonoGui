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

        public Control() : this(null)
        {

        }

        public Control(Region parent) : base(parent)
        {
            this.textRegion = new TextRegion(this);
        }

        public override void Designer()
        {
            base.Designer();
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.Visible)
            {
                base.Draw(gameTime);

                var texture = this.TextureManager.Textures.Current;
                if (texture != null)
                {
                    int x, y, w, h;

                    x = (int)this.Position.X;
                    y = (int)this.Position.Y;

                    switch (this.TextureScale)
                    {
                        case ScaleMode.Strech:
                        {
                            w = this.Width;
                            h = this.Height;
                        }
                        break;
                        default:
                        {
                            w = (int)(this.TextureManager.Textures.Current.Width * this.Scale);
                            h = (int)(this.TextureManager.Textures.Current.Height * this.Scale);
                        }
                        break;
                    }

                    this.spriteBatch.Draw(texture, new Rectangle(x, y, w, h), Color.White);
                }

                this.textRegion.Draw(gameTime);
            }
        }

        protected override bool IsEntry(float x, float y)
        {
            float x1 = this.Position.X;
            float y1 = this.Position.Y;
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
