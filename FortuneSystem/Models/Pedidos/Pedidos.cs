using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Pedidos
{
    public partial class Pedidos
    {
        [Display(Name = "Id PO")]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "Ingrese el número de referencia de la orden.")]
        [Display(Name = "Orden Ref.")]
        public string PO { get; set; }

        [Required(ErrorMessage = "Ingrese el número de PO.")]
        [Display(Name = "PO")]
        public int VPO { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre del Cliente.")]
        [Display(Name = "Cliente")]
        public int Cliente { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre del Cliente Orden.")]
        [Display(Name = "Cliente Orden")]
        public int ClienteFinal { get; set; }


        [Required(ErrorMessage = "Ingrese la fecha de cancelación.")]
        [Display(Name = "Fecha Cancelación")]
        public DateTime FechaCancel { get; set; }


        [Required(ErrorMessage = "Ingrese la fecha de registro.")]
        [Display(Name = "Fecha Registro")]
        public DateTime FechaOrden { get; set; }

        [Required(ErrorMessage = "Ingrese el total de unidades.")]
        [Display(Name = "Total Unidades")]
        public int TotalUnidades { get; set; }
    }
}