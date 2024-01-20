using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signa.GrupoOcorrencia.Api1.Domain.Models
{
    public class ExcelModel
    {
        public string Title { get; set; }
        public List<Headers> Headers { get; set; }
        public object Fields { get; set; }
    }

    public class Headers
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string Align { get; set; }
        public bool Sortable { get; set; } = true;
    }

    public static class DiretoriosExcel
    {
        public static string CaminhoFisico { get; set; }
        public static string CaminhoLogico { get; set; }
    }
}