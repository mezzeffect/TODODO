using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Web.Http;
using AutoMapper;
using TodoSampleBackend.DataObjects;
using Microsoft.Azure.Mobile.Server.Login;
using TodoSampleBackend.Business;
using TodoSampleBackend.Interfaces.Business;
using TodoSampleBackend.Models;

namespace TodoSampleBackend.Controllers
{
    [Route(".auth/login/custom")]
    public class CustomAuthController : ApiController
    {
        private readonly IUserServiceManager _userServiceManager;

        public CustomAuthController(IUserServiceManager userServiceManager)
        {
            _userServiceManager = userServiceManager;
        }

        public IHttpActionResult Post([FromBody] LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                var userBusinessObject =  _userServiceManager.Authenticate(Mapper.Map<LoginBusinessObject>(loginRequest));
                if (userBusinessObject != null) // user-defined function, checks against a database
                {
                    var token = AppServiceLoginHandler.CreateToken(
                                new[] { new Claim(JwtRegisteredClaimNames.Sub, loginRequest.UserName) },
                                Environment.GetEnvironmentVariable("WEBSITE_AUTH_SIGNING_KEY"),
                                ConfigurationManager.AppSettings["ValidAudience"],
                                ConfigurationManager.AppSettings["ValidIssuer"],
                                TimeSpan.FromHours(24));
                    UserModel userModel = null;
                    userModel = Mapper.Map<UserModel>(userBusinessObject);
                    return Ok(new LoginRepsponse
                    {
                        AuthenticationToken = token.RawData,
                        User = userModel,
                        IsSuccessful = true
                    });
                }
            }
            // user authentication was not successfull
            return Unauthorized();
        }
    }
}

