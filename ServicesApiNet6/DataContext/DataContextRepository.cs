using DataContext.DAO;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public class DataContextRepository: IDataContext
    {
        private readonly Dao _dao;
        public DataContextRepository(Dao dao)
        {
            _dao = dao;
        }

        public List<Fabricante> ConsultaFabricantes()
        {
            return _dao.GetAllFabricantes();
        }

        public int AgregrrFabricante(string nombre)
        {
            return _dao.InsertFabricante(nombre);
        }

        public void ActualizarFabricante(int id, string nombre)
        {
            _dao.UpdateFabricante(id, nombre);
        }

        public void EliminarFabricante(int id)
        {
            _dao.DeleteFabricante(id);
        }

        #region Producto

        public List<Producto> ConsultaProducto()
        {
            return _dao.GetAllProductos();
        }

        public int AgregrarProducto(Producto producto)
        {
            return _dao.InsertProducto(producto);
        }

        public void ActualizarProducto(Producto producto)
        {
            _dao.UpdateProducto(producto);
        }

        public void EliminarProducto(int id)
        {
            _dao.DeleteProducto(id);
        }

        #endregion
    }
}
