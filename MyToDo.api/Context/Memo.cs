﻿namespace MyToDo.api.Context
{
    public class Memo: BaseEntity
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public int Status { get; set; }
    }
}
