using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTradutor.Helpers
{
    class TranslateHelper
    {
        public static string ParametrosTraducao(string expressao)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("?client=webapp");
            sb.Append("&sl=auto&");
            sb.Append("tl =pt&");
            sb.Append("hl =pt-BR&");
            sb.Append("dt =at&");
            sb.Append("dt=bd&");
            sb.Append("dt=ex&");
            sb.Append("dt=ld&");
            sb.Append("dt=md&");
            sb.Append("dt=qca&");
            sb.Append("dt=rw&");
            sb.Append("dt=rm&");
            sb.Append("dt=ss&");
            sb.Append("dt=t&");
            sb.Append("dt=gt&");
            sb.Append("otf=1&");
            sb.Append("ssel=0&");
            sb.Append("tsel=0&");
            sb.Append("kc=5&");
            sb.Append("tk=782078.871213&");
            sb.Append($"q={expressao}");
            return sb.ToString();
        }
    }
}
