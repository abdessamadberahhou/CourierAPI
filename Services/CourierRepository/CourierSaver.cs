using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using CourierApi.Models.courier;
using Microsoft.EntityFrameworkCore;

namespace CourierApi.Services.CourierRepository
{
    public class CourierSaver : ICourierRepository
    {
        public Task<Courrier> CreateCourier(Courrier courier)
        {
            courier.Id = Guid.NewGuid();
            using (var context = new CouriersContext())
            {
                    context.Courriers.Add(courier);
                    context.SaveChanges();
                    return Task.FromResult(courier);

            }
        }

        public Task<Courrier> DeleteCourier(Guid id)
        {
            using (var context = new CouriersContext())
            {
                var courrier = context.Courriers.Where(c => c.Id == id).FirstOrDefault();
                if (courrier != null)
                {
                    context.Courriers.Remove(courrier);
                    context.SaveChanges();
                    return Task.FromResult(courrier);
                }
                else
                {
                    return Task.FromResult(courrier);
                }
            }
        }

        public Task<Courrier> ModifyCourier(Courrier c, Guid id)
        {
            using (var context = new CouriersContext())
            {
                var courrier = context.Courriers.Where(c => c.Id == id).FirstOrDefault();
                if(courrier != null)
                {
                    courrier.TypeCourrier = c.TypeCourrier;
                    courrier.ObjetCourrier = c.ObjetCourrier;
                    courrier.ExpiditeurCourrier = c.ExpiditeurCourrier;
                    courrier.DestinataireCourrier = c.DestinataireCourrier;
                    courrier.TagsCourrier = c.TagsCourrier;
                    courrier.DateCourrier = c.DateCourrier;
                    courrier.CourrierUrgent = c.CourrierUrgent;
                    context.SaveChanges();
                    return Task.FromResult(courrier);
                }
                else
                {
                    return Task.FromResult(courrier); ;
                }
            }
        }
    }
}
