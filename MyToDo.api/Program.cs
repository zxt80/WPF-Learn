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
    #region �汾����

    #endregion

    #region ����չʾע����Ϣ
    // xml �ļ�·��
    var file = Path.Combine(AppContext.BaseDirectory, "MyToDo.api.xml");
    // true: ��ʾ��������ע��
    option.IncludeXmlComments(file, true);
    // ��action�����ƽ�����������ж������Ч����
    option.OrderActionsBy(o => o.RelativePath);

    #endregion


    #region ��չ����token
    // ����JwtBearer��֤��ʽһ
    option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description="������token����ʽΪ: Bearer XXXXXXXXXXXXXXX (ע���м����Ҫ�пո�)",
        Name="Authorization",                                   //jwt Ĭ������
        In=Microsoft.OpenApi.Models.ParameterLocation.Header,   //jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
        Type =Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat="JWT",
        Scheme="Bearer"
    });

    //����JwtBearer��֤��ʽ��
    //options.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
    //{
    //    Description = "���Ƿ�ʽ��(JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�)",
    //    Name = "Authorization",//jwtĬ�ϵĲ�������
    //    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
    //    Type = SecuritySchemeType.ApiKey
    //});

    //����һ��Scheme��ע�������IdҪ������AddSecurityDefinition�еĲ���nameһ��
    var scheme = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };

    // ��Ӱ�ȫҪ��
    option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        [scheme] = new string[0]
    });
    #endregion
});

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
builder.Services.AddTransient<IMemoService, MemoService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IFileService, FileService>();

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
