//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StopStation
    {
        public int ID { get; set; }
        public string StopLocation { get; set; }
        public int RouteID { get; set; }
        public System.TimeSpan TimeStop { get; set; }
    
        public virtual Route Route { get; set; }
    }
}
