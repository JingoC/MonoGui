using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.Containers
{
    using global::System.Collections.Specialized;
    using Microsoft.Xna.Framework;
    using MonoGuiFramework.Base;

    public class VerticalContainer : Container
    {
        public VerticalContainer(Region parent = null) : base(parent)
        {
            this.Width = this.MaxWidth;
            this.Height = this.MaxHeight;
        }

        public override void UpdateBounds()
        {
            var yt = this.Position.Absolute.Y;
            var yb = this.Position.Absolute.Y + this.MaxHeight;

            foreach (var item in this.Items)
            {
                float x = 0;
                float y = 0;

                if (item.IsAlign(AlignmentType.Left)) { x = this.Position.Absolute.X + item.Position.Relative.X; }
                else if (item.IsAlign(AlignmentType.Center)) { x = (this.Position.Absolute.X + this.MaxWidth / 2) + (item.Position.Relative.X - item.Width / 2); }
                else if (item.IsAlign(AlignmentType.Right)) { x = this.Position.Absolute.X + this.MaxWidth - item.Width - item.Position.Relative.X; }
                else { x = this.Position.Absolute.X + item.Position.Relative.X; }

                if (item.IsAlign(AlignmentType.Bottom))
                {
                    y = yb - item.Height - item.Position.Relative.Y;
                    yb = y;
                }
                else
                {
                    y = yt + item.Position.Relative.Y;
                    yt = y + item.Height;
                }

                item.Position.Absolute = new Vector2(x, y);
                
                if (item is Container)
                    (item as Container).UpdateBounds();
            }
        }

        public override void SetBounds(int x, int y, int width, int height)
        {
            base.SetBounds(x, y, width, height);
            this.UpdateBounds();
        }

    }
}
