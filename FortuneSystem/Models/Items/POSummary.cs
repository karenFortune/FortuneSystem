﻿using FortuneSystem.Models.Pedidos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FortuneSystem.Models.Items;
using FortuneSystem.Models.Catalogos;
using System.Web.ModelBinding;
using FortuneSystem.Models.Item;

namespace FortuneSystem.Models.POSummary
{

    public class POSummary
    {

        [Display(Name = "Id")]
        public int IdItems { get; set; }

        [Required(ErrorMessage = "Ingrese el estilo del Item.")]
        [Display(Name = "Item")]
        public string EstiloItem { get; set; }
        public virtual ItemDescripcion ItemDescripcion { get; set; }

        public List<ItemDescripcion> ListaItems { get; set; }

        [Required(ErrorMessage = "Ingrese el color")]
        [Display(Name = "Color")]
        public string IdColor { get; set; }
        public virtual CatColores CatColores { get; set; }

        public List<CatColores> ListaColores { get; set; }

        [Required(ErrorMessage = "Ingrese la cantidad total del Item.")]
        [Display(Name = "QTY")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Ingrese el precio del Item.")]
        [Display(Name = "Precio")]
        [RegularExpression("[0-9]\\d{0,9}(\\.\\d{1,3})?%?$", ErrorMessage = "El Precio debe contener sólo números(.35 o 2.5)")]
        //[DisplayFormat(DataFormatString = "{0:#.####}")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public float Precio { get; set; }

        [Display(Name = "No. PO")]
        [Column("ID_PEDIDOS")]
        [ForeignKey("PO_SUMMARY")]
        public virtual int PedidosId { get; set; }
        public virtual OrdenesCompra Pedidos { get; set; }

        [Display(Name = "Género")]
        [Column("ID_GENDER")]
        [ForeignKey("ID_GENDER")]

        public virtual string IdGenero { get; set; }
        public virtual CatGenero CatGenero { get; set; }
        public List<CatGenero> ListaGeneros { get; set; }

        public List<CatGenero> ListarTallasPorGenero { get; set; }

        public virtual ItemTalla ItemTalla { get; set; }
        [Display(Name = "Tela")]
        [Column("ID_TELA")]
        [ForeignKey("ID")]
        public int IdTela { get; set; }
        public List<CatTela> ListaTelas { get; set; }

        [Display(Name = "Tipo Camiseta")]
        public string TipoCamiseta { get; set; }

        public List<CatTipoCamiseta> ListaTipoCamiseta { get; set; }

        public List<CatTallaItem> ListaTallas { get; set; }
        






    }
}