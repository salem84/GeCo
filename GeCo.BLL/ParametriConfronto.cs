using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Infrastructure;

namespace GeCo.BLL
{
    public class ParametriConfronto
    {
        public int PMAX_HrTotali { get { return PMAX_HrDiscrezionali + PMAX_HrComportamentali; } }
        public int PMAX_HrDiscrezionali { get; set; }
        public int PMAX_HrComportamentali { get; set; }
        public int PMAX_LineaTotali { get { return PMAX_Comportamentali + PMAX_TecnicTotali; } }
        public int PMAX_Comportamentali { get; set; }
        public int PMAX_TecnicTotali { get { return PMAX_TecnStrategicSupport + PMAX_TecnStrategicSupport; } }
        public int PMAX_TecnStrategicSupport { get; set; }
        public int PMAX_TecnCompetitiveAdv { get; set; }
        public int PERC_SogliaFoundational { get; set; }

        public ParametriConfronto()
        {
            PMAX_HrDiscrezionali = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_HR_DISCREZIONALI);
            PMAX_HrComportamentali = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_HR_COMPORTAMENTALI);
            PMAX_Comportamentali = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_COMPORTAMENTALI);
            PMAX_TecnStrategicSupport = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_TECN_STRATEGIC);
            PMAX_TecnCompetitiveAdv = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_TECN_COMPETITIVE);
            PERC_SogliaFoundational = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PERCENTUALE_SOGLIA_FOUNDATIONAL);
        }
    }
}
