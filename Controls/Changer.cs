using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.Controls
{
    using MonoGuiFramework.System;

    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;

    using MonoGuiFramework.Base;

    public class ValueRange
    {
        private double value;
        
        public double Min { get; set; }
        public double Max { get; set; }
        public double Value
        {
            get { return Math.Round(this.value, 2); }
            set
            {
                if (value >= this.Max)
                    this.value = this.Max;
                else if (value < this.Min)
                    this.value = this.Min;
                else
                    this.value = value;
            }
        }
        
        public ValueRange(double min, double max)
        {
            if (min > max)
                throw new Exception("ValueRange error: min not will be more max");

            this.Min = min;
            this.Max = max;

            this.Value = Math.Round((this.Max - this.Min) / 2 + this.Min);
        }
    }

    public class Changer : Container
    {
        Button btnDown = new Button();
        Button btnUp = new Button();
        Label labelValue = new Label();

        public ValueRange Current { get; set; }
        public double Step { get; set; }

        public event EventHandler ClickToDown;
        public event EventHandler ClickToUp;

        public string Text { get; set; }
        public Color ForeColor { get; set; }

        //public string Text { get => this.labelValue.Text; set => this.labelValue.Text = value; }
        //public Color ForeColor { get => this.labelValue.ForeColor; set => this.labelValue.ForeColor = value; }
        
        public Changer() : this(new ValueRange(-10000, 10000))
        {

        }

        public Changer(ValueRange current)
        {
            this.Current = current;

            this.Items.Add(this.btnDown);
            this.Items.Add(this.labelValue);
            this.Items.Add(this.btnUp);
        }
        
        public override void Designer()
        {
            this.btnDown.Name = "Down";
            this.btnDown.Position = new Position(0, 0);
            this.btnDown.TextureManager.Textures.Add(Resources.GetResource("defaultChangerDown") as Texture2D);
            this.btnDown.OnClick += this.OnClickDown_Handler;

            this.labelValue.Name = "Value";
            //this.labelValue.ForeColor = Color.White;
            this.labelValue.Position = new Position(this.btnDown.Width + 2, 0);
            //this.labelValue.Text = this.Current.Value.ToString();

            this.btnUp.Name = "Up";
            this.btnUp.Position = new Position((int)this.labelValue.Position.Relative.X + 26, 0);
            this.btnUp.TextureManager.Textures.Add(Resources.GetResource("defaultChangerUp") as Texture2D);
            this.btnUp.OnClick += this.OnClickUp_Handler;
            
            base.Designer();
        }

        private void OnClickDown_Handler(Object sender, EventArgs e)
        {
            this.Current.Value -= this.Step;
            //this.labelValue.Text = this.Current.Value.ToString();
            this.ClickExecute(this.ClickToDown);
        }

        private void OnClickUp_Handler(Object sender, EventArgs e)
        {
            this.Current.Value += this.Step;
            //this.labelValue.Text = this.Current.Value.ToString();
            this.ClickExecute(this.ClickToUp);
        }

        private void ClickExecute(EventHandler click)
        {
            if (click != null)
                click(this, EventArgs.Empty);
        }

        public override void Draw(GameTime gameTime)
        {
            var ofsX = (int)this.Position.Absolute.X;
            var ofsY = (int)this.Position.Absolute.Y;
            /*
            this.btnDown.Position = new Position(ofsX, ofsY);
            this.labelValue.Position = new Vector2((int)this.btnDown.Position.Relative.X + this.btnDown.Width + 20, ofsY + (this.btnDown.Height / 2) - (this.labelValue.Height / 2));
            this.btnUp.Position = new Vector2(this.labelValue.Position.X + this.labelValue.Width + 20, ofsY);
            */
            base.Draw(gameTime);
        }
    }
}
