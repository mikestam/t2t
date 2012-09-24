
namespace T2T.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies SemiFinishedMaterialInventoryMetadata as the class
    // that carries additional metadata for the SemiFinishedMaterialInventory class.
    [MetadataTypeAttribute(typeof(SemiFinishedMaterialInventory.SemiFinishedMaterialInventoryMetadata))]
    public partial class SemiFinishedMaterialInventory
    {

        // This class allows you to attach custom attributes to properties
        // of the SemiFinishedMaterialInventory class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SemiFinishedMaterialInventoryMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SemiFinishedMaterialInventoryMetadata()
            {
            }

            public int Count { get; set; }

            public string InternalCode { get; set; }
        }
    }
}
