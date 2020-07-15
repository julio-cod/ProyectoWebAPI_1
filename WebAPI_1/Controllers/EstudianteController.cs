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
    public class EstudianteController : ApiController
    {
        SqlConnection conectar = new SqlConnection(ConexionDB.connectionString);


        [HttpGet]
        public IHttpActionResult ListaEstudiante()
        {

            DataTable dt = new DataTable();
            using (conectar)
            {
                conectar.Open();
                string query = "Select * from TBestudiante";
                SqlDataAdapter sda = new SqlDataAdapter(query, conectar);
                sda.Fill(dt);
            }



            return Ok(dt);


        }

        [HttpGet]
        public IHttpActionResult ConsultarEstudianteId(int id)
        {
            EstudianteViewModel estudianteViewModel = new EstudianteViewModel();
            DataTable dt = new DataTable();
            using (conectar)
            {
                conectar.Open();
                string query = "Select * from TBestudiante Where id = @id";
                SqlDataAdapter sda = new SqlDataAdapter(query, conectar);
                sda.SelectCommand.Parameters.AddWithValue("@id", id);
                sda.Fill(dt);
            }
            if (dt.Rows.Count == 1)
            {

                estudianteViewModel.Id = Convert.ToInt32(dt.Rows[0][0]);
                estudianteViewModel.Nombre = dt.Rows[0][1].ToString();
                estudianteViewModel.Matricula = dt.Rows[0][2].ToString();
                estudianteViewModel.Edad = Convert.ToInt32(dt.Rows[0][3]);
                estudianteViewModel.Telefono = dt.Rows[0][4].ToString();
                estudianteViewModel.Direccion = dt.Rows[0][5].ToString();

                return Ok(estudianteViewModel);
            }
            else
            {
                return NotFound();
            }
            
            //return Ok("mm");


        }

        [HttpGet]
        public IHttpActionResult ConsultarEstudianteNombre(string nombre)
        {
            EstudianteViewModel estudianteViewModel = new EstudianteViewModel();
            DataTable dt = new DataTable();
            using (conectar)
            {
                conectar.Open();
                string query = "Select * from TBestudiante Where nombre like '%" + nombre + "%'";
                SqlDataAdapter sda = new SqlDataAdapter(query, conectar);
                //sda.SelectCommand.Parameters.AddWithValue("@nombre", nombre);
                sda.Fill(dt);
            }
            if (dt.Rows.Count == 1)
            {

                estudianteViewModel.Id = Convert.ToInt32(dt.Rows[0][0]);
                estudianteViewModel.Nombre = dt.Rows[0][1].ToString();
                estudianteViewModel.Matricula = dt.Rows[0][2].ToString();
                estudianteViewModel.Edad = Convert.ToInt32(dt.Rows[0][3]);
                estudianteViewModel.Telefono = dt.Rows[0][4].ToString();
                estudianteViewModel.Direccion = dt.Rows[0][5].ToString();

                return Ok(estudianteViewModel);
            }
            else
            {
                return NotFound();
            }

            //return Ok("mm");


        }

        [HttpPost]
        public IHttpActionResult GuardarEstudiante(EstudianteViewModel estudianteViewModel)
        {
            using (conectar)
            {
                if (estudianteViewModel.Nombre == "")
                {
                    return NotFound();

                }
                else
                {
                    conectar.Open();
                    string query = "insert Into TBestudiante Values (@nombre,@matricula,@edad,@telefono,@direccion)";
                    SqlCommand cmd = new SqlCommand(query, conectar);
                    cmd.Parameters.AddWithValue("@nombre", estudianteViewModel.Nombre);
                    cmd.Parameters.AddWithValue("@matricula", estudianteViewModel.Matricula);
                    cmd.Parameters.AddWithValue("@edad", estudianteViewModel.Edad);
                    cmd.Parameters.AddWithValue("@telefono", estudianteViewModel.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", estudianteViewModel.Direccion);
                    cmd.ExecuteNonQuery();
                }

            }

            return Ok("Guardado");
        }



        [HttpPut]
        public IHttpActionResult EditarEstudiante(EstudianteViewModel estudianteViewModel)
        {
            using (conectar)
            {
                conectar.Open();
                string query = "Update TBestudiante Set nombre = @nombre,matricula = @matricula, edad = @edad, telefono = @telefono, direccion = @direccion Where id = @id";
                SqlCommand cmd = new SqlCommand(query, conectar);
                cmd.Parameters.AddWithValue("@id", estudianteViewModel.Id);
                cmd.Parameters.AddWithValue("@nombre", estudianteViewModel.Nombre);
                cmd.Parameters.AddWithValue("@matricula", estudianteViewModel.Matricula);
                cmd.Parameters.AddWithValue("@edad", estudianteViewModel.Edad);
                cmd.Parameters.AddWithValue("@telefono", estudianteViewModel.Telefono);
                cmd.Parameters.AddWithValue("@direccion", estudianteViewModel.Direccion);
                cmd.ExecuteNonQuery();
            }

            return Ok("Actualizado");
        }

        [HttpDelete]
        public IHttpActionResult EliminarEstudiante(int id)
        {
            using (conectar)
            {
                conectar.Open();
                string query = "Delete from TBestudiante Where id = @id";
                SqlCommand cmd = new SqlCommand(query, conectar);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }


            return Ok("Eliminado");
        }
    }
}
