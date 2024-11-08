using Microsoft.EntityFrameworkCore;
using NatroDomainManager.Application.Interfaces.Repositories;
using NatroDomainManager.Domain.Common;
using NatroDomainManager.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        private DbSet<T> Table { get => _context.Set<T>(); }
        public async Task<T> AddAsync(T entity, params Expression<Func<T, object>>[] includeProperties)
        {
            await Table.AddAsync(entity);
            await _context.SaveChangesAsync();

            IQueryable<T> query = Table;

            // includeProperties ile belirtilen tüm ilişkileri Include ederek sorguya dahil ediyoruz.
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            // İlgili kaydı ilişkileriyle birlikte yüklüyoruz.
            return await query.FirstOrDefaultAsync(e => e.Id == entity.Id);
            //return entity;
        }
        //public async Task<List<T>> GetAsync() => await Table.ToListAsync();

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            try
            {
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
            }
            catch (Exception e)
            {
                return await query.AsNoTracking().ToListAsync();
            }


            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                query = query.Where(predicate);

                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }

                return await query.AsNoTracking().SingleOrDefaultAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public async Task<T> GetByIdAsync(int id) => await Table.FindAsync(id);

        public async Task<T> UpdateAsync(T entity)
        {
            //await Table.AddAsync(entity);

            //await _context.SaveChangesAsync();

            //return entity;
            // Veritabanında var olan bir kaydı bulmak için
            var existingEntity = await Table.FirstOrDefaultAsync(e => e.Id == entity.Id); // Id'nin doğru şekilde eşleştiğinden emin olun

            if (existingEntity == null)
            {
                throw new InvalidOperationException("Entity not found for update.");
            }

            // Var olan kaydı güncelle
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
