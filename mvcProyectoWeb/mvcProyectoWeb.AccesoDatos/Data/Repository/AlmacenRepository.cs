using mvcProyectoWeb.AccesoDatos.Data.Repository.IRepository;
using mvcProyectoWeb.Data;
using mvcProyectoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcProyectoWeb.AccesoDatos.Data.Repository
{
    public class AlmacenRepository:Repository<Almacen>,IAlmacenRepository
    {
        private readonly ApplicationDbContext _db;
        public AlmacenRepository(ApplicationDbContext db): base(db) 
        {  _db = db; }
        public void Update(Almacen almacen)
        {
            var objDesdeDB = _db.Almacen.FirstOrDefault(s=>s.Id == almacen.Id);
            objDesdeDB.NombreAlmacen=almacen.NombreAlmacen;
            objDesdeDB.Direccion = almacen.Direccion;
            objDesdeDB.UrlImagen= almacen.UrlImagen;
        }
    }
}
