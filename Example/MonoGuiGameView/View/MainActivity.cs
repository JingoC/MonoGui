using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using MonoGuiFramework;
using MonoGuiFramework.Base;
using MonoGuiFramework.Containers;
using MonoGuiFramework.Controls;

namespace MonoGuiGameView.View
{
    public class MainActivity : Activity
    {
        public MainActivity(Activity parent = null) : base(parent)
        {
            
        }

        public override void Designer()
        {
            VerticalContainer mainContainer = new VerticalContainer(this);

            VerticalContainer top = new VerticalContainer(this) {  };

            HorizontalContainer content = new HorizontalContainer(this) {  };
            VerticalContainer contentGame = new VerticalContainer(this) { BorderColor = Color.Gray, TextureScale = ScaleMode.None };
            VerticalContainer contentMenu = new VerticalContainer(this) { Align = AlignmentType.Right | AlignmentType.Middle };
            contentMenu.MaxHeight = 500;
            contentMenu.MaxWidth = 500;
            contentMenu.SetBounds(20, 0, 500, 500);

            Label topLabel = new Label(top) { ForeColor = Color.White, Text = "Top text", Align = AlignmentType.Center };
            topLabel.SetBounds(0, 10, 10, 10);
            
            Button btn1 = new Button(contentMenu) { Scale = 2f };
            btn1.SetBounds(0, 10, 100, 100);
            Button btn2 = new Button(contentMenu) { Scale = 1.5f };
            btn2.SetBounds(0, 10, 100, 100);
            Button btn3 = new Button(contentMenu);
            btn3.SetBounds(0, 10, 100, 100);

            top.Items.Add(topLabel);

            contentMenu.Items.Add(btn1);
            contentMenu.Items.Add(btn2);
            contentMenu.Items.Add(btn3);

            content.Items.Add(contentGame);
            content.Items.Add(contentMenu);

            mainContainer.Items.Add(top);
            mainContainer.Items.Add(content);

            this.Items.Add(mainContainer);

            base.Designer();
        }
    }
}
