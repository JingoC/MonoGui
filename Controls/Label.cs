using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.Controls
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

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

        public string Text { get; set; }
        public Color ForeColor { get; set; }

        public Label()
        {
            
        }

        
    }
}
