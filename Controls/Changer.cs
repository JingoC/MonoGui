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
    using MonoGuiFramework.Containers;

    public class ValueRange
    {
        public static ValueRange Default = new ValueRange(-10000, 10000);

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

    public class Changer : HorizontalContainer
    {
        Button btnDown;
        Button btnUp;
        Label labelValue;

        public ValueRange Current { get; set; }
        public double Step { get; set; }

        public event EventHandler ClickToDown;
        public event EventHandler ClickToUp;
        
        public string Text { get => this.labelValue.Text; set { this.labelValue.Text = value; this.UpdateBounds(); } }
        public Color ForeColor { get => this.labelValue.ForeColor; set => this.labelValue.ForeColor = value; }
        
        public Changer(ValueRange current, Region parent = null) : base(parent)
        {
            this.Current = current;

            this.Name = "Changer1";

            this.btnDown = new Button() { Name="BtnDown" };
            this.labelValue = new Label() { Name = "LblValue" };
            this.btnUp = new Button() { Name="BtnUp" };
            
            this.Items.Add(this.btnDown);
            this.Items.Add(this.labelValue);
            this.Items.Add(this.btnUp);
        }
        
        public override void Designer()
        {
            this.btnDown.Name = "Down";
            this.btnDown.SetBounds(0, 0, 10, 10);
            this.btnDown.TextureManager.Textures.Add(Resources.GetResource("defaultChangerDown") as Texture2D);
            this.btnDown.OnClick += this.OnClickDown_Handler;

            this.labelValue.Name = "Value";
            this.labelValue.ForeColor = Color.White;
            this.labelValue.Text = this.Current.Value.ToString();
            this.labelValue.SetBounds(5, 0, 10, 10);

            this.btnUp.Name = "Up";
            this.btnUp.SetBounds(5, 0, 10, 10);
            this.btnUp.TextureManager.Textures.Add(Resources.GetResource("defaultChangerUp") as Texture2D);
            this.btnUp.OnClick += this.OnClickUp_Handler;
            
            base.Designer();
        }

        private void OnClickDown_Handler(Object sender, EventArgs e)
        {
            this.Current.Value -= this.Step;
            this.Text = this.Current.Value.ToString();
            this.ClickToDown?.Invoke(this, EventArgs.Empty);
        }

        private void OnClickUp_Handler(Object sender, EventArgs e)
        {
            this.Current.Value += this.Step;
            this.Text = this.Current.Value.ToString();
            this.ClickToUp?.Invoke(this, EventArgs.Empty);
        }

        public override void UpdateBounds()
        {
            this.labelValue.SetBounds(5, (this.btnDown.Height / 2) - (this.labelValue.Height / 2), 10, 10);
            base.UpdateBounds();
        }
    }
}
