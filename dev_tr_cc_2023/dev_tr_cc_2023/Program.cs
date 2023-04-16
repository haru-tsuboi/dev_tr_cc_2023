using System;
using System.Net;
using System.Net.Sockets;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        int listenPort = 1234; // 受信するUDPパケットのポート番号
        string apiEndpoint = "https://httpbin.org/post"; // APIのエンドポイント
        string httpMethod = "POST"; // HTTPメソッド

        string requestBody = ""; // リクエストボディ

        UdpClient udpClient = new UdpClient(listenPort);
        Console.WriteLine($"Listening for UDP packets on port {listenPort}...");

        while (true)
        {
            IPEndPoint remoteEP = null;
            byte[] data = udpClient.Receive(ref remoteEP);
            Console.WriteLine($"Received a UDP packet from {remoteEP.Address}:{remoteEP.Port}");

            SendApiRequest(apiEndpoint, httpMethod, requestBody).Wait();
            Console.WriteLine("API request sent successfully.");
        }
    }

    static async Task SendApiRequest(string url, string method, string body)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(method), url);
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}