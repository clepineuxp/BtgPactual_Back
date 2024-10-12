using Autofac;
using Autofac.Extensions.DependencyInjection;
using BtgPactual.Back.Api;
using BtgPactual.Back.Core;
using BtgPactual.Back.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new CoreModule());
        containerBuilder.RegisterModule(new InfrastructureModule());
    });

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

