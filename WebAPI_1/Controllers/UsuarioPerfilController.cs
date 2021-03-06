﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
                //operacion image
                FileStream stream = new FileStream(dt.Rows[0][5].ToString(), FileMode.Open, FileAccess.Read);

                //Se inicializa un arreglo de Bytes del tamaño de la imagen
                byte[] binData = new byte[stream.Length];

                usuarioPerfilViewModel.IdUsuario = Convert.ToInt32(dt.Rows[0][0]);
                usuarioPerfilViewModel.NombreUsuario = dt.Rows[0][1].ToString();
                usuarioPerfilViewModel.ApellidoUsuario = dt.Rows[0][2].ToString();
                usuarioPerfilViewModel.Correo = dt.Rows[0][3].ToString();
                usuarioPerfilViewModel.NumCell = dt.Rows[0][4].ToString();
                //usuarioPerfilViewModel.FotoUsuario = dt.Rows[0][5].ToString();

                usuarioPerfilViewModel.FotoUsuario = binData;

                return Ok(usuarioPerfilViewModel);
            }
            else
            {
                return NotFound();
            }

            //return Ok("mm");


        }

        [HttpGet]
        public IHttpActionResult ConsultarUsuarioPerfilNumeCell(string numCell)
        {
            UsuarioPerfilViewModel usuarioPerfilViewModel = new UsuarioPerfilViewModel();
            DataTable dt = new DataTable();
            using (conectar)
            {
                conectar.Open();
                string query = "Select * from TBusuarioPerfil Where numCell = @numCell";
                SqlDataAdapter sda = new SqlDataAdapter(query, conectar);
                sda.SelectCommand.Parameters.AddWithValue("@numCell", numCell);
                sda.Fill(dt);
            }


            return Ok(dt);


        }


        [HttpPost]
        public IHttpActionResult RegistrarUsuariosPerfil(UsuarioPerfilViewModel usuarioPerfilViewModel)
        {
            using (conectar)
            {
                if (usuarioPerfilViewModel.NombreUsuario == "")
                {
                    return NotFound();
                    
                }
                else
                {
                    conectar.Open();
                    string query = "insert Into TBusuarioPerfil Values (@nombreUsuario,@apellidoUsuario,@correo,@numCell,@fotoUsuario)";
                    SqlCommand cmd = new SqlCommand(query, conectar);
                    cmd.Parameters.AddWithValue("@nombreUsuario", usuarioPerfilViewModel.NombreUsuario);
                    cmd.Parameters.AddWithValue("@apellidoUsuario", usuarioPerfilViewModel.ApellidoUsuario);
                    cmd.Parameters.AddWithValue("@correo", usuarioPerfilViewModel.Correo);
                    cmd.Parameters.AddWithValue("@numCell", usuarioPerfilViewModel.NumCell);
                    cmd.Parameters.AddWithValue("@fotoUsuario", usuarioPerfilViewModel.FotoUsuario);
                    cmd.ExecuteNonQuery();
                }

            }

            return Ok("Usuario Guardado");
        }

        [HttpPut]
        public IHttpActionResult EditarUsuariosPerfil(UsuarioPerfilViewModel usuarioPerfilViewModel)
        {
            using (conectar)
            {
                conectar.Open();
                string query = "Update TBusuarioPerfil Set nombreUsuario = @nombreUsuario,apellidoUsuario = @apellidoUsuario, correo = @correo, numCell = @numCell, fotoUsuario = @fotoUsuario Where idUsuario = @idUsuario";
                SqlCommand cmd = new SqlCommand(query, conectar);
                cmd.Parameters.AddWithValue("@idUsuario", usuarioPerfilViewModel.IdUsuario);
                cmd.Parameters.AddWithValue("@nombreUsuario", usuarioPerfilViewModel.NombreUsuario);
                cmd.Parameters.AddWithValue("@apellidoUsuario", usuarioPerfilViewModel.ApellidoUsuario);
                cmd.Parameters.AddWithValue("@correo", usuarioPerfilViewModel.Correo);
                cmd.Parameters.AddWithValue("@numCell", usuarioPerfilViewModel.NumCell);
                cmd.Parameters.AddWithValue("@fotoUsuario", usuarioPerfilViewModel.FotoUsuario);
                cmd.ExecuteNonQuery();
            }

            return Ok("Actualizado");
        }



        
    }
}
