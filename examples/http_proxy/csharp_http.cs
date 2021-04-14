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

            // 代理服务器
            string proxy_ip = "59.38.241.25";
            int proxy_port = 23916;

            // 用户名密码 <私密代理/独享代理>
            string username = "myusername";
            string password = "mypassword";
            
            // 设置代理 <开放代理或私密/独享代理&已添加白名单>
            // request.Proxy = new WebProxy(proxy_ip, proxy_port);
            
            // 设置代理 <私密/独享代理&未添加白名单>
            WebProxy proxy = new WebProxy();
            proxy.Address = new Uri(String.Format("http://{0}:{1}", proxy_ip, proxy_port));
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
