using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Mozo.Helper.Enu;
using Mozo.Helper.Global;
using Mozo.Model;
using Mozo.Model.Maestro;
using Mozo.MaestroBusiness;

namespace Mozo.ApiMaestro.EndPoint.Report
{
    public static partial class TipoEndPoints
    {       
        public ActionResult XlsAll([FromQuery] TipoModel c)
        {
            TipoModel Tipo = _tipoService.GetByIdGrupo(new TipoModel() { CoGrupo = c.Filter.CoGrupo });

            IEnumerable<TipoModel> TipoLst = _tipoRptService.RptAll(c);

            MemoryStream memoryStream = new MemoryStream();

            SpreadsheetDocument document = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook, true);
            WorkbookPart workbookPart = document.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet();


            Columns columns = new Columns();
            columns.Append(1.CreateColumnData(10));//Orden,	
            columns.Append(2.CreateColumnData(20));//Nombre
            columns.Append(3.CreateColumnData(10)); //Sigla,
            columns.Append(4.CreateColumnData(9));//Estado,
            columns.Append(5.CreateColumnData(25)); //Descripci�n
            columns.Append(6.CreateColumnData(13));	//Valor
            columns.Append(7.CreateColumnData(13));// Color,
            columns.Append(8.CreateColumnData(13)); // Icono
            columns.Append(9.CreateColumnData(10));//Default,
            columns.Append(10.CreateColumnData(18));//Costo total,


            worksheetPart.Worksheet.Append(columns);

            Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
            Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Tipos" };
            sheets.Append(sheet);
            worksheetPart.Worksheet.Save();


            SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());


            WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();
            stylePart.Stylesheet = ExcelXml.VocStyleSheet();
            stylePart.Stylesheet.CellFormats.Count = UInt32Value.FromUInt32((uint)stylePart.Stylesheet.CellFormats.ChildElements.Count);

            stylePart.Stylesheet.Save();



            MergeCells mergeCells = new MergeCells();

            Row row;
            //row = new Row() {  RowIndex = 1 };
            row = new Row();

            row.Append(
                ("TIPOS " + Tipo.NoGrupo + " - " + Tipo.TxDescripcion).ConstructCell(CellValues.String, 3U),
                "".ConstructCell(CellValues.String, 7),
                "".ConstructCell(CellValues.String, 7),
                "".ConstructCell(CellValues.String, 7),
                "".ConstructCell(CellValues.String, 7),
                "".ConstructCell(CellValues.String, 7),
                "".ConstructCell(CellValues.String, 7),
                "".ConstructCell(CellValues.String, 7),
                "".ConstructCell(CellValues.String, 7),
                "".ConstructCell(CellValues.String, 7)
             ); sheetData.AppendChild(row);

            mergeCells.Append(new MergeCell() { Reference = new StringValue("A1:J1") });


            int nuRow = 16;
            worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());

            //mergeCells.Append(new MergeCell() { Reference = new StringValue("A1:T1") });
            row = new Row();
            row.Append(
                "Orden".ConstructCell(CellValues.String, 2),
                "Nombre".ConstructCell(CellValues.String, 2),
                "Sigla".ConstructCell(CellValues.String, 2),
                "Estado".ConstructCell(CellValues.String, 2),
                "Descripción".ConstructCell(CellValues.String, 2),
                "Valor".ConstructCell(CellValues.String, 2),
                "Color".ConstructCell(CellValues.String, 2),
                "Icono".ConstructCell(CellValues.String, 2),
                "Default".ConstructCell(CellValues.String, 2),
                "Cantidad Sub Items".ConstructCell(CellValues.String, 2)
                ); sheetData.AppendChild(row);


            nuRow++;


            foreach (TipoModel item in TipoLst)
            {
                nuRow++;
                row = new Row();
                row.Append(
                   item.NuOrden.Text().ConstructCell(CellValues.Number, 7),
                   item.NoTipo.Text().ConstructCell(CellValues.String, 7),

                   item.NoSigla.Text().ConstructCell(CellValues.String, 7),
                   (item.CoEstReg == 1 ? "Activo" : "Inactivo").ConstructCell(CellValues.String, 7),
                   item.TxDescripcion.ConstructCell(CellValues.String, 7),
                   item.NoValor.ConstructCell(CellValues.String, 7),
                   item.NoColor.ConstructCell(CellValues.String, 7),
                    item.NoIcono.ConstructCell(CellValues.String, 7),

                     (item.FlDefault == 1 ? "Default" : "").ConstructCell(CellValues.String, 7),

                     item.QtHijo.Text().ConstructCell(CellValues.Number, 7)
                );

                sheetData.AppendChild(row);
            }




            //string TxPathLogoEmpresa = EnuTipoGeneral.FormatoArchivo.Seguridad.LogoEmpresa.With(x => { x.NoArchivo = Etapa.NoArchivoEmpresa; x.NoExtension = Etapa.NoExtensionEmpresa; }).GetPathArchivoFile(_configuration);
            //ExcelXml.InsertImage(worksheetPart.Worksheet, 0, 0, 3, 1, TxPathLogoEmpresa);

            worksheetPart.Worksheet.Save();




            //WorkbookPart wbp = doc.WorkbookPart;
            //WorksheetPart wsp = wbp.WorksheetParts.First();

            //SheetViews sheetViews = new SheetViews();

            //SheetViews sheetviews = wsp.Worksheet.GetFirstChild<SheetViews>();
            //SheetView sv = sheetviews.GetFirstChild<SheetView>();
            //Selection selection = sv.GetFirstChild<Selection>();
            //Pane pane = new Pane() { VerticalSplit = 1D, TopLeftCell = "A2", ActivePane = PaneValues.BottomLeft, State = PaneStateValues.Frozen };
            //sv.InsertBefore(pane, selection);
            //selection.Pane = PaneValues.BottomLeft;

            // #region Freeze Panel

            // SheetViews sheetViews = sheet.GetFirstChild<SheetViews>();
            // if (sheetViews == null)
            // {
            //     sheetViews= new SheetViews();

            // }

            // SheetView sheetView = new SheetView();// { TabSelected = true, WorkbookViewId = (UInt32Value)0U };

            //// SheetView sheetView = sheetViews.GetFirstChild<SheetView>();

            // Selection selection1 = new Selection() { Pane = PaneValues.BottomLeft };

            // Pane pane1 = new Pane() { VerticalSplit = 1D, TopLeftCell = "A2", ActivePane = PaneValues.BottomLeft, State = PaneStateValues.Frozen };

            // sheetView.Append(pane1);
            // sheetView.Append(selection1);

            // workbookPart.Workbook.Append(sheetViews);
            // #endregion





            ///*Print Area*/
            //DefinedNames definedNames = new DefinedNames();
            //DefinedName printAreaDefName = new DefinedName() { Name = "_xlnm.Print_Area", LocalSheetId = (UInt32Value)0U };
            //printAreaDefName.Text = "Creditos!$A$1:$F$" + (nuRow + 4);
            //definedNames.Append(printAreaDefName);
            //workbookPart.Workbook.Append(definedNames);
            ///*Print Area*/

            workbookPart.Workbook.Save();
            document.Close();


            string NoArchivoCompleto = "Tipos" + DateTime.Now.ToString("yyyyMMddHHmss") + ".xlsx";


            return File(memoryStream.ToArray(), NoArchivoCompleto.TypeMime(), NoArchivoCompleto);

        }



    }
}
