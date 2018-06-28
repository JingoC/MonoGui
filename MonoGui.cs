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
            this.Input.ClickTouch += (s, e) => this.ActivitySelected.Items.ForEach((x) => x.CheckEntry(e.X, e.Y));
            this.Input.ClickMouse += (s, e) => this.ActivitySelected.Items.ForEach((x) => x.CheckEntry(e.X, e.Y));
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
            //this.Graphics.RunOneFrame();
        }

        private void Input_Pressed(Object sender, DeviceEventArgs e)
        {
            foreach(var item in this.ActivitySelected.Items)
            {
                item.CheckEntryPressed(e.X, e.Y);
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
