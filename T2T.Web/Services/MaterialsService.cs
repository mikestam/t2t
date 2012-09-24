
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



    // Implements application logic using the T2TEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class MaterialsService : LinqToEntitiesDomainService<T2TEntities>
    {
        //public MaterialsService()
        //    : base()
        //{
        //    int i = 1;


        //}
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MaterialClasses' query.
        [Query(IsDefault = true)]
        public IQueryable<MaterialClass> GetMaterialClasses()
        {
            return this.ObjectContext.MaterialClasses;
        }

        public void InsertMaterialClass(MaterialClass materialClass)
        {
            if ((materialClass.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(materialClass, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MaterialClasses.AddObject(materialClass);
            }
        }

        public void UpdateMaterialClass(MaterialClass currentMaterialClass)
        {
            this.ObjectContext.MaterialClasses.AttachAsModified(currentMaterialClass, this.ChangeSet.GetOriginal(currentMaterialClass));
        }

        public void DeleteMaterialClass(MaterialClass materialClass)
        {
            if ((materialClass.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(materialClass, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MaterialClasses.Attach(materialClass);
                this.ObjectContext.MaterialClasses.DeleteObject(materialClass);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MaterialDefinitions' query.
        [Query(IsDefault = true)]
        public IQueryable<MaterialDefinition> GetMaterialDefinitions()
        {
            return this.ObjectContext.MaterialDefinitions.OrderBy(m => m.Name);
        }

        [Query]
        public IQueryable<MaterialDefinition> GetMaterialDefinitionsForClass(int? classID)
        {
            if (classID == null)
                return GetMaterialDefinitions();

            return  from def in GetMaterialDefinitions()
                    where (from mc in this.ObjectContext.MaterialClasses
                           where mc.MaterialClassID == classID || mc.ParentClassID == classID
                           select mc.MaterialClassID).Contains(def.MaterialClassID)
                    select def;
        }


        public void InsertMaterialDefinition(MaterialDefinition materialDefinition)
        {
            if ((materialDefinition.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(materialDefinition, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MaterialDefinitions.AddObject(materialDefinition);
            }
        }

        public void UpdateMaterialDefinition(MaterialDefinition currentMaterialDefinition)
        {
            this.ObjectContext.MaterialDefinitions.AttachAsModified(currentMaterialDefinition, this.ChangeSet.GetOriginal(currentMaterialDefinition));
        }

        public void DeleteMaterialDefinition(MaterialDefinition materialDefinition)
        {
            if ((materialDefinition.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(materialDefinition, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MaterialDefinitions.Attach(materialDefinition);
                this.ObjectContext.MaterialDefinitions.DeleteObject(materialDefinition);
            }
        }

    

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MaterialLots' query.
        [Query(IsDefault = true)]
        public IQueryable<MaterialLot> GetMaterialLots()
        {
            return this.ObjectContext.MaterialLots;
        }

        public IQueryable<MaterialLot> GetMaterialLotsForDate(DateTime date)
        {
            var date1 = date.Date.AddDays(1);
            return this.ObjectContext.MaterialLots.Where(l => l.CreationDate < date1 && l.CreationDate >= date.Date).OrderBy(l => l.EquipmentID);
        }

        public IQueryable<MaterialLot> GetMaterialLotsForEquipmentDate(DateTime date,int equipmentID)
        {
            var date1 = date.Date.AddDays(1);
            return this.ObjectContext.MaterialLots.Where(l => l.CreationDate < date1 && l.CreationDate >= date.Date).Where(l => l.EquipmentID == equipmentID).OrderBy(l => l.EquipmentID);
        }


        public void InsertMaterialLot(MaterialLot materialLot)
        {
            if ((materialLot.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(materialLot, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MaterialLots.AddObject(materialLot);
            }
        }

        public void UpdateMaterialLot(MaterialLot currentMaterialLot)
        {
            this.ObjectContext.MaterialLots.AttachAsModified(currentMaterialLot, this.ChangeSet.GetOriginal(currentMaterialLot));
        }

        public void DeleteMaterialLot(MaterialLot materialLot)
        {
            if ((materialLot.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(materialLot, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MaterialLots.Attach(materialLot);
                this.ObjectContext.MaterialLots.DeleteObject(materialLot);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MaterialLotStatuses' query.
        [Query(IsDefault = true)]
        public IQueryable<MaterialLotStatus> GetMaterialLotStatuses()
        {
            return this.ObjectContext.MaterialLotStatuses;
        }

        public void InsertMaterialLotStatus(MaterialLotStatus materialLotStatus)
        {
            if ((materialLotStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(materialLotStatus, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MaterialLotStatuses.AddObject(materialLotStatus);
            }
        }

        public void UpdateMaterialLotStatus(MaterialLotStatus currentMaterialLotStatus)
        {
            this.ObjectContext.MaterialLotStatuses.AttachAsModified(currentMaterialLotStatus, this.ChangeSet.GetOriginal(currentMaterialLotStatus));
        }

        public void DeleteMaterialLotStatus(MaterialLotStatus materialLotStatus)
        {
            if ((materialLotStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(materialLotStatus, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MaterialLotStatuses.Attach(materialLotStatus);
                this.ObjectContext.MaterialLotStatuses.DeleteObject(materialLotStatus);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MaterialLotTrackings' query.
        [Query(IsDefault = true)]
        public IQueryable<MaterialLotTracking> GetMaterialLotTrackings()
        {
            return this.ObjectContext.MaterialLotTrackings;
        }

        public void InsertMaterialLotTracking(MaterialLotTracking materialLotTracking)
        {
            if ((materialLotTracking.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(materialLotTracking, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MaterialLotTrackings.AddObject(materialLotTracking);
            }
        }

        public void UpdateMaterialLotTracking(MaterialLotTracking currentMaterialLotTracking)
        {
            this.ObjectContext.MaterialLotTrackings.AttachAsModified(currentMaterialLotTracking, this.ChangeSet.GetOriginal(currentMaterialLotTracking));
        }

        public void DeleteMaterialLotTracking(MaterialLotTracking materialLotTracking)
        {
            if ((materialLotTracking.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(materialLotTracking, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MaterialLotTrackings.Attach(materialLotTracking);
                this.ObjectContext.MaterialLotTrackings.DeleteObject(materialLotTracking);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MaterialTypes' query.
        [Query(IsDefault = true)]
        public IQueryable<MaterialType> GetMaterialTypes()
        {
            return this.ObjectContext.MaterialTypes;
        }

        public void InsertMaterialType(MaterialType materialType)
        {
            if ((materialType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(materialType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MaterialTypes.AddObject(materialType);
            }
        }

        public void UpdateMaterialType(MaterialType currentMaterialType)
        {
            this.ObjectContext.MaterialTypes.AttachAsModified(currentMaterialType, this.ChangeSet.GetOriginal(currentMaterialType));
        }

        public void DeleteMaterialType(MaterialType materialType)
        {
            if ((materialType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(materialType, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MaterialTypes.Attach(materialType);
                this.ObjectContext.MaterialTypes.DeleteObject(materialType);
            }
        }
    }
}


