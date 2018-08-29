using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.DescripcionItem
{
    public class DescripcionItem
    {
        [Display(Name = "Id")]
        public int IdItems { get; set; }

        [Required(ErrorMessage = "Ingrese el estilo del Item.")]
        [Display(Name = "Item")]
        public string EstiloItem { get; set; }

        [Required(ErrorMessage = "Ingrese la descripción del Item.")]
        [Display(Name = "Color")]
        public int IdColor { get; set; }

        [Required(ErrorMessage = "Ingrese la cantidad total del Item.")]
        [Display(Name = "QTY")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Ingrese el precio del Item.")]
        [Display(Name = "Precio.")]
        public float Precio { get; set; }

        [Display(Name = "No. PO")]
        [Column(Storage = "ID_PEDIDOS")]
        public Pedido pedido = new Pedido();

    }
}