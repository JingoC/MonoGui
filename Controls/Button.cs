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
    using Base;
    public class Button : Control
    {
        public Button(Region parent = null) : base(parent)
        {
            //this.OnPressed += (s, e) => this.TextureManager.Textures.Change(1);
            //this.OnClick += (s, e) => this.TextureManager.Textures.RestoreDefault();
        }

        public string Text { get; set; }
        protected virtual Vector2 TextPosition { get; set; }
        public Color ForeColor { get; set; }

        public override void Designer()
        {
            if (this.TextureManager.Textures.Current == null)
            {
                this.TextureManager.Textures.AddRange(Resources.GetResources(new List<string>()
                {
                    "btn_idle_tmp",
                    "btn_click_tmp"
                }).OfType<Texture2D>());
            }

            base.Designer();
        }
    }
}
