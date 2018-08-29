
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;



namespace FortuneSystem.Models.DescripcionItem
{
    public partial class DescripcionItem
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
        [Column("ID_PEDIDOS")]
        public int Pedidos { get; set; }
        

    }
}