#region Using Directives

using System;
using System.Windows;
using System.Windows.Navigation;
using Caliburn.Micro.Navigation.ContentLoaders;
using System.Linq;
using System.Windows.Controls;

#endregion

namespace Caliburn.Micro.Navigation
{
    /// <summary>
    /// A Utility class that simplifies creation of an INavigationContentLoader.
    /// </summary>
    public class CaliburnContentLoader : ContentLoaderBase, INavigationContentLoader
    {
        #region Declarations

        static readonly INavigationContentLoader _defaultLoader = new PageResourceContentLoader();
        private CaliburnLoaderAsyncResult currentResult;

        #endregion //Declarations

        #region Dependency Properties

        /// <summary>
        /// Gets or sets the INavigationContentLoader being wrapped by the NonLinearNavigationContentLoader
        /// </summary>
        /// <value>The INavigationContentLoader</value>
        public INavigationContentLoader ContentLoader
        {
            get { return (INavigationContentLoader)GetValue(ContentLoaderProperty); }
            set { SetValue(ContentLoaderProperty, value); }
        }

        public static readonly DependencyProperty ContentLoaderProperty =
            DependencyProperty.Register("ContentLoader", typeof(INavigationContentLoader), typeof(CaliburnContentLoader), new PropertyMetadata(null));


        #endregion //Dependency Properties

        #region Properties

        INavigationConductor _conductor;
        public INavigationConductor NavigationConductor
        {
            get { return _conductor; }
            set
            {
                if (_conductor != value)
                {
                    if (_conductor != null)
                        ((IConductor)_conductor).ActivationProcessed -= ActivationProcessed;
                    _conductor = value;
                    ((IConductor)_conductor).ActivationProcessed += ActivationProcessed;
                }
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Used for viewmodel first navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ActivationProcessed(object sender, ActivationProcessedEventArgs e)
        {
            if (!e.Success) return;
            var screen = e.Item as IViewAware;
            if (screen == null)
                return;
            //if (loader.Result  != null)
            //    loader.Result.Page = screen.GetView();
        //    NavigationConductor.NavigationService.TryInjectQueryString(screen, currentResult.Page);
           // base.Complete(loader.Result);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a value that indicates whether the specified URI can be loaded.
        /// </summary>
        /// <param name="targetUri">The URI to test.</param>
        /// <param name="currentUri">The URI that is currently loaded.</param>
        /// <returns>true if the URI can be loaded; otherwise, false.</returns>
        public override bool CanLoad(Uri targetUri, Uri currentUri)
        {
            return (this.ContentLoader ?? _defaultLoader).CanLoad(targetUri, currentUri);
        }


        CaliburnNavigationLoader loader;
        /// <summary>
        /// Creates an instance of a LoaderBase that will be used to handle loading.
        /// </summary>
        /// <returns>An instance of a LoaderBase.</returns>
        protected override LoaderBase CreateLoader()
        {
             loader = new CaliburnNavigationLoader(this);
          
            return loader;
        }

        #region caliburn based Complete
        //internal void Complete()
        //{
        //    lock (currentResult.Lock)
        //    {
        //        Execute.OnUIThread(() => currentResult.Callback(currentResult));
        //        currentResult.Complete();
        //    }
        //    currentResult = null;
        //}
        #endregion

        #endregion //Methods

        //#region INavigationContentLoader Members

        ///// <summary>
        ///// Begins asynchronous loading of the content for the specified target URI.
        ///// </summary>
        ///// <param name="targetUri">The URI to load content for.</param>
        ///// <param name="currentUri">The URI that is currently loaded.</param>
        ///// <param name="userCallback">The method to call when the content finishes loading.</param>
        ///// <param name="asyncState">An object for storing custom state information.</param>
        ///// <returns>An object that stores information about the asynchronous operation.</returns>
        //public IAsyncResult BeginLoad(Uri targetUri, Uri currentUri, AsyncCallback userCallback, object asyncState)
        //{
        //    var result = new CaliburnLoaderAsyncResult(asyncState, userCallback) { BeginLoadCompleted = false };
        //    currentResult = result;
        //    try
        //    {
        //        NavigationConductor.ActivateItem(targetUri);
        //        // TODO: set parent uri -> curernt uri
        //        //String currnetOriginalString = null;
        //        //if (currentUri == null || String.IsNullOrWhiteSpace(currentUri.OriginalString))
        //        //{
        //        //    currnetOriginalString = Constants.HOME_URI_STRING;
        //        //}
        //        //else
        //        //{
        //        //    currnetOriginalString = currentUri.OriginalString;
        //        //}
               
        //    }
        //    catch (Exception e)
        //    {
        //        Error(e, result);
        //    }
        //    result.BeginLoadCompleted = true;
        //    return result;
        //}

        ///// <summary>
        ///// Gets a value that indicates whether the specified URI can be loaded.
        ///// </summary>
        ///// <param name="targetUri">The URI to test.</param>
        ///// <param name="currentUri">The URI that is currently loaded.</param>
        ///// <returns>true if the URI can be loaded; otherwise, false.</returns>
        //public virtual bool CanLoad(Uri targetUri, Uri currentUri)
        //{
        //    return true;
        //}

        ///// <summary>
        ///// Ends loading with an error, delaying throwing the error until EndLoad() is called on
        ///// the INavigationContentLoader.
        ///// </summary>
        ///// <param name="error">The error that occurred.</param>
        ///// <param name="result"></param>
        //protected void Error(Exception error, CaliburnLoaderAsyncResult result)
        //{
        //    result.Error = error;
        //    Complete();
        //}

        ///// <summary>
        ///// Attempts to cancel content loading for the specified asynchronous operation.
        ///// </summary>
        ///// <param name="asyncResult">An object that identifies the asynchronous operation to cancel.</param>
        //public void CancelLoad(IAsyncResult asyncResult)
        //{
        //    return;
        //}

        ///// <summary>
        ///// Completes the asynchronous content loading operation.
        ///// </summary>
        ///// <param name="asyncResult">An object that identifies the asynchronous operation.</param>
        ///// <returns>An object that represents the result of the asynchronous content loading operation.</returns>
        //public LoadResult EndLoad(IAsyncResult asyncResult)
        //{
        //    CaliburnLoaderAsyncResult result = (CaliburnLoaderAsyncResult)asyncResult;
        //    if (result.Error != null)
        //    {
        //        throw result.Error;
        //    }
        //    result.Complete();
        //    return result.Page != null ? new LoadResult(result.Page) : new LoadResult(result.RedirectUri);
        //}

        //#endregion

        #region Nested CaliburnNavigationLoader Class

       
        class CaliburnNavigationLoader : LoaderBase
        {

            CaliburnContentLoader _parent;
            IAsyncResult _result;

            INavigationContentLoader Loader
            {
                get { return _parent.ContentLoader ?? _defaultLoader; }
            }

            NavigationConductor _conductor;

       
            public CaliburnNavigationLoader(CaliburnContentLoader parent)
            {
                _parent = parent;
                _conductor = _parent.NavigationConductor as NavigationConductor;
            }

            public override void Load(Uri targetUri, Uri currentUri)
            {

                //tamper protection 
                Boolean isMatchRequired = targetUri.OriginalString.Contains(Constants.ADDNEW) && targetUri.OriginalString.Contains(Constants.TRACKING_KEY);

                try
                {
                    PersitableScreen  viewmodel;
                    _conductor.ViewModels.TryGetValue(targetUri.OriginalString, out viewmodel);

                    if (isMatchRequired && viewmodel == null)
                    {
                        throw new InvalidOperationException("Uri missing from cache");
                    }

                    if (viewmodel != null)
                    {

                        Object result = viewmodel.GetView();
                        if (result is Uri)
                            base.Complete((Uri)result);
                        else
                            base.Complete(result);
                    }
                    else
                    {
                        _result = this.Loader.BeginLoad(targetUri, currentUri, (res) =>
                        {
                            try
                            {
                                LoadResult loadResult = this.Loader.EndLoad(res);
                                if (loadResult.RedirectUri != null)
                                    base.Complete(loadResult.RedirectUri);
                                else
                                {
                                    DependencyObject content = loadResult.LoadedContent as DependencyObject;
                                    if (content != null)
                                    {
                                        String currentOriginalString = null;
                                        if (currentUri == null || String.IsNullOrWhiteSpace(currentUri.OriginalString))
                                        {
                                            currentOriginalString = Constants.HOME_URI_STRING;
                                        }
                                        else
                                        {
                                            currentOriginalString = currentUri.OriginalString;
                                        }

                                        String targetUriString = targetUri.OriginalString;
                                        String trackingKey = String.Empty;
                                        if (targetUri.OriginalString.EndsWith(Constants.ADD_NEW_QUERYSTRING))
                                        {
                                            trackingKey = Guid.NewGuid().ToString();
                                            targetUriString = String.Concat(targetUriString.Replace(Constants.ADD_NEW_QUERYSTRING, String.Empty), Constants.TRACKING_KEY_QUERYSTRING, trackingKey);
                                        }

                                     
                                        ViewLocator.InitializeComponent(content);
                                       
                                        var vm = ViewModelLocator.LocateForView(content);

                                        if (vm == null)
                                        {
                                            // to avoid shellviewmodel to be set on cild screens datacontext property
                                            vm = new PersitableScreen();
                                        }

                                        viewmodel = vm as PersitableScreen;

                                        if (viewmodel != null)
                                        {
                                            viewmodel.ViewType = content.GetType();
                                            viewmodel.UriString = targetUriString;
                                            viewmodel.ParentUriOriginalString = currentOriginalString;
                                            viewmodel.TrackingKey = trackingKey;

                                            // TODO: Add query string
                                            Uri auri = new Uri("dummy://" + targetUriString, UriKind.Absolute);
                                            string query = auri.Query;

                                            //    _conductor.NavigationService.TryInjectQueryString(viewmodel,content);

                                           
                                        }

                                        // dont rebind when you're bound to yourself
                                        if (vm != content)
                                            ViewModelBinder.Bind(vm, content, null);

                                        base.Complete(content);
                                    }
                                    else
                                    {
                                        throw new InvalidOperationException("LoadedContent was null or not a DependencyObject");
                                    }
                                    return;
                                }

                            }
                            catch (Exception e)
                            {
                                base.Error(e);
                                return;
                            }
                        }, null);
                    }
                }
                catch (Exception e)
                {
                    base.Error(e);
                    return;
                }
            }

            public override void Cancel()
            {
                this.Loader.CancelLoad(_result);
            }
        }

        #endregion //Nested NonLinearNavigationLoader Class
    }
}
