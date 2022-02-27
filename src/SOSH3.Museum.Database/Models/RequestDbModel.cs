using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace SOSH3.Museum.Database.Models
{
    [Table("requests", Schema = "models")]
    public class RequestDbModel
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("ip")]
        public IPAddress? Ip { get; set; }

        [Column("url")]
        public string Url { get; set; } = null!;

        [Column("method")]
        public string Method { get; set; } = null!;

        [Column("user_agent")]
        public string UserAgent { get; set; } = null!;

        [Column("date_time")]
        public DateTime DateTime { get; set; }
    }
}
