namespace ASP.NET_MVC.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public Fabricante Fabricante { get; set; }
    }
}
