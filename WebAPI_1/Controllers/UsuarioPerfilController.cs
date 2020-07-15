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
    public class UsuarioPerfilController : ApiController
    {
        SqlConnection conectar = new SqlConnection(ConexionDB.connectionString);

        [HttpGet]
        public IHttpActionResult ListaUsuariosPerfil()
        {

            DataTable dt = new DataTable();
            using (conectar)
            {
                conectar.Open();
                string query = "Select * from TBusuarioPerfil";
                SqlDataAdapter sda = new SqlDataAdapter(query, conectar);
                sda.Fill(dt);
            }



            return Ok(dt);

        }

        [HttpGet]
        public IHttpActionResult ConsultarUsuarioPerfilId(int idUsuario)
        {
            UsuarioPerfilViewModel usuarioPerfilViewModel = new UsuarioPerfilViewModel();
            DataTable dt = new DataTable();
            using (conectar)
            {
                conectar.Open();
                string query = "Select * from TBusuarioPerfil Where idUsuario = @idUsuario";
                SqlDataAdapter sda = new SqlDataAdapter(query, conectar);
                sda.SelectCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
                sda.Fill(dt);
            }
            if (dt.Rows.Count == 1)
            {

                usuarioPerfilViewModel.IdUsuario = Convert.ToInt32(dt.Rows[0][0]);
                usuarioPerfilViewModel.NombreUsuario = dt.Rows[0][1].ToString();
                usuarioPerfilViewModel.ApellidoUsuario = dt.Rows[0][2].ToString();
                usuarioPerfilViewModel.Correo = dt.Rows[0][3].ToString();
                usuarioPerfilViewModel.NumCell = dt.Rows[0][4].ToString();
                usuarioPerfilViewModel.FotoUsuario = dt.Rows[0][5].ToString();

                return Ok(usuarioPerfilViewModel);
            }
            else
            {
                return NotFound();
            }

            //return Ok("mm");


        }

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
            if (dt.Rows.Count == 1)
            {

                contactoViewModel.IdGrupoContacto = Convert.ToInt32(dt.Rows[0][0]);
                contactoViewModel.IdUsuario = Convert.ToInt32(dt.Rows[0][1]);
                contactoViewModel.NumCellContacto = dt.Rows[0][2].ToString();
                

                return Ok(contactoViewModel);
            }
            else
            {
                return NotFound();
            }

            //return Ok("mm");


        }



    }
}
