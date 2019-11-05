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

            // 隧道代理服务器ip/host
            string proxy_ip = "tps136.kdlapi.com";
            int proxy_port = xx;

            // 用户名密码 <隧道代理>
            string username = "yourusername";
            string password = "yourpassword";

            // 设置代理 <隧道代理已添加白名单>
            //request.Proxy = new WebProxy(proxy_ip, proxy_port);

            // 设置代理 <隧道代理未添加白名单>
            WebProxy proxy = new WebProxy();
            proxy.Address = new Uri(String.Format("http://{0}:{1}", proxy_ip, proxy_port));
            proxy.Credentials = new NetworkCredential(username, password);
            request.Proxy = proxy;

            // 请求目标网页
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine((int)response.StatusCode);  // 获取状态码
            // 解压缩读取返回内容
            using (StreamReader reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))) {
                Console.WriteLine(reader.ReadToEnd());
            }
            
        }
    }
}
