using DataContext;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic
{
    public class BusssinesLogicRepository: IBussinesLogic
    {
        private readonly IDataContext _dataContext;
        Response response = null;

        public BusssinesLogicRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Fabricante> ObtenerFabricantes()
        {
            return _dataContext.ConsultaFabricantes();
        }

        public Response AgregarFabricante(string nombre)
        {
            response = new Response();
            if (string.IsNullOrEmpty(nombre))
            {
                response.code = 406;
                response.message = "El nombre del fabricante es obligatorio";
                return response;

            }

            int idNuevo = _dataContext.AgregrrFabricante(nombre);

            if (idNuevo == 0)
            {
                response.code = 500;
                response.message = "Ocurrio un error en el servidor al insertar el fabricante";
                return response;
            }
            else
            {
                response.code = 200;
                response.message = "Se agregó el fabricante correctamente";
            }

            return response;
        }


        public Response ActualizarFabricante(int id, string nombre)
        {
            response = new Response();
            if (string.IsNullOrEmpty(nombre))
            {
                response.code = 406;
                response.message = "El nombre del fabricante es obligatorio";
                return response;
            }

            if (id == 0)
            {
                response.code = 500;
                response.message = "Ocurrio un error en el servidor al insertar el fabricante";
                return response;
            }
            else
            {
                _dataContext.ActualizarFabricante(id, nombre);
                response.code = 200;
                response.message = "Se actualizó el fabricante correctamente";
            }

            return response;
        }


        public Response ElimnarFabricante(int id)
        {
            response = new Response();

            _dataContext.EliminarFabricante(id);
            response.code = 200;
            response.message = "Se Eliminó el fabricante correctamente";


            return response;
        }

        #region Producto

        public List<Producto> ObtenerProducto()
        {
            return _dataContext.ConsultaProducto();
        }

        public Response AgregarProducto(Producto producto)
        {
            response = new Response();
            if (string.IsNullOrEmpty(producto.Nombre))
            {
                response.code = 406;
                response.message = "El nombre del producto es obligatorio";
                return response;

            }

            int idNuevo = _dataContext.AgregrarProducto(producto);

            if (idNuevo == 0)
            {
                response.code = 500;
                response.message = "Ocurrio un error en el servidor al insertar el fabricante";
                return response;
            }
            else
            {
                response.code = 200;
                response.message = "Se agregó el producto correctamente";
            }

            return response;
        }


        public Response ActualizarProducto(Producto producto)
        {
            response = new Response();
            if (string.IsNullOrEmpty(producto.Nombre))
            {
                response.code = 406;
                response.message = "El nombre del produccto es obligatorio";
                return response;
            }

            if (producto.IdProducto == 0)
            {
                response.code = 406;
                response.message = "No existe ese producto";
                return response;
            }
            else
            {
                _dataContext.ActualizarProducto(producto);
                response.code = 200;
                response.message = "Se actualizó el producto correctamente";
            }

            return response;
        }


        public Response ElimnarProducto(int id)
        {
            response = new Response();

            _dataContext.EliminarProducto(id);
            response.code = 200;
            response.message = "Se Eliminó el producto correctamente";


            return response;
        }

        #endregion

    }

}

