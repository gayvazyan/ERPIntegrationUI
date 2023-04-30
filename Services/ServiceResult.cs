using System.Collections.Generic;

namespace lega.Core
{
    public class ServiceResult<TObject>
    {

        public TObject Entity { get; set; }
        public static ServiceResult<TObject> Success { get; }
        public List<ServiceError> Errors { get; set; }
        public bool Succeeded { get; set; }

        public ServiceResult()
        {
            Errors = new List<ServiceError>();
        }

        public ServiceResult(TObject entity, bool isSuccess)
        {
            Entity = entity;
            Succeeded = isSuccess;
        }
    }


    public class ServiceError
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
