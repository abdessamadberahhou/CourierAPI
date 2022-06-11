using System;
using System.Collections.Generic;

#nullable disable

namespace CourierApi.Models.courier
{
    public partial class Courrier
    {
        public Guid Id { get; set; }
        public string TypeCourrier { get; set; }
        public string ObjetCourrier { get; set; }
        public string ExpiditeurCourrier { get; set; }
        public string DestinataireCourrier { get; set; }
        public DateTime? DateCourrier { get; set; }
        public string TagsCourrier { get; set; }
        public int? CourrierFavoriser { get; set; }
        public int? CourrierArchiver { get; set; }
        public int? CourrierUrgent { get; set; }
        public Guid? IdUser { get; set; }
    }
}
