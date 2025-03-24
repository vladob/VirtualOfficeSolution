using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Attachment
    {
        public int Id { get; set; } // Primary Key
        public string? DocumentId { get; set; }
        public string? DownloadUrl { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
    }
}
