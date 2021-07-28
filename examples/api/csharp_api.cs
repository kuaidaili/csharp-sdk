using System;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace csharp_api
{
    class Program
    {
        static void Main(string[] args)
        {
            // api链接
            string api_url = "http://dev.kdlapi.com/api/getproxy/?orderid=96518362xxxxxx&num=100&protocol=1&method=2&an_ha=1&sep=1";

            // 请求目标网页
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api_url);
            request.Method = "GET";
            request.Headers.Add("Accept-Encoding", "Gzip");  // 使用gzip压缩传输数据让访问更快
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine((int)response.StatusCode);  // 获取状态码
            // 解压缩读取返回内容
            using (StreamReader reader =  new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))) {
                Console.WriteLine(reader.ReadToEnd());
            }
        }
    }
}
