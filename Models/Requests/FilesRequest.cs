using System;

namespace CourierApi.Models.Requests
{
    public class FileRequest
    {
        public Guid Id { get; set; }
        public string IdCourrier { get; set; }
        public byte[] FILE { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
    }
}
