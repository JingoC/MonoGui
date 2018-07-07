using MonoGuiFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiExample.View
{
    using MonoGuiFramework.Containers;
    using MonoGuiFramework.Controls;
    using MonoGuiFramework.Base;
    using Microsoft.Xna.Framework;

    public class ActivitiyAlign : Activity
    {
        public ActivitiyAlign(Activity parent = null) : base(parent)
        {
            VerticalContainer rows = new VerticalContainer(this);
            rows.TextureScale = ScaleMode.None;
            rows.BorderColor = Color.Red;

            Container c1 = new Container(rows);
            c1.TextureScale = ScaleMode.None;
            c1.SetBounds(100, 20, 100, 300);
            c1.BorderColor = Color.Yellow;
            rows.Items.Add(c1);

            Button b1 = new Button(c1);
            b1.Name = "b1";
            b1.Scale = 0.2f;
            b1.BorderColor = Color.Green;
            b1.Position = new Position(0, 0);
            b1.Align = AlignmentType.Left | AlignmentType.Top;
            c1.Items.Add(b1);

            Button b2 = new Button(c1);
            b2.Name = "b2";
            b2.Scale = 0.2f;
            b2.Align = AlignmentType.Center | AlignmentType.Middle;
            c1.Items.Add(b2);

            Button b3 = new Button(c1);
            b3.Name = "b3";
            b3.Scale = 0.2f;
            b3.Align = AlignmentType.Right | AlignmentType.Bottom;
            c1.Items.Add(b3);

            this.Items.Add(rows);

            Container c2 = new Container();
            c2.TextureScale = ScaleMode.None;
            c2.SetBounds(0, 10, 600, 200);
            c2.BorderColor = Color.Aqua;
            rows.Items.Add(c2);

            Button b4 = new Button(c2);
            b4.Name = "b4";
            b4.BorderColor = Color.Green;
            b4.Position = new Position(0, 0);
            b4.Align = AlignmentType.Right | AlignmentType.Top;
            c2.Items.Add(b4);

            Button b5 = new Button(c2);
            b5.Name = "b5";
            b5.Align = AlignmentType.Center | AlignmentType.Bottom;
            c2.Items.Add(b5);

            Button b6 = new Button(c2);
            b6.Name = "b6";
            b6.Align = AlignmentType.Left | AlignmentType.Middle;
            c2.Items.Add(b6);

            HorizontalContainer h1 = new HorizontalContainer(rows);
            h1.TextureScale = ScaleMode.None;
            h1.MaxHeight = 300;
            h1.SetBounds(0, 0, this.MaxWidth, 200);
            h1.BorderColor = Color.White;
            rows.Items.Add(h1);

            Button b7 = new Button(h1);
            b7.Align = AlignmentType.Right;
            h1.Items.Add(b7);

            Button b8 = new Button(h1);
            b8.Align = AlignmentType.Right | AlignmentType.Middle;
            b8.Position = new Position(10, 0);
            h1.Items.Add(b8);

            VerticalContainer v1 = new VerticalContainer(rows);
            v1.TextureScale = ScaleMode.None;
            v1.BorderColor = Color.Orange;
            v1.Align = AlignmentType.Left;
            v1.SetBounds(0, 0, 500, 400);
            h1.Items.Add(v1);

            Button b9 = new Button(v1);
            b9.Align = AlignmentType.Bottom | AlignmentType.Top;
            v1.Items.Add(b9);

            Button b10 = new Button(v1);
            b10.Align = AlignmentType.Bottom | AlignmentType.Right;
            v1.Items.Add(b10);

            this.Designer();
        }


    }
}
