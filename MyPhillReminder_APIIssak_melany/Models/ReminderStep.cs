using System;
using System.Collections.Generic;

namespace MyPhillReminder_APIIssak_melany.Models
{
    public partial class ReminderStep
    {
        public ReminderStep()
        {
            PillReminderReminders = new HashSet<PillReminder>();
        }

        public int ReminderStepId { get; set; }
        public string Step { get; set; } = null!;
        public string? Description { get; set; }
        public int UserId { get; set; }

        public virtual User? User { get; set; } = null!;

        public virtual ICollection<PillReminder>? PillReminderReminders { get; set; }
    }
}
