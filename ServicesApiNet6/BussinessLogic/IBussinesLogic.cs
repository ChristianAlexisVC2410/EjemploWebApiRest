using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic
{
    public interface IBussinesLogic
    {
        List<Fabricante> ObtenerFabricantes();
        Response AgregarFabricante(string nombre);
        Response ActualizarFabricante(int id, string nombre);
        Response ElimnarFabricante(int id);


        List<Producto> ObtenerProducto();
        Response AgregarProducto(Producto producto);
        Response ActualizarProducto(Producto producto);
        Response ElimnarProducto(int id);
    }
}
