using System;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace oclockvn.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<DbContext> db;
        private bool disposed;

        public UnitOfWork(DbContext db)
        {
            this.db = new Lazy<DbContext>(() => db);

#if DEBUG
            this.db.Value.Database.Log = msg => System.Diagnostics.Debug.WriteLine(msg);
#endif
        }

        public IRepository<T> Get<T>() where T : class => new Repository<T>(db.Value);

        public Tuple<int, Exception> Commit()
        {
            Exception exception = null;
            //var msg = string.Empty;
            try
            {
                var record = db.Value.SaveChanges();
                return new Tuple<int, Exception>(record, exception);
            }
            catch (DbEntityValidationException ex)
            {
                exception = ex;
                //msg = string.Join(", ",
                //    ex.EntityValidationErrors
                //    .SelectMany(e => e.ValidationErrors)
                //    .SelectMany(e => $"Property: {e.PropertyName} -> {e.ErrorMessage}"));
            }
            catch (Exception ex)
            {
                exception = ex;
                //msg = ex.ToErrorMessage();
            }

            // send email to config account
            // MailHelper.SendMail(string.Empty, string.Empty, "Save changes error", msg);
            // return new Tuple<bool, string>(false, msg);

            return new Tuple<int, Exception>(0, exception);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                if (db.Value != null)
                {
                    db.Value.Dispose();
                }
            }

            disposed = true;
        }
    }
}
