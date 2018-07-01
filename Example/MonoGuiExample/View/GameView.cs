using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiExample.View
{
    using MonoGuiFramework;
    using MonoGuiFramework.Controls;
    using MonoGuiFramework.System;
    using System.Timers;

    public class GameView : MonoGui
    {
        List<Activity> views = new List<Activity>();

        ActivityBase aBase = new ActivityBase();
        ActivitySwypeRight aSwypeR = new ActivitySwypeRight();
        ActivitySwypeLeft aSwypeL = new ActivitySwypeLeft();
        ActivityUp aSwypeU = new ActivityUp();
        ActivityDown aSwypeD = new ActivityDown();
        
        public GameView() : base()
        {
            /*
            this.aBase.Navigation[(int)TypeNavigationActivity.Right] = this.aSwypeR;
            this.aBase.Navigation[(int)TypeNavigationActivity.Left] = this.aSwypeL;
            this.aBase.Navigation[(int)TypeNavigationActivity.Up] = this.aSwypeU;
            this.aBase.Navigation[(int)TypeNavigationActivity.Down] = this.aSwypeD;

            this.aSwypeR.Navigation[(int)TypeNavigationActivity.Left] = this.aBase;
            this.aSwypeL.Navigation[(int)TypeNavigationActivity.Right] = this.aBase;
            this.aSwypeU.Navigation[(int)TypeNavigationActivity.Down] = this.aBase;
            this.aSwypeD.Navigation[(int)TypeNavigationActivity.Up] = this.aBase;
            */
            this.Activities.Add(this.aBase);
            this.Activities.Add(this.aSwypeR);
            this.Activities.Add(this.aSwypeL);
            this.Activities.Add(this.aSwypeU);
            this.Activities.Add(this.aSwypeD);

            this.ActivitySelected = this.aBase;
        }
    }
}
