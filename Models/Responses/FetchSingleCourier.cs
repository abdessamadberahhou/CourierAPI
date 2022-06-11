using System.Collections.Generic;
using CourierApi.Models.courier;
using CourierApi.Models.file;

namespace CourierApi.Models.Responses
{
    public class FetchSingleCourier
    {
        public Courrier Courier { get; set; }
        public IEnumerable<File> files { get; set; }
    }
}
