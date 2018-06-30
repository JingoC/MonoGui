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
        private List<int> offsetVertical = new List<int>();

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
                    this.offsetVertical.Add((int)newItem.Position.Y);
                }
                
                this.UpdateBounds();
            }
        }

        private void UpdateBounds()
        {
            int y = 0;
            int count = 0;
            foreach (var item in this.Items)
            {
                item.Position = new Vector2(item.Position.X, y + this.offsetVertical[count]);
                y = (int)item.Position.Y + item.Height;
                count++;
            }
        }

    }
}
