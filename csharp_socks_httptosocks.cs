using System;
using System.Net.Http;
using System.IO;
using System.IO.Compression;
using MihaZupan;

namespace httptosocks
{
    class Program
    {
        static void Main(string[] args)
        {
            // 目标网页
            string page_url = "http://dev.kdlapi.com/testproxy";

            // 代理服务器
            string proxy_ip = "59.38.241.25";
            int proxy_port = 23916;

            // 用户名密码 <私密代理/独享代理>
            string username = "myusername";
            string password = "mypassword";

            // 设置代理 <开放代理或私密/独享代理&已添加白名单>
            // var proxy = new HttpToSocks5Proxy(new[] {
            //     new ProxyInfo(proxy_ip, proxy_port),
            // });

            // 设置Socks5代理 <私密/独享代理&未添加白名单>
            var proxy = new HttpToSocks5Proxy(new[] {
                new ProxyInfo(proxy_ip, proxy_port, username, password),
            });

            // 请求目标网页
            var handler = new HttpClientHandler { Proxy = proxy };
            HttpClient httpClient = new HttpClient(handler, true);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, page_url);
            httpRequestMessage.Headers.Add("Accept-Encoding", "Gzip");  // 使用gzip压缩传输数据让访问更快
            var httpsGet = httpClient.SendAsync(httpRequestMessage);

            var result = httpsGet.Result;
            Console.WriteLine((int)result.StatusCode);  // 获取状态码

            // 解压缩读取返回内容
            using (StreamReader reader = new StreamReader(new GZipStream(result.Content.ReadAsStreamAsync().Result, CompressionMode.Decompress))) {
                Console.WriteLine(reader.ReadToEnd());
            }
        }
    }
}
