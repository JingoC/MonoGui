using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoGuiFramework;

using MonoGuiGameView.View;

namespace MonoGuiGameView
{
    public class TestGameView : MonoGui
    {
        public TestGameView()
        {
            this.Initialize += (s, e) =>
                {
                    var mainActivity = new MainActivity();
                    var swypeLeftActivity = new SwypeLeftActivity();

                    mainActivity.Navigation[(int)TypeNavigationActivity.Left] = swypeLeftActivity;
                    swypeLeftActivity.Navigation[(int)TypeNavigationActivity.Right] = mainActivity;

                    this.Activities.Add(mainActivity);
                    this.ActivitySelected = mainActivity;
                };
        }
    }
}
