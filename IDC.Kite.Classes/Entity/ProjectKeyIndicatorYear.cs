using System;

namespace IDC.Kite.Classes.Entity
{
    public class ProjectKeyIndicatorYear
    {
        public Guid Id { get; set; }
        public float Value { get; set; }
        public int Year { get; set; }
        public string Comment { get; set; }
        public Guid KeyIndicatorId { get; set; }
        public KeyIndicator KeyIndicator { get; set; }
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
