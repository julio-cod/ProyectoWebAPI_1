using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_1.Models;

namespace WebAPI_1.Controllers
{
    public class GrupoContactoController : ApiController
    {
        SqlConnection conectar = new SqlConnection(ConexionDB.connectionString);

        [HttpGet]
        public IHttpActionResult ListaContactos(int idUsuario)
        {
            ContactoViewModel contactoViewModel = new ContactoViewModel();
            DataTable dt = new DataTable();
            using (conectar)
            {
                conectar.Open();
                string query = "Select * from TBGrupoContacto Where idUsuario = @idUsuario";
                SqlDataAdapter sda = new SqlDataAdapter(query, conectar);
                sda.SelectCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
                sda.Fill(dt);
            }


            return Ok(dt);


        }

        [HttpPost]
        public IHttpActionResult RegistrarContacto(ContactoViewModel contactoViewModel)
        {
            using (conectar)
            {
                if (contactoViewModel.IdUsuario.ToString() == "")
                {
                    return NotFound();

                }
                else
                {
                    conectar.Open();
                    string query = "insert Into TBGrupoContacto Values (@idUsuario,@numCellContacto,@idUsuarioReceptor,@nombreUsuario,@apellidoUsuario,@correo,@fotoUsuario)";
                    SqlCommand cmd = new SqlCommand(query, conectar);
                    cmd.Parameters.AddWithValue("@idUsuario", contactoViewModel.IdUsuario);
                    cmd.Parameters.AddWithValue("@numCellContacto", contactoViewModel.NumCellContacto);
                    cmd.Parameters.AddWithValue("@idUsuarioReceptor", contactoViewModel.IdUsuarioReceptor);
                    cmd.Parameters.AddWithValue("@nombreUsuario", contactoViewModel.NombreUsuario);
                    cmd.Parameters.AddWithValue("@apellidoUsuario", contactoViewModel.ApellidoUsuario);
                    cmd.Parameters.AddWithValue("@correo", contactoViewModel.Correo);
                    cmd.Parameters.AddWithValue("@fotoUsuario", contactoViewModel.FotoUsuario);
                    cmd.ExecuteNonQuery();
                }

            }

            return Ok("Contacto Guardado");
        }

        [HttpDelete]
        public IHttpActionResult EliminarContacto(int IdGrupoContacto)
        {
            using (conectar)
            {
                conectar.Open();
                string query = "Delete from TBGrupoContacto Where IdGrupoContacto = @IdGrupoContacto";
                SqlCommand cmd = new SqlCommand(query, conectar);
                cmd.Parameters.AddWithValue("@IdGrupoContacto", IdGrupoContacto);
                cmd.ExecuteNonQuery();
            }


            return Ok("Contacto Eliminado");
        }

    }
}
