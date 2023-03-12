namespace MyToDo.api.Context
{
    public class User: BaseEntity
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string Passwd { get; set; }
    }
}
