using System;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace csharp_http
{
    class Program
    {
        static void Main(string[] args)
        {
            // 要访问的目标网页
            string page_url = "http://dev.kdlapi.com/testproxy";

            // 构造请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(page_url);
            request.Method = "GET";
            request.Headers.Add("Accept-Encoding", "Gzip");  // 使用gzip压缩传输数据让访问更快
            // request.KeepAlive = false   // 出现复用之前的IP时使用

            // 隧道域名、端口号
            string tunnelhost = "tpsXXX.kdlapi.com";
            int tunnelport = 15818;

            // 用户名密码, 若已添加白名单则不需要添加
            string username = "username";
            string password = "password";

            // 设置代理 <IP白名单>
            // request.Proxy = new WebProxy(tunnelhost, tunnelport);

            // 设置代理 <用户名密码>
            WebProxy proxy = new WebProxy();
            proxy.Address = new Uri(String.Format("http://{0}:{1}", tunnelhost, tunnelport));
            proxy.Credentials = new NetworkCredential(username, password);
            request.Proxy = proxy;

            // 请求目标网页
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine((int)response.StatusCode);  // 获取状态码
            // 解压缩读取返回内容
            using (StreamReader reader =  new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))) {
                Console.WriteLine(reader.ReadToEnd());
            }
        }
    }
}