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

// 绑定数据库
string? connectionString = builder.Configuration.GetConnectionString("ToDoConnections");
builder.Services.AddDbContext<MyToDoContext>(opt =>
{
    opt.UseSqlite(connectionString);
});

// NuGet控制台执行以下命令
// Add-Migration InitialCreate 生成数据库迁移文件：
// Remove-Migration 移除上一步的操作
// Update-Database  将迁移文件的的结果导入到数据库中


builder.Services.AddUnitOfWork<MyToDoContext>();
builder.Services.AddCustomRepository<ToDo, ToDoRepository>();
builder.Services.AddCustomRepository<Memo, MemoRepository>();
builder.Services.AddCustomRepository<User, UserRepository>();

//
builder.Services.AddTransient<IToDoService, ToDoService>();

// 配置automapper
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
