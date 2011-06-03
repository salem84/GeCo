using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.DAL;

namespace GeCo.Utility
{
    public class ParamsHelper
    {
        public static string GetParamValueStr(string nome)
        {
            using(PavimentalContext context = new PavimentalContext())
            {
                Parametro param = context.Parametri.Find(nome);
                if(param != null)
                    return param.Valore;
                else
                    return null;
            }
        }

        public static int GetParamValueInt(string nome)
        {
            using (PavimentalContext context = new PavimentalContext())
            {
                Parametro param = context.Parametri.Find(nome);
                if (param != null)
                    return Convert.ToInt32(param.Valore);
                else
                    return 0;
            }
        }
    }
}
