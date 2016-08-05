using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPortfolio.Models
{
    public class File
    {
        public int FileId { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }
        public FileType FileType { get; set; }

        [ForeignKey("AProject")]
        public Guid ProjectId { get; set; }

        public virtual  AProject AProject { get; set; }
    }
}