using System;
using System.Collections.Generic;

namespace IDC.Kite.Classes.Entity
{
    public class Project
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public Guid OperationalCompanyId { get; set; }
        public OperationalCompany OperationalCompany { get; set; }
    }
}
