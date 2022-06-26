using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace LenguajesIII.Models
{
    public class Detalles
    {
        public int IdDetalle { get; set; }
        
        public string NombreArticulo { get; set; }
                
        public int CantidadArticulo { get; set; }

        public int IdPedido { get; set; }
        public Pedidos Pedidos { get; set; }

    }
}
