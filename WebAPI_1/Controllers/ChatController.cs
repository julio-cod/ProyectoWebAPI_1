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
    public class ChatController : ApiController
    {
        SqlConnection conectar = new SqlConnection(ConexionDB.connectionString);

        [HttpGet]
        public IHttpActionResult MostrarMensajes()
        {

            DataTable dt = new DataTable();
            using (conectar)
            {
                conectar.Open();
                string query = "Select * from TBmensaje ORDER BY idMensaje DESC";
                SqlDataAdapter sda = new SqlDataAdapter(query, conectar);
                sda.Fill(dt);
            }



            return Ok(dt);


        }

        [HttpGet]
        public IHttpActionResult MostrarMensajeUser(int idEmisor, int idReceptor)
        {
            MensajeViewModel mensajeViewModel = new MensajeViewModel();
            DataTable dt = new DataTable();
            using (conectar)
            {
                conectar.Open();
                //string query = "Select * from TBmensaje Where idEmisor = @idEmisor and idReceptor = @idReceptor";

                string query = "Select * from TBmensaje Where idEmisor = '" + idEmisor + "'and idReceptor = '" + idReceptor + "' or idEmisor = '" + idReceptor + "'and idReceptor = '" + idEmisor + "'ORDER BY idMensaje DESC";

                SqlDataAdapter sda = new SqlDataAdapter(query, conectar);
                

                sda.Fill(dt);
            }
        

            return Ok(dt);


        }

        [HttpPost]
        public IHttpActionResult EnviarMensajeUser(MensajeViewModel mensajeViewModel)
        {
            using (conectar)
            {
                if (mensajeViewModel.IdReceptor == 0)
                {
                    return NotFound();

                }
                else
                {
                    conectar.Open();
                    string query = "insert Into TBmensaje Values (@IdEmisor,@IdReceptor,@Mensaje,@Fecha)";
                    SqlCommand cmd = new SqlCommand(query, conectar);
                    cmd.Parameters.AddWithValue("@IdEmisor", mensajeViewModel.IdEmisor);
                    cmd.Parameters.AddWithValue("@IdReceptor", mensajeViewModel.IdReceptor);
                    cmd.Parameters.AddWithValue("@Mensaje", mensajeViewModel.Mensaje);
                    cmd.Parameters.AddWithValue("@Fecha", mensajeViewModel.Fecha = DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

            }

            return Ok("Enviado");
        }


    }
}
