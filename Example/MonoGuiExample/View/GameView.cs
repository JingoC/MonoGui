using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiExample.View
{
    using MonoGuiFramework;
    using MonoGuiFramework.Controls;

    public class GameView : MonoGui
    {
        List<Activity> views = new List<Activity>();
        
        public GameView() : base()
        {
            this.Activities.Add(new ActivityBase(null));
            this.ActivitySelected = this.Activities.Last();
        }
    }
}
