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

namespace T2T.Repositories
{
    public class ResourceRepository :RepositoryBase 
    {
        public void GetResourceDefinition(Action<IEnumerable<ResourceDefinition>> callback)
        {
            this.LoadQuery<ResourceDefinition>(this.Context.GetResourceDefinitionsQuery(), callback);
        }

        public void GetResourceDifinitionsForSearch(string search, Action<IEnumerable<ResourceDefinition>> callback)
        {
            this.LoadQuery<ResourceDefinition>(this.Context.GetResourceDefinitionsQuery(), callback);
        }
    }
}
