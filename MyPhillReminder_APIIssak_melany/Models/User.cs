using System;
using System.Collections.Generic;

namespace MyPhillReminder_APIIssak_melany.Models
{
    public partial class User
    {
        public User()
        {
            PillReminders = new HashSet<PillReminder>();
            ReminderCategories = new HashSet<ReminderCategory>();
            ReminderSteps = new HashSet<ReminderStep>();
        }

        public int UserID { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string BackUpEmail { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }
        public bool Active { get; set; }
        public bool IsBlocked { get; set; }
        public int IDRoleUser { get; set; }

        public virtual UserRole? UserRoleId { get; set; } = null!;
        public virtual ICollection<PillReminder> PillReminders { get; set; }
        public virtual ICollection<ReminderCategory> ReminderCategories { get; set; }
        public virtual ICollection<ReminderStep> ReminderSteps { get; set; }
    }
}
