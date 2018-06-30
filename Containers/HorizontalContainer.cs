using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
namespace MonoGuiFramework.Containers
{
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
                foreach(var newItem in e.NewItems) (newItem as Region).BoundsChanged += (s, ex) => this.UpdateBounds();
            }
        }

        private void UpdateBounds()
        {
            foreach(var item in this.Items)
            {
                
            }
        }
    }
}
