﻿using System;
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
            int x = (int) this.Position.Absolute.X;
            int count = 0;
            foreach (var item in this.Items)
            {
                item.Position.Absolute = new Vector2(x + item.Position.Relative.X, item.Position.Relative.Y + this.Position.Absolute.Y);
                x = (int)item.Position.Absolute.X + item.Width;
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
