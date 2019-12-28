using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorageProject.Models
{

    using System.Collections.Generic;

    public class RolesViewModel
    {
        public IEnumerable<string> RoleNames { get; set; }
        public string UserName { get; set; }
        public string Id { get; set; }

    }
}