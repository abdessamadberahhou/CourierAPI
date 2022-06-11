using System.Collections.Generic;
using CourierApi.Models.courier;
using CourierApi.Models.file;

namespace CourierApi.Models.Responses
{
    public class FetchCourierResponse
    {
        public IEnumerable<Courrier> Courier { get; set; }
        public int page { get; set; }
        public int CurrentPage { get; set; }
        public int totalCourrier { get; set; }
    }
}
