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
    public partial class WorkProcessSegment
    {
        public WorkProcessSegment()
        {
            this.ResourceCapabilities = new HashSet<ResourceCapability>();
            this.ResourceSpecifications = new HashSet<ResourceSpecification>();
        }
    
        public int WorkProcessSegmentID { get; set; }
        public Nullable<int> ParentWorkProcessSegmentID { get; set; }
    
        public virtual ICollection<ResourceCapability> ResourceCapabilities { get; set; }
        public virtual ICollection<ResourceSpecification> ResourceSpecifications { get; set; }
    }
    
}