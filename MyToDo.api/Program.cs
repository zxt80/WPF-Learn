using Microsoft.EntityFrameworkCore;
using MyToDo.api.Context.Repository;
using MyToDo.api.Context;
using Arch.EntityFrameworkCore.UnitOfWork;
using MyToDo.api.Service;
using AutoMapper;
using MyToDo.api.Extentions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    #region 版本配置

    #endregion

    #region 配置展示注释信息
    // xml 文件路径
    var file = Path.Combine(AppContext.BaseDirectory, "MyToDo.api.xml");
    // true: 显示控制器层注释
    option.IncludeXmlComments(file, true);
    // 对action的名称进行排序，如果有多个就有效果了
    option.OrderActionsBy(o => o.RelativePath);

    #endregion


    #region 拓展传入token
    // 定义JwtBearer认证方式一
    option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description="请输入token，格式为: Bearer XXXXXXXXXXXXXXX (注意中间必须要有空格)",
        Name="Authorization",                                   //jwt 默认名称
        In=Microsoft.OpenApi.Models.ParameterLocation.Header,   //jwt默认存放Authorization信息的位置(请求头中)
        Type =Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat="JWT",
        Scheme="Bearer"
    });

    //定义JwtBearer认证方式二
    //options.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
    //{
    //    Description = "这是方式二(JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）)",
    //    Name = "Authorization",//jwt默认的参数名称
    //    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
    //    Type = SecuritySchemeType.ApiKey
    //});

    //声明一个Scheme，注意下面的Id要和上面AddSecurityDefinition中的参数name一致
    var scheme = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };

    // 添加安全要求
    option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        [scheme] = new string[0]
    });
    #endregion
});

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
builder.Services.AddTransient<IMemoService, MemoService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IFileService, FileService>();

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
