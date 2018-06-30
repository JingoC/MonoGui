using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.Base
{
    public class TextRegion : Region
    {
        public string Text { get; set; }
        public Color ForeColor { get; set; }

        public TextRegion() : this(null)
        {

        }

        public TextRegion(Region parent) : base(parent)
        {

        }

        public override void Designer()
        {
            base.Designer();
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                var font = this.TextureManager.Fonts.Current;
                if ((font != null) && !this.Text.Equals(String.Empty))
                {
                    this.spriteBatch.DrawString(font, this.Text, this.Position, this.ForeColor);
                }
            }

            base.Draw(gameTime);
        }
    }
}
