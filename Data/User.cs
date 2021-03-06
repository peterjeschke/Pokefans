﻿// Copyright 2015 the pokefans-core authors. See copying.md for legal info.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Pokefans.Data.Attributes;

namespace Pokefans.Data
{
    [Table("system_users")]
    public partial class User : IUser<int>
    {
        public User()
        {
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Du musst einen Benutzernamen angeben")]
        [MaxLength(45, ErrorMessage = "Dein Benutzername darf maximal 45 Zeichen lang sein.")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_ ]{0,43}[a-zA-Z0-9]$", ErrorMessage = "Dein Benutzername darf nur aus Großbuchstaben, Kleinbuchstaben, Bindestrich (-) und Unterstich(_) bestehen. Außerdem muss er mit einem Buchstaben beginnen und darf nicht mit Bindestrich oder Unterstrich aufhören.")]
        [Column("name", TypeName = "VARCHAR")]
        [Index]
        public virtual string UserName { get; set; }

        [Required]
        [Column("registered")]
        public virtual DateTime Registered { get; set; }

        [Required]
        [MaxLength(39)]
        [Column("registered_ip")]
        public virtual string RegisteredIp { get; set; }

        [Required]
        [MaxLength(45)]
        [Column("url")]
        [Index]
        public virtual string Url { get; set; }

        [Required]
        [Column("email_confirmed")]
        public virtual bool EmailConfirmed { get; set; }

        [Column("two_factor_enabled")]
        public virtual bool TwoFactorEnabled { get; set; }

        [MaxLength(45)]
        [Column("rank")]
        public virtual string Rank { get; set; }

        [MaxLength(9)]
        [Column("color")]
        public virtual string Color { get; set; }

        [Column("unread_notifications")]
        public virtual Nullable<short> UnreadNotificationCount { get; set; }

        [MaxLength(89)]
        [Column("password")]
        public virtual string Password { get; set; }

        [MaxLength(32)]
        [Column("activationkey")]
        public virtual string Activationkey { get; set; }

        [Column("security_stamp")]
        public virtual string SecurityStamp { get; set; }

        [Required]
        [Column("email")]
        [MaxLength(45, ErrorMessage = "Deine E-Mail-Addresse darf maximal 45 Zeichen lang sein.")]
        [RegularExpressionWithOptions(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Deine E-Mail-Adresse ist fehlerhaft.", RegexOptions = RegexOptions.IgnoreCase)]
        public virtual string Email { get; set; }

        [Column("is_locked_out")]
        public virtual bool IsLockedOut { get; set; }

        [Column("locked_out_date")]
        private Nullable<DateTime> lockedOutDate { get; set; }

        [NotMapped]
        public virtual Nullable<DateTimeOffset> LockedOutDate
        {
            get 
            {
                return lockedOutDate.HasValue ? new DateTimeOffset(lockedOutDate.Value) : (Nullable<DateTimeOffset>)null;
            }
            set 
            {
                lockedOutDate = value.Value.LocalDateTime;
            }
        }

        [Column("access_failed_count")]
        public virtual int AccessFailedCount { get; set; }

        private ICollection<RoleLogEntry> roleLogs;
        [InverseProperty("AffectedUser")]
        public virtual ICollection<RoleLogEntry> RoleLogs 
        {
            get { return roleLogs ?? (roleLogs = new HashSet<RoleLogEntry>()); }
            set { roleLogs = value; }
        }

        private ICollection<RoleLogEntry> givenRoleLogs;
        [InverseProperty("User")]
        public virtual ICollection<RoleLogEntry> GivenRoleLogs
        {
            get { return givenRoleLogs ?? (givenRoleLogs = new HashSet<RoleLogEntry>()); }
            set { givenRoleLogs = value; }
        }

        private ICollection<UserLogin> logins;
        [InverseProperty("User")]
        public virtual ICollection<UserLogin> Logins 
        {
            get { return logins ?? (logins = new HashSet<UserLogin>()); }
            set { logins = value; }
        }

        private ICollection<UserRole> roles;
        [InverseProperty("User")]
        public virtual ICollection<UserRole> Roles 
        {
            get { return roles ?? (roles = new HashSet<UserRole>()); }
            set { roles = value; }
        }

        private ICollection<UserLoginProvider> providers;
        [InverseProperty("User")]
        public virtual ICollection<UserLoginProvider> LoginProviders 
        {
            get { return providers ?? (providers = new HashSet<UserLoginProvider>()); }
            set { providers = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one 
            // defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}
