using BussinessLogic;
using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServicesApiNet6.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IBussinesLogic _bussinesLogic;

        public ProductoController(IBussinesLogic bussinesLogic)
        {
            _bussinesLogic = bussinesLogic;
        }

        [HttpPost("GetProductos")]
        public List<Producto> GetProductos()
        {
            return _bussinesLogic.ObtenerProducto();
        }

        [HttpPost("InsertProducto")]
        public Response InsertProducto(Producto producto)
        {
            return _bussinesLogic.AgregarProducto(producto);
        }

        [HttpPost("ActualizarProducto")]
        public Response ActualizarProducto(Producto producto)
        {
            return _bussinesLogic.ActualizarProducto(producto);
        }

        [HttpPost("EliminarProducto")]
        public Response EliminarProducto(int id)
        {
            return _bussinesLogic.ElimnarProducto(id);
        }
    }
}
