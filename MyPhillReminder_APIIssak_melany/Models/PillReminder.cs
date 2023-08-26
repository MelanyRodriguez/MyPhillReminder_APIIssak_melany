using System;
using System.Collections.Generic;

namespace MyPhillReminder_APIIssak_melany.Models
{
    public partial class PillReminder
    {
        public PillReminder()
        {
            ReminderStepReminderSteps = new HashSet<ReminderStep>();
        }

        public int ReminderId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int? Alarm { get; set; }
        public bool AlarmActive { get; set; }
        public bool AlarmJustinWeekDays { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }
        public int ReminderCategory { get; set; }

        public virtual ReminderCategory? ReminderCategoryNavigation { get; set; } = null!;
        public virtual User? User { get; set; } = null!;

        public virtual ICollection<ReminderStep>? ReminderStepReminderSteps { get; set; }
    }
}
