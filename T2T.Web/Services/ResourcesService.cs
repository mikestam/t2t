
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
    using T2T.Model;


    // Implements application logic using the T2TDb context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class ResourcesService : LinqToEntitiesDomainService<T2TDb>
    {

        #region BillOfResources
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'BillOfResources' query.
        [Query(IsDefault = true)]
        public IQueryable<BillOfResource> GetBillOfResources()
        {
            return this.ObjectContext.BillOfResources;
        }

        public void InsertBillOfResource(BillOfResource billOfResource)
        {
            if ((billOfResource.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(billOfResource, EntityState.Added);
            }
            else
            {
                this.ObjectContext.BillOfResources.AddObject(billOfResource);
            }
        }

        public void UpdateBillOfResource(BillOfResource currentBillOfResource)
        {
            this.ObjectContext.BillOfResources.AttachAsModified(currentBillOfResource, this.ChangeSet.GetOriginal(currentBillOfResource));
        }

        public void DeleteBillOfResource(BillOfResource billOfResource)
        {
            if ((billOfResource.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(billOfResource, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.BillOfResources.Attach(billOfResource);
                this.ObjectContext.BillOfResources.DeleteObject(billOfResource);
            }
        }

        #endregion

        #region Properties
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Properties' query.
        [Query(IsDefault = true)]
        public IQueryable<Property> GetProperties()
        {
            return this.ObjectContext.Properties;
        }

        public void InsertProperty(Property property)
        {
            if ((property.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(property, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Properties.AddObject(property);
            }
        }

        public void UpdateProperty(Property currentProperty)
        {
            this.ObjectContext.Properties.AttachAsModified(currentProperty, this.ChangeSet.GetOriginal(currentProperty));
        }

        public void DeleteProperty(Property property)
        {
            if ((property.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(property, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Properties.Attach(property);
                this.ObjectContext.Properties.DeleteObject(property);
            }
        }

        #endregion

        #region ResourceClasses
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ResourceClasses' query.
        [Query(IsDefault = true)]
        public IQueryable<ResourceClass> GetResourceClasses()
        {
            return this.ObjectContext.ResourceClasses.OrderBy(rc => rc.Name);
        }

        public IQueryable<ResourceClass> GetMaterialClasses()
        {
            return this.ObjectContext.ResourceClasses.Where(rc => rc.ResourceType == 21 && rc.LevelID >= 0).OrderBy(rc => rc.Name);
        }

        //public IQueryable<ResourceClass> GetMaterialSubclasses(int parentClassID)
        //{
 
        //    if (  this.GetMaterialClasses().Where(m => m.ResourceClassID == parentClassID).Count() != 1)
        //        return null;

        //    return   this.GetMaterialClasses().Where(m => m.ResourceClassID == parentClassID).First();
        //}


        public void InsertResourceClass(ResourceClass resourceClass)
        {
            if ((resourceClass.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceClass, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ResourceClasses.AddObject(resourceClass);
            }
        }

        public void UpdateResourceClass(ResourceClass currentResourceClass)
        {
            this.ObjectContext.ResourceClasses.AttachAsModified(currentResourceClass, this.ChangeSet.GetOriginal(currentResourceClass));
        }

        public void DeleteResourceClass(ResourceClass resourceClass)
        {
            if ((resourceClass.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceClass, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ResourceClasses.Attach(resourceClass);
                this.ObjectContext.ResourceClasses.DeleteObject(resourceClass);
            }
        }

        #endregion

        #region ResourceDefinitions
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ResourceDefinitions' query.
        [Query(IsDefault = true)]
        public IQueryable<ResourceDefinition> GetResourceDefinitions()
        {
            return this.ObjectContext.ResourceDefinitions.OrderBy(rd => rd.Name);
        }

        [Query]
        public IQueryable<ResourceDefinition> GetMaterialDefinitions()
        {
            return this.ObjectContext.ResourceDefinitions.OrderBy(rd => rd.Name).OfType<Material>();
        }

        // TODO: see default value for int in case of null -> ResourceClassID ?? default(int) or 0
        [Query]
        public IQueryable<ResourceDefinition> GetMaterialDefinitionsForClass(int? classID)
        {
            if (classID == null)
                return GetMaterialDefinitions();

            return from def in GetMaterialDefinitions()
                   where (from mc in this.ObjectContext.ResourceClasses
                          where mc.ResourceClassID == classID || mc.ParentClassID == classID
                          select mc.ResourceClassID).Contains<int>((int)def.ResourceClassID )
                   select def;
        }

        [Query]
        public IQueryable<ResourceDefinition> GetMaterialDefinitionsForClassSearch(int? classID,string keyword)
        {
            return GetMaterialDefinitionsForClass(classID).Where(md => md.Name.Contains(keyword) || md.Description.Contains(keyword));
        }

        public void InsertResourceDefinition(ResourceDefinition resourceDefinition)
        {
            if ((resourceDefinition.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceDefinition, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ResourceDefinitions.AddObject(resourceDefinition);
            }
        }

        public void UpdateResourceDefinition(ResourceDefinition currentResourceDefinition)
        {
            this.ObjectContext.ResourceDefinitions.AttachAsModified(currentResourceDefinition, this.ChangeSet.GetOriginal(currentResourceDefinition));
        }

        public void DeleteResourceDefinition(ResourceDefinition resourceDefinition)
        {
            if ((resourceDefinition.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceDefinition, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ResourceDefinitions.Attach(resourceDefinition);
                this.ObjectContext.ResourceDefinitions.DeleteObject(resourceDefinition);
            }
        }

        #endregion

        #region ResourceInventories
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ResourceInventories' query.
        [Query(IsDefault = true)]
        public IQueryable<ResourceInventory> GetResourceInventories()
        {
            return this.ObjectContext.ResourceInventories;
        }

        public void InsertResourceInventory(ResourceInventory resourceInventory)
        {
            if ((resourceInventory.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceInventory, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ResourceInventories.AddObject(resourceInventory);
            }
        }

        public void UpdateResourceInventory(ResourceInventory currentResourceInventory)
        {
            this.ObjectContext.ResourceInventories.AttachAsModified(currentResourceInventory, this.ChangeSet.GetOriginal(currentResourceInventory));
        }

        public void DeleteResourceInventory(ResourceInventory resourceInventory)
        {
            if ((resourceInventory.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceInventory, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ResourceInventories.Attach(resourceInventory);
                this.ObjectContext.ResourceInventories.DeleteObject(resourceInventory);
            }
        }

        #endregion

        #region ResourceLevels

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ResourceLevels' query.
        [Query(IsDefault = true)]
        public IQueryable<ResourceLevel> GetResourceLevels()
        {
            return this.ObjectContext.ResourceLevels;
        }

        public void InsertResourceLevel(ResourceLevel resourceLevel)
        {
            if ((resourceLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceLevel, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ResourceLevels.AddObject(resourceLevel);
            }
        }

        public void UpdateResourceLevel(ResourceLevel currentResourceLevel)
        {
            this.ObjectContext.ResourceLevels.AttachAsModified(currentResourceLevel, this.ChangeSet.GetOriginal(currentResourceLevel));
        }

        public void DeleteResourceLevel(ResourceLevel resourceLevel)
        {
            if ((resourceLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceLevel, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ResourceLevels.Attach(resourceLevel);
                this.ObjectContext.ResourceLevels.DeleteObject(resourceLevel);
            }
        }

        #endregion

        #region ResourceLots

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ResourceLots' query.
        [Query(IsDefault = true)]
        public IQueryable<ResourceLot> GetResourceLots()
        {
            return this.ObjectContext.ResourceLots;
        }

        public void InsertResourceLot(ResourceLot resourceLot)
        {
            if ((resourceLot.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceLot, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ResourceLots.AddObject(resourceLot);
            }
        }

        public void UpdateResourceLot(ResourceLot currentResourceLot)
        {
            this.ObjectContext.ResourceLots.AttachAsModified(currentResourceLot, this.ChangeSet.GetOriginal(currentResourceLot));
        }

        public void DeleteResourceLot(ResourceLot resourceLot)
        {
            if ((resourceLot.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceLot, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ResourceLots.Attach(resourceLot);
                this.ObjectContext.ResourceLots.DeleteObject(resourceLot);
            }
        }

        #endregion

        #region ResourceLotStatuses
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ResourceLotStatuses' query.
        [Query(IsDefault = true)]
        public IQueryable<ResourceLotStatus> GetResourceLotStatuses()
        {
            return this.ObjectContext.ResourceLotStatuses;
        }

        public void InsertResourceLotStatus(ResourceLotStatus resourceLotStatus)
        {
            if ((resourceLotStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceLotStatus, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ResourceLotStatuses.AddObject(resourceLotStatus);
            }
        }

        public void UpdateResourceLotStatus(ResourceLotStatus currentResourceLotStatus)
        {
            this.ObjectContext.ResourceLotStatuses.AttachAsModified(currentResourceLotStatus, this.ChangeSet.GetOriginal(currentResourceLotStatus));
        }

        public void DeleteResourceLotStatus(ResourceLotStatus resourceLotStatus)
        {
            if ((resourceLotStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceLotStatus, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ResourceLotStatuses.Attach(resourceLotStatus);
                this.ObjectContext.ResourceLotStatuses.DeleteObject(resourceLotStatus);
            }
        }

        #endregion

        #region ResourceLotTrackings

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ResourceLotTrackings' query.
        [Query(IsDefault = true)]
        public IQueryable<ResourceLotTracking> GetResourceLotTrackings()
        {
            return this.ObjectContext.ResourceLotTrackings;
        }

        public void InsertResourceLotTracking(ResourceLotTracking resourceLotTracking)
        {
            if ((resourceLotTracking.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceLotTracking, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ResourceLotTrackings.AddObject(resourceLotTracking);
            }
        }

        public void UpdateResourceLotTracking(ResourceLotTracking currentResourceLotTracking)
        {
            this.ObjectContext.ResourceLotTrackings.AttachAsModified(currentResourceLotTracking, this.ChangeSet.GetOriginal(currentResourceLotTracking));
        }

        public void DeleteResourceLotTracking(ResourceLotTracking resourceLotTracking)
        {
            if ((resourceLotTracking.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceLotTracking, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ResourceLotTrackings.Attach(resourceLotTracking);
                this.ObjectContext.ResourceLotTrackings.DeleteObject(resourceLotTracking);
            }
        }

        #endregion

        #region ResourceProperties

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ResourceProperties' query.
        [Query(IsDefault = true)]
        public IQueryable<ResourceProperty> GetResourceProperties()
        {
            return this.ObjectContext.ResourceProperties;
        }

        public void InsertResourceProperty(ResourceProperty resourceProperty)
        {
            if ((resourceProperty.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceProperty, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ResourceProperties.AddObject(resourceProperty);
            }
        }

        public void UpdateResourceProperty(ResourceProperty currentResourceProperty)
        {
            this.ObjectContext.ResourceProperties.AttachAsModified(currentResourceProperty, this.ChangeSet.GetOriginal(currentResourceProperty));
        }

        public void DeleteResourceProperty(ResourceProperty resourceProperty)
        {
            if ((resourceProperty.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceProperty, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ResourceProperties.Attach(resourceProperty);
                this.ObjectContext.ResourceProperties.DeleteObject(resourceProperty);
            }
        }

        #endregion

        #region ResourceTypes

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ResourceTypes' query.
        [Query(IsDefault = true)]
        public IQueryable<ResourceType> GetResourceTypes()
        {
            return this.ObjectContext.ResourceTypes;
        }

        public void InsertResourceType(ResourceType resourceType)
        {
            if ((resourceType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ResourceTypes.AddObject(resourceType);
            }
        }

        public void UpdateResourceType(ResourceType currentResourceType)
        {
            this.ObjectContext.ResourceTypes.AttachAsModified(currentResourceType, this.ChangeSet.GetOriginal(currentResourceType));
        }

        public void DeleteResourceType(ResourceType resourceType)
        {
            if ((resourceType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(resourceType, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ResourceTypes.Attach(resourceType);
                this.ObjectContext.ResourceTypes.DeleteObject(resourceType);
            }
        }

        #endregion

    }
}


