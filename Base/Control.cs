using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGuiFramework.Base
{
    public class Control : Region
    {
        public event EventHandler OnClick;
        public event EventHandler OnPressed;

        public override bool CheckEntry(float x, float y)
        {
            if (base.IsEntry(x, y))
            {
                Logger.Write($"{Environment.NewLine}ClickEvent [{this.Name}]: {this.ToString()}");
                this.OnClick?.Invoke(this, EventArgs.Empty);
                return true;
            }

            return false;
        }

        public override int MaxWidth
        {
            get
            {
                var texture = this.TextureManager.Textures.Current;
                if (texture != null)
                    return texture.Width;
                return base.MaxWidth;
            }
            set => base.MaxWidth = value;
        }

        public override int MaxHeight
        {
            get
            {
                var texture = this.TextureManager.Textures.Current;
                if (texture != null)
                    return texture.Height;
                return base.MaxHeight;
            }
            set => base.MaxHeight = value;
        }

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
        }

        public override void Designer()
        {
            base.Designer();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override bool IsEntry(float x, float y)
        {
            var texture = this.TextureManager.Textures.Current;
            if (texture != null)
            {
                float x1 = this.Position.Absolute.X;
                float y1 = this.Position.Absolute.Y;
                float x2 = x1 + texture.Width * this.Scale;
                float y2 = y1 + texture.Height * this.Scale;

                if (this.TextureScale == ScaleMode.Strech)
                {
                    x2 = x1 + this.Width;
                    y2 = y1 + this.Height;
                }

                return ((x > x1) && (x < x2) && (y > y1) && (y < y2));
            }

            return false;
        }
    }
}
