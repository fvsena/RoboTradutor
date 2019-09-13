using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace RoboTradutor.Helpers
{
    public class RequestHelper
    {
        private CookieContainer cookies = new CookieContainer();
        private HttpWebRequest request = null;
        private HttpWebResponse response = null;
        public string UrlBase;

        public RequestHelper(string url)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookies;

        }

        public void LoadRequest(string url)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookies;
        }

        public void LoadRequest(string url, string metodo)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookies;
            request.Method = metodo;
        }

        public void ExecuteRequest(string metodo)
        {
            request.Method = metodo;
            response = (HttpWebResponse)request.GetResponse();
        }

        /// <summary>
        /// Cria uma requisição e carrega a resposta no objeto
        /// </summary>
        /// <param name="endereco">URI da requisição</param>
        /// <param name="metodo">Método</param>
        /// <param name="postData">Dados do post</param>
        public void ExecuteRequest(string metodo, string postData = null)
        {
            request.Method = metodo;
            if (!string.IsNullOrEmpty(postData))
            {
                LoadPostData(postData);
            }
            response = (HttpWebResponse)request.GetResponse();
        }

        public void ExecuteRequest(string url, string metodo, string postData = null)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookies;

            request.Method = metodo;
            if (!string.IsNullOrEmpty(postData))
            {
                LoadPostData(postData);
            }
            response = (HttpWebResponse)request.GetResponse();
        }

        /// <summary>
        /// Carrega os dados do PostData
        /// </summary>
        /// <param name="postData">Conteúdo do PostData</param>
        private void LoadPostData(string postData)
        {
            byte[] data = Encoding.ASCII.GetBytes(postData);
            request.ContentLength = data.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// Retorna o conteúdo da resposta
        /// </summary>
        public string GetResponseBody()
        {
            string conteudo = null;
            if (response != null)
            {
                Stream stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    conteudo = reader.ReadToEnd();
                }
            }
            return conteudo;
        }

        public void AddRequestHeader(string header, string value)
        {
            if (request == null) throw new Exception("Não é possível adicionar o cabeçalho a uma requisição nula");
            request.Headers.Set(header, value);
        }

        public void AddRequestHeader(HttpRequestHeader header, string value)
        {
            if (request == null) throw new Exception("Não é possível adicionar o cabeçalho a uma requisição nula");
            request.Headers.Set(header, value);
        }

        public void Accept(string value)
        {
            if (request == null) throw new Exception("Não é possível adicionar o cabeçalho a uma requisição nula");
            request.Accept = value;
        }

        public void UserAgent(string valor)
        {
            if (request == null) throw new Exception("Não é possível adicionar o cabeçalho a uma requisição nula");
            request.UserAgent = valor;
        }

        public void ContentType(string valor)
        {
            if (request == null) throw new Exception("Não é possível adicionar o cabeçalho a uma requisição nula");
            request.ContentType = valor;
        }

        public void Connection(string valor)
        {
            if (request == null) throw new Exception("Não é possível adicionar o cabeçalho a uma requisição nula");
            request.Connection = valor;
        }

        public void KeepAlive(bool valor)
        {
            if (request == null) throw new Exception("Não é possível adicionar o cabeçalho a uma requisição nula");
            request.KeepAlive = valor;
        }

        public void Referer(string valor)
        {
            if (request == null) throw new Exception("Não é possível adicionar o cabeçalho a uma requisição nula");
            request.Referer = valor;
        }

        public void Host(string valor)
        {
            if (request == null) throw new Exception("Não é possível adicionar o cabeçalho a uma requisição nula");
            request.Host = valor;
        }

        public void WriteMultipartBodyToRequest(string body)
        {
            string[] multiparts = Regex.Split(body, @"<!>");
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                foreach (string part in multiparts)
                {
                    if (File.Exists(part))
                    {
                        bytes = File.ReadAllBytes(part);
                    }
                    else
                    {
                        bytes = Encoding.UTF8.GetBytes(part.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n"));
                    }
                    ms.Write(bytes, 0, bytes.Length);
                }
                request.ContentLength = ms.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    ms.WriteTo(stream);
                }
            }
        }
    }
}
