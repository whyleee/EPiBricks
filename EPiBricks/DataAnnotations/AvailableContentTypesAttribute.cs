using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Core;
using Perks;

namespace EPiBricks.DataAnnotations
{
    // NOTE: decompiled from EPiBoost library and upgraded (but code hasn't been refactored):
    //  - Added support for attribute on interface with content area property for restrictions reuse across the project
    //  - Better error output
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Interface)]
    public class AvailableContentTypesAttribute : ValidationAttribute
    {
        public Type IncludeFrom { get; set; }
        public Type[] Include
        {
            get;
            set;
        }
        public Type[] Exclude
        {
            get;
            set;
        }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            if (!(value is ContentArea))
            {
                throw new ValidationException("AvailableContentTypesAttribute is intended only for use with ContentArea properties");
            }
            ContentArea contentArea = value as ContentArea;
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            if (contentArea != null)
            {
                if (IncludeFrom != null)
                {
                    var includedTypes = IncludeFrom.GetCustomAttribute<AvailableContentTypesAttribute>().IfNotNull(x => x.Include);

                    if (includedTypes != null)
                    {
                        Include = Include != null ? Include.Union(includedTypes).ToArray() : includedTypes;
                    }
                }
                if (this.Include != null)
                {
                    System.Collections.Generic.IEnumerable<IContent> source =
                        from x in contentArea.Contents
                        where !this.ContainsType(this.Include, x.GetType())
                        select x;
                    if (source.Any<IContent>())
                    {
                        list.AddRange(
                            from x in source
                            select string.Format("[{2}] {0} ({1})", x.Name, x.ContentLink.ID, GetFriendlyBlockTypeName(x)));
                    }
                }
                if (this.Exclude != null)
                {
                    System.Collections.Generic.IEnumerable<IContent> source =
                        from x in contentArea.Contents
                        where this.ContainsType(this.Exclude, x.GetType())
                        select x;
                    if (source.Any<IContent>())
                    {
                        list.AddRange(
                            from x in source
                            select string.Format("[{2}] {0} ({1})", x.Name, x.ContentLink.ID, GetFriendlyBlockTypeName(x)));
                    }
                }
            }
            bool result;
            if (list.Any<string>())
            {
                base.ErrorMessage = "contains invalid content items: ";
                foreach (string current in list)
                {
                    base.ErrorMessage = base.ErrorMessage + " " + current + ", ";
                }
                base.ErrorMessage = base.ErrorMessage.TrimEnd(new char[]
				{
					',', ' '
				});
                ErrorMessage += ". Supported block types: " + string.Join(", ", Include.Select(type => GetFriendlyName(type.Name))) + ".";
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        private bool ContainsType(System.Type[] include, System.Type type)
        {
            return include.Any((System.Type inc) => inc.IsAssignableFrom(type));
        }

        private string GetFriendlyBlockTypeName(IContent block)
        {
            return GetFriendlyName(block.GetType().Name);
        }

        private string GetFriendlyName(string name)
        {
            var loweredName = new StringBuilder(name.Replace("Proxy", "").ToFriendlyString().ToLower());
            loweredName[0] = char.ToUpper(loweredName[0]);

            return loweredName.ToString();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = base.IsValid(value, validationContext);
            if (validationResult != null && !string.IsNullOrEmpty(validationResult.ErrorMessage))
            {
                validationResult.ErrorMessage = string.Format("{0} {1}", validationContext.DisplayName, base.ErrorMessage);
            }
            return validationResult;
        }
    }
}
