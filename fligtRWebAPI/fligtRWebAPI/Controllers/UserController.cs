using Model;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace fligtRWebAPI.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        UnitOfWork work = new UnitOfWork();

        [Route("users")]
        [HttpPost]
        public User GetUser(User user)
        {
            User userResult = new Model.User();
            try
            {
                userResult = work.UserRepository.GetLast((x => x.UserName == user.UserName && x.Password == user.Password), null, "");


                return userResult;
            }
            catch (Exception e)
            {
                
            }
            return userResult;
        }
    }
}
