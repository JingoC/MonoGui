using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using MonoGuiFramework;
using MonoGuiFramework.Base;
using MonoGuiFramework.Controls;
using MonoGuiFramework.Containers;

namespace MonoGuiGameView.View
{
    public class SwypeLeftActivity : Activity
    {
        public SwypeLeftActivity(Activity parent = null) : base(parent)
        {

        }

        public override void Designer()
        {
            VerticalContainer mainContainer = new VerticalContainer(this);
            
            VerticalContainer contentMenu = new VerticalContainer(this) { Align = AlignmentType.Center | AlignmentType.Middle };
            contentMenu.MaxHeight = 500;
            contentMenu.MaxWidth = 500;
            contentMenu.SetBounds(20, 0, 500, 500);

            Changer changer1 = new Changer(new ValueRange(0, 10), contentMenu) { Scale = 2f };
            changer1.Step = 1;
            changer1.SetBounds(0, 20, 100, 100);

            Toggle toggle1 = new Toggle(contentMenu, true) { Scale = 1.5f };
            toggle1.SetBounds(0, 20, 100, 100);
            
            Button btn1 = new Button(contentMenu);
            btn1.SetBounds(0, 20, 100, 100);
            
            contentMenu.Items.Add(changer1);
            contentMenu.Items.Add(toggle1);
            contentMenu.Items.Add(btn1);
            
            mainContainer.Items.Add(contentMenu);

            this.Items.Add(mainContainer);

            base.Designer();
        }
    }
}
