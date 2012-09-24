
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
  


    // Implements application logic using the EquipmentDb context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class EquipmentService : LinqToEntitiesDomainService<T2TEntities>
    {
        

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Equipments' query.
        [Query(IsDefault = true)]
        public IQueryable<Equipment> GetEquipments()
        {
            //this.ObjectContext.Equipments.AddObject(new Equipment(){ EquipmentID = 7, Name="Herbert1", ShortName="Her1"});
            //this.ObjectContext.Equipments.AddObject(new Equipment() { EquipmentID = 8, Name = "Herbert2", ShortName = "Her2" });
            //this.ObjectContext.Equipments.AddObject(new Equipment() { EquipmentID = 9, Name = "Herbert3", ShortName = "Her3" });
            //this.ObjectContext.Equipments.AddObject(new Equipment() { EquipmentID = 10, Name = "Herbert4", ShortName = "Her4" });
            //this.ObjectContext.Equipments.AddObject(new Equipment() { EquipmentID = 11, Name = "Herbert5", ShortName = "Her5" });
            //this.ObjectContext.Equipments.AddObject(new Equipment() { EquipmentID = 12, Name = "Herbert8", ShortName = "Her8" });
            //this.ObjectContext.Equipments.AddObject(new Equipment() { EquipmentID = 13, Name = "Herbert9", ShortName = "Her9" });
                       
            return this.ObjectContext.Equipments.Where( e => e.ParentEquipment == 5);

        }

        public void InsertEquipment(Equipment equipment)
        {
            if ((equipment.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(equipment, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Equipments.AddObject(equipment);
            }
        }

        public void UpdateEquipment(Equipment currentEquipment)
        {
            this.ObjectContext.Equipments.AttachAsModified(currentEquipment, this.ChangeSet.GetOriginal(currentEquipment));
        }

        public void DeleteEquipment(Equipment equipment)
        {
            if ((equipment.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(equipment, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Equipments.Attach(equipment);
                this.ObjectContext.Equipments.DeleteObject(equipment);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'EquipmentClasses' query.
        [Query(IsDefault = true)]
        public IQueryable<EquipmentClass> GetEquipmentClasses()
        {
            return this.ObjectContext.EquipmentClasses;
        }

        public void InsertEquipmentClass(EquipmentClass equipmentClass)
        {
            if ((equipmentClass.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(equipmentClass, EntityState.Added);
            }
            else
            {
                this.ObjectContext.EquipmentClasses.AddObject(equipmentClass);
            }
        }

        public void UpdateEquipmentClass(EquipmentClass currentEquipmentClass)
        {
            this.ObjectContext.EquipmentClasses.AttachAsModified(currentEquipmentClass, this.ChangeSet.GetOriginal(currentEquipmentClass));
        }

        public void DeleteEquipmentClass(EquipmentClass equipmentClass)
        {
            if ((equipmentClass.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(equipmentClass, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.EquipmentClasses.Attach(equipmentClass);
                this.ObjectContext.EquipmentClasses.DeleteObject(equipmentClass);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'EquipmentElementLevels' query.
        [Query(IsDefault = true)]
        public IQueryable<EquipmentElementLevel> GetEquipmentElementLevels()
        {
            return this.ObjectContext.EquipmentElementLevels;
        }

        public void InsertEquipmentElementLevel(EquipmentElementLevel equipmentElementLevel)
        {
            if ((equipmentElementLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(equipmentElementLevel, EntityState.Added);
            }
            else
            {
                this.ObjectContext.EquipmentElementLevels.AddObject(equipmentElementLevel);
            }
        }

        public void UpdateEquipmentElementLevel(EquipmentElementLevel currentEquipmentElementLevel)
        {
            this.ObjectContext.EquipmentElementLevels.AttachAsModified(currentEquipmentElementLevel, this.ChangeSet.GetOriginal(currentEquipmentElementLevel));
        }

        public void DeleteEquipmentElementLevel(EquipmentElementLevel equipmentElementLevel)
        {
            if ((equipmentElementLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(equipmentElementLevel, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.EquipmentElementLevels.Attach(equipmentElementLevel);
                this.ObjectContext.EquipmentElementLevels.DeleteObject(equipmentElementLevel);
            }
        }
    }
}


