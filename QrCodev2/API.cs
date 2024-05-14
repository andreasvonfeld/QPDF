using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace QrCodev2
{
    public class HttpClientSingleton
    {
        private static HttpClient _instance;
        private static readonly object _lock = new object();

        private HttpClientSingleton() { }

        public static HttpClient Instance
        {
            get
            {
                // Utilisation d'un double verrouillage pour assurer la sécurité en cas de multithreading
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new HttpClient();
                            string apiKey = "key";
                            _instance.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
