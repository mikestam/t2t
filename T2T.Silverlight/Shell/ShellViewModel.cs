namespace T2T {
    using System.ComponentModel.Composition;
    using Caliburn.Micro.Navigation;
    using Caliburn.Micro;
    using System.Windows.Data;

    [Export(typeof(IShell))]
    public class ShellViewModel : NavigationConductor, IShell
    {
   
        [ImportingConstructor]
        public ShellViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigationService.Conductor = this;
          
        }

        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            NavigationService.RootFrame = ((ShellView)view).ActiveItem;
        }

        public void ShowPageOne()
        {
            //ActivateItem<PageOneViewModel>();
            int count = this.Items.Count;
            NavigationService.UriFor<PageOneViewModel>().WithParam(x=>x.Count ,5).Navigate();

        }

        public void ShowPageTwo()
        {
            //ActivateItem<PageTwoViewModel>();
            NavigationService.UriFor<PageTwoViewModel>().Navigate();
        }

        public void Proba()
        {
            NavigationService.UriFor<PageTwoViewModel>().Navigate();
        }

        private CollectionViewSource _activeScreens = new CollectionViewSource();
        public CollectionViewSource ActiveScreens { get { return _activeScreens; } }

        protected override void OnActivate()
        {
            base.OnActivate();

            ActiveScreens.Source = this.Items;
            Count = this.Names.Count;
        }

        int _count = 0;

        public int Count
        {
            get { return _count; }
            set { _count = value; NotifyOfPropertyChange("Count"); }
        }

        protected override void OnActivationProcessed(object item, bool success)
        {
            base.OnActivationProcessed(item, success);
            ActiveScreens.Source = this.Items;
            Count = this.Names.Count;
        }

    }
}
