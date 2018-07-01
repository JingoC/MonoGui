using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MonoGuiFramework
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Controls;
    using MonoGuiFramework.System;
    using MonoGuiFramework.Base;

    public class MonoGui : IDisposable
    {
        public Graphics Graphics { get; private set; } = GraphicsSingleton.GetInstance();
        public Input Input { get; private set; } = InputSingleton.GetInstance();

        public List<Activity> Activities { get; private set; } = new List<Activity>();

        public Activity ActivitySelected;
        
        public event EventHandler UpdateEvent;
        public event EventHandler Exit;

        public MonoGui()
        {
            int clickMove = 0;
            this.Input.ClickTouch += (s, e) => { foreach (Region item in this.ActivitySelected.Items) item.CheckEntry(e.X, e.Y); };
            this.Input.ClickMouse += (s, e) => { clickMove = (int)e.Y; foreach (Region item in this.ActivitySelected.Items) item.CheckEntry(e.X, e.Y); };
            this.Input.PressedMouse += this.Input_Pressed;
            this.Input.PressedTouch += this.Input_Pressed;

            this.Input.BackKeyboard += delegate (Object sender, DeviceEventArgs e)
            {
                if ((this.ActivitySelected != null) && (this.ActivitySelected.Parent != null))
                {
                    this.ActivitySelected.ChangeActivity(false);
                    this.ActivitySelected.Parent.ChangeActivity(true);
                    this.ActivitySelected = this.ActivitySelected.Parent;
                }
                else
                {
                    if (this.Exit != null)
                        this.Exit(this, EventArgs.Empty);
                }
            };

            this.Input.ScrollingMouse += delegate (Object sender, DeviceEventArgs e)
            {
                if (this.ActivitySelected != null)
                {
                    float s = (float) (e.ScrollValue / 120 * 0.1);
                    foreach (Region item in this.ActivitySelected.Items) item.Scale += s;
                }
            };

            this.Input.Swype += delegate (Object sender, DeviceEventArgs e)
            {
                var s = e.Swype;
                if (s != TypeSwype.None)
                {
                    if ((s == TypeSwype.Up) || (s == TypeSwype.Down))
                    {
                        //int scrollValue = (int) ((e.Y2 - e.Y) / 10);
                        //this.ActivitySelected.Scroll(scrollValue);
                    }
                    else
                    {
                        var slctActivity = this.ActivitySelected.Navigation[(int)s];
                        if (slctActivity != null)
                        {
                            this.ActivitySelected.ChangeActivity(false);
                            slctActivity.ChangeActivity(true);
                            this.ActivitySelected = slctActivity;
                        }
                    }
                }
            };
            
            this.Input.ClickMoveMouse += delegate (object sender, DeviceEventArgs e)
            {
                int dy = (int) (e.Y2 - clickMove);
                
                if (Math.Abs(dy) > 5)
                {
                    this.ActivitySelected.Scroll(dy);
                    clickMove = (int)e.Y2;
                }
            };

            this.Activities.Add(new Activity(null));
            this.Activities.Last().Background = Color.Black;
            this.ActivitySelected = this.Activities.Last();

            this.Graphics.DrawEvent += (s, e) => { if (this.ActivitySelected != null) { this.ActivitySelected.Draw(s as GameTime); } };
            this.Graphics.UpdateEvent += delegate (object s, EventArgs e)
            {
                this.Input.Update();

                if (this.ActivitySelected != null)
                    this.ActivitySelected.Update(s as GameTime);

                if (this.UpdateEvent != null)
                    this.UpdateEvent(s, e);
            };

            this.Graphics.LoadContentEvent += (s, e) => { Resources.LoadResource(); this.Activities.ForEach(x => x.Designer()); };
        }

        private void Input_Pressed(Object sender, DeviceEventArgs e)
        {
            foreach(var item in this.ActivitySelected.Items)
            {
                //item.CheckEntryPressed(e.X, e.Y);
            }
        }

        public void Run()
        {
            this.Graphics.Run();
        }

        public void SelectActivity(String name)
        {
            var activity = this.Activities.FirstOrDefault((x) => x.Name.Equals(name));
            if (activity != null)
                this.ActivitySelected = activity;
        }

        public void Dispose()
        {
            
        }
    }
}
