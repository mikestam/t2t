using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Collections.Specialized;

//TODO: new logic (conductor) for next active item since when we close active item it shold return to parent workspace not next item in collection
namespace Caliburn.Micro.Navigation
{
    public class NavigationConductor : ConductorBase<object>, IConductActiveItem, INavigationConductor
    {
        private readonly Dictionary<string, PersitableScreen> _viewModels;
        private string _activeName;
        object _activeItem;

        /// <summary>
        /// The currently active item.
        /// </summary>
        public object ActiveItem
        {
            get { return _activeItem; }
            set { ActivateItem(value); }
        }

        /// <summary>
        /// The currently active item.
        /// </summary>
        /// <value></value>
        object IHaveActiveItem.ActiveItem
        {
            get { return ActiveItem; }
            set { ActiveItem = value; }
        }

    
        public NavigationConductor()
        {
            _viewModels = new Dictionary<string, PersitableScreen>();


            //this wont work
            Items.CollectionChanged += (s, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                    case NotifyCollectionChangedAction.Replace:
                        e.NewItems.OfType<IChild>().Apply(x => x.Parent = this);
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        Items.OfType<IChild>().Apply(x => x.Parent = this);
                        break;
                }
            };

            // if we inject views on startup
            //viewModels.ForEach(vm => _viewModels.Add(vm.GetType().Name, vm));

           // EventFns.Subscribe(this);
        }

        public INavigationService NavigationService { get; set; }

        public Dictionary<string, PersitableScreen> ViewModels { get { return _viewModels; } }

        /// <summary>
        ///   Bindable collection exposing the names of all discovered ViewModels.
        /// </summary>
        public BindableCollection<string> Names
        {
            get { return new BindableCollection<string>(_viewModels.Keys.OrderBy(k => k)); }
        }

        /// <summary>
        ///   Bindable collection exposing  all discovered ViewModels.
        /// </summary>
        public IObservableCollection<PersitableScreen> Items
        {
            get { return new BindableCollection<PersitableScreen>(_viewModels.Values.OrderBy(vm => vm.DisplayName)); }
        }

        /// <summary>
        ///   Returns the name of the current active view model.
        /// </summary>
        public string ActiveName
        {
            get { return _activeName; }
            set
            {
                _activeName = value;
                NotifyOfPropertyChange(() => ActiveName);
            }
        }

     

        /// <summary>
        /// Activates the specified item.
        /// </summary>
        /// <param name="item">The item to activate.</param>
        public override void ActivateItem(object item)
        {
            if (item != null && item.Equals(ActiveItem))
            {
                if (IsActive)
                {
                    ScreenExtensions.TryActivate(item);
                    OnActivationProcessed(item, true);
                }

                return;
            }

            bool shouldClosePreviosly = true;
            var screen = ActiveItem as PersitableScreen;

            if (screen != null)
                shouldClosePreviosly = screen.ShouldClose;

            if (shouldClosePreviosly)
            {
                CloseStrategy.Execute(new[] { ActiveItem }, (canClose, items) =>
                {
                    if (canClose)
                    {
                        ChangeActiveItem(item, true);
                    }
                    else OnActivationProcessed(item, false);
                });
            }
            else
            {

                ChangeActiveItem(item, false);
            }
        }

        /// <summary>
        /// Changes the active item.
        /// </summary>
        /// <param name="newItem">The new item to activate.</param>
        /// <param name="closePrevious">Indicates whether or not to close the previous active item.</param>
        protected virtual void ChangeActiveItem(object newItem, bool closePrevious)
        {
            ScreenExtensions.TryDeactivate(ActiveItem, closePrevious);
            if (ActiveItem != null && closePrevious)
            {
                var activescreen = ActiveItem as PersitableScreen;
                if ((activescreen != null) && _viewModels.ContainsValue(activescreen))
                    _viewModels.Remove(activescreen.UriString);
            }

            var screen = newItem as PersitableScreen;

            if (screen != null)
            {
                newItem = EnsureItem(screen);
            }
            
            if (IsActive)
                ScreenExtensions.TryActivate(newItem);

            _activeItem = newItem;
     
            NotifyOfPropertyChange("ActiveItem");
            OnActivationProcessed(_activeItem, true);
        }

        /// Need to work out
        /// <summary>
        /// Deactivates the specified item.
        /// </summary>
        /// <param name="item">The item to close.</param>
        /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
        public override void DeactivateItem(object item, bool close)
        {
            if (item == null)
            {
                return;
            }

            if (!close)
            {
                ScreenExtensions.TryDeactivate(item, false);
            }
            else
            {
                CloseStrategy.Execute(new[] { item }, (canClose, closable) =>
                {
                    if (canClose)
                    {
                        ScreenExtensions.TryDeactivate(item, true);
                       
                    }
                });
            }
        }

   

        /// <summary>
        /// Called to check whether or not this instance can close.
        /// </summary>
        /// <param name="callback">The implementor calls this action with the result of the close check.</param>
        public override void CanClose(Action<bool> callback)
        {
            CloseStrategy.Execute(Items, (canClose, closable) =>
            {
                if (!canClose && closable.Any())
                {
                    closable.OfType<IDeactivate>().Apply(x => x.Deactivate(true));

                    Items.RemoveRange(closable.OfType<PersitableScreen>());
                }

                callback(canClose);
            });
        }


        /// <summary>
        /// Called when activating.
        /// </summary>
        protected override void OnActivate()
        {
            ScreenExtensions.TryActivate(ActiveItem);
        }

        /// <summary>
        /// Called when deactivating.
        /// </summary>
        /// <param name="close">Inidicates whether this instance will be closed.</param>
        protected override void OnDeactivate(bool close)
        {
            ScreenExtensions.TryDeactivate(ActiveItem, close);
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <returns>The collection of children.</returns>
        public override IEnumerable<object> GetChildren()
        {
            return Items.ToList<object>();
        }

        /// <summary>
        /// Ensures that an item is ready to be activated.
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns>The item to be activated.</returns>
        //protected override T EnsureItem(T newItem)
        //{
        //    var node = newItem as IChild;
        //    if (node != null && node.Parent != this)
        //        node.Parent = this;

        //    return newItem;
        //} 

        protected override object EnsureItem(object newItem)
        {
            var screen = newItem as PersitableScreen;
            if (screen != null)
            {
                if ((screen != null) && !_viewModels.ContainsValue(screen))
                    _viewModels.Add(screen.UriString, screen);
            }
            return base.EnsureItem(newItem);
        }

    }
}
     