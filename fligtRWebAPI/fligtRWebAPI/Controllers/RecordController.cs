using System;
using System.Web.Http;
using Model;
using Repo;
using System.Linq;

namespace fligtRWebAPI.Controllers
{
    [RoutePrefix("api/record")]
    public class RecordController : ApiController
    {
        UnitOfWork work = new UnitOfWork();

        [Route("getall/{id}")]// kayıları getir
        public MobileResult GetRecords(int id)
        {
            MobileResult result = new MobileResult();
            result.Result = true;

            try
            {
                var records = work.RecordRepository.Get(x=>x.UserId == id, q=>q.OrderByDescending(x=>x.CreatedDate), "");
                

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
        [Route("getbyid")] //id ile kayıt getir
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
        [Route("insert")] //kayıt ekle
        public MobileResult InsertRecord(Record model)
        {
            MobileResult result = new MobileResult();
            result.Result = true;
            try
            {
                work.RecordRepository.Insert(model);
                work.Save();
                result.Message = "New Record created";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [Route("getlast/{id}")]  //son eklenen kaydı getir
        public Record GetLast(int id)
        {
            var record = work.RecordRepository.GetLast(x => x.UserId == id, q => q.OrderByDescending(x => x.CreatedDate), "");
            return record;
        }

        [HttpPost]
        [Route("delete")] //kayıt sil
        public MobileResult DeleteRecord(Record model)
        {
            MobileResult result = new MobileResult();
            result.Result = true;
            try
            {
                //Logging
                work.RecordRepository.Delete(model.Id);
                work.Save();
                result.Message = "Selected Record has been deleted";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("update")]  //kayıt güncelle
        public MobileResult UpdateRecord(Record model)
        {
            MobileResult result = new MobileResult();
            result.Result = true;
            try
            {
                var record = GetRecords(model);
                var recordModel = (Record)record.Data;
                recordModel.CreatedDate = DateTime.Now;
                work.RecordRepository.Update(recordModel);
                work.Save();
                result.Message = "Selected Record has been updated";
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
