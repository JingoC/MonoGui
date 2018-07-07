using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
namespace MonoGuiFramework.Containers
{
    using Microsoft.Xna.Framework;
    using MonoGuiFramework.Base;

    public class HorizontalContainer : Container
    {
        public HorizontalContainer(Region parent = null) : base(parent)
        {
            this.Items.CollectionChanged += this.Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Region newItem in e.NewItems)
                {
                    newItem.BoundsChanged += (s, ex) => this.UpdateBounds();
                }

                this.UpdateBounds();
            }
        }

        public override void UpdateBounds()
        {
            float xl = this.Position.Absolute.X;
            float xr = this.Position.Absolute.X + this.MaxWidth;

            foreach (var item in this.Items)
            {
                float x = 0;
                float y = 0;

                if (item.IsAlign(AlignmentType.Right))
                {
                    x = xr - item.Width - item.Position.Relative.X;
                    xr = x;
                }
                else
                {
                    x = xl + item.Position.Relative.X;
                    xl = x + item.Width;
                }

                if (item.IsAlign(AlignmentType.Top)) { y = this.Position.Absolute.Y + item.Position.Relative.Y; }
                else if (item.IsAlign(AlignmentType.Middle)) { y = (this.Position.Absolute.Y + this.MaxHeight / 2) + (item.Position.Relative.Y - item.Height / 2); }
                else if (item.IsAlign(AlignmentType.Bottom)) { y = this.Position.Absolute.Y + this.MaxHeight - item.Height - item.Position.Relative.Y; }
                else { y = this.Position.Absolute.Y + item.Position.Relative.Y; }

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
