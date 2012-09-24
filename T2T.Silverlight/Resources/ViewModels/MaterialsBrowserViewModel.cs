using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using T2T.Model;
using T2T.Repositories;

using System.Linq;
using T2T.Resources.Services;
using System.Text;
using System.Globalization;
using Caliburn.Micro;

namespace T2T.Resources.ViewModels
{ 
    //indication of isbusy !!!
    public class MaterialsBrowserViewModel : PropertyChangedBase,IResourceBrowser
    { 
        
        #region creation
        // do we need reference since domaincontext is static ? - > to je za single view model
        // sada nam treba instance domaincontext inside repo and instance repo
        private MaterialRepository _materialRepository;

        public  MaterialsBrowserViewModel()
        {
            _materialRepository = new MaterialRepository();

            IsBusy = true;
            _materialRepository.GetMaterialClasses((rcs) =>
            {
                //handle error
                //should be done on ui thread
                // do we need to wrapped them in observable collection or in viewmodel
                ResourceClasses = rcs.ToList();
                IsBusy = false;
                 NotifyOfPropertyChange ("ResourceClasses");
         
            });

        }

        private void GetResourceClasses()
        {
            Action<IEnumerable<ResourceClass>> handler = (rcs) =>
            {
                //handle error
                //should be done on ui thread
                ResourceClasses = rcs;
            };

            _materialRepository.GetMaterialClasses(handler);
            
        }

        #endregion

        #region Properties

        public IEnumerable<ResourceClass> ResourceClasses { get; set; }

        public IEnumerable<ResourceDefinition> ResourceDefinitions { get; set; }

        private ResourceClass _currentResourceClass;

        public ResourceClass CurrentResourceClass
        {
            get { return _currentResourceClass; }
            set
            {
                if (_currentResourceClass != value)
                {
                    _currentResourceClass = value;
                     NotifyOfPropertyChange ("CurrentResourceClass");
                    CreateNavigationUri();
                }
            }
        }



        private ResourceDefinition _selectedResourceDefinition;
        public ResourceDefinition SelectedResourceDefinition
        {
            get { return _selectedResourceDefinition; }
            set
            {
                if (_selectedResourceDefinition != value)
                {
                    _selectedResourceDefinition = value;
                     NotifyOfPropertyChange ("SelectedResourceDefinition");
                }
            }
        }


        private string _filterCriteria = String.Empty;
        public string FilterCriteria
        {
            get { return _filterCriteria; }
            set
            {
                if (_filterCriteria != value)
                {
                    _filterCriteria = value;
                     NotifyOfPropertyChange ("FilterCriteria");
                }
            }
        }

        private Uri _uri;
        public Uri Uri
        {
            get { return _uri; }
            set
            {
                if (_uri != value)
                {
                    _uri = value;
                     NotifyOfPropertyChange ("Uri");
                }
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                     NotifyOfPropertyChange ("IsBusy");
                }
            }
        }

        private bool _isMBusy;
        public bool IsMBusy
        {
            get { return _isMBusy; }
            set
            {
                if (_isMBusy != value)
                {
                    _isMBusy = value;
                     NotifyOfPropertyChange ("IsMBusy");
                }
            }
        }


        private ResourcesSearchFilter _searchFilter = new ResourcesSearchFilter();
        public ResourcesSearchFilter SearchFilter
        {
            get { return _searchFilter; }
            set
            {
                if (_searchFilter != value)
                {
                    _searchFilter = value;
                     NotifyOfPropertyChange ("SearchFilter");
                    CreateNavigationUri();
                }
            }
        }
        #endregion

        #region Fragmet navigation for search and filter functionality

        public void FragmentNavigation(string fragment)
        {

            Dictionary<string, string> fragmentQS = GetFragmentQueryString(fragment);

            CurrentResourceClass = GetClassFromFragmentQS(fragmentQS);

           int? _resourceClassID  = CurrentResourceClass == null ? null : CurrentResourceClass.ResourceClassID as int?;

            SearchFilter = GetSearchFilterFromFragmentQS(fragmentQS);


            Action<IEnumerable<ResourceDefinition>> handler =
             (mds) =>
             {
                 //should be done on ui thread
                 // do we need to wrapped them in observable collection or in viewmodel
                 ResourceDefinitions = mds.ToList();
                 IsMBusy = false;
                  NotifyOfPropertyChange ("ResourceDefinitions");

             };

            IsMBusy = true;
            if (SearchFilter.Keyword != null)
                 _materialRepository.GetMaterialeDifinitionsForClassSearch(_resourceClassID, SearchFilter, handler);
            else
                _materialRepository.GetMaterialeDifinitionsForClass(_resourceClassID, handler);
    
        }

        private Dictionary<string, string> GetFragmentQueryString(string fragment)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            string[] keyValuePaires = fragment.Split('/');

            foreach (string keyValuePair in keyValuePaires)
            {
                string[] values = keyValuePair.Split('=');
                if (values.Length == 2)
                    keyValues[values[0]] = values[1];
            }

            return keyValues;

        }

        private ResourcesSearchFilter GetSearchFilterFromFragmentQS(Dictionary<string, string> fragmentQS)
        {
            ResourcesSearchFilter filter = new ResourcesSearchFilter();

            if (fragmentQS.Keys.Contains("Keyword"))
                filter.Keyword = fragmentQS["Keyword"];

            return filter;
        }
    
        private ResourceClass GetClassFromFragmentQS(Dictionary<string, string> fragmentQS)
        {
          
            if (!fragmentQS.ContainsKey("Class"))
                return null;

            int materialClassID;

            try
            {
                materialClassID = Convert.ToInt32(fragmentQS["Class"]);
            }
            catch(FormatException ex)
            {
                return null;
            }

            // will always hit db !!!!!!!!!!!!!
            if (ResourceClasses.Where(m => m.ResourceClassID == materialClassID).Count() != 1)
                return null;

            return  ResourceClasses.Where(m => m.ResourceClassID == materialClassID).First();
        }

        private Uri CreateNavigationUri()
        {
            StringBuilder queryString = new StringBuilder("#");

            if (CurrentResourceClass != null)
                queryString.Append(String.Format(CultureInfo.InvariantCulture, "Class={0}/", CurrentResourceClass.ResourceClassID));

            if (_searchFilter.Keyword != null)
                queryString.Append(String.Format(CultureInfo.InvariantCulture, "Keyword={0}/", _searchFilter.Keyword));

            queryString.Remove(queryString.Length - 1, 1);
            // Uri uri = new Uri("/Views/Resources2/MaterialsBrowserView2.xaml" + queryString.ToString(), UriKind.Relative);
            // return new Uri("/Views/Resources2/MaterialsBrowserView2.xaml" + queryString.ToString(), UriKind.Relative);
           Uri = new Uri("/Resources/Materials" + queryString.ToString(), UriKind.Relative);

           return Uri;
        }

        #endregion

    }
}
