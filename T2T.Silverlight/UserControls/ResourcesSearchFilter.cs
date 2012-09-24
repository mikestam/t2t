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
using System.ComponentModel;

namespace T2T
{
    public class ResourcesSearchFilter : INotifyPropertyChanged
    {

        private string _keyword;
   

        public event PropertyChangedEventHandler PropertyChanged;

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                NotifyPropertyChanged("Keyword");
            }
        }

        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
}
