// Copyright 2015 the pokefans authors. See copying.md for legal info
//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pokefans.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PermissionLogEntry
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int affected_user_id { get; set; }
        public int permission_id { get; set; }
        public string ip { get; set; }
    
        public virtual User system_users { get; set; }
        public virtual Permission system_permissions { get; set; }
        public virtual User system_users1 { get; set; }
    }
}
