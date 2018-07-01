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

    public class ActivityDown : Activity
    {
        Button button1;

        public ActivityDown(Activity parent = null) : base(parent)
        {

        }

        public override void Designer()
        {
            this.button1 = new Button();
            this.button1.BorderColor = Color.Red;
            this.button1.SetBounds(200, 100, 100, 50);

            this.Items.Add(this.button1);

            base.Designer();
        }
    }
}
