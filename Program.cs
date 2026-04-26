using DHC_FSAP.Data;
using DHC_FSAP.Services.About;
using DHC_FSAP.Services.Auth;
using DHC_FSAP.Services.Blogs;
using DHC_FSAP.Services.Booking;
using DHC_FSAP.Services.Contact;
using DHC_FSAP.Services.jwt;
using DHC_FSAP.Services.Services;
using DHC_FSAP.Services.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using DHC_FSAP.Services.Portfolios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
 //Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<EmailService>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IContactService,  ContactService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryService>();
builder.Services.AddScoped<IBlogService,  BlogService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IServiceCategoryService,  ServiceCategoryService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IBookingService , BookingService>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<IPortfolioCategoryService, PortfolioCategoryService>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer (options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])
        )
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT")?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();