//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CanvasScriptServer.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Scripts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ScriptAsJson { get; set; }
        public int UserId { get; set; }
        public System.DateTime Created { get; set; }
    
        public virtual Users User { get; set; }
    }
}
