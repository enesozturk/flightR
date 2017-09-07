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

        [Route("getall")] //pointleri getir
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

        [HttpPost]
        [Route("insert")]  //point ekle
        public MobileResult InsertPoint(Point model)
        {
            MobileResult result = new MobileResult();
            result.Result = true;
            try
            {
                work.PointRepository.Insert(model);
                work.Save();
                result.Message = "New Point created";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("insertmany")]
        public MobileResult InsertMany(List<Point> pointList)
        {
            MobileResult result = new MobileResult();
            result.Result = true;
            try
            {
                work.PointRepository.InsertMany(pointList);
                work.Save();
                result.Message = "New Records created";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
