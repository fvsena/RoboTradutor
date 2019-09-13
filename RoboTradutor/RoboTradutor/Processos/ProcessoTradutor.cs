using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoboTradutor.Models;
using RoboTradutor.Helpers;
using System.Net;

namespace RoboTradutor.Processos
{
    public class ProcessoTradutor : IProcesso
    {
        private int etapa = 0;
        private RequestHelper requestHelper = null;
        public void Erro()
        {
            throw new NotImplementedException();
        }

        public void Successo()
        {
            throw new NotImplementedException();
        }

        public Resultado Iniciar()
        {
            Resultado resultado = new Resultado();

            try
            {
                do
                {
                    switch (etapa)
                    {
                        case 0:
                            AcessaTradutor();
                            break;
                        case 1:
                            EnviaTraducao("start");
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                    etapa++;

                } while (etapa < 5);
            }
            catch (Exception ex)
            {
                resultado.Sucesso = false;
                resultado.Mensagem = ex.Message;
            }

            return resultado;
        }

        private void AcessaTradutor()
        {
            try
            {
                requestHelper = new RequestHelper("https://translate.google.com/");
                requestHelper.Host("translate.google.com");
                requestHelper.KeepAlive(true);
                requestHelper.AddRequestHeader("Upgrade-Insecure-Requests","1");
                requestHelper.UserAgent("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
                requestHelper.Accept("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                requestHelper.AddRequestHeader("Sec-Fetch-Site", "none");
                requestHelper.AddRequestHeader(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                requestHelper.AddRequestHeader(HttpRequestHeader.AcceptLanguage, "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
                requestHelper.ExecuteRequest("GET");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EnviaTraducao(string expressao)
        {
            try
            {
                requestHelper.LoadRequest("https://translate.google.com/translate_a/single" + TranslateHelper.ParametrosTraducao(expressao));
                requestHelper.Host("translate.google.com");
                requestHelper.KeepAlive(true);
                requestHelper.AddRequestHeader("Upgrade-Insecure-Requests", "1");
                requestHelper.UserAgent("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
                requestHelper.Accept("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                requestHelper.AddRequestHeader("Sec-Fetch-Site", "none");
                requestHelper.AddRequestHeader(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                requestHelper.AddRequestHeader(HttpRequestHeader.AcceptLanguage, "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
                requestHelper.ExecuteRequest("GET");
                string response = requestHelper.GetResponseBody();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
