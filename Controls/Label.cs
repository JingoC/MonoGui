using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.Controls
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGuiFramework.Base;
    using System;

    public class Label : MonoObject
    {
        //public override int Width { get => (int) this.Font.MeasureString(this.Text).X; }
        //public override int Height => this.Font.GetGlyphs().First(x => x.Value.Character == '0').Value.BoundsInTexture.Height;
        /*
        public override Vector2 Position
        {
            get => base.Position; set { base.Position = value; base.TextPosition = value; }
        }
        */

        public string Text { get; set; } = String.Empty;
        public Position TextPosition { get; set; } = new Position();
        public Color ForeColor { get; set; } = Color.White;
        public bool WordWrap { get; set; } = false;

        public override int Width
        {
            get
            {
                if (this.TextureScale == ScaleMode.Wrap)
                {
                    var font = this.TextureManager.Fonts.Current;
                    if (font != null)
                        return (int)font.MeasureString(this.Text).X;
                }

                return base.Width;
            }
            protected set => base.Width = value;
        }

        public override int Height
        {
            get
            {
                if (this.TextureScale == ScaleMode.Wrap)
                {
                    var font = this.TextureManager.Fonts.Current;
                    if (font != null)
                        return (int)font.MeasureString(this.Text).Y;
                }

                return base.Height;
            }
            protected set => base.Height = value;
        }

        public Label()
        {
           
        }

        public override void Designer()
        {
            if (this.TextureManager.Fonts.Count() == 0)
                this.TextureManager.Fonts.Add(Resources.GetResource("defaultFont") as SpriteFont);

            base.Designer();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var font = this.TextureManager.Fonts.Current;
            if (font != null)
                this.SpriteBatch.DrawString(font, this.Text, this.TextPosition.Absolute, this.ForeColor);
        }

        protected override void Render()
        {
            if (this.IsRequireRendering)
            {
                base.Render();

                this.TextPosition.Absolute = new Vector2(this.TextPosition.Relative.X + this.Position.Absolute.X,
                    this.TextPosition.Relative.Y + this.Position.Absolute.Y);
            }
        }
    }
}
