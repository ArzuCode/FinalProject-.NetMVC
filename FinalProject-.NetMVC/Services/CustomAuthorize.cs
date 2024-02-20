using Rent_a_Car_.Net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Rent_a_Car_.Net.Services
{

    public class CustomAuthorize : AuthorizationFilterAttribute
    {
        
    }
}
