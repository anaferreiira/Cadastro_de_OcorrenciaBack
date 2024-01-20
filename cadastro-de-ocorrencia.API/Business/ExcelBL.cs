using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Signa.GrupoOcorrencia.Api1.Domain.Models;
using Signa.Library.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Signa.GrupoOcorrencia.Api1.Business
{
    public class ExcelBL
    {
        private static readonly Dictionary<string, HorizontalAlignment> align = new Dictionary<string, HorizontalAlignment>()
        {
            { "center", HorizontalAlignment.Center },
            { "left", HorizontalAlignment.Left },
            { "right", HorizontalAlignment.Right },
            { "start", HorizontalAlignment.Left },
            { "end", HorizontalAlignment.Right }
        };
        private List<ICellStyle> cellStyles = new List<ICellStyle>();

        public string GerarExcel(ExcelModel excelData)
        {
            int numLinha = 0;
            int numColuna = 0;
            int numLinhaHeaders = 0;
            int tamanhoPlanilha = excelData.Headers.Count - 1;

            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            if (!excelData.Title.IsNullEmptyOrWhiteSpace())
            {
                EscreverTitulo(workbook, sheet, ref numLinha, ref numLinhaHeaders, tamanhoPlanilha, excelData);
            }

            EscreverCabeçalho(workbook, sheet, ref numLinha, ref numColuna, tamanhoPlanilha, excelData);

            EscreverCorpoPlanilha(workbook, sheet, ref numLinha, excelData);

            RedimensionarPlanilha(workbook, sheet, excelData.Headers, tamanhoPlanilha);

            string urlArquivo = SalvarExcel(workbook);

            return urlArquivo;
        }

        private void EscreverTitulo(IWorkbook workbook, ISheet sheet, ref int numLinha, ref int numLinhaHeaders, int tamanhoPlanilha, ExcelModel dadosPlanilha)
        {
            IRow row = sheet.CreateRow(numLinha);
            row.HeightInPoints = 22;
            ICell cell = row.CreateCell(0);
            cell.SetCellValue(dadosPlanilha.Title);
            row.GetCell(0).CellStyle = DefinirEstiloTitulo(workbook);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, tamanhoPlanilha));

            numLinha += 2;
            numLinhaHeaders = numLinha;
        }

        private void EscreverCabeçalho(IWorkbook workbook, ISheet sheet, ref int numLinha, ref int numColuna, int tamanhoPlanilha, ExcelModel excelData)
        {
            IRow row = sheet.CreateRow(numLinha);

            foreach (var item in excelData.Headers)
            {
                ICell cell = row.CreateCell(numColuna);
                cell.SetCellValue(item.Text);
                row.GetCell(numColuna).CellStyle = DefinirEstiloHeader(workbook);
                cellStyles.Add(DefinirEstiloCorpoPlanilha(workbook, item.Align));
                numColuna++;
            }

            numLinha++;
        }

        private void EscreverCorpoPlanilha(IWorkbook workbook, ISheet sheet, ref int numLinha, ExcelModel dadosPlanilha)
        {
            var deserialized = DeserializarJsonEntrada(dadosPlanilha);

            foreach (var dictionary in deserialized)
            {
                int numColuna = 0;
                IRow row = sheet.CreateRow(numLinha);
                string text = "";

                foreach (var key in dadosPlanilha.Headers)
                {
                    if (!dictionary.TryGetValue(key.Value, out text))
                    {
                        numColuna++;
                        continue;
                    }

                    ICell cell = row.CreateCell(numColuna);
                    cell.SetCellType(CellType.String);
                    cell.SetCellValue(text);
                    row.GetCell(numColuna).CellStyle = cellStyles[numColuna];
                    numColuna++;
                }
                numLinha++;
            }
        }

        private void RedimensionarPlanilha(IWorkbook workbook, ISheet sheet, List<Headers> headers, int tamanhoPlanilha)
        {
            for (int i = 0; i <= tamanhoPlanilha; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }

        private static string SalvarExcel(IWorkbook workbook)
        {
            string nomeArquivo = $"Relatorio-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

            if (!Directory.Exists(DiretoriosExcel.CaminhoFisico))
            {
                Directory.CreateDirectory(DiretoriosExcel.CaminhoFisico);
            }

            string caminhoFisicoCompleto = Path.Combine(DiretoriosExcel.CaminhoFisico, nomeArquivo);

            using (FileStream stream = new FileStream(caminhoFisicoCompleto, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(stream);
            }

            string caminhoLogicoCompleto = Path.Combine(DiretoriosExcel.CaminhoLogico, nomeArquivo);

            return caminhoLogicoCompleto;
        }

        internal dynamic DeserializarJsonEntrada(ExcelModel entrada)
        {
            var arr = entrada.Fields.ToString();
            dynamic deserialized = JsonConvert.DeserializeObject<IEnumerable<IDictionary<string, string>>>(arr);

            return deserialized;
        }

        private ICellStyle DefinirEstiloCorpoPlanilha(IWorkbook workbook, string alignment)
        {
            var alignDefault = HorizontalAlignment.Center;
            if (!alignment.IsNullEmptyOrWhiteSpace() && align.ContainsKey(alignment))
            {
                alignDefault = align[alignment];
            }

            var cellStyle = workbook.CreateCellStyle();
            cellStyle.Alignment = alignDefault;

            return cellStyle;
        }

        private ICellStyle DefinirEstiloHeader(IWorkbook workbook)
        {
            IFont headersFont = workbook.CreateFont();
            headersFont.IsBold = true;

            ICellStyle headersStyle = workbook.CreateCellStyle();
            headersStyle.FillForegroundColor = IndexedColors.PaleBlue.Index;
            headersStyle.FillPattern = FillPattern.SolidForeground;
            headersStyle.Alignment = HorizontalAlignment.Center;
            headersStyle.SetFont(headersFont);

            return headersStyle;
        }

        private ICellStyle DefinirEstiloTitulo(IWorkbook workbook)
        {
            IFont fontSub = workbook.CreateFont();
            fontSub.IsBold = true;
            fontSub.FontHeightInPoints = 14;

            ICellStyle titleStyle = workbook.CreateCellStyle();
            titleStyle.FillForegroundColor = IndexedColors.PaleBlue.Index;
            titleStyle.FillPattern = FillPattern.SolidForeground;
            titleStyle.Alignment = HorizontalAlignment.Center;
            titleStyle.VerticalAlignment = VerticalAlignment.Center;
            titleStyle.SetFont(fontSub);

            return titleStyle;
        }
    }
}
