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
    public partial class Property
    {
        public Property()
        {
            this.ResourceActualProperties = new HashSet<ResourceActualProperty>();
            this.ResourceProperties = new HashSet<ResourceProperty>();
            this.ResourceRequirementProperties = new HashSet<ResourceRequirementProperty>();
            this.ResourceSpecificationProperties = new HashSet<ResourceSpecificationProperty>();
            this.ResourceCapabilityProperties = new HashSet<ResourceCapabilityProperty>();
        }
    
        public int PropertyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public Nullable<int> ParentPropertyID { get; set; }
    
        public virtual ICollection<ResourceActualProperty> ResourceActualProperties { get; set; }
        public virtual ICollection<ResourceProperty> ResourceProperties { get; set; }
        public virtual ICollection<ResourceRequirementProperty> ResourceRequirementProperties { get; set; }
        public virtual ICollection<ResourceSpecificationProperty> ResourceSpecificationProperties { get; set; }
        public virtual ICollection<ResourceCapabilityProperty> ResourceCapabilityProperties { get; set; }
    }
    
}
