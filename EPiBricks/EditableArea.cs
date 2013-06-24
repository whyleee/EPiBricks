using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiGlue;
using Perks;

namespace EPiBricks
{
    public class EditableArea : IDisposable
    {
        private readonly ViewContext _viewContext;
        private readonly IEditHtmlString _field;
        private bool _disposed;

        public EditableArea(ViewContext viewContext, IEditHtmlString field)
        {
            Ensure.ArgumentNotNull(viewContext, "viewContext");

            _viewContext = viewContext;
            _field = field;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (_field != null)
                {
                    _viewContext.Writer.Write(_field.EditorEnd);
                }
            }
        }
    }
}