using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiExample.View
{
    using Microsoft.Xna.Framework;
    using MonoGuiFramework;
    using MonoGuiFramework.Base;
    using MonoGuiFramework.Controls;
    using MonoGuiFramework.System;
    using System.Timers;

    public class GameView : MonoGui
    {
        public GameView() : base()
        {
            this.Initialize += (s, e) =>
            {
                List<Activity> views = new List<Activity>();

                ActivityBase aBase = new ActivityBase();
                ActivitySwypeRight aSwypeR = new ActivitySwypeRight();
                ActivitySwypeLeft aSwypeL = new ActivitySwypeLeft();
                ActivityUp aSwypeU = new ActivityUp();
                ActivityDown aSwypeD = new ActivityDown();

                ActivitiyAlign aAlign = new ActivitiyAlign();

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

                this.Activities.Add(aBase);
                this.Activities.Add(aSwypeR);
                this.Activities.Add(aSwypeL);
                this.Activities.Add(aSwypeU);
                this.Activities.Add(aSwypeD);
                this.Activities.Add(aAlign);
                
                this.ActivitySelected = aAlign;
            };
        }
    }
}
