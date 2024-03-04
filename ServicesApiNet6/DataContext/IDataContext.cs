using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public interface IDataContext
    {
        List<Fabricante> ConsultaFabricantes();
        int AgregrrFabricante(string nombre);
        void ActualizarFabricante(int id, string nombre);
        void EliminarFabricante(int id);

        #region Producto
        List<Producto> ConsultaProducto();
        int AgregrarProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        void EliminarProducto(int id);

        #endregion
    }
}
