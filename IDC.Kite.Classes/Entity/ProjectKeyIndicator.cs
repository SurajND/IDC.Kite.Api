using System;

namespace IDC.Kite.Classes.Entity
{
    public class ProjectKeyIndicator
    {
        public Guid Id { get; set; }
        public float Percentage { get; set; }
        public float Actual { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Comment { get; set; }
        public Guid KeyIndicatorId { get; set; }
        public KeyIndicator KeyIndicator { get; set; }
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
