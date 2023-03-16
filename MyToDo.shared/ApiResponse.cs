namespace MyToDo.shared
{
    public class ApiResponse
    {
        public ApiResponse() { }
        public ApiResponse(string message, bool status = false)
        {
            Message = message;
            Status = status;
        }
        public ApiResponse(bool status,object result)
        {
            Status = status;
            Result = result;
        }

        public string Message { set; get; }

        public bool Status { get; set; }

        public object Result { set; get; }
    }

    public class ApiResponse<T>
    {
        public ApiResponse()
        {

        }
        public ApiResponse(string message, bool status = false)
        {
            Message = message;
            Status = status;
        }
        public ApiResponse(bool status, T result)
        {
            Status = status;
            Result = result;
        }

        public string Message { set; get; }

        public bool Status { get; set; }

        public T Result { set; get; }
    }
}
