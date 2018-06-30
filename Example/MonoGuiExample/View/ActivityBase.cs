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

    using MonoGuiFramework.Containers;

    public class ActivityBase : Activity
    {
        VerticalContainer vContainer;
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
            this.vContainer = new VerticalContainer();
            this.vContainer.SetBounds(0, 0, 100, 200);
            this.vContainer.BorderColor = Color.Aqua;
            this.vContainer.FillColor = Color.Transparent;

            this.region = new RectangleRegion();
            this.region.FillColor = Color.Gray;
            this.region.BorderColor = Color.Blue;
            this.region.SetBounds(20, 20, 400, 100);
            this.region.TextureScale = ScaleMode.None;
            this.vContainer.Items.Add(this.region);

            this.button1 = new Button();
            this.button1.BorderColor = Color.White;
            this.button1.SetBounds(20, 10, 200, 200);
            this.button1.TextureScale = ScaleMode.None;
            this.button1.OnClick += (s, e) => this.button1.BorderColor = this.button1.BorderColor == Color.Red ? Color.Blue : Color.Red;
            this.vContainer.Items.Add(this.button1);

            this.button2 = new Button();
            this.button2.BorderColor = Color.White;
            this.button2.SetBounds(20, 10, 200, 200);
            this.button2.TextureScale = ScaleMode.Strech;
            this.button2.OnClick += (s, e) => this.button2.BorderColor = this.button2.BorderColor == Color.Red ? Color.Blue : Color.Red;
            this.vContainer.Items.Add(this.button2);

            this.button3 = new Button();
            this.button3.ScaleEnable = false;
            this.button3.BorderColor = Color.White;
            this.button3.SetBounds(500, 10, 200, 200);
            this.button3.TextureScale = ScaleMode.Wrap;
            this.button3.OnClick += (s, e) => this.button3.BorderColor = this.button3.BorderColor == Color.Red ? Color.Blue : Color.Red;
            this.vContainer.Items.Add(this.button3);

            this.Items.Add(this.vContainer);
            base.Designer();
        }
    }
}
