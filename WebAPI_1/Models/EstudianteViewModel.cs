using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_1.Models
{
    public class EstudianteViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Matricula { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }


    }
}