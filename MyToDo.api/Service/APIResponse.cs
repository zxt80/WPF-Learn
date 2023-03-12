namespace MyToDo.api.Service
{
    public class APIResponse
    {
        public APIResponse(string message, bool status = false)
        {
            this.Message = message;
            this.Status = status;
        }
        public APIResponse(bool status, Object result) 
        {
            this.Status = status;
            this.Result = result;
        }

        public string Message { set; get; }

        public bool Status { get; set; }  

        public Object Result { set; get; }
    }
}
