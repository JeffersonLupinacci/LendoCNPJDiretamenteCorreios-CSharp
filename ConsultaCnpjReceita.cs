using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading;
using System.Threading.Tasks;

namespace LeituraWeb
{
    public class ConsultaCnpjReceita
    {
        private String _erro;
        private CookieContainer cookieContainer = new CookieContainer();
        private String UrlDominio = "http://www.receita.fazenda.gov.br";
        private String UrlBase = "http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/";
        private String UrlGet = "cnpjreva_solicitacao2.asp";
        private String UrlPost = "Valida.asp";
        private String viewState;
        private String RetornoHtml = "";
        public String ErroDetectado { get{ return _erro;} }

        #region Recupera a Imagem do Captcha
        /// <summary>
        /// Chamada inicial da classe, resposável por popular a imagem e criar os cookies em memória
        /// para serem confrontados no ato do post de validação
        /// </summary>
        /// <returns></returns>
        public Bitmap RecuperaCaptcha(){
            String HtmlResponse;            
            _erro = null;            
            int PosString;
            String StrViewState = "<input type=hidden id=viewstate name=viewstate value='";
            String StrImagemCaptcha = "<img border='0' id='imgcaptcha' alt='Imagem com os caracteres anti rob";
            String UrlImagemCaptcha = "";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(UrlBase + UrlGet);
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "GET";
            httpWebRequest.AllowAutoRedirect = false;
            httpWebRequest.Timeout = 20000;
            try{
                StreamReader stHtml = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
                HtmlResponse = stHtml.ReadToEnd();
                stHtml.Close();
                viewState = HtmlResponse;
                PosString = viewState.IndexOf(StrViewState);
                if (PosString >= 0)
                    viewState = viewState.Substring(PosString + StrViewState.Length);
                PosString = viewState.IndexOf("'>");
                if (PosString >= 0)
                    viewState = viewState.Substring(0, PosString);
                UrlImagemCaptcha = HtmlResponse;
                PosString = UrlImagemCaptcha.IndexOf(StrImagemCaptcha);
                if (PosString >= 0)
                    UrlImagemCaptcha = UrlImagemCaptcha.Substring(PosString + 8 + StrImagemCaptcha.Length);
                PosString = UrlImagemCaptcha.IndexOf("'>");
                if (PosString >= 0)
                    UrlImagemCaptcha = UrlImagemCaptcha.Substring(0, PosString);
                UrlImagemCaptcha = UrlImagemCaptcha.Replace("amp;", "");
            } catch (Exception ex) {
                _erro = ex.Message;
            }                       
            try{
                if (UrlImagemCaptcha.Length > 0)
                    return new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(UrlDominio + UrlImagemCaptcha)));
                else
                    return null;
            }
            catch (Exception ex){
                _erro = ex.Message;
                return null;
            }
        }
        #endregion

        #region Consulta os Dados na Sefaz
        /// <summary>
        /// Consulta a Secretaria da Fazenda os Dados enviados para retornar o Cartão com os Dados do CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="captcha"></param>
        public void Consulta(string cnpj, string captcha){
            _erro = null;
            if (cnpj.Length == 0)
                _erro = "CNPJ não informado";
            else if (captcha.Length == 0)
                _erro = "Imagem de Validação não informado";            
            if (_erro == null) {                         
                // Montando a Lista de Parametros para o post
                var parametros = "origem=comprovante&" +
                                 "viewstate=" + System.Uri.EscapeDataString(viewState) + "&" +
                                 "cnpj=" + System.Uri.EscapeDataString(cnpj) + "&" +
                                 "captcha=" + System.Uri.EscapeDataString(captcha) + "&" +
                                 "captchaAudio=&" +
                                 "submit1=Consultar&" +
                                 "search_type=cnpj";
                try{
                    MyHttpPost(parametros);
                }
                catch (Exception ex){
                    _erro = "Ocorreu uma excessão ao consultar o site da Receita Federal\n\n" + ex.Message;
                }
            }
        }
        #endregion

        #region Retorno em Formato Html
        /// <summary>
        /// Retorno em Html da Requisição enviada a Sefaz
        /// </summary>
        /// <returns></returns>
        public string RetornoEmHtml(){
            return RetornoHtml;
        }
        #endregion

        #region Execução do Post com os parametros preenchidos
        private void MyHttpPost(String parametros){
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(UrlBase + UrlPost);
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 20000;
            httpWebRequest.AllowAutoRedirect = false;
            httpWebRequest.ContentLength = parametros.Length;
            try{
                StreamWriter stParametros = new StreamWriter(httpWebRequest.GetRequestStream(), Encoding.GetEncoding("ISO-8859-1"));
                stParametros.Write(parametros);
                stParametros.Close();
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                // Encaminha para o autoredirecionamento
                if (response.StatusCode == HttpStatusCode.Found){
                    string redirUrl = response.Headers["Location"];
                    MyHttpGet(redirUrl);
                }
                // Encaminha o Retorno do Html com Erro de Validação
                if (response.StatusCode == HttpStatusCode.OK){
                    StreamReader stHtml = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
                    MontaRetorno(stHtml.ReadToEnd());
                    stHtml.Close();
                }
                response.Close();
            }
            catch (Exception ex) {
                _erro = ex.Message;
            }
        }
        #endregion

        #region AutoRedirecionamento planejado
        private void MyHttpGet(String url){
            if (!url.Contains("http")){
                url = UrlBase + url;
            }
            try{
                HttpWebRequest httpWebRequestGet = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequestGet.CookieContainer = cookieContainer;
                httpWebRequestGet.ContentType = "application/x-www-form-urlencoded";
                httpWebRequestGet.Method = WebRequestMethods.Http.Get;
                httpWebRequestGet.AllowAutoRedirect = false;
                var task = Task.Factory.FromAsync<WebResponse>(httpWebRequestGet.BeginGetResponse, httpWebRequestGet.BetterEndGetResponse, this).ContinueWith(asyncResult =>
                {
                    var response = asyncResult.Result as HttpWebResponse;
                    string redirUrl = response.Headers["Location"];
                    if (response.StatusCode == HttpStatusCode.Found){
                        MyHttpGet(redirUrl);
                    } else{
                        if (response.StatusCode == HttpStatusCode.OK){
                            StreamReader stHtml = new StreamReader(httpWebRequestGet.GetResponse().GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
                            MontaRetorno(stHtml.ReadToEnd());
                            stHtml.Close();
                        }
                    }
                    response.Close();
                });
            }
            catch (Exception ex) {
                _erro = ex.Message;
            }
        }
        #endregion

        private void MontaRetorno(String response){
            if (response.Contains("Erro na Consulta")){
                _erro = "A imagem de validação se encontra incorreta. Verifique e tente novamente.";
            }
            RetornoHtml = response;
        }

        internal void PreencheFicha(CadastroCNPJ cad)
        {
            if (RetornoHtml.Length > 0) {
                if (RetornoHtml.Contains("Erro na Consulta")) {
                    throw new Exception("A imagem de validação se encontra incorreta. Verifique e tente novamente.");
                }
            }
            if (RetornoHtml.Length == 0) {
                throw new Exception("Não foi possível recuperar um retorno da Secretaria da Fazenda. Verifique e tente novamente.");
            }
        }
    }

    #region WebRequestExtensions
    public static class WebRequestExtensions
    {
        public static WebResponse BetterEndGetResponse(this WebRequest request, IAsyncResult asyncResult)
        {
            try{
                return request.EndGetResponse(asyncResult);
            }
            catch (WebException wex){
                if (wex.Response != null){
                    return wex.Response;
                }
                throw;
            }
        }

        public static WebResponse BetterGetResponse(this WebRequest request)
        {
            try{
                return request.GetResponse();
            }
            catch (WebException wex){
                if (wex.Response != null){
                    return wex.Response;
                }
                throw;
            }
        }
    }
    #endregion

}

