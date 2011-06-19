using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;

using GeCo.Infrastructure;

namespace GeCo.BLL.Excel
{
    public class ExcelModule
    {
        #region RELATION ID
        
        private string rIdSummary = "rId1";
        private string rIdHrDiscrezionali = "rId2";
        private string rIdHrComportamentali = "rId3";
        private string rIdComportamentali = "rId4";
        private string rIdTecniche = "rId5";

        private string rIdSummaryDrawing = "rId2";
        private string rIdSummaryChart = "rId1";

        #endregion

        public string NomeRuolo { get; set; }
        public List<ConfrontoConoscenzaCompetenza> Dati { get; set; }

        public ParametriConfronto PunteggiMassimi { get; set; }

        
        public void CreaExcel()
        {
            //Validazione dati
            if (string.IsNullOrEmpty(NomeRuolo))
                return;

            if (Dati == null)
                return;


            string filepath = "D:\\" + DateTime.Now.Ticks + ".xlsx";
            using (SpreadsheetDocument spreadsheet = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook))
            {
                GeneraWorkbookPart(spreadsheet);

                spreadsheet.Close();
            }

        }

        private void GeneraWorkbookPart(SpreadsheetDocument document)
        {
            ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
            GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

            WorkbookPart workbookPart = document.AddWorkbookPart();
            GenerateWorkbookPartContent(workbookPart);



            SharedStringTablePart sharedStringTablePart1 = workbookPart.AddNewPart<SharedStringTablePart>("rId8");
            GenerateSharedStringTablePart1Content(sharedStringTablePart1);

            UInt32 rigaAttesoStrategic, rigaAttesoCompetitive;
            CreaFoglioGenerico(workbookPart, rIdHrDiscrezionali, "COMPETENZE HR DISCREZIONALI", Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE);
            CreaFoglioGenerico(workbookPart, rIdHrComportamentali, "COMPETENZE HR COMPORTAMENTALI", Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE);
            CreaFoglioGenerico(workbookPart, rIdComportamentali, "COMPETENZE COMPORTAMENTALI", Tipologiche.Macrogruppi.MG_COMPORTAMENTALE);
            CreaFoglioTecniche(workbookPart, out rigaAttesoStrategic, out rigaAttesoCompetitive);
            CreaFoglioSummary(workbookPart, rigaAttesoStrategic, rigaAttesoCompetitive);

            WorkbookStylesPart workbookStylesPart1 = workbookPart.AddNewPart<WorkbookStylesPart>("rId7");
            GenerateWorkbookStylesPart1Content(workbookStylesPart1);

            ThemePart themePart1 = workbookPart.AddNewPart<ThemePart>("rId6");
            GenerateThemePart1Content(themePart1);

            workbookPart.Workbook.Save();
        }

        private void GenerateWorkbookPartContent(WorkbookPart workbookPart)
        {
            Workbook workbook = new Workbook();
            workbook.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            FileVersion fileVersion1 = new FileVersion() { ApplicationName = "xl", LastEdited = "4", LowestEdited = "4", BuildVersion = "4505" };
            WorkbookProperties workbookProperties1 = new WorkbookProperties() { DefaultThemeVersion = (UInt32Value)124226U };

            BookViews bookViews1 = new BookViews();
            WorkbookView workbookView1 = new WorkbookView() { XWindow = -180, YWindow = 45, WindowWidth = (UInt32Value)9885U, WindowHeight = (UInt32Value)8190U };

            bookViews1.Append(workbookView1);

            Sheets sheets1 = new Sheets();
            Sheet sheet1 = new Sheet() { Name = "Summary", SheetId = (UInt32Value)1U, Id = rIdSummary };
            Sheet sheet2 = new Sheet() { Name = "HR Discrezionali", SheetId = (UInt32Value)2U, Id = rIdHrDiscrezionali };
            Sheet sheet3 = new Sheet() { Name = "HR Comportamentali", SheetId = (UInt32Value)3U, Id = rIdHrComportamentali };
            Sheet sheet4 = new Sheet() { Name = "Competenze Comportamentali", SheetId = (UInt32Value)4U, Id = rIdComportamentali };
            Sheet sheet5 = new Sheet() { Name = "Conoscenze Tecniche", SheetId = (UInt32Value)5U, Id = rIdTecniche };

            sheets1.Append(sheet1);
            sheets1.Append(sheet2);
            sheets1.Append(sheet3);
            sheets1.Append(sheet4);
            sheets1.Append(sheet5);
            CalculationProperties calculationProperties1 = new CalculationProperties() { CalculationId = (UInt32Value)124519U };

            workbook.Append(fileVersion1);
            workbook.Append(workbookProperties1);
            workbook.Append(bookViews1);
            workbook.Append(sheets1);
            workbook.Append(calculationProperties1);

            workbookPart.Workbook = workbook;
        }

        private void GenerateExtendedFilePropertiesPart1Content(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
        {
            Ap.Properties properties1 = new Ap.Properties();
            properties1.AddNamespaceDeclaration("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
            Ap.Application application1 = new Ap.Application();
            application1.Text = "Microsoft Excel";
            Ap.DocumentSecurity documentSecurity1 = new Ap.DocumentSecurity();
            documentSecurity1.Text = "0";
            Ap.ScaleCrop scaleCrop1 = new Ap.ScaleCrop();
            scaleCrop1.Text = "false";

            Ap.HeadingPairs headingPairs1 = new Ap.HeadingPairs();

            Vt.VTVector vTVector1 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Variant, Size = (UInt32Value)2U };

            Vt.Variant variant1 = new Vt.Variant();
            Vt.VTLPSTR vTLPSTR1 = new Vt.VTLPSTR();
            vTLPSTR1.Text = "Fogli di lavoro";

            variant1.Append(vTLPSTR1);

            Vt.Variant variant2 = new Vt.Variant();
            Vt.VTInt32 vTInt321 = new Vt.VTInt32();
            vTInt321.Text = "4";

            variant2.Append(vTInt321);

            vTVector1.Append(variant1);
            vTVector1.Append(variant2);

            headingPairs1.Append(vTVector1);

            Ap.TitlesOfParts titlesOfParts1 = new Ap.TitlesOfParts();

            Vt.VTVector vTVector2 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Lpstr, Size = (UInt32Value)4U };
            Vt.VTLPSTR vTLPSTR2 = new Vt.VTLPSTR();
            vTLPSTR2.Text = "Summary";
            Vt.VTLPSTR vTLPSTR3 = new Vt.VTLPSTR();
            vTLPSTR3.Text = "HR";
            Vt.VTLPSTR vTLPSTR4 = new Vt.VTLPSTR();
            vTLPSTR4.Text = "Competenze Comportamentali";
            Vt.VTLPSTR vTLPSTR5 = new Vt.VTLPSTR();
            vTLPSTR5.Text = "Conoscenze Tecniche";

            vTVector2.Append(vTLPSTR2);
            vTVector2.Append(vTLPSTR3);
            vTVector2.Append(vTLPSTR4);
            vTVector2.Append(vTLPSTR5);

            titlesOfParts1.Append(vTVector2);
            Ap.LinksUpToDate linksUpToDate1 = new Ap.LinksUpToDate();
            linksUpToDate1.Text = "false";
            Ap.SharedDocument sharedDocument1 = new Ap.SharedDocument();
            sharedDocument1.Text = "false";
            Ap.HyperlinksChanged hyperlinksChanged1 = new Ap.HyperlinksChanged();
            hyperlinksChanged1.Text = "false";
            Ap.ApplicationVersion applicationVersion1 = new Ap.ApplicationVersion();
            applicationVersion1.Text = "12.0000";

            properties1.Append(application1);
            properties1.Append(documentSecurity1);
            properties1.Append(scaleCrop1);
            properties1.Append(headingPairs1);
            properties1.Append(titlesOfParts1);
            properties1.Append(linksUpToDate1);
            properties1.Append(sharedDocument1);
            properties1.Append(hyperlinksChanged1);
            properties1.Append(applicationVersion1);

            extendedFilePropertiesPart1.Properties = properties1;
        }

        #region FUNZIONI GENERICHE

        private Row CreaRigaTipoCompetenza(UInt32 rowIndex, string nome)
        {

            Row row = new Row() { RowIndex = (UInt32Value)rowIndex, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            Cell cell22 = new Cell() { CellReference = "B" + rowIndex, StyleIndex = (UInt32Value)6U, DataType = CellValues.String };
            CellValue cellValue8 = new CellValue();
            cellValue8.Text = nome;
            cell22.Append(cellValue8);
            row.Append(cell22);

            //PRIMO BLOCCO
            int startColumn = 3;
            for (int i = 0; i < 4; i++)
            {
                string columnName = ExcelHelper.GetColumnName(startColumn + i);

                Cell cell23 = new Cell() { CellReference = columnName + rowIndex, StyleIndex = (UInt32Value)8U };
                CellValue cellValue9 = new CellValue();
                cellValue9.Text = (i + 1).ToString();

                cell23.Append(cellValue9);
                row.Append(cell23);
            }


            startColumn = 9;
            for (int i = 0; i < 4; i++)
            {
                string columnName = ExcelHelper.GetColumnName(startColumn + i);

                Cell cell23 = new Cell() { CellReference = columnName + rowIndex, StyleIndex = (UInt32Value)8U };
                CellValue cellValue9 = new CellValue();
                cellValue9.Text = (i + 1).ToString();

                cell23.Append(cellValue9);
                row.Append(cell23);
            }


            return row;
        }

        private Row CreaRigaCompetenza(UInt32 rowIndex, string nome, int valoreAtteso, int valoreOsservato)
        {

            string[] nomeColonnePrimoBlocco = new string[] { "C", "D", "E", "F" };
            string[] nomeColonneSecondoBlocco = new string[] { "I", "J", "K", "L" };

            Row row6 = new Row() { RowIndex = (UInt32Value)rowIndex, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            //NOME COMPETENZA
            Cell cell31 = new Cell() { CellReference = "B" + rowIndex, StyleIndex = (UInt32Value)5U, DataType = CellValues.String };
            CellValue cellValue17 = new CellValue();
            cellValue17.Text = nome;
            cell31.Append(cellValue17);

            row6.Append(cell31);

            //Primo blocco
            for (int i = 0; i < nomeColonnePrimoBlocco.Count(); i++)
            {
                string colonna = nomeColonnePrimoBlocco[i];

                Cell cell = new Cell();
                cell.CellReference = colonna + rowIndex;
                cell.StyleIndex = (UInt32Value)11U;

                if (i == valoreAtteso - 1)
                {
                    cell.DataType = CellValues.SharedString;
                    CellValue cellValue = new CellValue();
                    cellValue.Text = "1";
                    cell.Append(cellValue);
                }

                row6.Append(cell);
            }


            //Secondo blocco
            for (int i = 0; i < nomeColonneSecondoBlocco.Count(); i++)
            {
                string colonna = nomeColonneSecondoBlocco[i];

                Cell cell = new Cell();
                cell.CellReference = colonna + rowIndex;
                cell.StyleIndex = (UInt32Value)11U;

                if (i == valoreOsservato - 1)
                {
                    cell.DataType = CellValues.SharedString;
                    CellValue cellValue = new CellValue();
                    cellValue.Text = "1";
                    cell.Append(cellValue);
                }


                row6.Append(cell);
            }





            Cell cell40 = new Cell() { CellReference = "M" + rowIndex, StyleIndex = (UInt32Value)16U, DataType = CellValues.String };
            CellFormula cellFormula3 = new CellFormula();
            cellFormula3.Text = "IF((COUNTIF(I6:L6,\"x\"))>1,\"check\",\"\")";
            CellValue cellValue20 = new CellValue();
            cellValue20.Text = "";

            cell40.Append(cellFormula3);
            cell40.Append(cellValue20);



            row6.Append(cell40);



            return row6;
        }

        #region FUNZIONI TOTALI

        private Row CreaRigaSubTotali(UInt32 rowIndex, UInt32 rowInizioDati, UInt32 rowFineDati)
        {
            Row row10 = new Row() { RowIndex = (UInt32Value)rowIndex, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            Cell cell71 = new Cell() { CellReference = "B" + rowIndex, StyleIndex = (UInt32Value)13U, DataType = CellValues.SharedString };
            CellValue cellValue30 = new CellValue();
            cellValue30.Text = "2";

            cell71.Append(cellValue30);


            string formulaTemplate = "COUNTIF({0}{1}:{2}{3},\"x\")*{4}";

            int startColumn = 3;//Colonna di partenza per i subtotali (C)

            //Stampo subtotali primo blocco
            for (int i = 0; i < 4; i++)
            {
                string columnName = ExcelHelper.GetColumnName(startColumn + i);
                Cell cell72 = new Cell() { CellReference = columnName + rowIndex, StyleIndex = (UInt32Value)14U };

                CellFormula cellFormula7 = new CellFormula();
                string formula = string.Format(formulaTemplate, columnName, rowInizioDati, columnName, rowFineDati, i + 1);
                cellFormula7.Text = formula;
                //CellValue cellValue31 = new CellValue();
                //cellValue31.Text = "0";

                cell72.Append(cellFormula7);
                //cell72.Append(cellValue31);

                row10.Append(cell72);
            }

            startColumn = 9;//Colonna di partenza per i subtotali (C)

            //Stampo subtotali secondo blocco
            for (int i = 0; i < 4; i++)
            {
                string columnName = ExcelHelper.GetColumnName(startColumn + i);
                Cell cell72 = new Cell() { CellReference = columnName + rowIndex, StyleIndex = (UInt32Value)14U };

                CellFormula cellFormula7 = new CellFormula();
                string formula = string.Format(formulaTemplate, columnName, rowInizioDati, columnName, rowFineDati, i + 1);
                cellFormula7.Text = formula;
                //CellValue cellValue31 = new CellValue();
                //cellValue31.Text = "0";

                cell72.Append(cellFormula7);
                //cell72.Append(cellValue31);

                row10.Append(cell72);
            }

            return row10;
        }

        private Row CreaRigaPunteggioAtteso(UInt32 rowIndex)
        {
            string formulaTemplate = "SUM(C{0}:F{1})";

            Row row11 = new Row() { RowIndex = (UInt32Value)rowIndex, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            //Stringa PUNTEGGIO ATTESO
            Cell cell80 = new Cell() { CellReference = "B" + rowIndex, StyleIndex = (UInt32Value)12U, DataType = CellValues.SharedString };
            CellValue cellValue39 = new CellValue();
            cellValue39.Text = "32";
            cell80.Append(cellValue39);

            Cell cell81 = new Cell() { CellReference = "C" + rowIndex, StyleIndex = (UInt32Value)19U };
            CellFormula cellFormula15 = new CellFormula();
            cellFormula15.Text = string.Format(formulaTemplate, rowIndex - 1, rowIndex - 1);
            cell81.Append(cellFormula15);


            row11.Append(cell80);
            row11.Append(cell81);

            return row11;
        }

        private Row CreaRigaPunteggioOsservato(UInt32 rowIndex)
        {
            string formulaTemplate = "SUM(I{0}:L{1})";

            Row row12 = new Row() { RowIndex = (UInt32Value)rowIndex, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            Cell cell83 = new Cell() { CellReference = "B" + rowIndex, StyleIndex = (UInt32Value)9U, DataType = CellValues.SharedString };
            CellValue cellValue41 = new CellValue();
            cellValue41.Text = "7";

            cell83.Append(cellValue41);

            //Fa solo la copia della cella accanto (ho spostato tutto)

            Cell cell85 = new Cell() { CellReference = "C" + rowIndex, StyleIndex = (UInt32Value)7U };
            CellFormula cellFormula17 = new CellFormula();
            cellFormula17.Text = string.Format(formulaTemplate, rowIndex - 2, rowIndex - 2);

            cell85.Append(cellFormula17);

            row12.Append(cell83);
            row12.Append(cell85);

            return row12;
        }

        #endregion

        #region FUNZIONI TOTALI PER FOUNDATIONALI

        private Row CreaRigaPunteggioAttesoMinimo(UInt32 riga)
        {
            int percentuale = 70;

            Row row83 = new Row() { RowIndex = (UInt32Value)riga, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            Cell cell525 = new Cell() { CellReference = "B" + riga, StyleIndex = (UInt32Value)12U, DataType = CellValues.String };
            CellValue cellValue230 = new CellValue();
            cellValue230.Text = string.Format("Punteggio Atteso Minimo ({0}%)", percentuale);
            cell525.Append(cellValue230);

            Cell cell526 = new Cell() { CellReference = "C" + riga, StyleIndex = (UInt32Value)19U };
            CellFormula cellFormula90 = new CellFormula();
            cellFormula90.Text = string.Format("C{0}*{1}%", riga - 2, percentuale);
            cell526.Append(cellFormula90);

            row83.Append(cell525);
            row83.Append(cell526);

            return row83;
        }

        private Row CreaRigaIdoneita(UInt32 riga)
        {
            Row row84 = new Row() { RowIndex = (UInt32Value)riga, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            Cell cell528 = new Cell() { CellReference = "B" + riga, StyleIndex = (UInt32Value)20U, DataType = CellValues.SharedString };
            CellValue cellValue232 = new CellValue();
            cellValue232.Text = "8";
            cell528.Append(cellValue232);

            Cell cell529 = new Cell() { CellReference = "C" + riga, StyleIndex = (UInt32Value)46U };
            CellFormula cellFormula91 = new CellFormula();
            //Attenzione sul foglio tecnico sono invertiti
            cellFormula91.Text = string.Format("C{0}/C{1}", riga - 2, riga - 3);
            cell529.Append(cellFormula91);


            Cell cell530 = new Cell() { CellReference = "E" + riga, StyleIndex = (UInt32Value)47U, DataType = CellValues.String };
            CellFormula cellFormula92 = new CellFormula();
            cellFormula92.Text = string.Format("IF(C{0}>C{0},\"Idoneo\",\"Non Idoneo\")", riga - 3, riga - 1);
            cell530.Append(cellFormula92);


            row84.Append(cell528);
            row84.Append(cell529);
            row84.Append(cell530);

            return row84;
        }

        #endregion


        #endregion


        #region CREAZIONE FOGLI

        private void CreaFoglioSummary(WorkbookPart workbookPart, UInt32 rowAttesoStrategic, UInt32 rowAttesoCompetitive)
        {
            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>(rIdSummary);

            DrawingsPart drawingsPart1 = worksheetPart.AddNewPart<DrawingsPart>(rIdSummaryDrawing);
            GenerateDrawingsPart1Content(drawingsPart1);

            ChartPart chartPart1 = drawingsPart1.AddNewPart<ChartPart>(rIdSummaryChart);
            GenerateChartPart1Content(chartPart1);

            CreaContenutoFoglioSummary(worksheetPart, rowAttesoStrategic, rowAttesoCompetitive);
        }

        private void CreaContenutoFoglioSummary(WorksheetPart worksheetPart, UInt32 rowAttesoStrategic, UInt32 rowAttesoCompetitive)
        {


            Worksheet worksheet1 = new Worksheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            worksheet1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            worksheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            worksheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");
            SheetDimension sheetDimension1 = new SheetDimension() { Reference = "A1:O22" };

            SheetViews sheetViews1 = new SheetViews();

            SheetView sheetView1 = new SheetView() { TabSelected = true, ZoomScale = (UInt32Value)60U, ZoomScaleNormal = (UInt32Value)60U, WorkbookViewId = (UInt32Value)0U };
            sheetViews1.Append(sheetView1);

            SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties() { DefaultRowHeight = 15D, DyDescent = 0.25D };

            Columns columns1 = new Columns();
            Column column1 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 28.42578125D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column2 = new Column() { Min = (UInt32Value)2U, Max = (UInt32Value)2U, Width = 22.85546875D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column3 = new Column() { Min = (UInt32Value)3U, Max = (UInt32Value)3U, Width = 24.140625D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column4 = new Column() { Min = (UInt32Value)4U, Max = (UInt32Value)4U, Width = 25.85546875D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column5 = new Column() { Min = (UInt32Value)5U, Max = (UInt32Value)5U, Width = 33D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column6 = new Column() { Min = (UInt32Value)6U, Max = (UInt32Value)6U, Width = 35.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column7 = new Column() { Min = (UInt32Value)7U, Max = (UInt32Value)7U, Width = 5.85546875D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column8 = new Column() { Min = (UInt32Value)8U, Max = (UInt32Value)8U, Width = 16.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column9 = new Column() { Min = (UInt32Value)9U, Max = (UInt32Value)9U, Width = 26D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column10 = new Column() { Min = (UInt32Value)10U, Max = (UInt32Value)10U, Width = 4.5703125D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column11 = new Column() { Min = (UInt32Value)11U, Max = (UInt32Value)11U, Width = 17.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column12 = new Column() { Min = (UInt32Value)12U, Max = (UInt32Value)12U, Width = 15.28515625D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column13 = new Column() { Min = (UInt32Value)13U, Max = (UInt32Value)13U, Width = 13.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column14 = new Column() { Min = (UInt32Value)14U, Max = (UInt32Value)14U, Width = 12.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column15 = new Column() { Min = (UInt32Value)15U, Max = (UInt32Value)16384U, Width = 9.140625D, Style = (UInt32Value)1U };

            columns1.Append(column1);
            columns1.Append(column2);
            columns1.Append(column3);
            columns1.Append(column4);
            columns1.Append(column5);
            columns1.Append(column6);
            columns1.Append(column7);
            columns1.Append(column8);
            columns1.Append(column9);
            columns1.Append(column10);
            columns1.Append(column11);
            columns1.Append(column12);
            columns1.Append(column13);
            columns1.Append(column14);
            columns1.Append(column15);

            SheetData sheetData1 = new SheetData();

            Row row1 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:15" }, Height = 15D, CustomHeight = true, DyDescent = 0.25D };

            Cell cell1 = new Cell() { CellReference = "A1", StyleIndex = (UInt32Value)70U, DataType = CellValues.String };
            CellValue cellValue1 = new CellValue();
            cellValue1.Text = NomeRuolo;

            cell1.Append(cellValue1);
            Cell cell2 = new Cell() { CellReference = "B1", StyleIndex = (UInt32Value)70U };
            Cell cell3 = new Cell() { CellReference = "C1", StyleIndex = (UInt32Value)70U };
            Cell cell4 = new Cell() { CellReference = "D1", StyleIndex = (UInt32Value)70U };
            Cell cell5 = new Cell() { CellReference = "E1", StyleIndex = (UInt32Value)70U };
            Cell cell6 = new Cell() { CellReference = "F1", StyleIndex = (UInt32Value)70U };
            Cell cell7 = new Cell() { CellReference = "G1", StyleIndex = (UInt32Value)70U };
            Cell cell8 = new Cell() { CellReference = "K1", StyleIndex = (UInt32Value)40U };
            Cell cell9 = new Cell() { CellReference = "L1", StyleIndex = (UInt32Value)2U };
            Cell cell10 = new Cell() { CellReference = "M1", StyleIndex = (UInt32Value)2U };

            row1.Append(cell1);
            row1.Append(cell2);
            row1.Append(cell3);
            row1.Append(cell4);
            row1.Append(cell5);
            row1.Append(cell6);
            row1.Append(cell7);
            row1.Append(cell8);
            row1.Append(cell9);
            row1.Append(cell10);

            Row row3 = new Row() { RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "1:15" }, Height = 15D, CustomHeight = true, DyDescent = 0.25D };

            Cell cell21 = new Cell() { CellReference = "B3", StyleIndex = (UInt32Value)71U, DataType = CellValues.String };
            CellValue cellValue2 = new CellValue();
            cellValue2.Text = "Valutazione HR";
            cell21.Append(cellValue2);

            Cell cell22 = new Cell() { CellReference = "C3", StyleIndex = (UInt32Value)71U };

            Cell cell23 = new Cell() { CellReference = "D3", StyleIndex = (UInt32Value)71U, DataType = CellValues.String };
            CellValue cellValue3 = new CellValue();
            cellValue3.Text = "Valutazione della Linea";

            cell23.Append(cellValue3);
            Cell cell24 = new Cell() { CellReference = "E3", StyleIndex = (UInt32Value)71U };
            Cell cell25 = new Cell() { CellReference = "F3", StyleIndex = (UInt32Value)71U };

            row3.Append(cell21);
            row3.Append(cell22);
            row3.Append(cell23);
            row3.Append(cell24);
            row3.Append(cell25);

            Row row4 = new Row() { RowIndex = (UInt32Value)4U, Spans = new ListValue<StringValue>() { InnerText = "1:15" }, Height = 30D, DyDescent = 0.25D };
            Cell cell34 = new Cell() { CellReference = "A4", StyleIndex = (UInt32Value)41U };

            Cell cell35 = new Cell() { CellReference = "B4", StyleIndex = (UInt32Value)65U, DataType = CellValues.String };
            CellValue cellValue4 = new CellValue();
            cellValue4.Text = "Discrezionali";

            cell35.Append(cellValue4);

            Cell cell36 = new Cell() { CellReference = "C4", StyleIndex = (UInt32Value)65U, DataType = CellValues.String };
            CellValue cellValue5 = new CellValue();
            cellValue5.Text = "Comportamentali";

            cell36.Append(cellValue5);

            Cell cell37 = new Cell() { CellReference = "D4", StyleIndex = (UInt32Value)65U, DataType = CellValues.String };
            CellValue cellValue6 = new CellValue();
            cellValue6.Text = "Compentenze comportamentali";

            cell37.Append(cellValue6);

            Cell cell38 = new Cell() { CellReference = "E4", StyleIndex = (UInt32Value)68U, DataType = CellValues.String };
            CellValue cellValue7 = new CellValue();
            cellValue7.Text = "Tecniche Strategic Support";

            cell38.Append(cellValue7);

            Cell cell39 = new Cell() { CellReference = "F4", StyleIndex = (UInt32Value)67U, DataType = CellValues.String };
            CellValue cellValue8 = new CellValue();
            cellValue8.Text = "Tecniche Competitive Advantage";

            cell39.Append(cellValue8);
            Cell cell40 = new Cell() { CellReference = "I4", StyleIndex = (UInt32Value)34U };
            Cell cell41 = new Cell() { CellReference = "J4", StyleIndex = (UInt32Value)34U };
            Cell cell42 = new Cell() { CellReference = "K4", StyleIndex = (UInt32Value)34U };
            Cell cell43 = new Cell() { CellReference = "L4", StyleIndex = (UInt32Value)34U };
            Cell cell44 = new Cell() { CellReference = "M4", StyleIndex = (UInt32Value)34U };
            Cell cell45 = new Cell() { CellReference = "N4", StyleIndex = (UInt32Value)34U };
            Cell cell46 = new Cell() { CellReference = "O4", StyleIndex = (UInt32Value)34U };

            row4.Append(cell34);
            row4.Append(cell35);
            row4.Append(cell36);
            row4.Append(cell37);
            row4.Append(cell38);
            row4.Append(cell39);
            row4.Append(cell40);
            row4.Append(cell41);
            row4.Append(cell42);
            row4.Append(cell43);
            row4.Append(cell44);
            row4.Append(cell45);
            row4.Append(cell46);

            #region PUNTEGGI MASSIMI

            Row row6 = new Row() { RowIndex = (UInt32Value)6U, Spans = new ListValue<StringValue>() { InnerText = "1:15" }, DyDescent = 0.25D };

            Cell cell60 = new Cell() { CellReference = "A6", StyleIndex = (UInt32Value)27U, DataType = CellValues.SharedString };
            CellValue cellValue9 = new CellValue();
            cellValue9.Text = "39"; //Punteggio Massimo
            cell60.Append(cellValue9);

            Cell cell61 = new Cell() { CellReference = "B6", StyleIndex = (UInt32Value)30U };
            CellValue cellValue10 = new CellValue();
            cellValue10.Text = "20";
            cell61.Append(cellValue10);

            Cell cell62 = new Cell() { CellReference = "C6", StyleIndex = (UInt32Value)30U };
            CellValue cellValue11 = new CellValue();
            cellValue11.Text = "30";
            cell62.Append(cellValue11);

            Cell cell63 = new Cell() { CellReference = "D6", StyleIndex = (UInt32Value)30U };
            CellValue cellValue12 = new CellValue();
            cellValue12.Text = "20";
            cell63.Append(cellValue12);

            Cell cell64 = new Cell() { CellReference = "E6", StyleIndex = (UInt32Value)30U };
            CellValue cellValue13 = new CellValue();
            cellValue13.Text = "12";
            cell64.Append(cellValue13);

            Cell cell65 = new Cell() { CellReference = "F6", StyleIndex = (UInt32Value)30U };
            CellValue cellValue14 = new CellValue();
            cellValue14.Text = "18";
            cell65.Append(cellValue14);

            Cell cell66 = new Cell() { CellReference = "G6", StyleIndex = (UInt32Value)45U };
            CellFormula cellFormula1 = new CellFormula();
            cellFormula1.Text = "SUM(B6:F6)";
            cell66.Append(cellFormula1);

            row6.Append(cell60);
            row6.Append(cell61);
            row6.Append(cell62);
            row6.Append(cell63);
            row6.Append(cell64);
            row6.Append(cell65);
            row6.Append(cell66);

            #endregion

            //    OSSERVATO / ATTESO * PMAX

            Row row7 = new Row() { RowIndex = (UInt32Value)7U, Spans = new ListValue<StringValue>() { InnerText = "1:15" }, Height = 20.100000000000001D, CustomHeight = true, DyDescent = 0.25D };

            Cell cell74 = new Cell() { CellReference = "A7", StyleIndex = (UInt32Value)56U, DataType = CellValues.SharedString };
            CellValue cellValue16 = new CellValue();
            cellValue16.Text = "40";
            cell74.Append(cellValue16);

            Cell cell75 = new Cell() { CellReference = "B7", StyleIndex = (UInt32Value)57U };
            CellFormula cellFormula2 = new CellFormula();
            cellFormula2.Text = "(\'HR Discrezionali\'!H2*Summary!C6)/\'HR Discrezionali\'!H1";
            cell75.Append(cellFormula2);

            Cell cell76 = new Cell() { CellReference = "C7", StyleIndex = (UInt32Value)57U };
            CellFormula cellFormula2b = new CellFormula();
            cellFormula2b.Text = "(\'HR Comportamentali\'!H2*Summary!C6)/\'HR Comportamentali\'!H1";
            cell76.Append(cellFormula2b);

            Cell cell77 = new Cell() { CellReference = "D7", StyleIndex = (UInt32Value)58U };
            CellFormula cellFormula3 = new CellFormula();
            cellFormula3.Text = "(\'Competenze Comportamentali\'!H2*Summary!D6)/\'Competenze Comportamentali\'!H1";
            cell77.Append(cellFormula3);

            Cell cell78 = new Cell() { CellReference = "E7", StyleIndex = (UInt32Value)58U };
            CellFormula cellFormula4 = new CellFormula();
            cellFormula4.Text = string.Format("(\'Conoscenze Tecniche\'!C{0}*Summary!E6)/\'Conoscenze Tecniche\'!C{1}", rowAttesoStrategic + 1, rowAttesoStrategic);
            cell78.Append(cellFormula4);

            Cell cell79 = new Cell() { CellReference = "F7", StyleIndex = (UInt32Value)58U };
            CellFormula cellFormula5 = new CellFormula();
            cellFormula5.Text = string.Format("(\'Conoscenze Tecniche\'!C{0}*Summary!F6)/\'Conoscenze Tecniche\'!C{1}", rowAttesoCompetitive + 1, rowAttesoCompetitive);
            cell79.Append(cellFormula5);

            Cell cell80 = new Cell() { CellReference = "G7", StyleIndex = (UInt32Value)24U };
            CellFormula cellFormula6 = new CellFormula();
            cellFormula6.Text = "SUM(C7:F7)";
            cell80.Append(cellFormula6);


            row7.Append(cell74);
            row7.Append(cell75);
            row7.Append(cell76);
            row7.Append(cell77);
            row7.Append(cell78);
            row7.Append(cell79);
            row7.Append(cell80);

            sheetData1.Append(row1);
            sheetData1.Append(row3);
            sheetData1.Append(row4);
            sheetData1.Append(row6);
            sheetData1.Append(row7);


            MergeCells mergeCells1 = new MergeCells() { Count = (UInt32Value)3U };
            MergeCell mergeCell1 = new MergeCell() { Reference = "A1:G1" };
            MergeCell mergeCell2 = new MergeCell() { Reference = "D3:F3" };
            MergeCell mergeCell3 = new MergeCell() { Reference = "B3:C3" };

            mergeCells1.Append(mergeCell1);
            mergeCells1.Append(mergeCell2);
            mergeCells1.Append(mergeCell3);
            PageMargins pageMargins2 = new PageMargins() { Left = 0.70866141732283472D, Right = 0.70866141732283472D, Top = 0.74803149606299213D, Bottom = 0.74803149606299213D, Header = 0.31496062992125984D, Footer = 0.31496062992125984D };
            PageSetup pageSetup2 = new PageSetup() { PaperSize = (UInt32Value)9U, Scale = (UInt32Value)93U, Orientation = OrientationValues.Landscape, Id = "rId1" };
            Drawing drawing1 = new Drawing() { Id = rIdSummaryDrawing };

            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(columns1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(mergeCells1);
            worksheet1.Append(pageMargins2);
            worksheet1.Append(pageSetup2);
            worksheet1.Append(drawing1);

            worksheetPart.Worksheet = worksheet1;

        }

        #region DRAWING CHART

        private void GenerateDrawingsPart1Content(DrawingsPart drawingsPart1)
        {
            Xdr.WorksheetDrawing worksheetDrawing1 = new Xdr.WorksheetDrawing();
            worksheetDrawing1.AddNamespaceDeclaration("xdr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            worksheetDrawing1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            Xdr.TwoCellAnchor twoCellAnchor1 = new Xdr.TwoCellAnchor();

            Xdr.FromMarker fromMarker1 = new Xdr.FromMarker();
            Xdr.ColumnId columnId1 = new Xdr.ColumnId();
            columnId1.Text = "0";
            Xdr.ColumnOffset columnOffset1 = new Xdr.ColumnOffset();
            columnOffset1.Text = "42863";
            Xdr.RowId rowId1 = new Xdr.RowId();
            rowId1.Text = "7";
            Xdr.RowOffset rowOffset1 = new Xdr.RowOffset();
            rowOffset1.Text = "266700";

            fromMarker1.Append(columnId1);
            fromMarker1.Append(columnOffset1);
            fromMarker1.Append(rowId1);
            fromMarker1.Append(rowOffset1);

            Xdr.ToMarker toMarker1 = new Xdr.ToMarker();
            Xdr.ColumnId columnId2 = new Xdr.ColumnId();
            columnId2.Text = "6";
            Xdr.ColumnOffset columnOffset2 = new Xdr.ColumnOffset();
            columnOffset2.Text = "371475";
            Xdr.RowId rowId2 = new Xdr.RowId();
            rowId2.Text = "37";
            Xdr.RowOffset rowOffset2 = new Xdr.RowOffset();
            rowOffset2.Text = "95250";

            toMarker1.Append(columnId2);
            toMarker1.Append(columnOffset2);
            toMarker1.Append(rowId2);
            toMarker1.Append(rowOffset2);

            Xdr.GraphicFrame graphicFrame1 = new Xdr.GraphicFrame() { Macro = "" };

            Xdr.NonVisualGraphicFrameProperties nonVisualGraphicFrameProperties1 = new Xdr.NonVisualGraphicFrameProperties();
            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)1096U, Name = "Grafico 2" };

            Xdr.NonVisualGraphicFrameDrawingProperties nonVisualGraphicFrameDrawingProperties1 = new Xdr.NonVisualGraphicFrameDrawingProperties();
            A.GraphicFrameLocks graphicFrameLocks1 = new A.GraphicFrameLocks();

            nonVisualGraphicFrameDrawingProperties1.Append(graphicFrameLocks1);

            nonVisualGraphicFrameProperties1.Append(nonVisualDrawingProperties1);
            nonVisualGraphicFrameProperties1.Append(nonVisualGraphicFrameDrawingProperties1);

            Xdr.Transform transform1 = new Xdr.Transform();
            A.Offset offset1 = new A.Offset() { X = 0L, Y = 0L };
            A.Extents extents1 = new A.Extents() { Cx = 0L, Cy = 0L };

            transform1.Append(offset1);
            transform1.Append(extents1);

            A.Graphic graphic1 = new A.Graphic();

            A.GraphicData graphicData1 = new A.GraphicData() { Uri = "http://schemas.openxmlformats.org/drawingml/2006/chart" };

            C.ChartReference chartReference1 = new C.ChartReference() { Id = rIdSummaryChart };
            chartReference1.AddNamespaceDeclaration("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
            chartReference1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

            graphicData1.Append(chartReference1);

            graphic1.Append(graphicData1);

            graphicFrame1.Append(nonVisualGraphicFrameProperties1);
            graphicFrame1.Append(transform1);
            graphicFrame1.Append(graphic1);
            Xdr.ClientData clientData1 = new Xdr.ClientData();

            twoCellAnchor1.Append(fromMarker1);
            twoCellAnchor1.Append(toMarker1);
            twoCellAnchor1.Append(graphicFrame1);
            twoCellAnchor1.Append(clientData1);

            worksheetDrawing1.Append(twoCellAnchor1);

            drawingsPart1.WorksheetDrawing = worksheetDrawing1;
        }

        private void GenerateChartPart1Content(ChartPart chartPart1)
        {
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            chartSpace1.AddNamespaceDeclaration("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
            chartSpace1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            chartSpace1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = "it-IT" };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            /*AlternateContent alternateContent1 = new AlternateContent();
            alternateContent1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

            AlternateContentChoice alternateContentChoice1 = new AlternateContentChoice() { Requires = "c14" };
            alternateContentChoice1.AddNamespaceDeclaration("c14", "http://schemas.microsoft.com/office/drawing/2007/8/2/chart");
            C.Style style1 = new C.Style() { Val = 26 };

            alternateContentChoice1.Append(style1);

            AlternateContentFallback alternateContentFallback1 = new AlternateContentFallback();*/
            C.Style style2 = new C.Style() { Val = 26 };

            /*alternateContentFallback1.Append(style2);

            alternateContent1.Append(alternateContentChoice1);
            alternateContent1.Append(alternateContentFallback1);*/

            C.Chart chart1 = new C.Chart();
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.View3D view3D1 = new C.View3D();
            C.RotateX rotateX1 = new C.RotateX() { Val = 15 };
            C.RotateY rotateY1 = new C.RotateY() { Val = (UInt16Value)20U };
            C.DepthPercent depthPercent1 = new C.DepthPercent() { Val = (UInt16Value)100U };
            C.RightAngleAxes rightAngleAxes1 = new C.RightAngleAxes() { Val = true };

            view3D1.Append(rotateX1);
            view3D1.Append(rotateY1);
            view3D1.Append(depthPercent1);
            view3D1.Append(rightAngleAxes1);

            C.Floor floor1 = new C.Floor();
            C.Thickness thickness1 = new C.Thickness() { Val = 0 };

            floor1.Append(thickness1);

            C.SideWall sideWall1 = new C.SideWall();
            C.Thickness thickness2 = new C.Thickness() { Val = 0 };

            sideWall1.Append(thickness2);

            C.BackWall backWall1 = new C.BackWall();
            C.Thickness thickness3 = new C.Thickness() { Val = 0 };

            backWall1.Append(thickness3);

            C.PlotArea plotArea1 = new C.PlotArea();

            C.Layout layout1 = new C.Layout();

            C.ManualLayout manualLayout1 = new C.ManualLayout();
            C.LayoutTarget layoutTarget1 = new C.LayoutTarget() { Val = C.LayoutTargetValues.Inner };
            C.LeftMode leftMode1 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode1 = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left1 = new C.Left() { Val = 0.31595663368682292D };
            C.Top top1 = new C.Top() { Val = 3.2234432234432238E-2D };
            C.Width width1 = new C.Width() { Val = 0.65458569460290161D };
            C.Height height1 = new C.Height() { Val = 0.76759116809953065D };

            manualLayout1.Append(layoutTarget1);
            manualLayout1.Append(leftMode1);
            manualLayout1.Append(topMode1);
            manualLayout1.Append(left1);
            manualLayout1.Append(top1);
            manualLayout1.Append(width1);
            manualLayout1.Append(height1);

            layout1.Append(manualLayout1);

            C.Bar3DChart bar3DChart1 = new C.Bar3DChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Bar };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.Clustered };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
            C.Index index1 = new C.Index() { Val = (UInt32Value)0U };
            C.Order order1 = new C.Order() { Val = (UInt32Value)0U };

            C.SeriesText seriesText1 = new C.SeriesText();

            C.StringReference stringReference1 = new C.StringReference();
            C.Formula formula1 = new C.Formula();
            formula1.Text = "Summary!$A$6";

            C.StringCache stringCache1 = new C.StringCache();
            C.PointCount pointCount1 = new C.PointCount() { Val = (UInt32Value)1U };

            C.StringPoint stringPoint1 = new C.StringPoint() { Index = (UInt32Value)0U };
            C.NumericValue numericValue1 = new C.NumericValue();
            numericValue1.Text = "Punteggio massimo";

            stringPoint1.Append(numericValue1);

            stringCache1.Append(pointCount1);
            stringCache1.Append(stringPoint1);

            stringReference1.Append(formula1);
            stringReference1.Append(stringCache1);

            seriesText1.Append(stringReference1);
            C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };

            C.DataLabels dataLabels1 = new C.DataLabels();

            C.DataLabel dataLabel1 = new C.DataLabel();
            C.Index index2 = new C.Index() { Val = (UInt32Value)0U };

            C.Layout layout2 = new C.Layout();

            C.ManualLayout manualLayout2 = new C.ManualLayout();
            C.Left left2 = new C.Left() { Val = 1.1579668395109153E-2D };
            C.Top top2 = new C.Top() { Val = 0D };

            manualLayout2.Append(left2);
            manualLayout2.Append(top2);

            layout2.Append(manualLayout2);
            C.ShowLegendKey showLegendKey1 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue1 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName1 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName1 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent1 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize1 = new C.ShowBubbleSize() { Val = false };

            dataLabel1.Append(index2);
            dataLabel1.Append(layout2);
            dataLabel1.Append(showLegendKey1);
            dataLabel1.Append(showValue1);
            dataLabel1.Append(showCategoryName1);
            dataLabel1.Append(showSeriesName1);
            dataLabel1.Append(showPercent1);
            dataLabel1.Append(showBubbleSize1);

            C.DataLabel dataLabel2 = new C.DataLabel();
            C.Index index3 = new C.Index() { Val = (UInt32Value)1U };

            C.Layout layout3 = new C.Layout();

            C.ManualLayout manualLayout3 = new C.ManualLayout();
            C.Left left3 = new C.Left() { Val = 1.9851119211050917E-2D };
            C.Top top3 = new C.Top() { Val = 0D };

            manualLayout3.Append(left3);
            manualLayout3.Append(top3);

            layout3.Append(manualLayout3);
            C.ShowLegendKey showLegendKey2 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue2 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName2 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName2 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent2 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize2 = new C.ShowBubbleSize() { Val = false };

            dataLabel2.Append(index3);
            dataLabel2.Append(layout3);
            dataLabel2.Append(showLegendKey2);
            dataLabel2.Append(showValue2);
            dataLabel2.Append(showCategoryName2);
            dataLabel2.Append(showSeriesName2);
            dataLabel2.Append(showPercent2);
            dataLabel2.Append(showBubbleSize2);

            C.DataLabel dataLabel3 = new C.DataLabel();
            C.Index index4 = new C.Index() { Val = (UInt32Value)2U };

            C.Layout layout4 = new C.Layout();

            C.ManualLayout manualLayout4 = new C.ManualLayout();
            C.Left left4 = new C.Left() { Val = 1.4634146341463415E-2D };
            C.Top top4 = new C.Top() { Val = 0D };

            manualLayout4.Append(left4);
            manualLayout4.Append(top4);

            layout4.Append(manualLayout4);
            C.ShowLegendKey showLegendKey3 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue3 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName3 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName3 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent3 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize3 = new C.ShowBubbleSize() { Val = false };

            dataLabel3.Append(index4);
            dataLabel3.Append(layout4);
            dataLabel3.Append(showLegendKey3);
            dataLabel3.Append(showValue3);
            dataLabel3.Append(showCategoryName3);
            dataLabel3.Append(showSeriesName3);
            dataLabel3.Append(showPercent3);
            dataLabel3.Append(showBubbleSize3);

            C.DataLabel dataLabel4 = new C.DataLabel();
            C.Index index5 = new C.Index() { Val = (UInt32Value)3U };

            C.Layout layout5 = new C.Layout();

            C.ManualLayout manualLayout5 = new C.ManualLayout();
            C.Left left5 = new C.Left() { Val = 6.6170397370169517E-3D };
            C.Top top5 = new C.Top() { Val = 0D };

            manualLayout5.Append(left5);
            manualLayout5.Append(top5);

            layout5.Append(manualLayout5);
            C.ShowLegendKey showLegendKey4 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue4 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName4 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName4 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent4 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize4 = new C.ShowBubbleSize() { Val = false };

            dataLabel4.Append(index5);
            dataLabel4.Append(layout5);
            dataLabel4.Append(showLegendKey4);
            dataLabel4.Append(showValue4);
            dataLabel4.Append(showCategoryName4);
            dataLabel4.Append(showSeriesName4);
            dataLabel4.Append(showPercent4);
            dataLabel4.Append(showBubbleSize4);
            C.ShowLegendKey showLegendKey5 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue5 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName5 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName5 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent5 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize5 = new C.ShowBubbleSize() { Val = false };
            C.ShowLeaderLines showLeaderLines1 = new C.ShowLeaderLines() { Val = false };

            dataLabels1.Append(dataLabel1);
            dataLabels1.Append(dataLabel2);
            dataLabels1.Append(dataLabel3);
            dataLabels1.Append(dataLabel4);
            dataLabels1.Append(showLegendKey5);
            dataLabels1.Append(showValue5);
            dataLabels1.Append(showCategoryName5);
            dataLabels1.Append(showSeriesName5);
            dataLabels1.Append(showPercent5);
            dataLabels1.Append(showBubbleSize5);
            dataLabels1.Append(showLeaderLines1);

            C.CategoryAxisData categoryAxisData1 = new C.CategoryAxisData();

            C.MultiLevelStringReference multiLevelStringReference1 = new C.MultiLevelStringReference();
            C.Formula formula2 = new C.Formula();
            formula2.Text = "Summary!$B$3:$F$4";

            C.MultiLevelStringCache multiLevelStringCache1 = new C.MultiLevelStringCache();
            C.PointCount pointCount2 = new C.PointCount() { Val = (UInt32Value)5U };

            C.Level level1 = new C.Level();

            C.StringPoint stringPoint2 = new C.StringPoint() { Index = (UInt32Value)0U };
            C.NumericValue numericValue2 = new C.NumericValue();
            numericValue2.Text = "Discrezionali";

            stringPoint2.Append(numericValue2);

            C.StringPoint stringPoint3 = new C.StringPoint() { Index = (UInt32Value)1U };
            C.NumericValue numericValue3 = new C.NumericValue();
            numericValue3.Text = "Comportamentali";

            stringPoint3.Append(numericValue3);

            C.StringPoint stringPoint4 = new C.StringPoint() { Index = (UInt32Value)2U };
            C.NumericValue numericValue4 = new C.NumericValue();
            numericValue4.Text = "Compentenze comportamentali";

            stringPoint4.Append(numericValue4);

            C.StringPoint stringPoint5 = new C.StringPoint() { Index = (UInt32Value)3U };
            C.NumericValue numericValue5 = new C.NumericValue();
            numericValue5.Text = "Tecniche Strategic Support";

            stringPoint5.Append(numericValue5);

            C.StringPoint stringPoint6 = new C.StringPoint() { Index = (UInt32Value)4U };
            C.NumericValue numericValue6 = new C.NumericValue();
            numericValue6.Text = "Tecniche Competitive Advantage";

            stringPoint6.Append(numericValue6);

            level1.Append(stringPoint2);
            level1.Append(stringPoint3);
            level1.Append(stringPoint4);
            level1.Append(stringPoint5);
            level1.Append(stringPoint6);

            C.Level level2 = new C.Level();

            C.StringPoint stringPoint7 = new C.StringPoint() { Index = (UInt32Value)0U };
            C.NumericValue numericValue7 = new C.NumericValue();
            numericValue7.Text = "Valutazione HR";

            stringPoint7.Append(numericValue7);

            C.StringPoint stringPoint8 = new C.StringPoint() { Index = (UInt32Value)2U };
            C.NumericValue numericValue8 = new C.NumericValue();
            numericValue8.Text = "Valutazione della Linea";

            stringPoint8.Append(numericValue8);

            level2.Append(stringPoint7);
            level2.Append(stringPoint8);

            multiLevelStringCache1.Append(pointCount2);
            multiLevelStringCache1.Append(level1);
            multiLevelStringCache1.Append(level2);

            multiLevelStringReference1.Append(formula2);
            multiLevelStringReference1.Append(multiLevelStringCache1);

            categoryAxisData1.Append(multiLevelStringReference1);

            C.Values values1 = new C.Values();

            C.NumberReference numberReference1 = new C.NumberReference();
            C.Formula formula3 = new C.Formula();
            formula3.Text = "Summary!$B$6:$F$6";

            C.NumberingCache numberingCache1 = new C.NumberingCache();
            C.FormatCode formatCode1 = new C.FormatCode();
            formatCode1.Text = "0";
            C.PointCount pointCount3 = new C.PointCount() { Val = (UInt32Value)5U };

            C.NumericPoint numericPoint1 = new C.NumericPoint() { Index = (UInt32Value)0U };
            C.NumericValue numericValue9 = new C.NumericValue();
            numericValue9.Text = "20";

            numericPoint1.Append(numericValue9);

            C.NumericPoint numericPoint2 = new C.NumericPoint() { Index = (UInt32Value)1U };
            C.NumericValue numericValue10 = new C.NumericValue();
            numericValue10.Text = "20";

            numericPoint2.Append(numericValue10);

            C.NumericPoint numericPoint3 = new C.NumericPoint() { Index = (UInt32Value)2U };
            C.NumericValue numericValue11 = new C.NumericValue();
            numericValue11.Text = "20";

            numericPoint3.Append(numericValue11);

            C.NumericPoint numericPoint4 = new C.NumericPoint() { Index = (UInt32Value)3U };
            C.NumericValue numericValue12 = new C.NumericValue();
            numericValue12.Text = "12";

            numericPoint4.Append(numericValue12);

            C.NumericPoint numericPoint5 = new C.NumericPoint() { Index = (UInt32Value)4U };
            C.NumericValue numericValue13 = new C.NumericValue();
            numericValue13.Text = "18";

            numericPoint5.Append(numericValue13);

            numberingCache1.Append(formatCode1);
            numberingCache1.Append(pointCount3);
            numberingCache1.Append(numericPoint1);
            numberingCache1.Append(numericPoint2);
            numberingCache1.Append(numericPoint3);
            numberingCache1.Append(numericPoint4);
            numberingCache1.Append(numericPoint5);

            numberReference1.Append(formula3);
            numberReference1.Append(numberingCache1);

            values1.Append(numberReference1);

            barChartSeries1.Append(index1);
            barChartSeries1.Append(order1);
            barChartSeries1.Append(seriesText1);
            barChartSeries1.Append(invertIfNegative1);
            barChartSeries1.Append(dataLabels1);
            barChartSeries1.Append(categoryAxisData1);
            barChartSeries1.Append(values1);

            C.BarChartSeries barChartSeries2 = new C.BarChartSeries();
            C.Index index6 = new C.Index() { Val = (UInt32Value)1U };
            C.Order order2 = new C.Order() { Val = (UInt32Value)1U };

            C.SeriesText seriesText2 = new C.SeriesText();

            C.StringReference stringReference2 = new C.StringReference();
            C.Formula formula4 = new C.Formula();
            formula4.Text = "Summary!$A$7";

            C.StringCache stringCache2 = new C.StringCache();
            C.PointCount pointCount4 = new C.PointCount() { Val = (UInt32Value)1U };

            C.StringPoint stringPoint9 = new C.StringPoint() { Index = (UInt32Value)0U };
            C.NumericValue numericValue14 = new C.NumericValue();
            numericValue14.Text = "Punteggio rilevato";

            stringPoint9.Append(numericValue14);

            stringCache2.Append(pointCount4);
            stringCache2.Append(stringPoint9);

            stringReference2.Append(formula4);
            stringReference2.Append(stringCache2);

            seriesText2.Append(stringReference2);
            C.InvertIfNegative invertIfNegative2 = new C.InvertIfNegative() { Val = false };

            C.DataLabels dataLabels2 = new C.DataLabels();

            C.DataLabel dataLabel5 = new C.DataLabel();
            C.Index index7 = new C.Index() { Val = (UInt32Value)0U };

            C.Layout layout6 = new C.Layout();

            C.ManualLayout manualLayout6 = new C.ManualLayout();
            C.Left left6 = new C.Left() { Val = 1.4803661737404775E-2D };
            C.Top top6 = new C.Top() { Val = 0D };

            manualLayout6.Append(left6);
            manualLayout6.Append(top6);

            layout6.Append(manualLayout6);
            C.ShowLegendKey showLegendKey6 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue6 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName6 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName6 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent6 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize6 = new C.ShowBubbleSize() { Val = false };

            dataLabel5.Append(index7);
            dataLabel5.Append(layout6);
            dataLabel5.Append(showLegendKey6);
            dataLabel5.Append(showValue6);
            dataLabel5.Append(showCategoryName6);
            dataLabel5.Append(showSeriesName6);
            dataLabel5.Append(showPercent6);
            dataLabel5.Append(showBubbleSize6);

            C.DataLabel dataLabel6 = new C.DataLabel();
            C.Index index8 = new C.Index() { Val = (UInt32Value)1U };

            C.Layout layout7 = new C.Layout();

            C.ManualLayout manualLayout7 = new C.ManualLayout();
            C.Left left7 = new C.Left() { Val = 1.9851119211050917E-2D };
            C.Top top7 = new C.Top() { Val = 0D };

            manualLayout7.Append(left7);
            manualLayout7.Append(top7);

            layout7.Append(manualLayout7);
            C.ShowLegendKey showLegendKey7 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue7 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName7 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName7 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent7 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize7 = new C.ShowBubbleSize() { Val = false };

            dataLabel6.Append(index8);
            dataLabel6.Append(layout7);
            dataLabel6.Append(showLegendKey7);
            dataLabel6.Append(showValue7);
            dataLabel6.Append(showCategoryName7);
            dataLabel6.Append(showSeriesName7);
            dataLabel6.Append(showPercent7);
            dataLabel6.Append(showBubbleSize7);

            C.DataLabel dataLabel7 = new C.DataLabel();
            C.Index index9 = new C.Index() { Val = (UInt32Value)2U };

            C.Layout layout8 = new C.Layout();

            C.ManualLayout manualLayout8 = new C.ManualLayout();
            C.Left left8 = new C.Left() { Val = 1.3234079474033981E-2D };
            C.Top top8 = new C.Top() { Val = -1.5968063872255453E-2D };

            manualLayout8.Append(left8);
            manualLayout8.Append(top8);

            layout8.Append(manualLayout8);
            C.ShowLegendKey showLegendKey8 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue8 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName8 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName8 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent8 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize8 = new C.ShowBubbleSize() { Val = false };

            dataLabel7.Append(index9);
            dataLabel7.Append(layout8);
            dataLabel7.Append(showLegendKey8);
            dataLabel7.Append(showValue8);
            dataLabel7.Append(showCategoryName8);
            dataLabel7.Append(showSeriesName8);
            dataLabel7.Append(showPercent8);
            dataLabel7.Append(showBubbleSize8);

            C.DataLabel dataLabel8 = new C.DataLabel();
            C.Index index10 = new C.Index() { Val = (UInt32Value)3U };

            C.Layout layout9 = new C.Layout();

            C.ManualLayout manualLayout9 = new C.ManualLayout();
            C.Left left9 = new C.Left() { Val = 1.3262275142436487E-2D };
            C.Top top9 = new C.Top() { Val = -2.3886636811907944E-3D };

            manualLayout9.Append(left9);
            manualLayout9.Append(top9);

            layout9.Append(manualLayout9);
            C.ShowLegendKey showLegendKey9 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue9 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName9 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName9 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent9 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize9 = new C.ShowBubbleSize() { Val = false };

            dataLabel8.Append(index10);
            dataLabel8.Append(layout9);
            dataLabel8.Append(showLegendKey9);
            dataLabel8.Append(showValue9);
            dataLabel8.Append(showCategoryName9);
            dataLabel8.Append(showSeriesName9);
            dataLabel8.Append(showPercent9);
            dataLabel8.Append(showBubbleSize9);
            C.ShowLegendKey showLegendKey10 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue10 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName10 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName10 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent10 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize10 = new C.ShowBubbleSize() { Val = false };
            C.ShowLeaderLines showLeaderLines2 = new C.ShowLeaderLines() { Val = false };

            dataLabels2.Append(dataLabel5);
            dataLabels2.Append(dataLabel6);
            dataLabels2.Append(dataLabel7);
            dataLabels2.Append(dataLabel8);
            dataLabels2.Append(showLegendKey10);
            dataLabels2.Append(showValue10);
            dataLabels2.Append(showCategoryName10);
            dataLabels2.Append(showSeriesName10);
            dataLabels2.Append(showPercent10);
            dataLabels2.Append(showBubbleSize10);
            dataLabels2.Append(showLeaderLines2);

            C.CategoryAxisData categoryAxisData2 = new C.CategoryAxisData();

            C.MultiLevelStringReference multiLevelStringReference2 = new C.MultiLevelStringReference();
            C.Formula formula5 = new C.Formula();
            formula5.Text = "Summary!$B$3:$F$4";

            C.MultiLevelStringCache multiLevelStringCache2 = new C.MultiLevelStringCache();
            C.PointCount pointCount5 = new C.PointCount() { Val = (UInt32Value)5U };

            C.Level level3 = new C.Level();

            C.StringPoint stringPoint10 = new C.StringPoint() { Index = (UInt32Value)0U };
            C.NumericValue numericValue15 = new C.NumericValue();
            numericValue15.Text = "Discrezionali";

            stringPoint10.Append(numericValue15);

            C.StringPoint stringPoint11 = new C.StringPoint() { Index = (UInt32Value)1U };
            C.NumericValue numericValue16 = new C.NumericValue();
            numericValue16.Text = "Comportamentali";

            stringPoint11.Append(numericValue16);

            C.StringPoint stringPoint12 = new C.StringPoint() { Index = (UInt32Value)2U };
            C.NumericValue numericValue17 = new C.NumericValue();
            numericValue17.Text = "Compentenze comportamentali";

            stringPoint12.Append(numericValue17);

            C.StringPoint stringPoint13 = new C.StringPoint() { Index = (UInt32Value)3U };
            C.NumericValue numericValue18 = new C.NumericValue();
            numericValue18.Text = "Tecniche Strategic Support";

            stringPoint13.Append(numericValue18);

            C.StringPoint stringPoint14 = new C.StringPoint() { Index = (UInt32Value)4U };
            C.NumericValue numericValue19 = new C.NumericValue();
            numericValue19.Text = "Tecniche Competitive Advantage";

            stringPoint14.Append(numericValue19);

            level3.Append(stringPoint10);
            level3.Append(stringPoint11);
            level3.Append(stringPoint12);
            level3.Append(stringPoint13);
            level3.Append(stringPoint14);

            C.Level level4 = new C.Level();

            C.StringPoint stringPoint15 = new C.StringPoint() { Index = (UInt32Value)0U };
            C.NumericValue numericValue20 = new C.NumericValue();
            numericValue20.Text = "Valutazione  HR";

            stringPoint15.Append(numericValue20);

            C.StringPoint stringPoint16 = new C.StringPoint() { Index = (UInt32Value)2U };
            C.NumericValue numericValue21 = new C.NumericValue();
            numericValue21.Text = "Valutazione della Linea";

            stringPoint16.Append(numericValue21);

            level4.Append(stringPoint15);
            level4.Append(stringPoint16);

            multiLevelStringCache2.Append(pointCount5);
            multiLevelStringCache2.Append(level3);
            multiLevelStringCache2.Append(level4);

            multiLevelStringReference2.Append(formula5);
            multiLevelStringReference2.Append(multiLevelStringCache2);

            categoryAxisData2.Append(multiLevelStringReference2);

            C.Values values2 = new C.Values();

            C.NumberReference numberReference2 = new C.NumberReference();
            C.Formula formula6 = new C.Formula();
            formula6.Text = "Summary!$B$7:$F$7";

            C.NumberingCache numberingCache2 = new C.NumberingCache();
            C.FormatCode formatCode2 = new C.FormatCode();
            formatCode2.Text = "General";
            C.PointCount pointCount6 = new C.PointCount() { Val = (UInt32Value)5U };

            C.NumericPoint numericPoint6 = new C.NumericPoint() { Index = (UInt32Value)0U };
            C.NumericValue numericValue22 = new C.NumericValue();
            numericValue22.Text = "11";

            numericPoint6.Append(numericValue22);

            C.NumericPoint numericPoint7 = new C.NumericPoint() { Index = (UInt32Value)1U };
            C.NumericValue numericValue23 = new C.NumericValue();
            numericValue23.Text = "13";

            numericPoint7.Append(numericValue23);

            C.NumericPoint numericPoint8 = new C.NumericPoint() { Index = (UInt32Value)2U, FormatCode = "0" };
            C.NumericValue numericValue24 = new C.NumericValue();
            numericValue24.Text = "27.586206896551722";

            numericPoint8.Append(numericValue24);

            C.NumericPoint numericPoint9 = new C.NumericPoint() { Index = (UInt32Value)3U, FormatCode = "0" };
            C.NumericValue numericValue25 = new C.NumericValue();
            numericValue25.Text = "8.5714285714285712";

            numericPoint9.Append(numericValue25);

            C.NumericPoint numericPoint10 = new C.NumericPoint() { Index = (UInt32Value)4U, FormatCode = "0" };
            C.NumericValue numericValue26 = new C.NumericValue();
            numericValue26.Text = "18";

            numericPoint10.Append(numericValue26);

            numberingCache2.Append(formatCode2);
            numberingCache2.Append(pointCount6);
            numberingCache2.Append(numericPoint6);
            numberingCache2.Append(numericPoint7);
            numberingCache2.Append(numericPoint8);
            numberingCache2.Append(numericPoint9);
            numberingCache2.Append(numericPoint10);

            numberReference2.Append(formula6);
            numberReference2.Append(numberingCache2);

            values2.Append(numberReference2);

            barChartSeries2.Append(index6);
            barChartSeries2.Append(order2);
            barChartSeries2.Append(seriesText2);
            barChartSeries2.Append(invertIfNegative2);
            barChartSeries2.Append(dataLabels2);
            barChartSeries2.Append(categoryAxisData2);
            barChartSeries2.Append(values2);

            C.DataLabels dataLabels3 = new C.DataLabels();
            C.ShowLegendKey showLegendKey11 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue11 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName11 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName11 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent11 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize11 = new C.ShowBubbleSize() { Val = false };

            dataLabels3.Append(showLegendKey11);
            dataLabels3.Append(showValue11);
            dataLabels3.Append(showCategoryName11);
            dataLabels3.Append(showSeriesName11);
            dataLabels3.Append(showPercent11);
            dataLabels3.Append(showBubbleSize11);
            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)75U };
            C.Shape shape1 = new C.Shape() { Val = C.ShapeValues.Box };
            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)48360064U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)49635712U };
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)0U };

            bar3DChart1.Append(barDirection1);
            bar3DChart1.Append(barGrouping1);
            bar3DChart1.Append(varyColors1);
            bar3DChart1.Append(barChartSeries1);
            bar3DChart1.Append(barChartSeries2);
            bar3DChart1.Append(dataLabels3);
            bar3DChart1.Append(gapWidth1);
            bar3DChart1.Append(shape1);
            bar3DChart1.Append(axisId1);
            bar3DChart1.Append(axisId2);
            bar3DChart1.Append(axisId3);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)48360064U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MinMax };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };
            C.NumberingFormat numberingFormat1 = new C.NumberingFormat() { FormatCode = "General", SourceLinked = true };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.None };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };
            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)49635712U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId4);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(numberingFormat1);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId5 = new C.AxisId() { Val = (UInt32Value)49635712U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 100D };

            scaling2.Append(orientation2);
            scaling2.Append(maxAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Bottom };
            C.NumberingFormat numberingFormat2 = new C.NumberingFormat() { FormatCode = "0", SourceLinked = true };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.None };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };
            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)48360064U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };

            valueAxis1.Append(axisId5);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(numberingFormat2);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);

            plotArea1.Append(layout1);
            plotArea1.Append(bar3DChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);

            C.Legend legend1 = new C.Legend();
            C.LegendPosition legendPosition1 = new C.LegendPosition() { Val = C.LegendPositionValues.Bottom };

            C.Layout layout10 = new C.Layout();

            C.ManualLayout manualLayout10 = new C.ManualLayout();
            C.LeftMode leftMode2 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode2 = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left10 = new C.Left() { Val = 0.32841905971206153D };
            C.Top top10 = new C.Top() { Val = 0.89771904380327783D };
            C.Width width2 = new C.Width() { Val = 0.33366076271345013D };
            C.Height height2 = new C.Height() { Val = 5.033516888636784E-2D };

            manualLayout10.Append(leftMode2);
            manualLayout10.Append(topMode2);
            manualLayout10.Append(left10);
            manualLayout10.Append(top10);
            manualLayout10.Append(width2);
            manualLayout10.Append(height2);

            layout10.Append(manualLayout10);
            C.Overlay overlay1 = new C.Overlay() { Val = false };

            legend1.Append(legendPosition1);
            legend1.Append(layout10);
            legend1.Append(overlay1);
            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };
            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(autoTitleDeleted1);
            chart1.Append(view3D1);
            chart1.Append(floor1);
            chart1.Append(sideWall1);
            chart1.Append(backWall1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            C.PrintSettings printSettings1 = new C.PrintSettings();
            C.HeaderFooter headerFooter1 = new C.HeaderFooter();
            C.PageMargins pageMargins1 = new C.PageMargins() { Left = 0.70866141732283561D, Right = 0.70866141732283561D, Top = 0.74803149606299291D, Bottom = 0.74803149606299291D, Header = 0.3149606299212605D, Footer = 0.3149606299212605D };
            C.PageSetup pageSetup1 = new C.PageSetup() { Orientation = C.PageSetupOrientationValues.Landscape };

            printSettings1.Append(headerFooter1);
            printSettings1.Append(pageMargins1);
            printSettings1.Append(pageSetup1);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            //chartSpace1.Append(alternateContent1);
            chartSpace1.Append(chart1);
            chartSpace1.Append(printSettings1);

            chartPart1.ChartSpace = chartSpace1;
        }


        #endregion



        private void CreaFoglioGenerico(WorkbookPart workbookPart, string rId, string headerFoglio, string macrogruppo)
        {
            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>(rId);

            Worksheet worksheet1 = new Worksheet();
            worksheet1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            SheetDimension sheetDimension1 = new SheetDimension() { Reference = "A1:M40" };

            SheetViews sheetViews1 = new SheetViews();

            SheetView sheetView1 = new SheetView() { ZoomScale = (UInt32Value)70U, ZoomScaleNormal = (UInt32Value)70U, WorkbookViewId = (UInt32Value)0U };
            Selection selection1 = new Selection() { ActiveCell = "C33", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "C33:F36" } };

            sheetView1.Append(selection1);

            sheetViews1.Append(sheetView1);
            SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties() { DefaultRowHeight = 15D };

            Columns columns1 = new Columns();
            Column column1 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 9.140625D, Style = (UInt32Value)1U };
            Column column2 = new Column() { Min = (UInt32Value)2U, Max = (UInt32Value)2U, Width = 47.140625D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column3 = new Column() { Min = (UInt32Value)3U, Max = (UInt32Value)6U, Width = 5.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column4 = new Column() { Min = (UInt32Value)7U, Max = (UInt32Value)7U, Width = 36.28515625D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column5 = new Column() { Min = (UInt32Value)8U, Max = (UInt32Value)8U, Width = 6.85546875D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column6 = new Column() { Min = (UInt32Value)9U, Max = (UInt32Value)12U, Width = 5.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column7 = new Column() { Min = (UInt32Value)13U, Max = (UInt32Value)16384U, Width = 9.140625D, Style = (UInt32Value)1U };

            columns1.Append(column1);
            columns1.Append(column2);
            columns1.Append(column3);
            columns1.Append(column4);
            columns1.Append(column5);
            columns1.Append(column6);
            columns1.Append(column7);

            SheetData sheetData1 = new SheetData();

            #region RIGA 1 (TITOLO, SOMMA PUNTEGGIO ATTESO)

            Row row1 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            //COMPETENZE COMPORTAMENTALI
            Cell cell1 = new Cell() { CellReference = "A1", StyleIndex = (UInt32Value)4U, DataType = CellValues.String };
            CellValue cellValue1 = new CellValue();
            cellValue1.Text = headerFoglio;
            cell1.Append(cellValue1);

            //PUNTEGGIO ATTESO
            Cell cell2 = new Cell() { CellReference = "G1", StyleIndex = (UInt32Value)18U, DataType = CellValues.SharedString };
            CellValue cellValue2 = new CellValue();
            cellValue2.Text = "34";
            cell2.Append(cellValue2);

            //SOMMA PUNTEGGI ATTESI
            Cell cell3 = new Cell() { CellReference = "H1", StyleIndex = (UInt32Value)30U };
            CellFormula cellFormula1 = new CellFormula();
            cellFormula1.Text = "C11+C20+C29+C38";
            CellValue cellValue3 = new CellValue();
            cellValue3.Text = "26";

            cell3.Append(cellFormula1);
            cell3.Append(cellValue3);

            row1.Append(cell1);
            row1.Append(cell2);
            row1.Append(cell3);

            sheetData1.Append(row1);

            #endregion

            #region RIGA 2 (SOMMA PUNTEGGIO OSSERVATO)

            Row row2 = new Row() { RowIndex = (UInt32Value)2U, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            //PUNTEGGIO OSSERVATO
            Cell cell4 = new Cell() { CellReference = "G2", StyleIndex = (UInt32Value)29U, DataType = CellValues.SharedString };
            CellValue cellValue4 = new CellValue();
            cellValue4.Text = "11";
            cell4.Append(cellValue4);

            //SOMMA PUNTEGGI OSSERVATI
            Cell cell5 = new Cell() { CellReference = "H2", StyleIndex = (UInt32Value)28U };
            CellFormula cellFormula2 = new CellFormula();
            cellFormula2.Text = "C12+C21+C30+C39";
            CellValue cellValue5 = new CellValue();
            cellValue5.Text = "44";

            cell5.Append(cellFormula2);
            cell5.Append(cellValue5);

            row2.Append(cell4);
            row2.Append(cell5);

            sheetData1.Append(row2);

            #endregion

            #region RIGA 3 (RIGA VUOTA)

            Row row3 = new Row() { RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };
            Cell cell6 = new Cell() { CellReference = "C3", StyleIndex = (UInt32Value)36U };
            Cell cell7 = new Cell() { CellReference = "D3", StyleIndex = (UInt32Value)36U };
            Cell cell8 = new Cell() { CellReference = "E3", StyleIndex = (UInt32Value)36U };
            Cell cell9 = new Cell() { CellReference = "F3", StyleIndex = (UInt32Value)36U };
            Cell cell10 = new Cell() { CellReference = "I3", StyleIndex = (UInt32Value)36U };
            Cell cell11 = new Cell() { CellReference = "J3", StyleIndex = (UInt32Value)36U };
            Cell cell12 = new Cell() { CellReference = "K3", StyleIndex = (UInt32Value)36U };
            Cell cell13 = new Cell() { CellReference = "L3", StyleIndex = (UInt32Value)36U };

            row3.Append(cell6);
            row3.Append(cell7);
            row3.Append(cell8);
            row3.Append(cell9);
            row3.Append(cell10);
            row3.Append(cell11);
            row3.Append(cell12);
            row3.Append(cell13);

            sheetData1.Append(row3);

            #endregion

            #region RIGA 4 (TESTO: VALORI ATTESI - VALORI OSSERVATI)

            Row row4 = new Row() { RowIndex = (UInt32Value)4U, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            Cell cell14 = new Cell() { CellReference = "C4", StyleIndex = (UInt32Value)74U, DataType = CellValues.SharedString };
            CellValue cellValue6 = new CellValue();
            cellValue6.Text = "36";

            cell14.Append(cellValue6);
            Cell cell15 = new Cell() { CellReference = "D4", StyleIndex = (UInt32Value)74U };
            Cell cell16 = new Cell() { CellReference = "E4", StyleIndex = (UInt32Value)74U };
            Cell cell17 = new Cell() { CellReference = "F4", StyleIndex = (UInt32Value)74U };

            Cell cell18 = new Cell() { CellReference = "I4", StyleIndex = (UInt32Value)74U, DataType = CellValues.SharedString };
            CellValue cellValue7 = new CellValue();
            cellValue7.Text = "37";

            cell18.Append(cellValue7);
            Cell cell19 = new Cell() { CellReference = "J4", StyleIndex = (UInt32Value)74U };
            Cell cell20 = new Cell() { CellReference = "K4", StyleIndex = (UInt32Value)74U };
            Cell cell21 = new Cell() { CellReference = "L4", StyleIndex = (UInt32Value)74U };

            row4.Append(cell14);
            row4.Append(cell15);
            row4.Append(cell16);
            row4.Append(cell17);
            row4.Append(cell18);
            row4.Append(cell19);
            row4.Append(cell20);
            row4.Append(cell21);

            sheetData1.Append(row4);

            #endregion

            //TODO
            List<UInt32> righePunteggiAttesi = new List<UInt32>();

            var comportamentali = Dati.Where(c => c.Competenza.TipologiaCompetenza.MacroGruppo == macrogruppo);

            var gruppi = from c in comportamentali
                         group c by c.Competenza.TipologiaCompetenza.Titolo into g
                         select new { tipologia = g.Key, gruppo = g };


            UInt32 riga = 5;
            foreach (var g in gruppi)
            {


                Row rowTitolo = CreaRigaTipoCompetenza(riga, g.tipologia);
                sheetData1.Append(rowTitolo);

                Console.WriteLine("*** " + g.tipologia);

                riga++;

                uint rigaInizioDati = riga;

                foreach (var inner in g.gruppo)
                {
                    string titolo = inner.Competenza.Titolo;
                    int valAtteso = inner.LivelloConoscenzaAtteso.Valore;
                    int valOsservato = inner.LivelloConoscenzaOsservato.Valore;

                    Row rowCompetenza = CreaRigaCompetenza(riga, titolo, valAtteso, valOsservato);
                    sheetData1.Append(rowCompetenza);

                    Console.WriteLine("Comp: " + inner.Competenza.Titolo);
                    Console.WriteLine("PA: " + valAtteso);
                    Console.WriteLine("PO: " + valOsservato);

                    riga++;
                }

                uint rigaFineDati = riga - 1;

                //Stampo i subtotali e le altre somme
                Row rowSubTotali = CreaRigaSubTotali(riga, rigaInizioDati, rigaFineDati);
                sheetData1.Append(rowSubTotali);

                riga++;
                Row rowPunteggioAtteso = CreaRigaPunteggioAtteso(riga);
                sheetData1.Append(rowPunteggioAtteso);

                riga++;
                Row rowPunteggioOsservato = CreaRigaPunteggioOsservato(riga);
                sheetData1.Append(rowPunteggioOsservato);

                riga++;

                //RIGA VUOTA 
                riga++;
            }



            //Celle Unite per stringhe allra riga 4
            MergeCells mergeCells1 = new MergeCells() { Count = (UInt32Value)2U };
            MergeCell mergeCell1 = new MergeCell() { Reference = "C4:F4" };
            MergeCell mergeCell2 = new MergeCell() { Reference = "I4:L4" };

            mergeCells1.Append(mergeCell1);
            mergeCells1.Append(mergeCell2);

            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(columns1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(mergeCells1);

            worksheetPart.Worksheet = worksheet1;

        }

        private void CreaFoglioTecniche(WorkbookPart workbookPart, out uint rowAttesoStrategic, out uint rowAttesoCompetitive)
        {
            rowAttesoStrategic = rowAttesoCompetitive = 0;

            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>(rIdTecniche);

            Worksheet worksheet1 = new Worksheet();
            worksheet1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            SheetDimension sheetDimension1 = new SheetDimension() { Reference = "A1:M40" };

            SheetViews sheetViews1 = new SheetViews();

            SheetView sheetView1 = new SheetView() { ZoomScale = (UInt32Value)70U, ZoomScaleNormal = (UInt32Value)70U, WorkbookViewId = (UInt32Value)0U };
            sheetViews1.Append(sheetView1);

            SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties() { DefaultRowHeight = 15D };

            Columns columns1 = new Columns();
            Column column1 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 9.140625D, Style = (UInt32Value)1U };
            Column column2 = new Column() { Min = (UInt32Value)2U, Max = (UInt32Value)2U, Width = 47.140625D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column3 = new Column() { Min = (UInt32Value)3U, Max = (UInt32Value)6U, Width = 5.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column4 = new Column() { Min = (UInt32Value)7U, Max = (UInt32Value)7U, Width = 36.28515625D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column5 = new Column() { Min = (UInt32Value)8U, Max = (UInt32Value)8U, Width = 6.85546875D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column6 = new Column() { Min = (UInt32Value)9U, Max = (UInt32Value)12U, Width = 5.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column7 = new Column() { Min = (UInt32Value)13U, Max = (UInt32Value)16384U, Width = 9.140625D, Style = (UInt32Value)1U };

            columns1.Append(column1);
            columns1.Append(column2);
            columns1.Append(column3);
            columns1.Append(column4);
            columns1.Append(column5);
            columns1.Append(column6);
            columns1.Append(column7);

            SheetData sheetData1 = new SheetData();

            #region RIGA 1 (TITOLO, SOMMA PUNTEGGIO ATTESO)

            Row row1 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            //COMPETENZE COMPORTAMENTALI
            Cell cell1 = new Cell() { CellReference = "A1", StyleIndex = (UInt32Value)4U, DataType = CellValues.SharedString };
            CellValue cellValue1 = new CellValue();
            cellValue1.Text = "47";
            cell1.Append(cellValue1);

            //PUNTEGGIO ATTESO
            Cell cell2 = new Cell() { CellReference = "G1", StyleIndex = (UInt32Value)18U, DataType = CellValues.SharedString };
            CellValue cellValue2 = new CellValue();
            cellValue2.Text = "34";
            cell2.Append(cellValue2);

            //SOMMA PUNTEGGI ATTESI
            Cell cell3 = new Cell() { CellReference = "H1", StyleIndex = (UInt32Value)30U };
            CellFormula cellFormula1 = new CellFormula();
            cellFormula1.Text = "C25+C36";//TODO
            cell3.Append(cellFormula1);

            row1.Append(cell1);
            row1.Append(cell2);
            row1.Append(cell3);

            sheetData1.Append(row1);

            #endregion

            #region RIGA 2 (SOMMA PUNTEGGIO OSSERVATO)

            Row row2 = new Row() { RowIndex = (UInt32Value)2U, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            //PUNTEGGIO OSSERVATO
            Cell cell4 = new Cell() { CellReference = "G2", StyleIndex = (UInt32Value)29U, DataType = CellValues.SharedString };
            CellValue cellValue4 = new CellValue();
            cellValue4.Text = "11";
            cell4.Append(cellValue4);

            //SOMMA PUNTEGGI OSSERVATI
            Cell cell5 = new Cell() { CellReference = "H2", StyleIndex = (UInt32Value)28U };
            CellFormula cellFormula2 = new CellFormula();
            cellFormula2.Text = "C24+C35";
            cell5.Append(cellFormula2);


            row2.Append(cell4);
            row2.Append(cell5);

            sheetData1.Append(row2);

            #endregion

            #region RIGA 3 (RIGA VUOTA)

            Row row3 = new Row() { RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };
            Cell cell6 = new Cell() { CellReference = "C3", StyleIndex = (UInt32Value)36U };
            Cell cell7 = new Cell() { CellReference = "D3", StyleIndex = (UInt32Value)36U };
            Cell cell8 = new Cell() { CellReference = "E3", StyleIndex = (UInt32Value)36U };
            Cell cell9 = new Cell() { CellReference = "F3", StyleIndex = (UInt32Value)36U };
            Cell cell10 = new Cell() { CellReference = "I3", StyleIndex = (UInt32Value)36U };
            Cell cell11 = new Cell() { CellReference = "J3", StyleIndex = (UInt32Value)36U };
            Cell cell12 = new Cell() { CellReference = "K3", StyleIndex = (UInt32Value)36U };
            Cell cell13 = new Cell() { CellReference = "L3", StyleIndex = (UInt32Value)36U };

            row3.Append(cell6);
            row3.Append(cell7);
            row3.Append(cell8);
            row3.Append(cell9);
            row3.Append(cell10);
            row3.Append(cell11);
            row3.Append(cell12);
            row3.Append(cell13);

            sheetData1.Append(row3);

            #endregion

            #region RIGA 4 (TESTO: VALORI ATTESI - VALORI OSSERVATI)

            Row row4 = new Row() { RowIndex = (UInt32Value)4U, Spans = new ListValue<StringValue>() { InnerText = "1:13" } };

            Cell cell14 = new Cell() { CellReference = "C4", StyleIndex = (UInt32Value)74U, DataType = CellValues.SharedString };
            CellValue cellValue6 = new CellValue();
            cellValue6.Text = "36";

            cell14.Append(cellValue6);
            Cell cell15 = new Cell() { CellReference = "D4", StyleIndex = (UInt32Value)74U };
            Cell cell16 = new Cell() { CellReference = "E4", StyleIndex = (UInt32Value)74U };
            Cell cell17 = new Cell() { CellReference = "F4", StyleIndex = (UInt32Value)74U };

            Cell cell18 = new Cell() { CellReference = "I4", StyleIndex = (UInt32Value)74U, DataType = CellValues.SharedString };
            CellValue cellValue7 = new CellValue();
            cellValue7.Text = "37";

            cell18.Append(cellValue7);
            Cell cell19 = new Cell() { CellReference = "J4", StyleIndex = (UInt32Value)74U };
            Cell cell20 = new Cell() { CellReference = "K4", StyleIndex = (UInt32Value)74U };
            Cell cell21 = new Cell() { CellReference = "L4", StyleIndex = (UInt32Value)74U };

            row4.Append(cell14);
            row4.Append(cell15);
            row4.Append(cell16);
            row4.Append(cell17);
            row4.Append(cell18);
            row4.Append(cell19);
            row4.Append(cell20);
            row4.Append(cell21);

            sheetData1.Append(row4);

            #endregion

            //TODO
            List<UInt32> righePunteggiAttesi = new List<UInt32>();

            var tecniche = Dati.Where(c => c.Competenza.TipologiaCompetenza.MacroGruppo == Tipologiche.Macrogruppi.MG_TECNICO);

            #region BLOCCO FOUNDATIONAL

            var foundational = tecniche.Where(t => t.Competenza.TipologiaCompetenza.Titolo == "Foundational");
            UInt32 riga = 5;

            Row rowTitoloFound = CreaRigaTipoCompetenza(riga, "Foundational");
            sheetData1.Append(rowTitoloFound);

            riga++;

            UInt32 rigaInizioDatiFound = riga;
            foreach (var comp in foundational)
            {
                string titolo = comp.Competenza.Titolo;
                int valAtteso = comp.LivelloConoscenzaAtteso.Valore;
                int valOsservato = comp.LivelloConoscenzaOsservato.Valore;

                Row rowCompetenza = CreaRigaCompetenza(riga, titolo, valAtteso, valOsservato);
                sheetData1.Append(rowCompetenza);

                Console.WriteLine("Comp: " + comp.Competenza.Titolo);
                Console.WriteLine("PA: " + valAtteso);
                Console.WriteLine("PO: " + valOsservato);

                riga++;
            }

            UInt32 rigaFineDatiFound = riga - 1;

            //Stampo i subtotali e le altre somme
            Row rowSubTotaliF = CreaRigaSubTotali(riga, rigaInizioDatiFound, rigaFineDatiFound);
            sheetData1.Append(rowSubTotaliF);

            riga++;
            Row rowPunteggioAttesoF = CreaRigaPunteggioAtteso(riga);
            sheetData1.Append(rowPunteggioAttesoF);

            riga++;
            Row rowPunteggioOsservatoF = CreaRigaPunteggioOsservato(riga);
            sheetData1.Append(rowPunteggioOsservatoF);

            riga++;
            Row rowPunteggioAttesoMinimoF = CreaRigaPunteggioAttesoMinimo(riga);
            sheetData1.Append(rowPunteggioAttesoMinimoF);

            riga++;
            Row rowIdoneitaF = CreaRigaIdoneita(riga);
            sheetData1.Append(rowIdoneitaF);

            //Me la segno perchè poi faccio il merge
            UInt32 rigaIdoneita = riga;

            riga++;
            //RIGA VUOTA
            riga++;

            #endregion


            var altreTecniche = tecniche.Where(t => t.Competenza.TipologiaCompetenza.Titolo != "Foundational");

            var gruppi = from c in altreTecniche
                         group c by c.Competenza.TipologiaCompetenza.Titolo into g
                         select new { tipologia = g.Key, gruppo = g };

            foreach (var g in gruppi)
            {


                Row rowTitolo = CreaRigaTipoCompetenza(riga, g.tipologia);
                sheetData1.Append(rowTitolo);

                Console.WriteLine("*** " + g.tipologia);

                riga++;

                uint rigaInizioDati = riga;

                foreach (var competenza in g.gruppo)
                {
                    string titolo = competenza.Competenza.Titolo;
                    int valAtteso = competenza.LivelloConoscenzaAtteso.Valore;
                    int valOsservato = competenza.LivelloConoscenzaOsservato.Valore;

                    Row rowCompetenza = CreaRigaCompetenza(riga, titolo, valAtteso, valOsservato);
                    sheetData1.Append(rowCompetenza);

                    Console.WriteLine("Comp: " + competenza.Competenza.Titolo);
                    Console.WriteLine("PA: " + valAtteso);
                    Console.WriteLine("PO: " + valOsservato);

                    riga++;
                }

                uint rigaFineDati = riga - 1;

                //Stampo i subtotali e le altre somme
                Row rowSubTotali = CreaRigaSubTotali(riga, rigaInizioDati, rigaFineDati);
                sheetData1.Append(rowSubTotali);

                riga++;
                Row rowPunteggioAtteso = CreaRigaPunteggioAtteso(riga);
                sheetData1.Append(rowPunteggioAtteso);

                if (g.tipologia == "Strategic Support")
                {
                    rowAttesoStrategic = riga;
                }
                else if (g.tipologia == "Competitive Advantage")
                {
                    rowAttesoCompetitive = riga;
                }

                riga++;
                Row rowPunteggioOsservato = CreaRigaPunteggioOsservato(riga);
                sheetData1.Append(rowPunteggioOsservato);

                riga++;

                //RIGA VUOTA
                riga++;
            }



            //Celle Unite per stringhe alla riga 4
            MergeCells mergeCells1 = new MergeCells() { Count = (UInt32Value)2U };
            MergeCell mergeCell1 = new MergeCell() { Reference = "C4:F4" };
            MergeCell mergeCell2 = new MergeCell() { Reference = "I4:L4" };
            MergeCell mergeCell3 = new MergeCell() { Reference = string.Format("C{0}:D{1}", rigaIdoneita, rigaIdoneita) };

            mergeCells1.Append(mergeCell1);
            mergeCells1.Append(mergeCell2);
            mergeCells1.Append(mergeCell3);

            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(columns1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(mergeCells1);

            worksheetPart.Worksheet = worksheet1;

        }

        #endregion


        #region COPIATI DAL TEMPLATE


        private void GenerateSharedStringTablePart1Content(SharedStringTablePart sharedStringTablePart1)
        {
            SharedStringTable sharedStringTable1 = new SharedStringTable() { Count = (UInt32Value)128U, UniqueCount = (UInt32Value)62U };

            SharedStringItem sharedStringItem1 = new SharedStringItem();
            Text text1 = new Text();
            text1.Text = "Contabilità Lavori";

            sharedStringItem1.Append(text1);

            SharedStringItem sharedStringItem2 = new SharedStringItem();
            Text text2 = new Text();
            text2.Text = "x";

            sharedStringItem2.Append(text2);

            SharedStringItem sharedStringItem3 = new SharedStringItem();
            Text text3 = new Text();
            text3.Text = "Sub Totale";

            sharedStringItem3.Append(text3);

            SharedStringItem sharedStringItem4 = new SharedStringItem();
            Text text4 = new Text();
            text4.Text = "1) FOUNDATIONAL";

            sharedStringItem4.Append(text4);

            SharedStringItem sharedStringItem5 = new SharedStringItem();
            Text text5 = new Text();
            text5.Text = "Planning breve-medio periodo";

            sharedStringItem5.Append(text5);

            SharedStringItem sharedStringItem6 = new SharedStringItem();
            Text text6 = new Text();
            text6.Text = "Planning medio-lungo periodo";

            sharedStringItem6.Append(text6);

            SharedStringItem sharedStringItem7 = new SharedStringItem();
            Text text7 = new Text();
            text7.Text = "Controlling";

            sharedStringItem7.Append(text7);

            SharedStringItem sharedStringItem8 = new SharedStringItem();
            Text text8 = new Text();
            text8.Text = "Punteggio Osservato";

            sharedStringItem8.Append(text8);

            SharedStringItem sharedStringItem9 = new SharedStringItem();
            Text text9 = new Text();
            text9.Text = "Punt. Osservato vs Valore Atteso";

            sharedStringItem9.Append(text9);

            SharedStringItem sharedStringItem10 = new SharedStringItem();
            Text text10 = new Text();
            text10.Text = "Fattore Ponderazione";

            sharedStringItem10.Append(text10);

            SharedStringItem sharedStringItem11 = new SharedStringItem();
            Text text11 = new Text();
            text11.Text = "Punteggio Osservato Ponderato";

            sharedStringItem11.Append(text11);

            SharedStringItem sharedStringItem12 = new SharedStringItem();
            Text text12 = new Text();
            text12.Text = "PUNTEGGIO OSSERVATO COMPLESSIVO";

            sharedStringItem12.Append(text12);

            SharedStringItem sharedStringItem13 = new SharedStringItem();
            Text text13 = new Text();
            text13.Text = "1) MANAGERIALI";

            sharedStringItem13.Append(text13);

            SharedStringItem sharedStringItem14 = new SharedStringItem();
            Text text14 = new Text();
            text14.Text = "Integrazione";

            sharedStringItem14.Append(text14);

            SharedStringItem sharedStringItem15 = new SharedStringItem();
            Text text15 = new Text();
            text15.Text = "Teamwork";

            sharedStringItem15.Append(text15);

            SharedStringItem sharedStringItem16 = new SharedStringItem();
            Text text16 = new Text();
            text16.Text = "Gestione delle Risorse Umane";

            sharedStringItem16.Append(text16);

            SharedStringItem sharedStringItem17 = new SharedStringItem();
            Text text17 = new Text();
            text17.Text = "Leadership";

            sharedStringItem17.Append(text17);

            SharedStringItem sharedStringItem18 = new SharedStringItem();
            Text text18 = new Text();
            text18.Text = "2) RELAZIONALI";

            sharedStringItem18.Append(text18);

            SharedStringItem sharedStringItem19 = new SharedStringItem();
            Text text19 = new Text();
            text19.Text = "Comunicazione";

            sharedStringItem19.Append(text19);

            SharedStringItem sharedStringItem20 = new SharedStringItem();
            Text text20 = new Text();
            text20.Text = "Assertività";

            sharedStringItem20.Append(text20);

            SharedStringItem sharedStringItem21 = new SharedStringItem();
            Text text21 = new Text();
            text21.Text = "Negoziazione";

            sharedStringItem21.Append(text21);

            SharedStringItem sharedStringItem22 = new SharedStringItem();
            Text text22 = new Text();
            text22.Text = "Networking";

            sharedStringItem22.Append(text22);

            SharedStringItem sharedStringItem23 = new SharedStringItem();
            Text text23 = new Text();
            text23.Text = "3) COGNITIVE";

            sharedStringItem23.Append(text23);

            SharedStringItem sharedStringItem24 = new SharedStringItem();
            Text text24 = new Text();
            text24.Text = "Capacità di Analisi";

            sharedStringItem24.Append(text24);

            SharedStringItem sharedStringItem25 = new SharedStringItem();
            Text text25 = new Text();
            text25.Text = "Problem solving";

            sharedStringItem25.Append(text25);

            SharedStringItem sharedStringItem26 = new SharedStringItem();
            Text text26 = new Text();
            text26.Text = "Visione d\'insieme";

            sharedStringItem26.Append(text26);

            SharedStringItem sharedStringItem27 = new SharedStringItem();
            Text text27 = new Text();
            text27.Text = "Orientamento al cliente";

            sharedStringItem27.Append(text27);

            SharedStringItem sharedStringItem28 = new SharedStringItem();
            Text text28 = new Text();
            text28.Text = "Orientamento al risultato";

            sharedStringItem28.Append(text28);

            SharedStringItem sharedStringItem29 = new SharedStringItem();
            Text text29 = new Text();
            text29.Text = "Responsabilità";

            sharedStringItem29.Append(text29);

            SharedStringItem sharedStringItem30 = new SharedStringItem();
            Text text30 = new Text();
            text30.Text = "Efficienza";

            sharedStringItem30.Append(text30);

            SharedStringItem sharedStringItem31 = new SharedStringItem();
            Text text31 = new Text();
            text31.Text = "Proattività";

            sharedStringItem31.Append(text31);

            SharedStringItem sharedStringItem32 = new SharedStringItem();
            Text text32 = new Text();
            text32.Text = "4) REALIZZATIVE";

            sharedStringItem32.Append(text32);

            SharedStringItem sharedStringItem33 = new SharedStringItem();
            Text text33 = new Text();
            text33.Text = "Punteggio Atteso";

            sharedStringItem33.Append(text33);

            SharedStringItem sharedStringItem34 = new SharedStringItem();
            Text text34 = new Text();
            text34.Text = "Punteggio Atteso Minimo (70%)";

            sharedStringItem34.Append(text34);

            SharedStringItem sharedStringItem35 = new SharedStringItem();
            Text text35 = new Text();
            text35.Text = "PUNTEGGIO ATTESO";

            sharedStringItem35.Append(text35);

            SharedStringItem sharedStringItem36 = new SharedStringItem();
            Text text36 = new Text();
            text36.Text = "% Osservata Ponderata";

            sharedStringItem36.Append(text36);

            SharedStringItem sharedStringItem37 = new SharedStringItem();
            Text text37 = new Text();
            text37.Text = "VALORI ATTESI";

            sharedStringItem37.Append(text37);

            SharedStringItem sharedStringItem38 = new SharedStringItem();
            Text text38 = new Text();
            text38.Text = "VALORI OSSERVATI";

            sharedStringItem38.Append(text38);

            SharedStringItem sharedStringItem39 = new SharedStringItem();
            Text text39 = new Text();
            text39.Text = "Punteggio Atteso Ponderato";

            sharedStringItem39.Append(text39);

            SharedStringItem sharedStringItem40 = new SharedStringItem();
            Text text40 = new Text();
            text40.Text = "Punteggio massimo";

            sharedStringItem40.Append(text40);

            SharedStringItem sharedStringItem41 = new SharedStringItem();
            Text text41 = new Text();
            text41.Text = "Punteggio rilevato";

            sharedStringItem41.Append(text41);

            SharedStringItem sharedStringItem42 = new SharedStringItem();
            Text text42 = new Text();
            text42.Text = "3) COMPETITIVE ADVANTAGE";

            sharedStringItem42.Append(text42);

            SharedStringItem sharedStringItem43 = new SharedStringItem();
            Text text43 = new Text();
            text43.Text = "Normative di Settore";

            sharedStringItem43.Append(text43);

            SharedStringItem sharedStringItem44 = new SharedStringItem();
            Text text44 = new Text();
            text44.Text = "VALUTAZIONE HR";

            sharedStringItem44.Append(text44);

            SharedStringItem sharedStringItem45 = new SharedStringItem();
            Text text45 = new Text();
            text45.Text = "2) CONSIDERAZIONI GESTIONALI";

            sharedStringItem45.Append(text45);

            SharedStringItem sharedStringItem46 = new SharedStringItem();
            Text text46 = new Text();
            text46.Text = "Inserire il Punteggio Osservato";

            sharedStringItem46.Append(text46);

            SharedStringItem sharedStringItem47 = new SharedStringItem();
            Text text47 = new Text();
            text47.Text = "Valutazione della Linea";

            sharedStringItem47.Append(text47);

            SharedStringItem sharedStringItem48 = new SharedStringItem();
            Text text48 = new Text();
            text48.Text = "CONOSCENZE TECNICHE";

            sharedStringItem48.Append(text48);

            SharedStringItem sharedStringItem49 = new SharedStringItem();
            Text text49 = new Text();
            text49.Text = "COMPETENZE COMPORTAMENTALI";

            sharedStringItem49.Append(text49);

            SharedStringItem sharedStringItem50 = new SharedStringItem();
            Text text50 = new Text();
            text50.Text = "Compentenze comportamentali";

            sharedStringItem50.Append(text50);

            SharedStringItem sharedStringItem51 = new SharedStringItem();

            Run run1 = new Run();

            RunProperties runProperties1 = new RunProperties();
            Bold bold1 = new Bold();
            FontSize fontSize1 = new FontSize() { Val = 11D };
            Color color1 = new Color() { Theme = (UInt32Value)1U };
            RunFont runFont1 = new RunFont() { Val = "Calibri" };
            FontFamily fontFamily1 = new FontFamily() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

            runProperties1.Append(bold1);
            runProperties1.Append(fontSize1);
            runProperties1.Append(color1);
            runProperties1.Append(runFont1);
            runProperties1.Append(fontFamily1);
            runProperties1.Append(fontScheme1);
            Text text51 = new Text();
            text51.Text = "1)";

            run1.Append(runProperties1);
            run1.Append(text51);

            Run run2 = new Run();

            RunProperties runProperties2 = new RunProperties();
            Bold bold2 = new Bold();
            Italic italic1 = new Italic();
            FontSize fontSize2 = new FontSize() { Val = 11D };
            Color color2 = new Color() { Theme = (UInt32Value)1U };
            RunFont runFont2 = new RunFont() { Val = "Calibri" };
            FontFamily fontFamily2 = new FontFamily() { Val = 2 };
            FontScheme fontScheme2 = new FontScheme() { Val = FontSchemeValues.Minor };

            runProperties2.Append(bold2);
            runProperties2.Append(italic1);
            runProperties2.Append(fontSize2);
            runProperties2.Append(color2);
            runProperties2.Append(runFont2);
            runProperties2.Append(fontFamily2);
            runProperties2.Append(fontScheme2);
            Text text52 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text52.Text = " ASSESSMENT";

            run2.Append(runProperties2);
            run2.Append(text52);

            sharedStringItem51.Append(run1);
            sharedStringItem51.Append(run2);

            SharedStringItem sharedStringItem52 = new SharedStringItem();

            Run run3 = new Run();

            RunProperties runProperties3 = new RunProperties();
            Bold bold3 = new Bold();
            FontSize fontSize3 = new FontSize() { Val = 11D };
            Color color3 = new Color() { Theme = (UInt32Value)1U };
            RunFont runFont3 = new RunFont() { Val = "Calibri" };
            FontFamily fontFamily3 = new FontFamily() { Val = 2 };

            runProperties3.Append(bold3);
            runProperties3.Append(fontSize3);
            runProperties3.Append(color3);
            runProperties3.Append(runFont3);
            runProperties3.Append(fontFamily3);
            Text text53 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text53.Text = "Tecniche ";

            run3.Append(runProperties3);
            run3.Append(text53);

            Run run4 = new Run();

            RunProperties runProperties4 = new RunProperties();
            Bold bold4 = new Bold();
            Italic italic2 = new Italic();
            FontSize fontSize4 = new FontSize() { Val = 11D };
            Color color4 = new Color() { Theme = (UInt32Value)1U };
            RunFont runFont4 = new RunFont() { Val = "Calibri" };
            FontFamily fontFamily4 = new FontFamily() { Val = 2 };

            runProperties4.Append(bold4);
            runProperties4.Append(italic2);
            runProperties4.Append(fontSize4);
            runProperties4.Append(color4);
            runProperties4.Append(runFont4);
            runProperties4.Append(fontFamily4);
            Text text54 = new Text();
            text54.Text = "Competitive Advantage";

            run4.Append(runProperties4);
            run4.Append(text54);

            sharedStringItem52.Append(run3);
            sharedStringItem52.Append(run4);

            SharedStringItem sharedStringItem53 = new SharedStringItem();
            Text text55 = new Text();
            text55.Text = "Valutazione  HR";

            sharedStringItem53.Append(text55);

            SharedStringItem sharedStringItem54 = new SharedStringItem();
            Text text56 = new Text();
            text56.Text = "Lettura e Interpretazione del Progetto";

            sharedStringItem54.Append(text56);

            SharedStringItem sharedStringItem55 = new SharedStringItem();
            Text text57 = new Text();
            text57.Text = "Monitoraggio e Rilievo dell\'opera eseguita";

            sharedStringItem55.Append(text57);

            SharedStringItem sharedStringItem56 = new SharedStringItem();
            Text text58 = new Text();
            text58.Text = "Incidenza dei Costi";

            sharedStringItem56.Append(text58);

            SharedStringItem sharedStringItem57 = new SharedStringItem();
            Text text59 = new Text();
            text59.Text = "Analisi Scostamenti";

            sharedStringItem57.Append(text59);

            SharedStringItem sharedStringItem58 = new SharedStringItem();
            Text text60 = new Text();
            text60.Text = "Nuovi Prezzi";

            sharedStringItem58.Append(text60);

            SharedStringItem sharedStringItem59 = new SharedStringItem();
            Text text61 = new Text();
            text61.Text = "Gestione Riserve e Contenzioso";

            sharedStringItem59.Append(text61);

            SharedStringItem sharedStringItem60 = new SharedStringItem();
            Text text62 = new Text();
            text62.Text = "SCHEDA CONTABILIZZATORE SR.";

            sharedStringItem60.Append(text62);

            SharedStringItem sharedStringItem61 = new SharedStringItem();

            Run run5 = new Run();

            RunProperties runProperties5 = new RunProperties();
            Bold bold5 = new Bold();
            FontSize fontSize5 = new FontSize() { Val = 11D };
            Color color5 = new Color() { Theme = (UInt32Value)1U };
            RunFont runFont5 = new RunFont() { Val = "Calibri" };
            FontFamily fontFamily5 = new FontFamily() { Val = 2 };

            runProperties5.Append(bold5);
            runProperties5.Append(fontSize5);
            runProperties5.Append(color5);
            runProperties5.Append(runFont5);
            runProperties5.Append(fontFamily5);
            Text text63 = new Text();
            text63.Text = "Tecniche";

            run5.Append(runProperties5);
            run5.Append(text63);

            Run run6 = new Run();

            RunProperties runProperties6 = new RunProperties();
            Bold bold6 = new Bold();
            Italic italic3 = new Italic();
            FontSize fontSize6 = new FontSize() { Val = 11D };
            Color color6 = new Color() { Theme = (UInt32Value)1U };
            RunFont runFont6 = new RunFont() { Val = "Calibri" };
            FontFamily fontFamily6 = new FontFamily() { Val = 2 };

            runProperties6.Append(bold6);
            runProperties6.Append(italic3);
            runProperties6.Append(fontSize6);
            runProperties6.Append(color6);
            runProperties6.Append(runFont6);
            runProperties6.Append(fontFamily6);
            Text text64 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text64.Text = " Strategic Support";

            run6.Append(runProperties6);
            run6.Append(text64);

            sharedStringItem61.Append(run5);
            sharedStringItem61.Append(run6);

            SharedStringItem sharedStringItem62 = new SharedStringItem();
            Text text65 = new Text();
            text65.Text = "2) STRATEGIC SUPPORT";

            sharedStringItem62.Append(text65);

            sharedStringTable1.Append(sharedStringItem1);
            sharedStringTable1.Append(sharedStringItem2);
            sharedStringTable1.Append(sharedStringItem3);
            sharedStringTable1.Append(sharedStringItem4);
            sharedStringTable1.Append(sharedStringItem5);
            sharedStringTable1.Append(sharedStringItem6);
            sharedStringTable1.Append(sharedStringItem7);
            sharedStringTable1.Append(sharedStringItem8);
            sharedStringTable1.Append(sharedStringItem9);
            sharedStringTable1.Append(sharedStringItem10);
            sharedStringTable1.Append(sharedStringItem11);
            sharedStringTable1.Append(sharedStringItem12);
            sharedStringTable1.Append(sharedStringItem13);
            sharedStringTable1.Append(sharedStringItem14);
            sharedStringTable1.Append(sharedStringItem15);
            sharedStringTable1.Append(sharedStringItem16);
            sharedStringTable1.Append(sharedStringItem17);
            sharedStringTable1.Append(sharedStringItem18);
            sharedStringTable1.Append(sharedStringItem19);
            sharedStringTable1.Append(sharedStringItem20);
            sharedStringTable1.Append(sharedStringItem21);
            sharedStringTable1.Append(sharedStringItem22);
            sharedStringTable1.Append(sharedStringItem23);
            sharedStringTable1.Append(sharedStringItem24);
            sharedStringTable1.Append(sharedStringItem25);
            sharedStringTable1.Append(sharedStringItem26);
            sharedStringTable1.Append(sharedStringItem27);
            sharedStringTable1.Append(sharedStringItem28);
            sharedStringTable1.Append(sharedStringItem29);
            sharedStringTable1.Append(sharedStringItem30);
            sharedStringTable1.Append(sharedStringItem31);
            sharedStringTable1.Append(sharedStringItem32);
            sharedStringTable1.Append(sharedStringItem33);
            sharedStringTable1.Append(sharedStringItem34);
            sharedStringTable1.Append(sharedStringItem35);
            sharedStringTable1.Append(sharedStringItem36);
            sharedStringTable1.Append(sharedStringItem37);
            sharedStringTable1.Append(sharedStringItem38);
            sharedStringTable1.Append(sharedStringItem39);
            sharedStringTable1.Append(sharedStringItem40);
            sharedStringTable1.Append(sharedStringItem41);
            sharedStringTable1.Append(sharedStringItem42);
            sharedStringTable1.Append(sharedStringItem43);
            sharedStringTable1.Append(sharedStringItem44);
            sharedStringTable1.Append(sharedStringItem45);
            sharedStringTable1.Append(sharedStringItem46);
            sharedStringTable1.Append(sharedStringItem47);
            sharedStringTable1.Append(sharedStringItem48);
            sharedStringTable1.Append(sharedStringItem49);
            sharedStringTable1.Append(sharedStringItem50);
            sharedStringTable1.Append(sharedStringItem51);
            sharedStringTable1.Append(sharedStringItem52);
            sharedStringTable1.Append(sharedStringItem53);
            sharedStringTable1.Append(sharedStringItem54);
            sharedStringTable1.Append(sharedStringItem55);
            sharedStringTable1.Append(sharedStringItem56);
            sharedStringTable1.Append(sharedStringItem57);
            sharedStringTable1.Append(sharedStringItem58);
            sharedStringTable1.Append(sharedStringItem59);
            sharedStringTable1.Append(sharedStringItem60);
            sharedStringTable1.Append(sharedStringItem61);
            sharedStringTable1.Append(sharedStringItem62);

            sharedStringTablePart1.SharedStringTable = sharedStringTable1;
        }

        private void GenerateWorkbookStylesPart1Content(WorkbookStylesPart workbookStylesPart1)
        {
            Stylesheet stylesheet1 = new Stylesheet();

            NumberingFormats numberingFormats1 = new NumberingFormats() { Count = (UInt32Value)3U };
            NumberingFormat numberingFormat3 = new NumberingFormat() { NumberFormatId = (UInt32Value)43U, FormatCode = "_-* #,##0.00_-;\\-* #,##0.00_-;_-* \"-\"??_-;_-@_-" };
            NumberingFormat numberingFormat4 = new NumberingFormat() { NumberFormatId = (UInt32Value)164U, FormatCode = "_-* #,##0_-;\\-* #,##0_-;_-* \"-\"??_-;_-@_-" };
            NumberingFormat numberingFormat5 = new NumberingFormat() { NumberFormatId = (UInt32Value)165U, FormatCode = "0.0" };

            numberingFormats1.Append(numberingFormat3);
            numberingFormats1.Append(numberingFormat4);
            numberingFormats1.Append(numberingFormat5);

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)17U };

            Font font1 = new Font();
            FontSize fontSize7 = new FontSize() { Val = 11D };
            Color color7 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme3 = new FontScheme() { Val = FontSchemeValues.Minor };

            font1.Append(fontSize7);
            font1.Append(color7);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme3);

            Font font2 = new Font();
            FontSize fontSize8 = new FontSize() { Val = 10D };
            FontName fontName2 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering() { Val = 2 };

            font2.Append(fontSize8);
            font2.Append(fontName2);
            font2.Append(fontFamilyNumbering2);

            Font font3 = new Font();
            FontSize fontSize9 = new FontSize() { Val = 11D };
            Color color8 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName3 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering3 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme4 = new FontScheme() { Val = FontSchemeValues.Minor };

            font3.Append(fontSize9);
            font3.Append(color8);
            font3.Append(fontName3);
            font3.Append(fontFamilyNumbering3);
            font3.Append(fontScheme4);

            Font font4 = new Font();
            Bold bold7 = new Bold();
            FontSize fontSize10 = new FontSize() { Val = 11D };
            Color color9 = new Color() { Theme = (UInt32Value)0U };
            FontName fontName4 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering4 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme5 = new FontScheme() { Val = FontSchemeValues.Minor };

            font4.Append(bold7);
            font4.Append(fontSize10);
            font4.Append(color9);
            font4.Append(fontName4);
            font4.Append(fontFamilyNumbering4);
            font4.Append(fontScheme5);

            Font font5 = new Font();
            Bold bold8 = new Bold();
            FontSize fontSize11 = new FontSize() { Val = 11D };
            Color color10 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName5 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering5 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme6 = new FontScheme() { Val = FontSchemeValues.Minor };

            font5.Append(bold8);
            font5.Append(fontSize11);
            font5.Append(color10);
            font5.Append(fontName5);
            font5.Append(fontFamilyNumbering5);
            font5.Append(fontScheme6);

            Font font6 = new Font();
            Bold bold9 = new Bold();
            FontSize fontSize12 = new FontSize() { Val = 11D };
            Color color11 = new Color() { Rgb = "FF0070C0" };
            FontName fontName6 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering6 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme7 = new FontScheme() { Val = FontSchemeValues.Minor };

            font6.Append(bold9);
            font6.Append(fontSize12);
            font6.Append(color11);
            font6.Append(fontName6);
            font6.Append(fontFamilyNumbering6);
            font6.Append(fontScheme7);

            Font font7 = new Font();
            Bold bold10 = new Bold();
            FontSize fontSize13 = new FontSize() { Val = 9D };
            Color color12 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName7 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering7 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme8 = new FontScheme() { Val = FontSchemeValues.Minor };

            font7.Append(bold10);
            font7.Append(fontSize13);
            font7.Append(color12);
            font7.Append(fontName7);
            font7.Append(fontFamilyNumbering7);
            font7.Append(fontScheme8);

            Font font8 = new Font();
            Bold bold11 = new Bold();
            FontSize fontSize14 = new FontSize() { Val = 11D };
            Color color13 = new Color() { Rgb = "FFFF0000" };
            FontName fontName8 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering8 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme9 = new FontScheme() { Val = FontSchemeValues.Minor };

            font8.Append(bold11);
            font8.Append(fontSize14);
            font8.Append(color13);
            font8.Append(fontName8);
            font8.Append(fontFamilyNumbering8);
            font8.Append(fontScheme9);

            Font font9 = new Font();
            Bold bold12 = new Bold();
            FontSize fontSize15 = new FontSize() { Val = 11D };
            FontName fontName9 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering9 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme10 = new FontScheme() { Val = FontSchemeValues.Minor };

            font9.Append(bold12);
            font9.Append(fontSize15);
            font9.Append(fontName9);
            font9.Append(fontFamilyNumbering9);
            font9.Append(fontScheme10);

            Font font10 = new Font();
            FontSize fontSize16 = new FontSize() { Val = 11D };
            FontName fontName10 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering10 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme11 = new FontScheme() { Val = FontSchemeValues.Minor };

            font10.Append(fontSize16);
            font10.Append(fontName10);
            font10.Append(fontFamilyNumbering10);
            font10.Append(fontScheme11);

            Font font11 = new Font();
            Bold bold13 = new Bold();
            Italic italic4 = new Italic();
            FontSize fontSize17 = new FontSize() { Val = 11D };
            Color color14 = new Color() { Theme = (UInt32Value)3U, Tint = 0.39997558519241921D };
            FontName fontName11 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering11 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme12 = new FontScheme() { Val = FontSchemeValues.Minor };

            font11.Append(bold13);
            font11.Append(italic4);
            font11.Append(fontSize17);
            font11.Append(color14);
            font11.Append(fontName11);
            font11.Append(fontFamilyNumbering11);
            font11.Append(fontScheme12);

            Font font12 = new Font();
            Bold bold14 = new Bold();
            FontSize fontSize18 = new FontSize() { Val = 10D };
            FontName fontName12 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering12 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme13 = new FontScheme() { Val = FontSchemeValues.Minor };

            font12.Append(bold14);
            font12.Append(fontSize18);
            font12.Append(fontName12);
            font12.Append(fontFamilyNumbering12);
            font12.Append(fontScheme13);

            Font font13 = new Font();
            FontSize fontSize19 = new FontSize() { Val = 10D };
            FontName fontName13 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering13 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme14 = new FontScheme() { Val = FontSchemeValues.Minor };

            font13.Append(fontSize19);
            font13.Append(fontName13);
            font13.Append(fontFamilyNumbering13);
            font13.Append(fontScheme14);

            Font font14 = new Font();
            Bold bold15 = new Bold();
            Italic italic5 = new Italic();
            FontSize fontSize20 = new FontSize() { Val = 11D };
            Color color15 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName14 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering14 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme15 = new FontScheme() { Val = FontSchemeValues.Minor };

            font14.Append(bold15);
            font14.Append(italic5);
            font14.Append(fontSize20);
            font14.Append(color15);
            font14.Append(fontName14);
            font14.Append(fontFamilyNumbering14);
            font14.Append(fontScheme15);

            Font font15 = new Font();
            Bold bold16 = new Bold();
            FontSize fontSize21 = new FontSize() { Val = 11D };
            Color color16 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName15 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering15 = new FontFamilyNumbering() { Val = 2 };

            font15.Append(bold16);
            font15.Append(fontSize21);
            font15.Append(color16);
            font15.Append(fontName15);
            font15.Append(fontFamilyNumbering15);

            Font font16 = new Font();
            Bold bold17 = new Bold();
            Italic italic6 = new Italic();
            FontSize fontSize22 = new FontSize() { Val = 11D };
            Color color17 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName16 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering16 = new FontFamilyNumbering() { Val = 2 };

            font16.Append(bold17);
            font16.Append(italic6);
            font16.Append(fontSize22);
            font16.Append(color17);
            font16.Append(fontName16);
            font16.Append(fontFamilyNumbering16);

            Font font17 = new Font();
            Bold bold18 = new Bold();
            FontSize fontSize23 = new FontSize() { Val = 11D };
            Color color18 = new Color() { Theme = (UInt32Value)4U };
            FontName fontName17 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering17 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme16 = new FontScheme() { Val = FontSchemeValues.Minor };

            font17.Append(bold18);
            font17.Append(fontSize23);
            font17.Append(color18);
            font17.Append(fontName17);
            font17.Append(fontFamilyNumbering17);
            font17.Append(fontScheme16);

            fonts1.Append(font1);
            fonts1.Append(font2);
            fonts1.Append(font3);
            fonts1.Append(font4);
            fonts1.Append(font5);
            fonts1.Append(font6);
            fonts1.Append(font7);
            fonts1.Append(font8);
            fonts1.Append(font9);
            fonts1.Append(font10);
            fonts1.Append(font11);
            fonts1.Append(font12);
            fonts1.Append(font13);
            fonts1.Append(font14);
            fonts1.Append(font15);
            fonts1.Append(font16);
            fonts1.Append(font17);

            Fills fills1 = new Fills() { Count = (UInt32Value)7U };

            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };

            fill1.Append(patternFill1);

            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };

            fill2.Append(patternFill2);

            Fill fill3 = new Fill();

            PatternFill patternFill3 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Theme = (UInt32Value)0U };
            BackgroundColor backgroundColor1 = new BackgroundColor() { Indexed = (UInt32Value)64U };

            patternFill3.Append(foregroundColor1);
            patternFill3.Append(backgroundColor1);

            fill3.Append(patternFill3);

            Fill fill4 = new Fill();

            PatternFill patternFill4 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor2 = new ForegroundColor() { Rgb = "FF0070C0" };
            BackgroundColor backgroundColor2 = new BackgroundColor() { Indexed = (UInt32Value)64U };

            patternFill4.Append(foregroundColor2);
            patternFill4.Append(backgroundColor2);

            fill4.Append(patternFill4);

            Fill fill5 = new Fill();

            PatternFill patternFill5 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor3 = new ForegroundColor() { Theme = (UInt32Value)0U, Tint = -0.34998626667073579D };
            BackgroundColor backgroundColor3 = new BackgroundColor() { Indexed = (UInt32Value)64U };

            patternFill5.Append(foregroundColor3);
            patternFill5.Append(backgroundColor3);

            fill5.Append(patternFill5);

            Fill fill6 = new Fill();

            PatternFill patternFill6 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor4 = new ForegroundColor() { Rgb = "FF990033" };
            BackgroundColor backgroundColor4 = new BackgroundColor() { Indexed = (UInt32Value)64U };

            patternFill6.Append(foregroundColor4);
            patternFill6.Append(backgroundColor4);

            fill6.Append(patternFill6);

            Fill fill7 = new Fill();

            PatternFill patternFill7 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor5 = new ForegroundColor() { Theme = (UInt32Value)0U, Tint = -4.9989318521683403E-2D };
            BackgroundColor backgroundColor5 = new BackgroundColor() { Indexed = (UInt32Value)64U };

            patternFill7.Append(foregroundColor5);
            patternFill7.Append(backgroundColor5);

            fill7.Append(patternFill7);

            fills1.Append(fill1);
            fills1.Append(fill2);
            fills1.Append(fill3);
            fills1.Append(fill4);
            fills1.Append(fill5);
            fills1.Append(fill6);
            fills1.Append(fill7);

            Borders borders1 = new Borders() { Count = (UInt32Value)6U };

            Border border1 = new Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);

            Border border2 = new Border();

            LeftBorder leftBorder2 = new LeftBorder() { Style = BorderStyleValues.Medium };
            Color color19 = new Color() { Indexed = (UInt32Value)64U };

            leftBorder2.Append(color19);

            RightBorder rightBorder2 = new RightBorder() { Style = BorderStyleValues.Medium };
            Color color20 = new Color() { Indexed = (UInt32Value)64U };

            rightBorder2.Append(color20);

            TopBorder topBorder2 = new TopBorder() { Style = BorderStyleValues.Medium };
            Color color21 = new Color() { Indexed = (UInt32Value)64U };

            topBorder2.Append(color21);

            BottomBorder bottomBorder2 = new BottomBorder() { Style = BorderStyleValues.Medium };
            Color color22 = new Color() { Indexed = (UInt32Value)64U };

            bottomBorder2.Append(color22);
            DiagonalBorder diagonalBorder2 = new DiagonalBorder();

            border2.Append(leftBorder2);
            border2.Append(rightBorder2);
            border2.Append(topBorder2);
            border2.Append(bottomBorder2);
            border2.Append(diagonalBorder2);

            Border border3 = new Border();

            LeftBorder leftBorder3 = new LeftBorder() { Style = BorderStyleValues.Thin };
            Color color23 = new Color() { Indexed = (UInt32Value)64U };

            leftBorder3.Append(color23);

            RightBorder rightBorder3 = new RightBorder() { Style = BorderStyleValues.Thin };
            Color color24 = new Color() { Indexed = (UInt32Value)64U };

            rightBorder3.Append(color24);

            TopBorder topBorder3 = new TopBorder() { Style = BorderStyleValues.Thin };
            Color color25 = new Color() { Indexed = (UInt32Value)64U };

            topBorder3.Append(color25);

            BottomBorder bottomBorder3 = new BottomBorder() { Style = BorderStyleValues.Thin };
            Color color26 = new Color() { Indexed = (UInt32Value)64U };

            bottomBorder3.Append(color26);
            DiagonalBorder diagonalBorder3 = new DiagonalBorder();

            border3.Append(leftBorder3);
            border3.Append(rightBorder3);
            border3.Append(topBorder3);
            border3.Append(bottomBorder3);
            border3.Append(diagonalBorder3);

            Border border4 = new Border();

            LeftBorder leftBorder4 = new LeftBorder() { Style = BorderStyleValues.Hair };
            Color color27 = new Color() { Theme = (UInt32Value)0U, Tint = -0.499984740745262D };

            leftBorder4.Append(color27);

            RightBorder rightBorder4 = new RightBorder() { Style = BorderStyleValues.Hair };
            Color color28 = new Color() { Theme = (UInt32Value)0U, Tint = -0.499984740745262D };

            rightBorder4.Append(color28);

            TopBorder topBorder4 = new TopBorder() { Style = BorderStyleValues.Hair };
            Color color29 = new Color() { Theme = (UInt32Value)0U, Tint = -0.499984740745262D };

            topBorder4.Append(color29);

            BottomBorder bottomBorder4 = new BottomBorder() { Style = BorderStyleValues.Hair };
            Color color30 = new Color() { Theme = (UInt32Value)0U, Tint = -0.499984740745262D };

            bottomBorder4.Append(color30);
            DiagonalBorder diagonalBorder4 = new DiagonalBorder();

            border4.Append(leftBorder4);
            border4.Append(rightBorder4);
            border4.Append(topBorder4);
            border4.Append(bottomBorder4);
            border4.Append(diagonalBorder4);

            Border border5 = new Border();

            LeftBorder leftBorder5 = new LeftBorder() { Style = BorderStyleValues.Thin };
            Color color31 = new Color() { Indexed = (UInt32Value)64U };

            leftBorder5.Append(color31);

            RightBorder rightBorder5 = new RightBorder() { Style = BorderStyleValues.Thin };
            Color color32 = new Color() { Indexed = (UInt32Value)64U };

            rightBorder5.Append(color32);

            TopBorder topBorder5 = new TopBorder() { Style = BorderStyleValues.Thin };
            Color color33 = new Color() { Indexed = (UInt32Value)64U };

            topBorder5.Append(color33);
            BottomBorder bottomBorder5 = new BottomBorder();
            DiagonalBorder diagonalBorder5 = new DiagonalBorder();

            border5.Append(leftBorder5);
            border5.Append(rightBorder5);
            border5.Append(topBorder5);
            border5.Append(bottomBorder5);
            border5.Append(diagonalBorder5);

            Border border6 = new Border();

            LeftBorder leftBorder6 = new LeftBorder() { Style = BorderStyleValues.Thin };
            Color color34 = new Color() { Indexed = (UInt32Value)64U };

            leftBorder6.Append(color34);

            RightBorder rightBorder6 = new RightBorder() { Style = BorderStyleValues.Thin };
            Color color35 = new Color() { Indexed = (UInt32Value)64U };

            rightBorder6.Append(color35);
            TopBorder topBorder6 = new TopBorder();

            BottomBorder bottomBorder6 = new BottomBorder() { Style = BorderStyleValues.Thin };
            Color color36 = new Color() { Indexed = (UInt32Value)64U };

            bottomBorder6.Append(color36);
            DiagonalBorder diagonalBorder6 = new DiagonalBorder();

            border6.Append(leftBorder6);
            border6.Append(rightBorder6);
            border6.Append(topBorder6);
            border6.Append(bottomBorder6);
            border6.Append(diagonalBorder6);

            borders1.Append(border1);
            borders1.Append(border2);
            borders1.Append(border3);
            borders1.Append(border4);
            borders1.Append(border5);
            borders1.Append(border6);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)4U };
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)43U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, ApplyFont = false, ApplyFill = false, ApplyBorder = false, ApplyAlignment = false, ApplyProtection = false };
            CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };
            CellFormat cellFormat4 = new CellFormat() { NumberFormatId = (UInt32Value)9U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, ApplyFont = false, ApplyFill = false, ApplyBorder = false, ApplyAlignment = false, ApplyProtection = false };

            cellStyleFormats1.Append(cellFormat1);
            cellStyleFormats1.Append(cellFormat2);
            cellStyleFormats1.Append(cellFormat3);
            cellStyleFormats1.Append(cellFormat4);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)75U };
            CellFormat cellFormat5 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };
            CellFormat cellFormat6 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat7 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)5U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };

            CellFormat cellFormat8 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)5U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment1 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat8.Append(alignment1);

            CellFormat cellFormat9 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)5U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment2 = new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center };

            cellFormat9.Append(alignment2);

            CellFormat cellFormat10 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment3 = new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Indent = (UInt32Value)2U };

            cellFormat10.Append(alignment3);
            CellFormat cellFormat11 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)4U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true };

            CellFormat cellFormat12 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)4U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment4 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat12.Append(alignment4);

            CellFormat cellFormat13 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)6U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment5 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat13.Append(alignment5);

            CellFormat cellFormat14 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)4U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment6 = new Alignment() { Horizontal = HorizontalAlignmentValues.Right };

            cellFormat14.Append(alignment6);

            CellFormat cellFormat15 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)3U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment7 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat15.Append(alignment7);

            CellFormat cellFormat16 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)3U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment8 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat16.Append(alignment8);

            CellFormat cellFormat17 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)5U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment9 = new Alignment() { Horizontal = HorizontalAlignmentValues.Right, Vertical = VerticalAlignmentValues.Center };

            cellFormat17.Append(alignment9);

            CellFormat cellFormat18 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment10 = new Alignment() { Horizontal = HorizontalAlignmentValues.Right };

            cellFormat18.Append(alignment10);

            CellFormat cellFormat19 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment11 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat19.Append(alignment11);
            CellFormat cellFormat20 = new CellFormat() { NumberFormatId = (UInt32Value)9U, FontId = (UInt32Value)2U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)3U, ApplyFont = true, ApplyFill = true };

            CellFormat cellFormat21 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)7U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment12 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat21.Append(alignment12);

            CellFormat cellFormat22 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)3U, FillId = (UInt32Value)3U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment13 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat22.Append(alignment13);
            CellFormat cellFormat23 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)3U, FillId = (UInt32Value)3U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true };

            CellFormat cellFormat24 = new CellFormat() { NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)5U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment14 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat24.Append(alignment14);

            CellFormat cellFormat25 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)8U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment15 = new Alignment() { Horizontal = HorizontalAlignmentValues.Right, Vertical = VerticalAlignmentValues.Center };

            cellFormat25.Append(alignment15);

            CellFormat cellFormat26 = new CellFormat() { NumberFormatId = (UInt32Value)43U, FontId = (UInt32Value)5U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)1U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment16 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat26.Append(alignment16);
            CellFormat cellFormat27 = new CellFormat() { NumberFormatId = (UInt32Value)43U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFill = true };
            CellFormat cellFormat28 = new CellFormat() { NumberFormatId = (UInt32Value)9U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFill = true };

            CellFormat cellFormat29 = new CellFormat() { NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)4U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment17 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat29.Append(alignment17);
            CellFormat cellFormat30 = new CellFormat() { NumberFormatId = (UInt32Value)9U, FontId = (UInt32Value)4U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true };
            CellFormat cellFormat31 = new CellFormat() { NumberFormatId = (UInt32Value)9U, FontId = (UInt32Value)5U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true };

            CellFormat cellFormat32 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)3U, FillId = (UInt32Value)3U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment18 = new Alignment() { Vertical = VerticalAlignmentValues.Center };

            cellFormat32.Append(alignment18);

            CellFormat cellFormat33 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)8U, FillId = (UInt32Value)4U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment19 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat33.Append(alignment19);
            CellFormat cellFormat34 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)8U, FillId = (UInt32Value)4U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true };

            CellFormat cellFormat35 = new CellFormat() { NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)3U, FillId = (UInt32Value)3U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment20 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat35.Append(alignment20);
            CellFormat cellFormat36 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)8U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true };

            CellFormat cellFormat37 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)8U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment21 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat37.Append(alignment21);

            CellFormat cellFormat38 = new CellFormat() { NumberFormatId = (UInt32Value)9U, FontId = (UInt32Value)4U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)3U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment22 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat38.Append(alignment22);

            CellFormat cellFormat39 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)9U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment23 = new Alignment() { Horizontal = HorizontalAlignmentValues.Right };

            cellFormat39.Append(alignment23);
            CellFormat cellFormat40 = new CellFormat() { NumberFormatId = (UInt32Value)164U, FontId = (UInt32Value)2U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)1U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true };

            CellFormat cellFormat41 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment24 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat41.Append(alignment24);
            CellFormat cellFormat42 = new CellFormat() { NumberFormatId = (UInt32Value)165U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFill = true };

            CellFormat cellFormat43 = new CellFormat() { NumberFormatId = (UInt32Value)9U, FontId = (UInt32Value)5U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment25 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat43.Append(alignment25);

            CellFormat cellFormat44 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)4U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment26 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat44.Append(alignment26);

            CellFormat cellFormat45 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)4U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment27 = new Alignment() { Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat45.Append(alignment27);

            CellFormat cellFormat46 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)5U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment28 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat46.Append(alignment28);

            CellFormat cellFormat47 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)10U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment29 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat47.Append(alignment29);

            CellFormat cellFormat48 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment30 = new Alignment() { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat48.Append(alignment30);

            CellFormat cellFormat49 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment31 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat49.Append(alignment31);

            CellFormat cellFormat50 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment32 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat50.Append(alignment32);
            CellFormat cellFormat51 = new CellFormat() { NumberFormatId = (UInt32Value)9U, FontId = (UInt32Value)7U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)3U, ApplyFont = true, ApplyFill = true };

            CellFormat cellFormat52 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)7U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment33 = new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center };

            cellFormat52.Append(alignment33);
            CellFormat cellFormat53 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)9U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true };

            CellFormat cellFormat54 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)11U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment34 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat54.Append(alignment34);

            CellFormat cellFormat55 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)11U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment35 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat55.Append(alignment35);

            CellFormat cellFormat56 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)11U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment36 = new Alignment() { Vertical = VerticalAlignmentValues.Center };

            cellFormat56.Append(alignment36);
            CellFormat cellFormat57 = new CellFormat() { NumberFormatId = (UInt32Value)2U, FontId = (UInt32Value)9U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true };

            CellFormat cellFormat58 = new CellFormat() { NumberFormatId = (UInt32Value)2U, FontId = (UInt32Value)9U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment37 = new Alignment() { Horizontal = HorizontalAlignmentValues.Right };

            cellFormat58.Append(alignment37);

            CellFormat cellFormat59 = new CellFormat() { NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)12U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment38 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat59.Append(alignment38);

            CellFormat cellFormat60 = new CellFormat() { NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)12U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment39 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat60.Append(alignment39);

            CellFormat cellFormat61 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)3U, FillId = (UInt32Value)5U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment40 = new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center };

            cellFormat61.Append(alignment40);

            CellFormat cellFormat62 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)3U, FillId = (UInt32Value)5U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment41 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat62.Append(alignment41);

            CellFormat cellFormat63 = new CellFormat() { NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)3U, FillId = (UInt32Value)5U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment42 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat63.Append(alignment42);

            CellFormat cellFormat64 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment43 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat64.Append(alignment43);

            CellFormat cellFormat65 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)5U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment44 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat65.Append(alignment44);

            CellFormat cellFormat66 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)6U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment45 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat66.Append(alignment45);

            CellFormat cellFormat67 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment46 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat67.Append(alignment46);

            CellFormat cellFormat68 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment47 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat68.Append(alignment47);

            CellFormat cellFormat69 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)4U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment48 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat69.Append(alignment48);

            CellFormat cellFormat70 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)4U, FillId = (UInt32Value)6U, BorderId = (UInt32Value)2U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment49 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat70.Append(alignment49);
            CellFormat cellFormat71 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)13U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true };

            CellFormat cellFormat72 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)13U, FillId = (UInt32Value)6U, BorderId = (UInt32Value)2U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment50 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat72.Append(alignment50);

            CellFormat cellFormat73 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)15U, FillId = (UInt32Value)6U, BorderId = (UInt32Value)2U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment51 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat73.Append(alignment51);

            CellFormat cellFormat74 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)5U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment52 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat74.Append(alignment52);

            CellFormat cellFormat75 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)16U, FillId = (UInt32Value)6U, BorderId = (UInt32Value)2U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment53 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat75.Append(alignment53);

            CellFormat cellFormat76 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)16U, FillId = (UInt32Value)6U, BorderId = (UInt32Value)4U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment54 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat76.Append(alignment54);

            CellFormat cellFormat77 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)16U, FillId = (UInt32Value)6U, BorderId = (UInt32Value)5U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment55 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat77.Append(alignment55);

            CellFormat cellFormat78 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment56 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat78.Append(alignment56);

            CellFormat cellFormat79 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyAlignment = true };
            Alignment alignment57 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat79.Append(alignment57);

            cellFormats1.Append(cellFormat5);
            cellFormats1.Append(cellFormat6);
            cellFormats1.Append(cellFormat7);
            cellFormats1.Append(cellFormat8);
            cellFormats1.Append(cellFormat9);
            cellFormats1.Append(cellFormat10);
            cellFormats1.Append(cellFormat11);
            cellFormats1.Append(cellFormat12);
            cellFormats1.Append(cellFormat13);
            cellFormats1.Append(cellFormat14);
            cellFormats1.Append(cellFormat15);
            cellFormats1.Append(cellFormat16);
            cellFormats1.Append(cellFormat17);
            cellFormats1.Append(cellFormat18);
            cellFormats1.Append(cellFormat19);
            cellFormats1.Append(cellFormat20);
            cellFormats1.Append(cellFormat21);
            cellFormats1.Append(cellFormat22);
            cellFormats1.Append(cellFormat23);
            cellFormats1.Append(cellFormat24);
            cellFormats1.Append(cellFormat25);
            cellFormats1.Append(cellFormat26);
            cellFormats1.Append(cellFormat27);
            cellFormats1.Append(cellFormat28);
            cellFormats1.Append(cellFormat29);
            cellFormats1.Append(cellFormat30);
            cellFormats1.Append(cellFormat31);
            cellFormats1.Append(cellFormat32);
            cellFormats1.Append(cellFormat33);
            cellFormats1.Append(cellFormat34);
            cellFormats1.Append(cellFormat35);
            cellFormats1.Append(cellFormat36);
            cellFormats1.Append(cellFormat37);
            cellFormats1.Append(cellFormat38);
            cellFormats1.Append(cellFormat39);
            cellFormats1.Append(cellFormat40);
            cellFormats1.Append(cellFormat41);
            cellFormats1.Append(cellFormat42);
            cellFormats1.Append(cellFormat43);
            cellFormats1.Append(cellFormat44);
            cellFormats1.Append(cellFormat45);
            cellFormats1.Append(cellFormat46);
            cellFormats1.Append(cellFormat47);
            cellFormats1.Append(cellFormat48);
            cellFormats1.Append(cellFormat49);
            cellFormats1.Append(cellFormat50);
            cellFormats1.Append(cellFormat51);
            cellFormats1.Append(cellFormat52);
            cellFormats1.Append(cellFormat53);
            cellFormats1.Append(cellFormat54);
            cellFormats1.Append(cellFormat55);
            cellFormats1.Append(cellFormat56);
            cellFormats1.Append(cellFormat57);
            cellFormats1.Append(cellFormat58);
            cellFormats1.Append(cellFormat59);
            cellFormats1.Append(cellFormat60);
            cellFormats1.Append(cellFormat61);
            cellFormats1.Append(cellFormat62);
            cellFormats1.Append(cellFormat63);
            cellFormats1.Append(cellFormat64);
            cellFormats1.Append(cellFormat65);
            cellFormats1.Append(cellFormat66);
            cellFormats1.Append(cellFormat67);
            cellFormats1.Append(cellFormat68);
            cellFormats1.Append(cellFormat69);
            cellFormats1.Append(cellFormat70);
            cellFormats1.Append(cellFormat71);
            cellFormats1.Append(cellFormat72);
            cellFormats1.Append(cellFormat73);
            cellFormats1.Append(cellFormat74);
            cellFormats1.Append(cellFormat75);
            cellFormats1.Append(cellFormat76);
            cellFormats1.Append(cellFormat77);
            cellFormats1.Append(cellFormat78);
            cellFormats1.Append(cellFormat79);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)4U };
            CellStyle cellStyle1 = new CellStyle() { Name = "Migliaia", FormatId = (UInt32Value)1U, BuiltinId = (UInt32Value)3U };
            CellStyle cellStyle2 = new CellStyle() { Name = "Normale", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };
            CellStyle cellStyle3 = new CellStyle() { Name = "Normale 2", FormatId = (UInt32Value)2U };
            CellStyle cellStyle4 = new CellStyle() { Name = "Percentuale", FormatId = (UInt32Value)3U, BuiltinId = (UInt32Value)5U };

            cellStyles1.Append(cellStyle1);
            cellStyles1.Append(cellStyle2);
            cellStyles1.Append(cellStyle3);
            cellStyles1.Append(cellStyle4);
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
            TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium9", DefaultPivotStyle = "PivotStyleLight16" };

            stylesheet1.Append(numberingFormats1);
            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);

            workbookStylesPart1.Stylesheet = stylesheet1;
        }

        private void GenerateThemePart1Content(ThemePart themePart1)
        {
            A.Theme theme1 = new A.Theme() { Name = "Tema di Office" };
            theme1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            A.ThemeElements themeElements1 = new A.ThemeElements();

            A.ColorScheme colorScheme1 = new A.ColorScheme() { Name = "Office" };

            A.Dark1Color dark1Color1 = new A.Dark1Color();
            A.SystemColor systemColor1 = new A.SystemColor() { Val = A.SystemColorValues.WindowText, LastColor = "000000" };

            dark1Color1.Append(systemColor1);

            A.Light1Color light1Color1 = new A.Light1Color();
            A.SystemColor systemColor2 = new A.SystemColor() { Val = A.SystemColorValues.Window, LastColor = "FFFFFF" };

            light1Color1.Append(systemColor2);

            A.Dark2Color dark2Color1 = new A.Dark2Color();
            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = "1F497D" };

            dark2Color1.Append(rgbColorModelHex1);

            A.Light2Color light2Color1 = new A.Light2Color();
            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex() { Val = "EEECE1" };

            light2Color1.Append(rgbColorModelHex2);

            A.Accent1Color accent1Color1 = new A.Accent1Color();
            A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex() { Val = "4F81BD" };

            accent1Color1.Append(rgbColorModelHex3);

            A.Accent2Color accent2Color1 = new A.Accent2Color();
            A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex() { Val = "C0504D" };

            accent2Color1.Append(rgbColorModelHex4);

            A.Accent3Color accent3Color1 = new A.Accent3Color();
            A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex() { Val = "9BBB59" };

            accent3Color1.Append(rgbColorModelHex5);

            A.Accent4Color accent4Color1 = new A.Accent4Color();
            A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex() { Val = "8064A2" };

            accent4Color1.Append(rgbColorModelHex6);

            A.Accent5Color accent5Color1 = new A.Accent5Color();
            A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex() { Val = "4BACC6" };

            accent5Color1.Append(rgbColorModelHex7);

            A.Accent6Color accent6Color1 = new A.Accent6Color();
            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "F79646" };

            accent6Color1.Append(rgbColorModelHex8);

            A.Hyperlink hyperlink1 = new A.Hyperlink();
            A.RgbColorModelHex rgbColorModelHex9 = new A.RgbColorModelHex() { Val = "0000FF" };

            hyperlink1.Append(rgbColorModelHex9);

            A.FollowedHyperlinkColor followedHyperlinkColor1 = new A.FollowedHyperlinkColor();
            A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex() { Val = "800080" };

            followedHyperlinkColor1.Append(rgbColorModelHex10);

            colorScheme1.Append(dark1Color1);
            colorScheme1.Append(light1Color1);
            colorScheme1.Append(dark2Color1);
            colorScheme1.Append(light2Color1);
            colorScheme1.Append(accent1Color1);
            colorScheme1.Append(accent2Color1);
            colorScheme1.Append(accent3Color1);
            colorScheme1.Append(accent4Color1);
            colorScheme1.Append(accent5Color1);
            colorScheme1.Append(accent6Color1);
            colorScheme1.Append(hyperlink1);
            colorScheme1.Append(followedHyperlinkColor1);

            A.FontScheme fontScheme17 = new A.FontScheme() { Name = "Office" };

            A.MajorFont majorFont1 = new A.MajorFont();
            A.LatinFont latinFont1 = new A.LatinFont() { Typeface = "Cambria" };
            A.EastAsianFont eastAsianFont1 = new A.EastAsianFont() { Typeface = "" };
            A.ComplexScriptFont complexScriptFont1 = new A.ComplexScriptFont() { Typeface = "" };
            A.SupplementalFont supplementalFont1 = new A.SupplementalFont() { Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
            A.SupplementalFont supplementalFont2 = new A.SupplementalFont() { Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont3 = new A.SupplementalFont() { Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont4 = new A.SupplementalFont() { Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont5 = new A.SupplementalFont() { Script = "Arab", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont6 = new A.SupplementalFont() { Script = "Hebr", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont7 = new A.SupplementalFont() { Script = "Thai", Typeface = "Tahoma" };
            A.SupplementalFont supplementalFont8 = new A.SupplementalFont() { Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont9 = new A.SupplementalFont() { Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont10 = new A.SupplementalFont() { Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont11 = new A.SupplementalFont() { Script = "Khmr", Typeface = "MoolBoran" };
            A.SupplementalFont supplementalFont12 = new A.SupplementalFont() { Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont13 = new A.SupplementalFont() { Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont14 = new A.SupplementalFont() { Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont15 = new A.SupplementalFont() { Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont16 = new A.SupplementalFont() { Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont17 = new A.SupplementalFont() { Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont18 = new A.SupplementalFont() { Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont19 = new A.SupplementalFont() { Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont20 = new A.SupplementalFont() { Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont21 = new A.SupplementalFont() { Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont22 = new A.SupplementalFont() { Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont23 = new A.SupplementalFont() { Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont24 = new A.SupplementalFont() { Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont25 = new A.SupplementalFont() { Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont26 = new A.SupplementalFont() { Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont27 = new A.SupplementalFont() { Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont28 = new A.SupplementalFont() { Script = "Viet", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont29 = new A.SupplementalFont() { Script = "Uigh", Typeface = "Microsoft Uighur" };

            majorFont1.Append(latinFont1);
            majorFont1.Append(eastAsianFont1);
            majorFont1.Append(complexScriptFont1);
            majorFont1.Append(supplementalFont1);
            majorFont1.Append(supplementalFont2);
            majorFont1.Append(supplementalFont3);
            majorFont1.Append(supplementalFont4);
            majorFont1.Append(supplementalFont5);
            majorFont1.Append(supplementalFont6);
            majorFont1.Append(supplementalFont7);
            majorFont1.Append(supplementalFont8);
            majorFont1.Append(supplementalFont9);
            majorFont1.Append(supplementalFont10);
            majorFont1.Append(supplementalFont11);
            majorFont1.Append(supplementalFont12);
            majorFont1.Append(supplementalFont13);
            majorFont1.Append(supplementalFont14);
            majorFont1.Append(supplementalFont15);
            majorFont1.Append(supplementalFont16);
            majorFont1.Append(supplementalFont17);
            majorFont1.Append(supplementalFont18);
            majorFont1.Append(supplementalFont19);
            majorFont1.Append(supplementalFont20);
            majorFont1.Append(supplementalFont21);
            majorFont1.Append(supplementalFont22);
            majorFont1.Append(supplementalFont23);
            majorFont1.Append(supplementalFont24);
            majorFont1.Append(supplementalFont25);
            majorFont1.Append(supplementalFont26);
            majorFont1.Append(supplementalFont27);
            majorFont1.Append(supplementalFont28);
            majorFont1.Append(supplementalFont29);

            A.MinorFont minorFont1 = new A.MinorFont();
            A.LatinFont latinFont2 = new A.LatinFont() { Typeface = "Calibri" };
            A.EastAsianFont eastAsianFont2 = new A.EastAsianFont() { Typeface = "" };
            A.ComplexScriptFont complexScriptFont2 = new A.ComplexScriptFont() { Typeface = "" };
            A.SupplementalFont supplementalFont30 = new A.SupplementalFont() { Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
            A.SupplementalFont supplementalFont31 = new A.SupplementalFont() { Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont32 = new A.SupplementalFont() { Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont33 = new A.SupplementalFont() { Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont34 = new A.SupplementalFont() { Script = "Arab", Typeface = "Arial" };
            A.SupplementalFont supplementalFont35 = new A.SupplementalFont() { Script = "Hebr", Typeface = "Arial" };
            A.SupplementalFont supplementalFont36 = new A.SupplementalFont() { Script = "Thai", Typeface = "Tahoma" };
            A.SupplementalFont supplementalFont37 = new A.SupplementalFont() { Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont38 = new A.SupplementalFont() { Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont39 = new A.SupplementalFont() { Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont40 = new A.SupplementalFont() { Script = "Khmr", Typeface = "DaunPenh" };
            A.SupplementalFont supplementalFont41 = new A.SupplementalFont() { Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont42 = new A.SupplementalFont() { Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont43 = new A.SupplementalFont() { Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont44 = new A.SupplementalFont() { Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont45 = new A.SupplementalFont() { Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont46 = new A.SupplementalFont() { Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont47 = new A.SupplementalFont() { Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont48 = new A.SupplementalFont() { Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont49 = new A.SupplementalFont() { Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont50 = new A.SupplementalFont() { Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont51 = new A.SupplementalFont() { Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont52 = new A.SupplementalFont() { Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont53 = new A.SupplementalFont() { Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont54 = new A.SupplementalFont() { Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont55 = new A.SupplementalFont() { Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont56 = new A.SupplementalFont() { Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont57 = new A.SupplementalFont() { Script = "Viet", Typeface = "Arial" };
            A.SupplementalFont supplementalFont58 = new A.SupplementalFont() { Script = "Uigh", Typeface = "Microsoft Uighur" };

            minorFont1.Append(latinFont2);
            minorFont1.Append(eastAsianFont2);
            minorFont1.Append(complexScriptFont2);
            minorFont1.Append(supplementalFont30);
            minorFont1.Append(supplementalFont31);
            minorFont1.Append(supplementalFont32);
            minorFont1.Append(supplementalFont33);
            minorFont1.Append(supplementalFont34);
            minorFont1.Append(supplementalFont35);
            minorFont1.Append(supplementalFont36);
            minorFont1.Append(supplementalFont37);
            minorFont1.Append(supplementalFont38);
            minorFont1.Append(supplementalFont39);
            minorFont1.Append(supplementalFont40);
            minorFont1.Append(supplementalFont41);
            minorFont1.Append(supplementalFont42);
            minorFont1.Append(supplementalFont43);
            minorFont1.Append(supplementalFont44);
            minorFont1.Append(supplementalFont45);
            minorFont1.Append(supplementalFont46);
            minorFont1.Append(supplementalFont47);
            minorFont1.Append(supplementalFont48);
            minorFont1.Append(supplementalFont49);
            minorFont1.Append(supplementalFont50);
            minorFont1.Append(supplementalFont51);
            minorFont1.Append(supplementalFont52);
            minorFont1.Append(supplementalFont53);
            minorFont1.Append(supplementalFont54);
            minorFont1.Append(supplementalFont55);
            minorFont1.Append(supplementalFont56);
            minorFont1.Append(supplementalFont57);
            minorFont1.Append(supplementalFont58);

            fontScheme17.Append(majorFont1);
            fontScheme17.Append(minorFont1);

            A.FormatScheme formatScheme1 = new A.FormatScheme() { Name = "Office" };

            A.FillStyleList fillStyleList1 = new A.FillStyleList();

            A.SolidFill solidFill1 = new A.SolidFill();
            A.SchemeColor schemeColor1 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill1.Append(schemeColor1);

            A.GradientFill gradientFill1 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList1 = new A.GradientStopList();

            A.GradientStop gradientStop1 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor2 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint1 = new A.Tint() { Val = 50000 };
            A.SaturationModulation saturationModulation1 = new A.SaturationModulation() { Val = 300000 };

            schemeColor2.Append(tint1);
            schemeColor2.Append(saturationModulation1);

            gradientStop1.Append(schemeColor2);

            A.GradientStop gradientStop2 = new A.GradientStop() { Position = 35000 };

            A.SchemeColor schemeColor3 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint2 = new A.Tint() { Val = 37000 };
            A.SaturationModulation saturationModulation2 = new A.SaturationModulation() { Val = 300000 };

            schemeColor3.Append(tint2);
            schemeColor3.Append(saturationModulation2);

            gradientStop2.Append(schemeColor3);

            A.GradientStop gradientStop3 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor4 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint3 = new A.Tint() { Val = 15000 };
            A.SaturationModulation saturationModulation3 = new A.SaturationModulation() { Val = 350000 };

            schemeColor4.Append(tint3);
            schemeColor4.Append(saturationModulation3);

            gradientStop3.Append(schemeColor4);

            gradientStopList1.Append(gradientStop1);
            gradientStopList1.Append(gradientStop2);
            gradientStopList1.Append(gradientStop3);
            A.LinearGradientFill linearGradientFill1 = new A.LinearGradientFill() { Angle = 16200000, Scaled = true };

            gradientFill1.Append(gradientStopList1);
            gradientFill1.Append(linearGradientFill1);

            A.GradientFill gradientFill2 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList2 = new A.GradientStopList();

            A.GradientStop gradientStop4 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor5 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade1 = new A.Shade() { Val = 51000 };
            A.SaturationModulation saturationModulation4 = new A.SaturationModulation() { Val = 130000 };

            schemeColor5.Append(shade1);
            schemeColor5.Append(saturationModulation4);

            gradientStop4.Append(schemeColor5);

            A.GradientStop gradientStop5 = new A.GradientStop() { Position = 80000 };

            A.SchemeColor schemeColor6 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade2 = new A.Shade() { Val = 93000 };
            A.SaturationModulation saturationModulation5 = new A.SaturationModulation() { Val = 130000 };

            schemeColor6.Append(shade2);
            schemeColor6.Append(saturationModulation5);

            gradientStop5.Append(schemeColor6);

            A.GradientStop gradientStop6 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor7 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade3 = new A.Shade() { Val = 94000 };
            A.SaturationModulation saturationModulation6 = new A.SaturationModulation() { Val = 135000 };

            schemeColor7.Append(shade3);
            schemeColor7.Append(saturationModulation6);

            gradientStop6.Append(schemeColor7);

            gradientStopList2.Append(gradientStop4);
            gradientStopList2.Append(gradientStop5);
            gradientStopList2.Append(gradientStop6);
            A.LinearGradientFill linearGradientFill2 = new A.LinearGradientFill() { Angle = 16200000, Scaled = false };

            gradientFill2.Append(gradientStopList2);
            gradientFill2.Append(linearGradientFill2);

            fillStyleList1.Append(solidFill1);
            fillStyleList1.Append(gradientFill1);
            fillStyleList1.Append(gradientFill2);

            A.LineStyleList lineStyleList1 = new A.LineStyleList();

            A.Outline outline1 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill2 = new A.SolidFill();

            A.SchemeColor schemeColor8 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade4 = new A.Shade() { Val = 95000 };
            A.SaturationModulation saturationModulation7 = new A.SaturationModulation() { Val = 105000 };

            schemeColor8.Append(shade4);
            schemeColor8.Append(saturationModulation7);

            solidFill2.Append(schemeColor8);
            A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline1.Append(solidFill2);
            outline1.Append(presetDash1);

            A.Outline outline2 = new A.Outline() { Width = 25400, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill3 = new A.SolidFill();
            A.SchemeColor schemeColor9 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill3.Append(schemeColor9);
            A.PresetDash presetDash2 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline2.Append(solidFill3);
            outline2.Append(presetDash2);

            A.Outline outline3 = new A.Outline() { Width = 38100, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill4 = new A.SolidFill();
            A.SchemeColor schemeColor10 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill4.Append(schemeColor10);
            A.PresetDash presetDash3 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline3.Append(solidFill4);
            outline3.Append(presetDash3);

            lineStyleList1.Append(outline1);
            lineStyleList1.Append(outline2);
            lineStyleList1.Append(outline3);

            A.EffectStyleList effectStyleList1 = new A.EffectStyleList();

            A.EffectStyle effectStyle1 = new A.EffectStyle();

            A.EffectList effectList1 = new A.EffectList();

            A.OuterShadow outerShadow1 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 20000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex11 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha1 = new A.Alpha() { Val = 38000 };

            rgbColorModelHex11.Append(alpha1);

            outerShadow1.Append(rgbColorModelHex11);

            effectList1.Append(outerShadow1);

            effectStyle1.Append(effectList1);

            A.EffectStyle effectStyle2 = new A.EffectStyle();

            A.EffectList effectList2 = new A.EffectList();

            A.OuterShadow outerShadow2 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex12 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha2 = new A.Alpha() { Val = 35000 };

            rgbColorModelHex12.Append(alpha2);

            outerShadow2.Append(rgbColorModelHex12);

            effectList2.Append(outerShadow2);

            effectStyle2.Append(effectList2);

            A.EffectStyle effectStyle3 = new A.EffectStyle();

            A.EffectList effectList3 = new A.EffectList();

            A.OuterShadow outerShadow3 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex13 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha3 = new A.Alpha() { Val = 35000 };

            rgbColorModelHex13.Append(alpha3);

            outerShadow3.Append(rgbColorModelHex13);

            effectList3.Append(outerShadow3);

            A.Scene3DType scene3DType1 = new A.Scene3DType();

            A.Camera camera1 = new A.Camera() { Preset = A.PresetCameraValues.OrthographicFront };
            A.Rotation rotation1 = new A.Rotation() { Latitude = 0, Longitude = 0, Revolution = 0 };

            camera1.Append(rotation1);

            A.LightRig lightRig1 = new A.LightRig() { Rig = A.LightRigValues.ThreePoints, Direction = A.LightRigDirectionValues.Top };
            A.Rotation rotation2 = new A.Rotation() { Latitude = 0, Longitude = 0, Revolution = 1200000 };

            lightRig1.Append(rotation2);

            scene3DType1.Append(camera1);
            scene3DType1.Append(lightRig1);

            A.Shape3DType shape3DType1 = new A.Shape3DType();
            A.BevelTop bevelTop1 = new A.BevelTop() { Width = 63500L, Height = 25400L };

            shape3DType1.Append(bevelTop1);

            effectStyle3.Append(effectList3);
            effectStyle3.Append(scene3DType1);
            effectStyle3.Append(shape3DType1);

            effectStyleList1.Append(effectStyle1);
            effectStyleList1.Append(effectStyle2);
            effectStyleList1.Append(effectStyle3);

            A.BackgroundFillStyleList backgroundFillStyleList1 = new A.BackgroundFillStyleList();

            A.SolidFill solidFill5 = new A.SolidFill();
            A.SchemeColor schemeColor11 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill5.Append(schemeColor11);

            A.GradientFill gradientFill3 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList3 = new A.GradientStopList();

            A.GradientStop gradientStop7 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor12 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint4 = new A.Tint() { Val = 40000 };
            A.SaturationModulation saturationModulation8 = new A.SaturationModulation() { Val = 350000 };

            schemeColor12.Append(tint4);
            schemeColor12.Append(saturationModulation8);

            gradientStop7.Append(schemeColor12);

            A.GradientStop gradientStop8 = new A.GradientStop() { Position = 40000 };

            A.SchemeColor schemeColor13 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint5 = new A.Tint() { Val = 45000 };
            A.Shade shade5 = new A.Shade() { Val = 99000 };
            A.SaturationModulation saturationModulation9 = new A.SaturationModulation() { Val = 350000 };

            schemeColor13.Append(tint5);
            schemeColor13.Append(shade5);
            schemeColor13.Append(saturationModulation9);

            gradientStop8.Append(schemeColor13);

            A.GradientStop gradientStop9 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor14 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade6 = new A.Shade() { Val = 20000 };
            A.SaturationModulation saturationModulation10 = new A.SaturationModulation() { Val = 255000 };

            schemeColor14.Append(shade6);
            schemeColor14.Append(saturationModulation10);

            gradientStop9.Append(schemeColor14);

            gradientStopList3.Append(gradientStop7);
            gradientStopList3.Append(gradientStop8);
            gradientStopList3.Append(gradientStop9);

            A.PathGradientFill pathGradientFill1 = new A.PathGradientFill() { Path = A.PathShadeValues.Circle };
            A.FillToRectangle fillToRectangle1 = new A.FillToRectangle() { Left = 50000, Top = -80000, Right = 50000, Bottom = 180000 };

            pathGradientFill1.Append(fillToRectangle1);

            gradientFill3.Append(gradientStopList3);
            gradientFill3.Append(pathGradientFill1);

            A.GradientFill gradientFill4 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList4 = new A.GradientStopList();

            A.GradientStop gradientStop10 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor15 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint6 = new A.Tint() { Val = 80000 };
            A.SaturationModulation saturationModulation11 = new A.SaturationModulation() { Val = 300000 };

            schemeColor15.Append(tint6);
            schemeColor15.Append(saturationModulation11);

            gradientStop10.Append(schemeColor15);

            A.GradientStop gradientStop11 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor16 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade7 = new A.Shade() { Val = 30000 };
            A.SaturationModulation saturationModulation12 = new A.SaturationModulation() { Val = 200000 };

            schemeColor16.Append(shade7);
            schemeColor16.Append(saturationModulation12);

            gradientStop11.Append(schemeColor16);

            gradientStopList4.Append(gradientStop10);
            gradientStopList4.Append(gradientStop11);

            A.PathGradientFill pathGradientFill2 = new A.PathGradientFill() { Path = A.PathShadeValues.Circle };
            A.FillToRectangle fillToRectangle2 = new A.FillToRectangle() { Left = 50000, Top = 50000, Right = 50000, Bottom = 50000 };

            pathGradientFill2.Append(fillToRectangle2);

            gradientFill4.Append(gradientStopList4);
            gradientFill4.Append(pathGradientFill2);

            backgroundFillStyleList1.Append(solidFill5);
            backgroundFillStyleList1.Append(gradientFill3);
            backgroundFillStyleList1.Append(gradientFill4);

            formatScheme1.Append(fillStyleList1);
            formatScheme1.Append(lineStyleList1);
            formatScheme1.Append(effectStyleList1);
            formatScheme1.Append(backgroundFillStyleList1);

            themeElements1.Append(colorScheme1);
            themeElements1.Append(fontScheme17);
            themeElements1.Append(formatScheme1);
            A.ObjectDefaults objectDefaults1 = new A.ObjectDefaults();
            A.ExtraColorSchemeList extraColorSchemeList1 = new A.ExtraColorSchemeList();

            theme1.Append(themeElements1);
            theme1.Append(objectDefaults1);
            theme1.Append(extraColorSchemeList1);

            themePart1.Theme = theme1;
        }

        #endregion
    }
}
