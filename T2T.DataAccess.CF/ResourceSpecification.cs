//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace T2T.Model
{
    public partial class ResourceSpecification
    {
        public ResourceSpecification()
        {
            this.ResourceSpecificationProperties = new HashSet<ResourceSpecificationProperty>();
        }
    
        public int ResourceSpecificationID { get; set; }
        public Nullable<int> WorkProcessSegmentID { get; set; }
        public Nullable<int> ResourceID { get; set; }
        public Nullable<int> ResourceTypeID { get; set; }
        public string ResourceUse { get; set; }
        public string Description { get; set; }
        public Nullable<float> Quantity { get; set; }
        public string QuantityUnitOfMeasure { get; set; }
    
        public virtual ResourceClass ResourceClass { get; set; }
        public virtual ResourceDefinition ResourceDefinition { get; set; }
        public virtual ResourceLot ResourceLot { get; set; }
        public virtual ResourceType ResourceType { get; set; }
        public virtual WorkProcessSegment WorkProcessSegment { get; set; }
        public virtual ICollection<ResourceSpecificationProperty> ResourceSpecificationProperties { get; set; }
    }
    
}