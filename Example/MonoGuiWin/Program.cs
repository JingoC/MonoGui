using System;

using MonoGuiFramework.System;

using MonoGuiGameView;

namespace MonoGuiWin
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        static string json = "[" +
                "{\"Name\": \"btn_idle_tmp\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"btn_click_tmp\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"defaultToggleOn\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"defaultToggleOff\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"defaultChangerDown\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"defaultChangerUp\", \"Type\": \"Texture2D\"}," +
                "{\"Name\": \"defaultFont\", \"Type\": \"Font\"}," +
                "]";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Support forms for debug
            System.Windows.Forms.Form cursorPos = new System.Windows.Forms.Form();
            cursorPos.MinimizeBox = false;
            cursorPos.MaximizeBox = false;
            cursorPos.Show();
            cursorPos.SetBounds(1340, 0, 200, 70);

            System.Windows.Forms.Form dbg = new System.Windows.Forms.Form();
            System.Windows.Forms.TextBox tb = new System.Windows.Forms.TextBox();
            tb.Multiline = true;
            tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            tb.Dock = System.Windows.Forms.DockStyle.Fill;
            tb.ReadOnly = true;
            dbg.Controls.Add(tb);
            dbg.MinimizeBox = false;
            dbg.MaximizeBox = false;
            dbg.Show();
            dbg.SetBounds(1340, 80, 580, 300);

            float x = 0;
            float y = 0;
            int scroll = 0;

            void PrintMouseInfo(DeviceEventArgs e)
            {
                x = e.X >= 0 ? e.X : x;
                y = e.Y >= 0 ? e.Y : y;
                scroll = e.ScrollValue >= 0 ? e.ScrollValue : scroll;
                cursorPos.Text = $"{x}:{y}:{scroll}";
            }

            // Add resources
            Resources.AddJsonLoadResources(json);

            // Setting logger io on Form
            var logger = LoggerSingleton.GetInstance();
            logger.Stream = new IOStream();
            logger.Stream.Write += (t) => tb.AppendText(t);

            // Create Application object and Run
            using (var monoGuiGame = new TestGameView())
            {
                // Set screen resolution
                monoGuiGame.Graphics.GetGraphics().PreferredBackBufferWidth = 1000;
                monoGuiGame.Graphics.GetGraphics().PreferredBackBufferHeight = 700;
                monoGuiGame.Graphics.GetGraphics().ApplyChanges();

                // set input debug mouse info
                monoGuiGame.Input.TouchEnable = false;
                monoGuiGame.Input.PositionChangedMouse += (s, e) => PrintMouseInfo(e);
                monoGuiGame.Input.ScrollingMouse += (s, e) => PrintMouseInfo(e);
                
                // close application event
                monoGuiGame.Exit += (s, e) => Environment.Exit(0);

                // Run application
                monoGuiGame.Run();
            }
        }
    }
#endif
}
