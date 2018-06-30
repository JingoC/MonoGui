using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGuiFramework.Controls
{
    using MonoGuiFramework.System;

    using Microsoft.Xna.Framework.Graphics;

    public class Toggle : MonoObject
    {
        public bool IsChecked { get; private set; } = false;

        public event EventHandler IsChanged;

        public Toggle() : this(false)
        {

        }

        public Toggle(bool isChecked)
        {
            this.IsChecked = isChecked;

            this.OnClick += delegate (Object sender, EventArgs e)
            {
                this.IsChecked = !this.IsChecked;
                if (this.IsChecked)
                    this.TextureManager.Textures.Change(1);
                else
                    this.TextureManager.Textures.Change(0);

                if (this.IsChanged != null)
                    this.IsChanged(this, EventArgs.Empty);
            };
        }

        public override void Designer()
        {
            if (this.TextureManager.Textures.Current == null)
            {
                this.TextureManager.Textures.AddRange(Resources.GetResources(new List<string>()
                {
                    "defaultToggleOff",
                    "defaultToggleOn"
                }).OfType<Texture2D>());
                this.TextureManager.Textures.Change(this.IsChecked ? 1 : 0);
            }

            base.Designer();
        }
    }
}
