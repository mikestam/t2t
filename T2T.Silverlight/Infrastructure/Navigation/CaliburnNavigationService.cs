namespace Caliburn.Micro.Navigation {
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;

    /// <summary>
    ///   Implemented by services that provide <see cref="Uri" /> based navigation.
    /// </summary>
    public interface INavigationService : INavigate {
        /// <summary>
        /// Root <see cref="Frame"/> of the Application in ShellView.
        /// </summary>
        Frame RootFrame { get; set; }

        /// <summary>
        /// Conductor that will conduct items instead of Frame NavigationService and also plays the role of ScreenCollection for conducted items.
        /// </summary>
        IConductor Conductor { get; set; }

        void TryInjectQueryString(object viewModel, object page);

        /// <summary>
        ///   The <see cref="Uri" /> source.
        /// </summary>
        Uri Source { get; set; }

        /// <summary>
        ///   Indicates whether the navigator can navigate back.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        ///   Indicates whether the navigator can navigate forward.
        /// </summary>
        bool CanGoForward { get; }

        /// <summary>
        ///   The current <see cref="Uri" /> source.
        /// </summary>
        Uri CurrentSource { get; }

        /// <summary>
        ///   The current content.
        /// </summary>
        object CurrentContent { get; }

        /// <summary>
        ///   Stops the loading process.
        /// </summary>
        void StopLoading();

        /// <summary>
        ///   Navigates back.
        /// </summary>
        void GoBack();

        /// <summary>
        ///   Navigates forward.
        /// </summary>
        void GoForward();

        /// <summary>
        ///   Removes the most recent entry from the back stack.
        /// </summary>
        /// <returns> The entry that was removed. </returns>
       // JournalEntry RemoveBackEntry();

        /// <summary>
        ///   Raised after navigation.
        /// </summary>
        event NavigatedEventHandler Navigated;

        /// <summary>
        ///   Raised prior to navigation.
        /// </summary>
        event NavigatingCancelEventHandler Navigating;

        /// <summary>
        ///   Raised when navigation fails.
        /// </summary>
        event NavigationFailedEventHandler NavigationFailed;

        /// <summary>
        ///   Raised when navigation is stopped.
        /// </summary>
        event NavigationStoppedEventHandler NavigationStopped;

        /// <summary>
        ///   Raised when a fragment navigation occurs.
        /// </summary>
        event FragmentNavigationEventHandler FragmentNavigation;
    }

    /// <summary>
    ///   A basic implementation of <see cref="INavigationService" /> designed to adapt the <see cref="Frame" /> control.
    /// </summary>
    public class CaliburnNavigationService : INavigationService {
        Frame _frame;
        readonly bool treatViewAsLoaded;
        event NavigatingCancelEventHandler ExternalNavigatingHandler = delegate { };
        CaliburnContentLoader _loader;


        /// <summary>
        ///   Creates an instance of <see cref="CaliburnNavigationService" />
        /// </summary>
        /// <param name="treatViewAsLoaded"> Tells the frame adapter to assume that the view has already been loaded by the time OnNavigated is called. This is necessary when using the TransitionFrame. </param>
        public CaliburnNavigationService(bool treatViewAsLoaded = false) {
           
            this.treatViewAsLoaded = treatViewAsLoaded;
        }

        /// <summary>
        ///   Creates an instance of <see cref="CaliburnNavigationService" />
        /// </summary>
        /// <param name="frame"> The frame to represent as a <see cref="INavigationService" /> . </param>
        /// <param name="treatViewAsLoaded"> Tells the frame adapter to assume that the view has already been loaded by the time OnNavigated is called. This is necessary when using the TransitionFrame. </param>
        public CaliburnNavigationService(Frame frame, bool treatViewAsLoaded = false)
        {
            this.RootFrame = frame;
            this.treatViewAsLoaded = treatViewAsLoaded;
        }


        public Frame RootFrame
        {
            get { return _frame; }
            set
            {
                this._frame = value;
                _loader = this._frame.ContentLoader as CaliburnContentLoader;
                if (_loader != null)
                     _loader.NavigationConductor = (INavigationConductor)Conductor;
                this._frame.Navigating += OnNavigating;
                this._frame.Navigated += OnNavigated;
                
            }
        }

        public IConductor Conductor { get; set; }
   

        /// <summary>
        ///   Occurs before navigation
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected virtual void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {

            ExternalNavigatingHandler(sender, e);
            if(e.Cancel) {
                return;
            }

            var fe = _frame.Content as FrameworkElement;
            if(fe == null) {
                return;
            }

          
            var guard = fe.DataContext as IGuardClose;
            if (guard != null && !e.Uri.IsAbsoluteUri)
            {
                var shouldCancel = false;
                guard.CanClose(result => { shouldCancel = !result; });

                if (shouldCancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

             _shouldClose = CanCloseOnNavigating(sender, e);
           
        }

        bool _shouldClose = false;

        /// <summary>
        /// Called to check whether or not to close current instance on navigating.
        /// </summary>
        /// <param name="sender"> The event sender from OnNavigating event. </param>
        /// <param name="e"> The event args from OnNavigating event. </param>
        protected virtual bool CanCloseOnNavigating(object sender, NavigatingCancelEventArgs e) {
  
            var fe = _frame.Content as FrameworkElement;
            if (fe == null)
            {
                return false;
            }
         

            var screen = fe.DataContext as PersitableScreen;
           
            if (screen != null)
            {
                return screen.ShouldClose;
            }

            return true;

        }

        /// <summary>
        ///   Occurs after navigation
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected virtual void OnNavigated(object sender, NavigationEventArgs e) {

            if (e.Uri.IsAbsoluteUri || e.Content == null)
            {
                return;
            }

            var page = e.Content as UserControl;
            if (page == null)
            {
                throw new ArgumentException("View '" + e.Content.GetType().FullName + "' should inherit from UserControl or one of its descendents.");
            }

            if (treatViewAsLoaded)
            {
                page.SetValue(View.IsLoadedProperty, true);
            }

            var screen = page.DataContext as PersitableScreen;
            if (screen != null)
            {
                screen.CurrentSource = e.Uri.OriginalString ;
                // TODO: is this should happend before or after binding
                TryInjectQueryString(screen, page);
            }
          
            var viewmodel = page.DataContext;

          
            ((NavigationConductor)Conductor).ActivateItem(viewmodel);

            var viewAware = viewmodel as ViewAware;
            if (viewAware != null)
            {
                //EventHandler onLayoutUpdate = null;
                //onLayoutUpdate = delegate
                //{
                //    viewAware.OnViewReady(page);
                //    page.LayoutUpdated -= onLayoutUpdate;
                //};
                //page.LayoutUpdated += onLayoutUpdate;
            }
            

         

            GC.Collect();
        }

        /// <summary>
        ///   Attempts to inject query string parameters from the view into the view model.
        /// </summary>
        /// <param name="viewModel"> The view model. </param>
        /// <param name="page"> The page. </param>
        public virtual void TryInjectQueryString(object viewModel, object  page) {
            var viewModelType = viewModel.GetType();
            Page _page = page as Page;
            if (_page != null)
            {
                foreach (var pair in _page.NavigationContext.QueryString)
                {
                    var property = viewModelType.GetPropertyCaseInsensitive(pair.Key);
                    if (property == null)
                    {
                        continue;
                    }

                    property.SetValue(
                        viewModel,
                        MessageBinder.CoerceValue(property.PropertyType, pair.Value, _page.NavigationContext),
                        null
                        );
                }

            }
        }

        /// <summary>
        ///   The <see cref="Uri" /> source.
        /// </summary>
        public Uri Source {
            get { return _frame.Source; }
            set { _frame.Source = value; }
        }

        /// <summary>
        ///   Indicates whether the navigator can navigate back.
        /// </summary>
        public bool CanGoBack {
            get { return _frame.CanGoBack; }
        }

        /// <summary>
        ///   Indicates whether the navigator can navigate forward.
        /// </summary>
        public bool CanGoForward {
            get { return _frame.CanGoForward; }
        }

        /// <summary>
        ///   The current <see cref="Uri" /> source.
        /// </summary>
        public Uri CurrentSource {
            get { return _frame.CurrentSource; }
        }

        /// <summary>
        ///   The current content.
        /// </summary>
        public object CurrentContent {
            get { return _frame.Content; }
        }

        /// <summary>
        ///   Stops the loading process.
        /// </summary>
        public void StopLoading() {
            _frame.StopLoading();
        }

        /// <summary>
        ///   Navigates back.
        /// </summary>
        public void GoBack() {
            _frame.GoBack();
        }

        /// <summary>
        ///   Navigates forward.
        /// </summary>
        public void GoForward() {
            _frame.GoForward();
        }

        /// <summary>
        ///   Navigates to the specified <see cref="Uri" /> .
        /// </summary>
        /// <param name="source"> The <see cref="Uri" /> to navigate to. </param>
        /// <returns> Whether or not navigation succeeded. </returns>
        public bool Navigate(Uri source) {
            return _frame.Navigate(source);
        }

        /// <summary>
        ///   Removes the most recent entry from the back stack.
        /// </summary>
        /// <returns> The entry that was removed. </returns>
        //public JournalEntry RemoveBackEntry() {
        //    return ((Page)frame.Content).NavigationService.RemoveBackEntry();
        //}

        /// <summary>
        ///   Raised after navigation.
        /// </summary>
        public event NavigatedEventHandler Navigated {
            add { _frame.Navigated += value; }
            remove { _frame.Navigated -= value; }
        }

        /// <summary>
        ///   Raised prior to navigation.
        /// </summary>
        public event NavigatingCancelEventHandler Navigating {
            add { ExternalNavigatingHandler += value; }
            remove { ExternalNavigatingHandler -= value; }
        }

        /// <summary>
        ///   Raised when navigation fails.
        /// </summary>
        public event NavigationFailedEventHandler NavigationFailed {
            add { _frame.NavigationFailed += value; }
            remove { _frame.NavigationFailed -= value; }
        }

        /// <summary>
        ///   Raised when navigation is stopped.
        /// </summary>
        public event NavigationStoppedEventHandler NavigationStopped {
            add { _frame.NavigationStopped += value; }
            remove { _frame.NavigationStopped -= value; }
        }

        /// <summary>
        ///   Raised when a fragment navigation occurs.
        /// </summary>
        public event FragmentNavigationEventHandler FragmentNavigation {
            add { _frame.FragmentNavigation += value; }
            remove { _frame.FragmentNavigation -= value; }
        }
    }
}