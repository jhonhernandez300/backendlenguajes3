using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace LenguajesIII.Models
{
    public class Pedidos
    {
        public int IdPedido { get; set; }

        //[Required(ErrorMessage = "Campo requerido")]
        public DateTime FechaPedido { get; set; }

        //[Required(ErrorMessage = "Campo requerido")]
        [DataType(DataType.Text, ErrorMessage = "Debe ser un texto")]
        public string NombreCliente { get; set; }     


        //[Required(ErrorMessage = "Campo requerido")]
        [DataType(DataType.Text, ErrorMessage = "Debe ser un texto")]
        public string DireccionCliente { get; set; }      

        public string TelefonoCliente { get; set; }

        public ICollection<Detalles> Detalle { get; set; }

    }
}
