using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGuiFramework.Controls
{
    using MonoGuiFramework.System;

    public class Form : Container
    {
        Label labelTitle = new Label() { };
        Label labelMessage = new Label() { };

        public string Title { get; set; }
        public string Message { get; set; }
        
        public Form()
        {
            this.Visible = false;
            
            this.Items.Add(this.labelTitle);
            this.Items.Add(this.labelMessage);
        }

        public void Show()
        {
            this.Visible = true;
        }

        public void Close()
        {
            this.Visible = false;
        }

        public override void Designer()
        {

            base.Designer();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsSingleton.GetInstance().GraphicsDevice.Clear(Color.Red);

            base.Draw(gameTime);
        }
    }
}
