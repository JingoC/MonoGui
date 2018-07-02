using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;

    using Base;
    using Controls;
    using System;

    public enum TypeNavigationActivity
    {
        None = 0,
        Left = 1,
        Right = 2,
        Down = 3,
        Up = 4,
        Back = 5,
        Last = 6
    }

    public class Activity : Container
    {
        static int countActivity = 0;

        public Activity[] Navigation { get; set; } = new Activity[(int)TypeNavigationActivity.Last];
        public Activity Parent { get; set; }
        public int Offset { get; private set; }
        public override int Width { get => GraphicsSingleton.GetInstance().GetGraphics().PreferredBackBufferWidth; }
        public override int Height { get => GraphicsSingleton.GetInstance().GetGraphics().PreferredBackBufferHeight; }

        public Texture2D BackgroundImage { get; set; } = null;
        public Color Background { get; set; }
        
        public virtual void ChangeActivity(bool active)
        {

        }

        public Activity(Activity parent)
        {
            this.Parent = parent;
            this.Name = $"Activity{countActivity}";
            countActivity++;
        }

        public override void Designer()
        {
            base.Designer();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = GraphicsSingleton.GetInstance().GetSpriteBatch();

            if (this.BackgroundImage != null)
            {
                sb.Begin();
                sb.Draw(this.BackgroundImage, new Rectangle(0, 0, this.Width, this.Height), Color.White);
                sb.End();
            }
            else
            {
                GraphicsSingleton.GetInstance().GraphicsDevice.Clear(this.Background);
            }

            sb.Begin();
            base.Draw(gameTime);
            sb.End();
        }

        public Region GetChild(string childName)
        {
            Region Find(IEnumerable<Region> src, string name)
            {
                foreach(Region item in src)
                {
                    if (item == null)
                        continue;

                    if (item is Container)
                    {
                        var r = Find((item as Container).Items, name);
                        if (r != null)
                            return r;
                    }
                    else if (item.Name.Equals(name))
                        return item;
                }

                return null;
            }

            return Find(this.Items, childName);
        }

        public void Scroll(int pixel)
        {
            this.SetBounds((int)this.Position.Absolute.X, (int)this.Position.Absolute.Y + pixel, this.Width, this.Height);
        }
    }
}
