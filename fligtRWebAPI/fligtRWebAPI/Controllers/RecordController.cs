using System;
using System.Web.Http;
using Model;
using Repo;

namespace fligtRWebAPI.Controllers
{
    [RoutePrefix("api/record")]
    public class RecordController : ApiController
    {
        UnitOfWork work = new UnitOfWork();
        flighREntities db = new flighREntities();
        [Route("getall")]
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

        [HttpPost]
        [Route("getbyid")]
        public MobileResult GetRecords(Record model)
        {
            MobileResult result = new MobileResult();
            result.Result = true;
            try
            {
                var student = work.RecordRepository.GetById(model.Id);

                result.Data = student;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("insert")]
        public MobileResult InsertStudent(Record model)
        {
            MobileResult result = new MobileResult();
            result.Result = true;
            try
            {
                work.RecordRepository.Insert(model);
                work.Save();
                result.Message = "New Student has been added";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("delete")]
        public MobileResult DeleteStudent(Record model)
        {
            MobileResult result = new MobileResult();
            result.Result = true;
            try
            {
                //Logging
                work.RecordRepository.Delete(model.Id);
                work.Save();
                result.Message = "Selected Student has been deleted";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public MobileResult UpdateStudent(Record model)
        {
            MobileResult result = new MobileResult();
            result.Result = true;
            try
            {
                var record = GetRecords(model);
                var recordModel = (Record)record.Data;
                recordModel.Latitude = model.Latitude;
                recordModel.Longitude = model.Longitude;
                work.RecordRepository.Update(recordModel);
                work.Save();
                result.Message = "Selected Student has been updated";
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
