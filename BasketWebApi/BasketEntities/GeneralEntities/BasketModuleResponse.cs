using System;

namespace BasketModuleEntities.GeneralEntities
{
    public enum StatusType
    {
        Error = 0,
        Success = 1

    }
    public class BasketModuleResponse
    {
        public BasketModuleResponse()
        {
            ResponseTime = DateTime.Now;
        }

        public StatusType Status { get; set; }
        public DateTime ResponseTime { get; set; }
        public string Message { get; set; }
        public object SuccessObject { get; set; }
        public object ErrorObject { get; set; }
    }
}
