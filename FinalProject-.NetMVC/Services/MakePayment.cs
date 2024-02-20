using Rent_a_Car_.Net.Models;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Rent_a_Car_.Net.Services
{
    public class MakePayment
    {
        public static async Task<dynamic> PayAsync(Payment payment)
        {
            try
            {

                StripeConfiguration.ApiKey = "sk_test_51LxSARGxJ1Zu4nZzSIc3HFCEPEeOdXoYgRdGD2Bmux1dhoIuybitQXDeNUNpGTk7fegTkfyCw0KxfCokS6U556x500Wtgzk2jy";
                var optionstoken = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Name = payment.CardHolder,
                        Number = payment.CardNumber,
                        ExpMonth = payment.Month,
                        ExpYear = payment.Year,
                        Cvc = payment.Cvc
                    }
                };

                var serviceToken = new TokenService();
                Token stripeToken = await serviceToken.CreateAsync(optionstoken);

                var options = new ChargeCreateOptions
                {
                    Amount = payment.Value,
                    Currency = "AZN",
                    Description = "icarə ödənişi",
                    Source = stripeToken.Id
                };

                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);

                if (charge.Paid)
                {
                    return "Success";
                }
                else
                {
                    return "Failed";
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
