using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FacilAssist.Front.Helper
{
    public class FuncoesString
    {
        public static string AplicarMascaraCpf(string cpfSemMascara)
        {
            
            if (string.IsNullOrEmpty(cpfSemMascara) || cpfSemMascara.Length != 11)
            {
                return null;
            }            
            return string.Format(@"{0:000\.000\.000\-00}", Convert.ToInt64(cpfSemMascara));
        }

        public static string RemoverFormatacao(string texto)
        {
       
            if (string.IsNullOrEmpty(texto))
            {
                return string.Empty;
            }
            return Regex.Replace(texto, @"[^0-9]", "");
        }
    }
}