
using library_Api;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Tous les services du projet ApplicationLayer doivent �tre ajout�s ici
IServiceCollection services = builder.Services;
StartupConfigurationEvolutive.InjecterServices(services);

StartupConfigurationEvolutive.InjecterRepositories(services);
//services.AddAutoMapper(
//    typeof(library_domain.Models.Member),
//   typeof(library_domain.Models.ConcreteMember));



// Add services to the container.

builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());

            });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAutoMapper(typeof(MemberProfil));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseOpenApi();
    //app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
