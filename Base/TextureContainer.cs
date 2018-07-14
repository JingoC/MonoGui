using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.Base
{
    using Microsoft.Xna.Framework.Graphics;

    public class ResourceContainter<T>
    {
        private List<T> items = new List<T>();
        private T defaultItem;

        public T Current { get; private set; }

        public void Change(int number)
        {
            if (number < this.items.Count)
                this.Current = this.items[number];
        }

        public void Add(T item)
        {
            if (item != null)
            {
                this.items.Add(item);

                if (this.items.Count == 1)
                {
                    this.SetDefault(0);
                    this.RestoreDefault();
                }
            }
        }

        public void Clear() => this.items.Clear();
        public void AddRange(IEnumerable<T> collection) { foreach (var item in collection) this.Add(item); }
        public int Count() => this.items.Count;
        public void RestoreDefault() => this.Current = this.defaultItem;

        public void SetDefault(int number)
        {
            if ((number < this.Count()) && (number >= 0))
                this.defaultItem = this.items[number];
        }
    }

    public class TextureContainer
    {
        public ResourceContainter<Texture2D> Textures { get; private set; } = new ResourceContainter<Texture2D>();
        public ResourceContainter<SpriteFont> Fonts { get; private set; } = new ResourceContainter<SpriteFont>();

        public TextureContainer()
        {

        }
    }
}
