using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Protocols;

namespace CodedUITestProject1.Common
{
    public class HttpHelper
    {
        /// <summary>
        /// 执行WebRequest请求，获取服务器的响应字符串
        /// </summary>
        /// <param name="requestUriString">标识 Internet 资源的 URI</param>
        /// <param name="requestData">请求数据</param>
        /// <param name="proxyIP">代理IP</param>
        /// <param name="timeOut">超时值（以毫秒为单位）</param>
        /// <param name="method">请求的方法</param>
        /// <param name="encodName">首选编码的代码页名称</param>
        /// <param name="contentType"> Content-typeHTTP 标头的值</param>
        /// <param name="headers">构成 HTTP 标头的名称/值对的集合。</param>
        /// <param name="credentials">设置请求的身份验证信息</param>
        /// <param name="throwWebException">出现WebException时是否抛出，默认抛出：true</param>
        /// <returns>返回来自 Internet 资源的响应字符串</returns>        

        public static string WebRequestToServer(string requestUriString, string requestData, string proxyIP, int timeOut,
            string method = "POST", string encodName = "utf-8", string contentType = "text", WebHeaderCollection headers = null, ICredentials credentials = null, bool throwWebException = true, bool writeLog = true)
        {
            string responseMessage = null;
            using (MemoryStream stream = WebResponseStream(requestUriString, requestData, proxyIP, timeOut, method, encodName, contentType, headers, credentials, throwWebException, writeLog))
            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(encodName)))
            {
                responseMessage = reader.ReadToEnd();
            }
            responseMessage = HttpHelper.Decode(responseMessage);
            //if (writeLog)
            //{
            //    SSLogging.SSLogger.LogInfo("[{0}] Url:{1}; Response:{2}", method, requestUriString, responseMessage);
            //    SSLogging.SSLogger.Flush();
            //}
            return responseMessage;
        }


        /// <summary>
        /// 执行WebRequest请求，获取服务器的响应数据流
        /// </summary>
        /// <param name="requestUriString">标识 Internet 资源的 URI</param>
        /// <param name="requestData">请求数据</param>
        /// <param name="proxyIP">代理IP</param>
        /// <param name="timeOut">超时值（以毫秒为单位）</param>
        /// <param name="method">请求的方法</param>
        /// <param name="encodName">首选编码的代码页名称</param>
        /// <param name="contentType"> Content-typeHTTP 标头的值</param>
        /// <param name="headers">构成 HTTP 标头的名称/值对的集合。</param>
        /// <param name="credentials">设置请求的身份验证信息</param>
        /// <param name="throwWebException">出现WebException时是否抛出，默认抛出：true</param>
        /// <returns>返回来自 Internet 资源的响应数据流</returns>        
        public static MemoryStream WebResponseStream(string requestUriString, string requestData, string proxyIP, int timeOut,
            string method = "POST", string encodName = "utf-8", string contentType = "text", WebHeaderCollection headers = null, ICredentials credentials = null, bool throwWebException = true, bool writeLog = true)
        {

            Encoding encoding = Encoding.GetEncoding(encodName);
            requestUriString = (method == "GET" && !string.IsNullOrEmpty(requestData)) ? string.Format("{0}?{1}", requestUriString, requestData) : requestUriString;

            //if (writeLog)
            //{
            //    SSLogging.SSLogger.LogInfo("[{0}] Url:{1}; Request:{2}", method, requestUriString, requestData);
            //    SSLogging.SSLogger.Flush();
            //}

            HttpWebRequest httpRequest = WebRequest.Create(requestUriString) as HttpWebRequest;
            //Setting.SetProxy(httpRequest, proxyIP);
            httpRequest.Method = method;
            httpRequest.Timeout = timeOut;
            httpRequest.Credentials = credentials;

            if (headers != null && headers.Count > 0)
                httpRequest.Headers = headers;

            switch ((contentType ?? "").ToLower())
            {
                case "xml": httpRequest.ContentType = string.Format("application/xml;charset={0}", encodName); break;
                case "json": httpRequest.ContentType = string.Format("application/json;charset={0}", encodName); break;
                case "text": httpRequest.ContentType = string.Format("application/x-www-form-urlencoded;charset={0}", encodName); break;
                case "html": httpRequest.ContentType = string.Format("text/html;charset={0}", encodName); break;
                case "soap":
                    {
                        httpRequest.ContentType = string.Format("text/xml;charset={0}", encodName);
                        httpRequest.Headers.Add("SOAPAction", "");
                    }
                    break;
                default: httpRequest.ContentType = null; break;
            }
            if (method == "POST" || method == "PUT")
            {
                httpRequest.ContentLength = 0;
                if (!string.IsNullOrEmpty(requestData))
                {
                    byte[] dataArray = encoding.GetBytes(requestData);
                    httpRequest.ContentLength = dataArray.Length;
                    using (Stream stream = httpRequest.GetRequestStream())
                    {
                        stream.Write(dataArray, 0, dataArray.Length);
                    }
                }
            }
            MemoryStream responseStream = new MemoryStream();
            try
            {
                using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    stream.CopyTo(responseStream);
                }
            }
            catch (WebException e)
            {
                if (e.Message.Contains("timed out"))
                {
                    var bytes = Encoding.UTF8.GetBytes("{\"Error\":\"调用超时\"}");
                    responseStream.Write(bytes, 0, bytes.Length);
                }
                else
                {
                    if (!throwWebException && e.Response != null)
                    {
                        using (WebResponse response = e.Response)
                        using (Stream stream = response.GetResponseStream())
                        {
                            stream.CopyTo(responseStream);
                        }
                    }
                    else
                        throw e;
                }
            }
            httpRequest.Abort();
            responseStream.Seek(0, SeekOrigin.Begin);
            return responseStream;
        }


        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="upfile"></param>
        /// <param name="service"></param>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static string UploadFile(string upfile, byte[] bs)
        {
            //文件上传地址
            //string uploadFileService = System.Configuration.ConfigurationManager.AppSettings["UploadFileService"];
            //Fat  http://ws.uploadfile.fx.fws.qa.nt.ctripcorp.com/fileupload/api/files/post
            //Uat  http://ws.uploadfile.fx.uat.sh.ctriptravel.com/fileupload/api/files/post
            //Prod http://ws.uploadfile.fx.uat.qa.nt.ctripcorp.com/fileupload/api/files/post

            string uploadFileService = "http://ws.uploadfile.fx.fws.qa.nt.ctripcorp.com/fileupload/api/files/post";

            try
            {
                //string upfile = "{\"Channel\":\"ttd\",\"Type\":\"1\"}";
                string content = string.Empty;
                string EncodingString = string.Empty;
                string json = JsonConvert.SerializeObject(bs);
                byte[] byteArray = Encoding.Default.GetBytes(json);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uploadFileService);
                request.Headers.Add("ctrip_upfile", upfile);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length);
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (string.IsNullOrEmpty(response.ContentType))
                {
                    EncodingString = "UTF-8";
                }
                else
                {
                    EncodingString = response.ContentType.Split(';')[1].Split('=')[1];
                }

                using (
                    StreamReader sr = new StreamReader(response.GetResponseStream(),
                        Encoding.GetEncoding(EncodingString)))
                {
                    content = sr.ReadToEnd();
                }
                //SSLogging.SSLogger.LogInfo("文件上传:" + content.Replace("\\", string.Empty).TrimStart('"').TrimEnd('"'));
                //SSLogging.SSLogger.Flush();

                return content.Replace("\\", string.Empty).TrimStart('"').TrimEnd('"');
            }
            catch (Exception ex)
            {
                //SSLogging.SSLogger.LogWarning("文件上传出错:" + ex);
                //SSLogging.SSLogger.Flush();
                return "";
            }
            finally
            {

            }
        }


        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string DownLoadFile(string path)
        {
            //var fileService = System.Configuration.ConfigurationManager.AppSettings["DownloadfileService"];//文件下载服务地址
            //Fat  http://ws.downloadfile.fx.fws.qa.nt.ctripcorp.com/filedownload/api/files/get
            //Uat  http://ws.downloadfile.fx.uat.sh.ctriptravel.com/filedownload/api/files/get
            //Prod http://ws.downloadfile.fx.ctripcorp.com/filedownload/api/files/get

            string fileService = "http://ws.downloadfile.fx.fws.qa.nt.ctripcorp.com/filedownload/api/files/get";
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("Content-Type", "application/json");
                    var longUrlStr = wc.DownloadData(fileService + "?path=" + HttpUtility.UrlEncode(path));
                    var html = Encoding.UTF8.GetString(longUrlStr);
                    //SSLogging.SSLogger.LogInfo("文件下载:" + path + "|||" + html + "|||" + html.Replace("\\", string.Empty).TrimStart('"').TrimEnd('"'));
                    //SSLogging.SSLogger.Flush();

                    return html.Replace("\\", string.Empty).TrimStart('"').TrimEnd('"');
                }
            }
            catch (Exception ex)
            {
                //SSLogging.SSLogger.LogWarning("文件下载出错:" + ex);
                //SSLogging.SSLogger.Flush();

                return "";
            }
        }

        public static string SoapService(SoapHttpClientProtocol webService, string webServiceMethod, object[] webServiceParameters)
        {
            object response = null;

            try
            {
                //SSLogging.SSLogger.LogInfo("SoapService:{0}; Method:{1}; Parameter:{2}", webService.Url, webServiceMethod, string.Join<object>("\r\n", webServiceParameters));
                //SSLogging.SSLogger.Flush();

                response = webService.GetType().GetMethod(webServiceMethod).Invoke(webService, webServiceParameters);
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            string result = (response ?? "").ToString();
            //SSLogging.SSLogger.LogInfo("SoapService:{0}; Method:{1}; Result:{2}", webService.Url, webServiceMethod, result);
            //SSLogging.SSLogger.Flush();

            return result;
        }

        public static string Decode(string s)
        {
            Regex reUnicode = new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.Compiled);
            return reUnicode.Replace(s, m =>
            {
                short c;
                if (short.TryParse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out c))
                {
                    return "" + (char)c;
                }
                return m.Value;
            });
        }

        public static string WebClientData(string url, string method, NameValueCollection data, string proxyIP)
        {
            string returnStr = string.Empty;
            WebClient WebClientObj = new System.Net.WebClient();
            //HttpHelper.SetProxy(WebClientObj, proxyIP);
            try
            {
                byte[] byRemoteInfo = WebClientObj.UploadValues(url, method, data);
                returnStr = System.Text.Encoding.UTF8.GetString(byRemoteInfo);
            }
            catch (Exception ex)
            {
                returnStr = ex.ToString();
            }
            return returnStr;
        }

        public static void SetProxy(WebClient wc, string proxyIP)
        {
            string isProxy = "true"; //config.IsProxy;
            string ipProxy = proxyIP;
            string user = ""; //config.User;
            string pwd = ""; //config.Pwd;
            string domain = ""; //config.Domain;

            if (isProxy.ToUpper() == "TRUE")
            {
                WebProxy proxy = new WebProxy(ipProxy, true);
                proxy.Credentials = new NetworkCredential(user, pwd, domain);
                wc.Proxy = proxy;
            }
        }


    }
}
