using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

using MonoGuiGameView;
using MonoGuiFramework;
using MonoGuiFramework.System;

namespace MonoGuiAndroid
{
    [Activity(Label = "MonoGuiAndroid"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class MainActivity : Microsoft.Xna.Framework.AndroidGameActivity
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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Add resources
            MonoGuiFramework.System.Resources.AddJsonLoadResources(json);

            // Setting dummy logger io
            var logger = LoggerSingleton.GetInstance();
            logger.Stream = new IOStream();
            logger.Stream.Write += (t) => { };

            // Create Application object and Run
            using (var monoGuiGame = new TestGameView())
            {
                // set input debug mouse info
                monoGuiGame.Input.MouseEnable = false;

                // close application event
                monoGuiGame.Exit += (s, e) => Java.Lang.JavaSystem.Exit(0);

                // Run application
                SetContentView((View)monoGuiGame.Graphics.Services.GetService(typeof(View)));
                monoGuiGame.Run();
            }
        }
    }
}

