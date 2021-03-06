﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.BLL.Excel;

namespace GeCo.BLL.Services
{
    public class ExcelServices : IExcelServices
    {

        public void EsportaExcel(string filepath, string nomeRuolo, List<ConfrontoConoscenzaCompetenza> confronti)
        {
            ExcelCreator excel = new ExcelCreator();
            excel.NomeRuolo = nomeRuolo;
            excel.Dati = confronti;
            excel.PunteggiMassimi = new ParametriConfronto();

            excel.CreaExcel(filepath);
        }
    }
}
