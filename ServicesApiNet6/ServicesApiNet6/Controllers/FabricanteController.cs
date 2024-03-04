using BussinessLogic;
using Entidades;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServicesApiNet6.Controllers
{
    [Route("api/prueba")]
    [ApiController]
    public class FabricanteController : ControllerBase
    {
        private readonly IBussinesLogic _bussinesLogic;

        public FabricanteController(IBussinesLogic bussinesLogic)
        {
            _bussinesLogic = bussinesLogic;
        }

        [HttpPost("GetFabricante")]
        public List<Fabricante> GetFabricantes()
        {
            return _bussinesLogic.ObtenerFabricantes();
        }

        [HttpPost("InsertFabricante")]
        public Response InsertFabricante(Fabricante fabricante)
        {
            return _bussinesLogic.AgregarFabricante(fabricante.Nombre);
        }

        [HttpPost("ActualizarFabricante")]
        public Response ActualizarFabricante(Fabricante fabricante)
        {
            return _bussinesLogic.ActualizarFabricante(fabricante.Id, fabricante.Nombre);
        }

        [HttpPost("EliminarFabricante")]
        public Response EliminarFabricante(int id)
        {
            return _bussinesLogic.ElimnarFabricante(id);
        }
    }
}
