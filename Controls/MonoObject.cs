using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.Controls
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;

    using MonoGuiFramework.System;

    using MonoGuiFramework.Base;

    public enum TextAlignHorizontal
    {
        Left = 1,
        Center = 2,
        Right = 3
    }

    public enum TextAlignVertical
    {
        Top = 1,
        Center = 2,
        Botton = 3
    }

    public class MonoObject : Control
    {
        public event EventHandler OnClick;
        public event EventHandler OnPressed;

        public MonoObject(Region parent = null) : base(parent)
        {

        }

        public override bool CheckEntry(float x, float y)
        {
            bool isEntry = base.IsEntry(x, y);

            if (isEntry)
            {
                this.OnClick?.Invoke(this, EventArgs.Empty);
            }

            return isEntry;
        }
    }
}
