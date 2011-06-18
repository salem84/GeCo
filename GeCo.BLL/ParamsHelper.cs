using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.DAL;
using GeCo.Model;
using Microsoft.Practices.ServiceLocation;
using GeCo.Infrastructure;

namespace GeCo.BLL
{
    public class ParamsHelper
    {
        public static string GetParamValueStr(string nome)
        {
            var paramRepos = ServiceLocator.Current.GetInstance<IRepository<Parametro>>();

            Parametro param = paramRepos.SingleOrDefault(p => p.Nome == nome);

            return param != null ? param.Valore : null;
            
        }

        public static int GetParamValueInt(string nome)
        {
            string value = GetParamValueStr(nome);
            return value != null ? Convert.ToInt32(value) : 0;
        }
    }
}
