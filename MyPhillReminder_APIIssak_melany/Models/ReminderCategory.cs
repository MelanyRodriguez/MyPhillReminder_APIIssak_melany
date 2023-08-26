using System;
using System.Collections.Generic;

namespace MyPhillReminder_APIIssak_melany.Models
{
    public partial class ReminderCategory
    {
        public ReminderCategory()
        {
            PillReminders = new HashSet<PillReminder>();
        }

        public int ReminderCategory1 { get; set; }
        public string Description { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<PillReminder> PillReminders { get; set; }
    }
}
