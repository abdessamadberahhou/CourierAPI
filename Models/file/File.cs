using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CourierApi.Models.file
{
    public partial class File
    {
        public Guid IdFile { get; set; }
        public Guid IdCourrier { get; set; }
        public byte[] File1 { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
    }
}
