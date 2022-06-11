using System;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models.Requests
{
    public class AddCourierRequest
    {
        [Required]
        public string TypeCourrier { get; set; }
        [Required]
        public string ObjetCourrier { get; set; }
        [Required]
        public string ExpiditeurCourrier { get; set; }
        [Required]
        public string DestinataireCourrier { get; set; }
        [Required]
        public DateTime? DateCourrier { get; set; }
        public string TagsCourrier { get; set; }
        public int CourrierUrgent { get; set; }
        public Guid idUser { get; set; }
        public FileRequest[] files { get; set; }
    }
}
