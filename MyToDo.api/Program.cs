using Microsoft.EntityFrameworkCore;
using MyToDo.api.Context.Repository;
using MyToDo.api.Context;
using Arch.EntityFrameworkCore.UnitOfWork;
using MyToDo.api.Service;
using AutoMapper;
using MyToDo.api.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// �����ݿ�
string? connectionString = builder.Configuration.GetConnectionString("ToDoConnections");
builder.Services.AddDbContext<MyToDoContext>(opt =>
{
    opt.UseSqlite(connectionString);
});

// NuGet����ִ̨����������
// Add-Migration InitialCreate �������ݿ�Ǩ���ļ���
// Remove-Migration �Ƴ���һ���Ĳ���
// Update-Database  ��Ǩ���ļ��ĵĽ�����뵽���ݿ���


builder.Services.AddUnitOfWork<MyToDoContext>();
builder.Services.AddCustomRepository<ToDo, ToDoRepository>();
builder.Services.AddCustomRepository<Memo, MemoRepository>();
builder.Services.AddCustomRepository<User, UserRepository>();

//
builder.Services.AddTransient<IToDoService, ToDoService>();

// ����automapper
var automapper = new MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperProFile());
});
builder.Services.AddSingleton(automapper.CreateMapper());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
