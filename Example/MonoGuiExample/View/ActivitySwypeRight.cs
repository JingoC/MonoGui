using MonoGuiFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGuiFramework.Base;

namespace MonoGuiExample.View
{
    using Microsoft.Xna.Framework;
    using MonoGuiFramework.Controls;

    public class ActivitySwypeRight : Activity
    {
        Button button1;

        public ActivitySwypeRight(Activity parent = null) : base(parent)
        {

        }

        public override void Designer()
        {
            this.button1 = new Button();
            this.button1.BorderColor = Color.Green;
            this.button1.SetBounds(400, 100, 50, 10);

            this.Items.Add(this.button1);

            base.Designer();
        }
    }
}
