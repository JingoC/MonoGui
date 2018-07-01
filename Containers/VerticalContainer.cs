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
            int y = (int)this.Position.Absolute.Y;
            int count = 0;
            foreach (var item in this.Items)
            {
                item.Position.Absolute = new Vector2(item.Position.Relative.X + this.Position.Absolute.X, y + item.Position.Relative.Y);
                y = (int)item.Position.Absolute.Y + item.Height;
                if (item is Container)
                    (item as Container).UpdateBounds();
                count++;
            }
        }

        public override void SetBounds(int x, int y, int width, int height)
        {
            base.SetBounds(x, y, width, height);
            this.UpdateBounds();
        }

    }
}
