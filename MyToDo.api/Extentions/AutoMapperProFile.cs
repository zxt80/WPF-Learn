using AutoMapper;
using MyToDo.api.Context;
using MyToDo.shared.Dtos;

namespace MyToDo.api.Extentions
{
    public class AutoMapperProFile:MapperConfigurationExpression
    {
        public AutoMapperProFile() {
            CreateMap<ToDo, ToDoDto>().ReverseMap();
        }
    }
}
