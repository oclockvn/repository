using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace oclockvn.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _db;
        private bool _disposed;

        public UnitOfWork(DbContext db)
        {
            _db = db;

#if DEBUG
            this._db.Database.Log = msg => System.Diagnostics.Debug.WriteLine(msg);
#endif
        }

        public IRepository<T> Get<T>() where T : class => new Repository<T>(_db);

        public int SaveChanges()
        {
            // If any error throw, handle exception by your own way
            var record = _db.SaveChanges();
            return record;
        }

        /// <inheritdoc />
        /// <summary>
        /// Save any change in database context, with async task
        /// </summary>
        /// <returns>
        /// Total effected records
        /// </returns>
        public async Task<int> SaveChangesAsync()
        {
            var record = await _db.SaveChangesAsync();
            return record;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }

            _disposed = true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Execute command queries like insert, update, delete
        /// </summary>
        /// <param name="query">a string of query command</param>
        /// <param name="args">a list of sql parameters</param>
        /// <returns>
        /// Total effected records
        /// </returns>
        public int ExecuteSql(string query, params object[] args) => _db.Database.ExecuteSqlCommand(query, args);

        /// <inheritdoc />
        /// <summary>
        /// Execute command queries like insert, update, delete asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public async Task<int> ExecuteSqlAsync(string query, params object[] args) => await _db.Database.ExecuteSqlCommandAsync(query, args);

        /// <inheritdoc />
        /// <summary>
        /// Execute queries sort of select, store proc or function
        /// </summary>
        /// <typeparam name="T">typeof return result</typeparam>
        /// <param name="query">a string of select query</param>
        /// <param name="args">a list of sql parameters</param>
        /// <returns>
        /// The collection of query
        /// </returns>
        public List<T> SqlQuery<T>(string query, params object[] args) => _db.Database.SqlQuery<T>(query, args).ToList();

        /// <inheritdoc />
        /// <summary>
        /// Execute queries sort of select, store proc or function asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public async Task<List<T>> SqlQueryAsync<T>(string query, params object[] args) => await _db.Database.SqlQuery<T>(query, args).ToListAsync();        
    }
}
