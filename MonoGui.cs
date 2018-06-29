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
        string json = "[" +
                "{\"Name\": \"gameBtn1\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"gameBtn2\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"gameBtn3\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"gameBtn4\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"gameBtn5\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"btn_newgame_idle\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"btn_newgame_click\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"btn_settings_idle\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"btn_settings_click\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"btn_idle_tmp\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"btn_click_tmp\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"defaultToggleOn\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"defaultToggleOff\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"defaultChangerDown\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"defaultChangerUp\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_green\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_red\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_yellow\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_brown\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_purple\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_orange\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_limegreen\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_blue\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_aqua\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_aqua_checked\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_gray\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_white\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"hexagon_bonus_bomb\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"background_startpage\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"defaultFont\", \"Type\": \"Font\"}," +
                "{\"Name\": \"gameBtnFont\", \"Type\": \"Font\"}," +
                "{\"Name\": \"settingsHeaderFont\", \"Type\": \"Font\"}," +
                "{\"Name\": \"endMessageFont\", \"Type\": \"Font\"}," +
                "]";

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

            this.Graphics.LoadContentEvent += (s, e) => { Resources.LoadResource(this.json); this.Activities.ForEach(x => x.Designer()); };
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
