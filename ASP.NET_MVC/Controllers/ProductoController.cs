using ASP.NET_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASP.NET_MVC.Controllers
{
    public class ProductoController : Controller
    {

        private  HttpClient _httpClient;

        public ProductoController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri("https://localhost:44341/api/producto/");
        }

        public async Task<List<Fabricante>> FabricantesLista()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri("https://localhost:44341/api/prueba/");
            var contenido = new StringContent("", System.Text.Encoding.UTF8, "application/json");

            var respuesta = await _httpClient.PostAsync("GetFabricante", contenido);


            if (respuesta.IsSuccessStatusCode)
            {
                var res = await respuesta.Content.ReadAsStringAsync();
                var fabricantes = JsonConvert.DeserializeObject<List<Fabricante>>(res);


                return fabricantes;
            }
            else
            {
                return null;
            }
        }

        // GET: ProductoController
        public async Task <ActionResult> IndexProducto()
        {
            var contenido = new StringContent("", System.Text.Encoding.UTF8, "application/json");

            var respuesta = await _httpClient.PostAsync("GetProductos", contenido);


            if (respuesta.IsSuccessStatusCode)
            {
                var res = await respuesta.Content.ReadAsStringAsync();


                var producto = JsonConvert.DeserializeObject<List<Producto>>(res);

                //ViewBag.msg = TempData["msg"] as string;
                //ViewBag.code = TempData["code"] as string;

                return View(producto);
            }
            else
            {
                return View("Error");
            }
        }


        // GET: ProductoController/Create
        public async Task<ActionResult> CreateProducto()
        {
            var listaFabricantes = await FabricantesLista();
            if (listaFabricantes!=null) 
            { 
                ViewBag.Fabricantes = listaFabricantes;

                return View();
            }
            else
            {
                return View("Error");
            }
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProducto(IFormCollection collection)
        {
            try
            {
                string nombre = collection["nombre"];
                decimal precio = Convert.ToDecimal(collection["precio"]);
                int idFabricante= Convert.ToInt16(collection["fabricante"]);

                Producto produto= new Producto();
                produto.Fabricante = new Fabricante();
                produto.Nombre=nombre;
                produto.Precio=precio;
                produto.Fabricante.Id=idFabricante;
                produto.Fabricante.Nombre = "";

                var json = JsonConvert.SerializeObject(produto);

                var contenido = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Realizar la solicitud POST
                var respuesta = await _httpClient.PostAsync("InsertProducto", contenido);

                if (respuesta.IsSuccessStatusCode)
                {
                    var res = await respuesta.Content.ReadAsStringAsync();


                    var response = JsonConvert.DeserializeObject<Response>(res);

                    TempData["codeP"] = response.code.ToString();

                    TempData["msgP"] = response.message;

                    return RedirectToAction("IndexProducto");
                }
                else
                {

                    return Redirect("IndexProducto");
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Edit/5
        public async Task<ActionResult> EditProducto(int id)
        {
            using (var httpClientProductos = new HttpClient())
            {

            }
            var contenido = new StringContent("", System.Text.Encoding.UTF8, "application/json");

            var respuesta = await _httpClient.PostAsync("GetProductos", contenido);


            if (respuesta.IsSuccessStatusCode)
            {
                var res = await respuesta.Content.ReadAsStringAsync();


                var productos = JsonConvert.DeserializeObject<List<Producto>>(res);

                var producto=productos.Where(p => p.IdProducto == id).FirstOrDefault();


                var listFabricantes=await FabricantesLista();

                ViewBag.Fabricantes = listFabricantes;



                return View(producto);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProducto(int id, IFormCollection collection)
        {
            try
            {
                string nombre = collection["nombre"];
                string precio = collection["precio"];
                string idFabricante= collection["fabricante.Id"];
                
                Producto producto = new Producto();
                producto.IdProducto = id;
                producto.Nombre = nombre;
                producto.Precio = Convert.ToDecimal(precio);
                producto.Fabricante = new Fabricante();
                producto.Fabricante.Id = Convert.ToInt32(idFabricante);
                producto.Fabricante.Nombre = "";

                var json = JsonConvert.SerializeObject(producto);

                var contenido = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Realizar la solicitud POST
                var respuesta = await _httpClient.PostAsync("ActualizarProducto", contenido);

                if (respuesta.IsSuccessStatusCode)
                {
                    var res = await respuesta.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response>(res);
                    return RedirectToAction("IndexProducto");
                }
                else
                {

                    return Redirect("IndexProducto");
                }
            }
            catch
            {
                return View();
            }
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        
        public async Task<ActionResult> DeleteProducto(int id)
        {
            string entero = id.ToString();
            ;
            var contenido = new StringContent(entero, System.Text.Encoding.UTF8, "text/plain");

            // Realizar la solicitud POST
            var respuesta = await _httpClient.PostAsync("EliminarProducto?id=" + id, null);

            if (respuesta.IsSuccessStatusCode)
            {
                var res = await respuesta.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Response>(res);
                return RedirectToAction("IndexProducto");
            }
            else
            {

                return RedirectToAction("IndexProducto");
            }
        }
    }
}
