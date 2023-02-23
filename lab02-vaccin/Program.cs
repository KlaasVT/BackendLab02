var builder = WebApplication.CreateBuilder(args);
var csvConfig = builder.Configuration.GetSection("CsvConfig");
builder.Services.Configure<CsvConfig>(csvConfig);
builder.Services.AddTransient<IVaccinationLocationRepository, CsvVaccinationLocationRepository>();
builder.Services.AddTransient<IVaccinTypeRepository, CsvVaccinTypeRepository>();
builder.Services.AddTransient<IVaccinRegistrationRepository, CsvVaccinRegistrationRepository>();
builder.Services.AddTransient<IVaccinService, VaccinService>();
builder.Services.AddValidatorsFromAssemblyContaining<RegistrationValidator>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/locations", (IVaccinService vaccinService) => Results.Ok(vaccinService.GetLocations()));

// app.MapGet("/registrations",(IMapper mapper,IVaccinService vaccinService) =>{
//     return Results.Ok(vaccinService.GetRegistrations());
// });

app.MapGet("/registrations", (IMapper mapper, IVaccinService vaccinationService) =>
{
    // var mapped = mapper.Map<List<VaccinRegistrationDTO>>(vaccinationService.GetRegistrations());
    var mapped = mapper.Map<List<VaccinRegistrationDTO>>(vaccinationService.GetRegistrations(), opts =>
    {
        opts.Items["locations"] = vaccinationService.GetLocations();
        opts.Items["vaccins"] = vaccinationService.GetTypes();
    });
    return Results.Ok(mapped);
});

app.MapGet("/types", (IVaccinService vaccinService) => Results.Ok(vaccinService.GetTypes()));

app.MapPost("/registrations", (IMapper mapper, IValidator<VaccinRegistration> validator, IVaccinService vaccinService, VaccinRegistration registration) =>
{
    var validatorResult = validator.Validate(registration);
    if (!validatorResult.IsValid)
    {
        var errors = validatorResult.Errors.Select(err => new { errors = err.ErrorMessage });
        return Results.BadRequest(errors);
    }
    registration.VaccinatinRegistrationId = Guid.NewGuid();
    vaccinService.AddRegistration(registration);
    return Results.Created($"/registrations/{registration.VaccinatinRegistrationId}", registration);
});

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerFeature>()
        ?.Error;
    if (exception is not null)
    {
        var response = new { error = exception.Message };
        context.Response.StatusCode = 400;

        await context.Response.WriteAsJsonAsync(response);
    }
}));

app.Run("http://localhost:5000");
