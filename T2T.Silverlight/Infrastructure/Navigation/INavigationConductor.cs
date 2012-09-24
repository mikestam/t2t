using System;
namespace Caliburn.Micro.Navigation
{
    public interface INavigationConductor
    {
        INavigationService NavigationService { get; set; }
       
    }
}
