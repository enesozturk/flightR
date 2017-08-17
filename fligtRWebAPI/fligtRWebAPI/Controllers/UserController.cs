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
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        UnitOfWork work = new UnitOfWork();

        [Route("users")]
        public MobileResult GetRecords()
        {
            MobileResult result = new MobileResult();
            result.Result = true;

            try
            {
                var records = work.RecordRepository.Get();

                result.Data = records;
                result.Message = "Success";
            }
            catch (Exception e)
            {
                result.Result = false;
                result.Message = e.Message;
            }
            return result;
        }
    }
}
