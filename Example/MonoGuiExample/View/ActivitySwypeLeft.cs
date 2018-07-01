using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiExample.View
{
    using MonoGuiFramework;
    using Microsoft.Xna.Framework;
    using MonoGuiFramework.Controls;

    public class ActivitySwypeLeft : Activity
    {
        Button button1;

        public ActivitySwypeLeft(Activity parent = null) : base(parent)
        {

        }

        public override void Designer()
        {
            this.button1 = new Button();
            this.button1.BorderColor = Color.Aqua;
            this.button1.SetBounds(400, 100, 100, 50);

            this.Items.Add(this.button1);

            base.Designer();
        }
    }
}
