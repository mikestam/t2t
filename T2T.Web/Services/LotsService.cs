
namespace T2T.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using T2T.Web;


    // Implements application logic using the T2TEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class LotsService : LinqToEntitiesDomainService<T2TEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SemiFinishedMaterialInventories' query.
        [Query(IsDefault = true)]
        public IQueryable<SemiFinishedMaterialInventory> GetSemiFinishedMaterialInventories()
        {
            return this.ObjectContext.SemiFinishedMaterialInventories;
        }
    }
}


