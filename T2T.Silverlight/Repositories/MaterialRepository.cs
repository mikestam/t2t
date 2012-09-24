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
//using System.Linq;

namespace T2T.Repositories
{
    public class MaterialRepository : RepositoryBase
    {
        public void GetMaterialDefinitions(Action<IEnumerable<ResourceDefinition>> callback)
        {
            this.LoadQuery<ResourceDefinition>(this.Context.GetMaterialDefinitionsQuery(), callback);
        }

        public void GetMaterialeDifinitionsForClass(int? classID, Action<IEnumerable<ResourceDefinition>> callback)
        {
            this.LoadQuery<ResourceDefinition>(this.Context.GetMaterialDefinitionsForClassQuery(classID), callback);
        }

        public void GetMaterialClasses(Action<IEnumerable<ResourceClass>> callback)
        {
            this.LoadQuery<ResourceClass>(this.Context.GetMaterialClassesQuery(), callback);
        }

         public void GetMaterialeDifinitionsForClassSearch(int? classID,ResourcesSearchFilter filter, Action<IEnumerable<ResourceDefinition>> callback)
        {
            this.LoadQuery<ResourceDefinition>(this.Context.GetMaterialDefinitionsForClassSearchQuery(classID, filter.Keyword), callback);
        }
    }
}
