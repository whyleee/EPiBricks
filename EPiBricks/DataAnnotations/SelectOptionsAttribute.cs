using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perks;

namespace EPiBricks.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SelectOptionsAttribute : Attribute
    {
        public SelectOptionsAttribute(string options)
        {
            Ensure.ArgumentNotNull(options, "options");

            Options = options;
        }

        public string Options { get; set; }
    }
}
