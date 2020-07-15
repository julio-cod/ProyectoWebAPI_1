using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_1.Models
{
    public class MensajeViewModel
    {
        public int IdMensaje { get; set; }
        public int IdEmisor { get; set; }
        public int IdReceptor { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }


    }
}