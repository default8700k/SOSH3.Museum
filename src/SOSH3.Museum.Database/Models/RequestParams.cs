using System.Net;

namespace SOSH3.Museum.Database.Models
{
    public class RequestParams
    {
        public IPAddress? Ip { get; set; }
        public string Url { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string UserAgent { get; set; } = null!;
        public DateTime DateTime { get; set; }
    }
}
