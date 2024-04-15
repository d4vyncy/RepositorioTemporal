using Microsoft.EntityFrameworkCore;
using mvcProyectoWeb.AccesoDatos.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mvcProyectoWeb.AccesoDatos.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;
        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public T GetById(int id)
        {
            return dbSet.Find(id);
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            // Se crea una consulta Iqueryable a partir del DbSet del context
            IQueryable<T> query = dbSet;

            //Se aplica el filtro si se proporciona
            if (filter != null)
            {
                query = query.Where(filter);
            }
            //Se incluye propiedades de navegacion si se proporcionan
            if (includeProperties != null)
            {
                // se divide la ccadena de propoideades por coma y se itera sobre ellas
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(includeProperty);
                }
            }

            //se aplica el ordenamiento siempre y cuando se proporcione
            if(orderBy != null)
            {
                // se ejecuta la funcion de ordenamiento y se convierte la consulta en una lista
                return orderBy(query).ToList();
            }
            //si no se proporciona el ordenamiento se convierte la consulta en una lista
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            
            //se crea una consulta Iqueryable a partir del dbset del contexto
            IQueryable<T> query = dbSet;

            //se aplica el filtro si se proporciona
            if(filter!= null)
            {
                query = query.Where(filter);
            }
            // se incluyen propiedades de navegacion
            if(includeProperties != null)
            {
                // se divide la cadena de propoideades por coma y se itera sobre ellas
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();
        }


        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
