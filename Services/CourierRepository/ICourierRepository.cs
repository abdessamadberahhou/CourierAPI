using System;
using System.Threading.Tasks;
using CourierApi.Models.courier;
using CourierApi.Models.Users;

namespace CourierApi.Services.CourierRepository
{
    public interface ICourierRepository
    {
        Task<Courrier> CreateCourier(Courrier user);
        Task<Courrier> ModifyCourier(Courrier user, Guid id);
        Task<Courrier> DeleteCourier(Guid id);
    }
}
