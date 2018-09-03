using FortuneSystem.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Pedidos
{

    public class OrdenesCompra
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
        [ForeignKey("CUSTOMER")]
        [Column("CUSTOMER")]
        public int Cliente { get; set; }

        public virtual CatCliente CatCliente { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre del Cliente Orden.")]
        [Display(Name = "Cliente Orden")]
        [ForeignKey("CUSTOMER_FINAL")]
        [Column("CUSTOMER_FINAL")]
        public int ClienteFinal { get; set; }

        public virtual CatClienteFinal CatClienteFinal { get; set; }

        [Required(ErrorMessage = "Seleccione un estado del PO")]
        [Display(Name = "Estado")]
        [ForeignKey("ID_STATUS")]
        [Column("ID_STATUS")]
        public int IdStatus { get; set; }

        public virtual CatStatus CatStatus { get; set; }


        [Required(ErrorMessage = "Ingrese la fecha de cancelación.")]
        [Display(Name = "Fecha Cancelación")]
        public DateTime FechaCancel { get; set; }

        [Required(ErrorMessage = "Ingrese la fecha de registro.")]
        [Display(Name = "Fecha Registro")]
        public DateTime FechaOrden { get; set; }

        [Required(ErrorMessage = "Ingrese el total de unidades.")]
        [Display(Name = "Total Unidades")]
        public int TotalUnidades { get; set; }

        public List<CatCliente> ListaClientes { get; set; }

        public List<CatClienteFinal> ListaClientesFinal { get; set; }

        public List<CatStatus> ListaCatStatus { get; set; }
    }
}