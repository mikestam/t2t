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
    public partial class Equipment1
    {
        public int EquipmentID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public Nullable<int> EquipmentClassID { get; set; }
        public Nullable<int> ParentEquipment { get; set; }
    
        public virtual EquipmentHmiData EquipmentHmiData { get; set; }
    }
    
}