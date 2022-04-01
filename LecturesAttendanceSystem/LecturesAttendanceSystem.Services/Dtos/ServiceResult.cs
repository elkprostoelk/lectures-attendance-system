using System.Collections.Generic;

namespace LecturesAttendanceSystem.Services.Dtos
{
    public class ServiceResult
    {
        public bool IsSuccessful { get; set; }
        
        public object ResultObject { get; set; }
        
        public Dictionary<string, string> Errors { get; set; }

        public ServiceResult()
        {
            Errors = new Dictionary<string, string>();
            IsSuccessful = true;
        }
    }
}