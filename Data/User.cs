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
    
    public partial class User
    {
        public User()
        {
            this.system_permission_log = new HashSet<PermissionLogEntry>();
            this.system_permission_log1 = new HashSet<PermissionLogEntry>();
            this.system_user_logins = new HashSet<UserLogin>();
            this.system_user_permissions = new HashSet<UserPermission>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public System.DateTime registered { get; set; }
        public string registered_ip { get; set; }
        public string url { get; set; }
        public sbyte status { get; set; }
        public string ban_reason { get; set; }
        public Nullable<System.DateTime> ban_time { get; set; }
        public string rank { get; set; }
        public string color { get; set; }
        public Nullable<short> unread_notifications { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public string activationkey { get; set; }
    
        public virtual ICollection<PermissionLogEntry> system_permission_log { get; set; }
        public virtual ICollection<PermissionLogEntry> system_permission_log1 { get; set; }
        public virtual ICollection<UserLogin> system_user_logins { get; set; }
        public virtual ICollection<UserPermission> system_user_permissions { get; set; }
    }
}
