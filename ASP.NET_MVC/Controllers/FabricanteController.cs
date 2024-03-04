using ASP.NET_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text;

namespace ASP.NET_MVC.Controllers
{
    public class FabricanteController : Controller
    {
        private readonly HttpClient _httpClient;
        // GET: FabricanteController

        public FabricanteController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri("https://localhost:44341/api/prueba/");
        }
        public async Task<ActionResult> Index()
        {

            var contenido = new StringContent("", System.Text.Encoding.UTF8, "application/json");

            var respuesta = await _httpClient.PostAsync("GetFabricante", contenido);


            if (respuesta.IsSuccessStatusCode)
            {
                var res = await respuesta.Content.ReadAsStringAsync();
                

                var fabricante = JsonConvert.DeserializeObject<List<Fabricante>>(res);

                ViewBag.msg = TempData["msg"] as string;
                ViewBag.code = TempData["code"] as string;

                return View(fabricante);
            }
            else
            {
                return View("Error");
            }
        }



        // GET: FabricanteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FabricanteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>Create(IFormCollection collection)
        {
            //int id =Convert.ToInt32( collection["id"]);
            string nombre = collection["nombre"];

            Fabricante fabricante = new Fabricante();

            fabricante.Nombre = nombre;

            var json = JsonConvert.SerializeObject(fabricante);

            var contenido = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            // Realizar la solicitud POST
            var respuesta = await _httpClient.PostAsync("InsertFabricante", contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                var res = await respuesta.Content.ReadAsStringAsync();
                

                var response = JsonConvert.DeserializeObject<Response>(res);

                TempData["code"] = response.code.ToString();

                TempData["msg"] = response.message;

                return Redirect("Index");
            }
            else
            {
               
                return Redirect("Index");
            }
        }

        // GET: FabricanteController/Edit/5
        public async Task<ActionResult> EditarFabricante(int id)
        {
            var contenido = new StringContent("", System.Text.Encoding.UTF8, "application/json");

            var respuesta = await _httpClient.PostAsync("GetFabricante", contenido);
            Fabricante objFabricante = new Fabricante();

            if (respuesta.IsSuccessStatusCode)
            {
                var res = await respuesta.Content.ReadAsStringAsync();


                var fabricante = JsonConvert.DeserializeObject<List<Fabricante>>(res);


                foreach(var iterador in fabricante)
                {
                    if(iterador.Id == id)
                    {
                        objFabricante=iterador;
                        break;
                    }
                }

                //ViewBag.msg = TempData["msg"] as string;
                //ViewBag.code = TempData["code"] as string;

                return View(objFabricante);
            }
            else
            {
                return View("Error");
            }
      
        }

        // POST: FabricanteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarFabricante(int id, IFormCollection collection)
        {
            try
            {
                string nombre = collection["nombre"];

                Fabricante fabricante = new Fabricante();
                fabricante.Id = id;
                fabricante.Nombre = nombre;

                var json = JsonConvert.SerializeObject(fabricante);

                var contenido = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Realizar la solicitud POST
                var respuesta = await _httpClient.PostAsync("ActualizarFabricante", contenido);

                if (respuesta.IsSuccessStatusCode)
                {
                    var res = await respuesta.Content.ReadAsStringAsync();


                    var response = JsonConvert.DeserializeObject<Response>(res);

                    //TempData["codeEdit"] = response.code.ToString();

                    //TempData["msgEdit"] = response.message;
                    return RedirectToAction("Index");
                }
                else
                {

                    return Redirect("Index");
                }
            }
            catch
            {
                return View();
            }
        }



        // POST: FabricanteController/Delete/5
        [HttpPost]
      
        public async Task<ActionResult> Delete(int id)
        {
            //string nombre = collection["nombre"];

            Fabricante fabricante = new Fabricante();
            //fabricante.Id = id;

            string entero = id.ToString();
;
            var contenido = new StringContent(entero, System.Text.Encoding.UTF8, "text/plain");

            // Realizar la solicitud POST
            var respuesta = await _httpClient.PostAsync("EliminarFabricante?id="+ id, null);

            if (respuesta.IsSuccessStatusCode)
            {
                var res = await respuesta.Content.ReadAsStringAsync();


                var response = JsonConvert.DeserializeObject<Response>(res);

                TempData["codeDeelete"] = response.code.ToString();

                TempData["msgDelete"] = response.message;
                return RedirectToAction("Index");
            }
            else
            {

                return RedirectToAction("Index");
            }
        }
       
     
    }
}
