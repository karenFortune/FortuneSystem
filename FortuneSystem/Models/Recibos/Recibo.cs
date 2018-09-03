using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;

namespace FortuneSystem.Models
{
    public class Recibo
    {
        //RECIBO GENERAL
        public string id_po { get; set; }
        public int id_recibo { get; set; }
        public string fecha { get; set; }
        public int usuario { get; set; }
        public int total_recibido { get; set; }
        public int total_pendiente { get; set; }

        //RECIBO POR ESTILO
        public int id_po_summary { get; set; }
        public int id_medida { get; set; }
    }
}