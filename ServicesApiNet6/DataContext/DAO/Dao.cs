using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace DataContext.DAO
{
    public class Dao
    {
        public readonly ConfiguracionConexion _conexion;

        public Dao(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion = conexion.Value;
        }


        public List<Fabricante> GetAllFabricantes()
        {
            List<Fabricante> listFabricantes = new List<Fabricante>();

            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GestorFabricante", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACCION", 1);
                    //cmd.Parameters.AddWithValue("@IdFabricante", 0);
                    //cmd.Parameters.AddWithValue("@NombreFabricante", "");
                    cmd.Parameters.AddWithValue("@NuevoID", 0);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listFabricantes.Add(
                               new Fabricante()
                               {
                                   Id = (int)reader["Id"],
                                   Nombre = (string)reader["Nombre"]
                               });
                        }
                    }
                    //conexion.Close();
                }

            }
            catch (Exception ex)
            {

            }
            return listFabricantes;

        }

        public int InsertFabricante(string nombre)
        {
            int IdNuevo = 0;
            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GestorFabricante", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACCION", 2);
                    cmd.Parameters.AddWithValue("@NombreFabricante", nombre);

                    var parameter = new SqlParameter("@NuevoID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output,
                    };
                    cmd.Parameters.Add(parameter);
                    cmd.ExecuteNonQuery();
                    IdNuevo = Convert.ToInt32(parameter.Value);
                    conexion.Close();
                }

            }
            catch (Exception e)
            {

            }
            return IdNuevo;
        }


        public void UpdateFabricante(int id, string nombre)
        {
            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GestorFabricante", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACCION", 3);
                    cmd.Parameters.AddWithValue("@IdFabricante", id);
                    cmd.Parameters.AddWithValue("@NombreFabricante", nombre);
                    cmd.Parameters.AddWithValue("@NuevoID", 0);
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }

            }
            catch (Exception e)
            {

            }
        }

        public void DeleteFabricante(int id)
        {
            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GestorFabricante", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACCION", 4);
                    cmd.Parameters.AddWithValue("@IdFabricante", id);
                    cmd.Parameters.AddWithValue("@NuevoID", 0);
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }

            }
            catch (Exception e)
            {

            }
        }

        #region Producto sp's

        public List<Producto> GetAllProductos()
        {
            List<Producto> listProductos = new List<Producto>();

            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GestorProducto", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACCION", 1);
                    //cmd.Parameters.AddWithValue("@IdFabricante", 0);
                    //cmd.Parameters.AddWithValue("@NombreFabricante", "");
                    cmd.Parameters.AddWithValue("@NuevoID", 0);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto producto = new Producto();
                            producto.Fabricante = new Fabricante();

                            producto.IdProducto = (int)reader["Id"];
                            producto.Nombre = (string)reader["Nombre"];
                            producto.Precio = (decimal)reader["Precio"];
                            producto.Fabricante.Id = (int)reader["IdFabricante"];
                            producto.Fabricante.Nombre = (string)reader["NombreFabricante"];

                            listProductos.Add(producto);
                        }
                    }
                    //conexion.Close();
                }

            }
            catch (Exception ex)
            {

            }
            return listProductos;

        }

        public int InsertProducto(Producto producto)
        {
            int IdNuevo = 0;
            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GestorProducto", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACCION", 2);
                    cmd.Parameters.AddWithValue("@NombreProducto", producto.Nombre);
                    cmd.Parameters.AddWithValue("@PrecioProducto", producto.Precio);
                    cmd.Parameters.AddWithValue("@IDFabricante", producto.Fabricante.Id);

                    var parameter = new SqlParameter("@NuevoID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output,
                    };
                    cmd.Parameters.Add(parameter);
                    cmd.ExecuteNonQuery();
                    IdNuevo = Convert.ToInt32(parameter.Value);
                    conexion.Close();
                }

            }
            catch (Exception e)
            {

            }
            return IdNuevo;
        }

        public void UpdateProducto(Producto producto)
        {
            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GestorProducto", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACCION", 3);
                    cmd.Parameters.AddWithValue("@IdPrducto", producto.IdProducto);
                    cmd.Parameters.AddWithValue("@NombreProducto", producto.Nombre);
                    cmd.Parameters.AddWithValue("@PrecioProducto", producto.Precio);
                    cmd.Parameters.AddWithValue("@IDFabricante", producto.Fabricante.Id);
                    cmd.Parameters.AddWithValue("@NuevoID", 0);
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }

            }
            catch (Exception e)
            {

            }
        }

        public void DeleteProducto(int id)
        {
            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GestorProducto", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACCION", 4);
                    cmd.Parameters.AddWithValue("@IdPrducto", id);
                    cmd.Parameters.AddWithValue("@NuevoID", 0);
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }

            }
            catch (Exception e)
            {

            }
        }


        #endregion


    }
}
