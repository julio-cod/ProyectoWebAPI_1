using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_1.Models
{
    public class ContactoViewModel
    {
        public int IdGrupoContacto { get; set; }

        public int IdUsuario { get; set; }

        public string NumCellContacto { get; set; }

        public int IdUsuarioReceptor { get; set; }

        public string NombreUsuario { get; set; }

        public string ApellidoUsuario { get; set; }

        public string Correo { get; set; }

        public byte[] FotoUsuario { get; set; }

    }
}