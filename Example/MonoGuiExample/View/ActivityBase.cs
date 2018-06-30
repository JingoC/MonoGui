using MonoGuiFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiExample.View
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGuiFramework.Base;
    using MonoGuiFramework.Controls;

    public class ActivityBase : Activity
    {
        RectangleRegion region;
        Button button1;
        Button button2;
        Button button3;

        public ActivityBase() : this(null)
        {

        }

        public ActivityBase(Activity parent) : base(parent)
        {

        }

        public override void Designer()
        {
            this.region = new RectangleRegion();
            this.region.FillColor = Color.Gray;
            this.region.BorderColor = Color.Blue;
            this.region.SetBounds(20, 20, 400, 100);
            this.region.TextureScale = ScaleMode.None;
            this.Items.Add(this.region);

            this.button1 = new Button();
            this.button1.BorderColor = Color.White;
            this.button1.SetBounds(20, 140, 200, 200);
            this.button1.TextureScale = ScaleMode.None;
            this.button1.OnClick += delegate (Object s, EventArgs e)
            {
                this.button1.BorderColor = this.button1.BorderColor == Color.Red ? Color.Blue : Color.Red;
            };
            this.Items.Add(this.button1);

            this.button2 = new Button();
            this.button2.BorderColor = Color.White;
            this.button2.SetBounds(20, 340, 200, 200);
            this.button2.TextureScale = ScaleMode.Strech;
            this.button2.OnClick += (s, e) => this.button2.BorderColor = this.button2.BorderColor == Color.Red ? Color.Blue : Color.Red;
            this.Items.Add(this.button2);

            this.button3 = new Button();
            this.button3.ScaleEnable = false;
            this.button3.BorderColor = Color.White;
            this.button3.SetBounds(500, 20, 200, 200);
            this.button3.TextureScale = ScaleMode.Wrap;
            this.button3.OnClick += (s, e) => this.button3.BorderColor = this.button3.BorderColor == Color.Red ? Color.Blue : Color.Red;
            this.Items.Add(this.button3);

            base.Designer();
        }
    }
}
