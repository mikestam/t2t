using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Caliburn.Micro.Navigation;

namespace T2T
{
    [Export(typeof(SimulationViewModel)),PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
   
    public class SimulationViewModel : PersitableScreen
    {
        Int32 _counter = 0;
        Timer _timer;

        public SimulationViewModel()
        {
            this.ShouldClose = false ;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _timer = new Timer(TimerCallback, null, 0, 1000);
        }

        void TimerCallback(Object state)
        {
            _counter += 1;
            ElapsedTime = _counter.ToString();
            NotifyOfPropertyChange(() => ElapsedTime);
        
        }

        public string ElapsedTime { get; set; }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            
        }

    }
}
