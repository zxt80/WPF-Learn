﻿namespace MyToDo.shared.Dtos
{
    public class ToDoDto : BaseDto,ICloneable
    {
        private string title;
        private string content;
        private int status;

        public string Title
        {
            get { return title; } 
            set { title = value;OnPropertyChanged(); } 
        }

        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }

        public int Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }

        public object Clone()
        {
            ToDoDto toDoDto = new ToDoDto();
            toDoDto.Title = Title;
            toDoDto.Content = Content;
            toDoDto.Status = Status;
            toDoDto.Id = Id;

            return toDoDto;
        }
    }
}