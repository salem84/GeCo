using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.BLL.Excel
{
    public class ExcelHelper
    {
        public static string GetColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = String.Empty;
            int modifier;

            while (dividend > 0)
            {
                modifier = (dividend - 1) % 26;
                columnName =
                    Convert.ToChar(65 + modifier).ToString() + columnName;
                dividend = (int)((dividend - modifier) / 26);
            }

            return columnName;
        }
    }
}
