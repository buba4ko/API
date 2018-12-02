using BookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Business
{
    public abstract class BaseService : IDisposable
    {
        #region Protected & Private members
        protected LibraryContext Context;
        private bool disposed;
        #endregion

        #region Constructors
        public BaseService()
        {
            this.Context = new LibraryContext();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (disposed || !disposing) return;

            Context.Dispose();
            disposed = true;
        }
        #endregion
    }
}
