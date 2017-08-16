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
    [RoutePrefix("api/point")]
    public class PointController : ApiController
    {
        UnitOfWork work = new UnitOfWork();
        flighREntities db = new flighREntities();
        [Route("getall")]
        public MobileResult GetPoints()
        {
            MobileResult result = new MobileResult();
            result.Result = true;
            try
            {
                var records = work.PointRepository.Get();

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
