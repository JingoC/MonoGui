using System;
using MonoGuiExample.View;
using MonoGuiFramework;
using MonoGuiFramework.System;

namespace MonoGuiExample
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Form cursorPos = new System.Windows.Forms.Form();
            cursorPos.MinimizeBox = false;
            cursorPos.MaximizeBox = false;
            cursorPos.Show();
            cursorPos.SetBounds(1340, 0, 10, 70);

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

            using (var monoGui = new GameView())
            {
                monoGui.Graphics.GetGraphics().PreferredBackBufferWidth = 1300;
                monoGui.Graphics.GetGraphics().PreferredBackBufferHeight = 800;
                monoGui.Graphics.GetGraphics().ApplyChanges();

                monoGui.Input.TouchEnable = false;
                monoGui.Input.PositionChangedMouse += (s, e) => PrintMouseInfo(e);
                monoGui.Input.ScrollingMouse += (s, e) => PrintMouseInfo(e);
                monoGui.Input.Swype += (s, e) => { System.Windows.Forms.MessageBox.Show($"{e.Device.ToString()}: {e.Swype.ToString()}"); };
                monoGui.Input.MoveMouse += (s, e) => { System.Windows.Forms.MessageBox.Show($"Move: ({e.X},{e.Y}) - ({e.X2},{e.Y2})"); };
                monoGui.Exit += (s, e) => Environment.Exit(0);

                monoGui.Run();
            }
        }
    }
#endif
}
