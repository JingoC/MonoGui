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
    using System.Timers;
    using MonoGuiFramework.System;

    public class ActivityBase : Activity
    {
        VerticalContainer vContainer;
        RectangleRegion region;
        Button button1;
        Button button2;
        Button button3;
        Changer changer1;

        HorizontalContainer hContainer;
        VerticalContainer vContainer2;

        int directionMove = 0;
        Random rand;
        Timer emulateActions = new Timer();
        
        public ActivityBase(Activity parent = null) : base(parent)
        {
            this.Designer();
        }

        public override void Designer()
        {
            this.rand = new Random((int)DateTime.Now.Ticks);
            this.emulateActions.Elapsed += this.EmulateActions_Elapsed;
            this.emulateActions.Interval = 5;
            this.emulateActions.Start();

            this.vContainer = new VerticalContainer();
            this.vContainer.Name = "VContainer";
            this.vContainer.BorderColor = Color.Aqua;
            this.vContainer.FillColor = Color.Transparent;
            this.vContainer.TextureScale = ScaleMode.Wrap;
            this.vContainer.SetBounds(0, 0, 100, 200);
            
            this.hContainer = new HorizontalContainer();
            this.hContainer.BorderColor = Color.Transparent;
            this.hContainer.FillColor = Color.Transparent;
            this.hContainer.SetBounds(120, 20, 100, 200);
            
            this.region = new RectangleRegion(this.vContainer);
            this.region.Name = "RectRegion";
            this.region.FillColor = Color.Gray;
            this.region.BorderColor = Color.Blue;
            this.region.SetBounds(20, 20, 400, 100);
            this.region.TextureScale = ScaleMode.None;
            this.vContainer.Items.Add(this.region);
            
            this.button1 = new Button();
            this.button1.BorderColor = Color.Transparent;
            this.button1.SetBounds(20, 10, 200, 200);
            this.button1.TextureScale = ScaleMode.Wrap;
            this.button1.OnClick += (s, e) => this.button1.BorderColor = this.button1.BorderColor == Color.Red ? Color.Blue : Color.Red;
            this.hContainer.Items.Add(this.button1);

            this.button2 = new Button();
            this.button2.BorderColor = Color.Transparent;
            this.button2.SetBounds(10, 10, 200, 200);
            this.button2.TextureScale = ScaleMode.Strech;
            this.button2.OnClick += (s, e) => this.button2.BorderColor = this.button2.BorderColor == Color.Red ? Color.Blue : Color.Red;
            this.hContainer.Items.Add(this.button2);
            
            this.vContainer.Items.Add(this.hContainer);
            
            this.button3 = new Button();
            this.button3.ScaleEnable = false;
            this.button3.BorderColor = Color.Transparent;
            this.button3.SetBounds(20, 10, 200, 200);
            this.button3.TextureScale = ScaleMode.Wrap;
            this.button3.OnClick += (s, e) => this.button3.BorderColor = this.button3.BorderColor == Color.Red ? Color.Blue : Color.Red;
            this.vContainer.Items.Add(this.button3);

            this.changer1 = new Changer(new ValueRange(1000, 10000));
            this.changer1.BorderColor = Color.Red;
            this.changer1.SetBounds(100, 0, 1, 1);
            this.vContainer.Items.Add(this.changer1);

            this.vContainer2 = new VerticalContainer();
            this.vContainer2.Items.Add(this.vContainer);

            Label l = new Label() { Text = "1245", ForeColor = Color.White };
            l.SetBounds(30, 30, 100, 100);
            l.BorderColor = Color.Yellow;
            this.vContainer2.Items.Add(l);

            this.Items.Add(this.vContainer2);
            
            base.Designer();
        }
        
        private void EmulateActions_Elapsed(object sender, ElapsedEventArgs e)
        {
            int mw = GraphicsSingleton.GetInstance().GetGraphics().PreferredBackBufferWidth;
            int mh = GraphicsSingleton.GetInstance().GetGraphics().PreferredBackBufferHeight;

            int x = (int)this.vContainer.Position.Absolute.X;
            int y = (int)this.vContainer.Position.Absolute.Y;
            int w = this.vContainer.Width;
            int h = this.vContainer.Height;

            switch (this.directionMove)
            {
                case 0: x -= this.rand.Next(1, 10); y -= this.rand.Next(1, 10); break;
                case 1: x += this.rand.Next(1, 10); y -= this.rand.Next(1, 10); break;
                case 2: x += this.rand.Next(1, 10); y += this.rand.Next(1, 10); break;
                case 3: x -= this.rand.Next(1, 10); y += this.rand.Next(1, 10); break;
                default: break;
            }

            //this.vContainer2.SetBounds(x, y, w, h);

            if ((this.directionMove == 0) && (x < 0))
                this.directionMove = 1;
            else if ((this.directionMove == 0) && (y < 0))
                this.directionMove = 3;
            else if ((this.directionMove == 1) && ((x + w) > mw))
                this.directionMove = 0;
            else if ((this.directionMove == 1) && (y < 0))
                this.directionMove = 2;
            else if ((this.directionMove == 2) && ((x + w) > mw))
                this.directionMove = 3;
            else if ((this.directionMove == 2) && ((y + h) > mh))
                this.directionMove = 1;
            else if ((this.directionMove == 3) && (x < 0))
                this.directionMove = 2;
            else if ((this.directionMove == 3) && ((y + h) > mh))
                this.directionMove = 0;
        }
    }
}
