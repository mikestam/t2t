
namespace T2T.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies MaterialClassMetadata as the class
    // that carries additional metadata for the MaterialClass class.
    [MetadataTypeAttribute(typeof(MaterialClass.MaterialClassMetadata))]
    public partial class MaterialClass
    {

        // This class allows you to attach custom attributes to properties
        // of the MaterialClass class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MaterialClassMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MaterialClassMetadata()
            {
            }

            public EntityCollection<MaterialClass> ChildClasses { get; set; }

            public string Description { get; set; }

            public int MaterialClassID { get; set; }

            public EntityCollection<MaterialDefinition> MaterialDefinitions { get; set; }

            public MaterialType MaterialType { get; set; }

            public byte MaterialTypeID { get; set; }

            public string Name { get; set; }

            public MaterialClass ParentClass { get; set; }

            public Nullable<int> ParentClassID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MaterialDefinitionMetadata as the class
    // that carries additional metadata for the MaterialDefinition class.
    [MetadataTypeAttribute(typeof(MaterialDefinition.MaterialDefinitionMetadata))]
    public partial class MaterialDefinition
    {

        // This class allows you to attach custom attributes to properties
        // of the MaterialDefinition class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MaterialDefinitionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MaterialDefinitionMetadata()
            {
            }

            public string Description { get; set; }

            public MaterialClass MaterialClass { get; set; }

            public int MaterialClassID { get; set; }

            public int MaterialDefinitionID { get; set; }

            public EntityCollection<MaterialLot> MaterialLots { get; set; }

            public string Name { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MaterialInventoryMetadata as the class
    // that carries additional metadata for the MaterialInventory class.
    [MetadataTypeAttribute(typeof(MaterialInventory.MaterialInventoryMetadata))]
    public partial class MaterialInventory
    {

        // This class allows you to attach custom attributes to properties
        // of the MaterialInventory class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MaterialInventoryMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MaterialInventoryMetadata()
            {
            }

            public string CAI { get; set; }

            public int Count { get; set; }

            public string InterniKod { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MaterialLotMetadata as the class
    // that carries additional metadata for the MaterialLot class.
    [MetadataTypeAttribute(typeof(MaterialLot.MaterialLotMetadata))]
    public partial class MaterialLot
    {

        // This class allows you to attach custom attributes to properties
        // of the MaterialLot class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MaterialLotMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MaterialLotMetadata()
            {
            }

            public DateTime CreationDate { get; set; }

            public string Description { get; set; }



            public int UserID { get; set; }

            public string LotID { get; set; }

            public MaterialDefinition MaterialDefinition { get; set; }

            public int MaterialDefinitionID { get; set; }

            public int MaterialLotID { get; set; }

            public MaterialLotStatus MaterialLotStatus { get; set; }

            public int MaterialLotStatusID { get; set; }

            public int EquipmentID { get; set; }

            public decimal Quantity { get; set; }

            public string QuantityUnitOfMeasure { get; set; }

            public Nullable<DateTime> ValidFrom { get; set; }

            public Nullable<DateTime> ValidTill { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MaterialLotStatusMetadata as the class
    // that carries additional metadata for the MaterialLotStatus class.
    [MetadataTypeAttribute(typeof(MaterialLotStatus.MaterialLotStatusMetadata))]
    public partial class MaterialLotStatus
    {

        // This class allows you to attach custom attributes to properties
        // of the MaterialLotStatus class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MaterialLotStatusMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MaterialLotStatusMetadata()
            {
            }

            public string Description { get; set; }

            public string LotStatus { get; set; }

            public EntityCollection<MaterialLot> MaterialLots { get; set; }

            public int LotStatusID { get; set; }

            public int SortOrder { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MaterialLotTrackingMetadata as the class
    // that carries additional metadata for the MaterialLotTracking class.
    [MetadataTypeAttribute(typeof(MaterialLotTracking.MaterialLotTrackingMetadata))]
    public partial class MaterialLotTracking
    {

        // This class allows you to attach custom attributes to properties
        // of the MaterialLotTracking class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MaterialLotTrackingMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MaterialLotTrackingMetadata()
            {
            }

            public string Data { get; set; }

            public string Description { get; set; }

   

            public int MaterialLotID { get; set; }

            public string Operation { get; set; }

            public DateTime TrackingDate { get; set; }

            public int TrackingID { get; set; }

            public int UserID { get; set; }
            public int EquipmentID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MaterialTypeMetadata as the class
    // that carries additional metadata for the MaterialType class.
    [MetadataTypeAttribute(typeof(MaterialType.MaterialTypeMetadata))]
    public partial class MaterialType
    {

        // This class allows you to attach custom attributes to properties
        // of the MaterialType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MaterialTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MaterialTypeMetadata()
            {
            }

            public string Description { get; set; }

            public EntityCollection<MaterialClass> MaterialClasses { get; set; }

            public byte MaterialTypeID { get; set; }

            public string TypeID { get; set; }
        }
    }
}
