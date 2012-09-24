using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Caliburn.Micro.Navigation
{
    public class PersitableScreen : Screen, IPersistableScreen 
    {

        public PersitableScreen()
        {
            ShouldClose = true;
        }

        //TODO: Some properties could be private set and passed through constructor for security reasons
        public Type ViewType { get;  set; }
        public String CurrentSource { get; set; }
        public String ApplicationSuite { get; set; }
        public String Mode { get; set; }
        public String Title { get; set; }
        public String Key { get; set; }
        public String ParentUriOriginalString { get; set; }
        public String UriString { get; set; }
        public String TrackingKey { get;  set; }
        public Boolean IsChainable { get; set; }

        /// <summary>
        /// By default NavigationScreen is NonPersistable (i.e. Transient, not Cached) and this value os true
        /// </summary>
        public virtual bool ShouldClose { get; set; }

        public virtual void Close()
        {
            this.ShouldClose = true;
            //TODO: Should go to its parent view
            ((NavigationConductor)this.Parent).NavigationService.Navigate(new Uri("/Home", UriKind.Relative));

        }
       
    }
}
