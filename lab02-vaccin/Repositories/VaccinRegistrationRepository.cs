namespace lab02_vaccin.Repositories;

public interface IVaccinRegistrationRepository

{
    List<VaccinRegistration> GetRegistrations();
    public void AddRegistration(VaccinRegistration registration);

}

public class VaccinRegistrationRepository : IVaccinRegistrationRepository
{

    private static List<VaccinRegistration> _registrations = new List<VaccinRegistration>();

    public VaccinRegistrationRepository()
    {
        if (!(_registrations.Any()))
        {
            _registrations.Add(new VaccinRegistration()
            {
                VaccinatinRegistrationId = Guid.Parse("2774e3d1-2b0f-47ab-b391-8ea43e6f9d80"),
                Name = "De Preester",
                FirstName= "Dieter",
                EMail = "dieter@dieter.com",
                YearOfBirth = 1963,
                VaccinationDate = "1/1/2021",
                VaccinTypeId = Guid.Parse("4e2a72fb-f4fa-416f-87cd-ea338b518519"),
                VaccinationLocationId = Guid.Parse("2774e3d1-2b0f-47ab-b391-8ea43e6f9d80")
            });
            _registrations.Add(new VaccinRegistration()
            {
                VaccinatinRegistrationId = Guid.Parse("0e2fa67e-e808-4a6d-af7a-f87cb47d85ee"),
                Name = "Van Thomme",
                FirstName= "Klaas",
                EMail = "klaas@klaas.com",
                YearOfBirth = 2000,
                VaccinationDate = "1/1/2022",
                VaccinTypeId = Guid.Parse("4e2a72fb-f4fa-416f-87cd-ea338b518519"),
                VaccinationLocationId = Guid.Parse("0bb537ea-8209-422f-a9e1-2c1e37d0cb4d")
            });
        }
    }

    public List<VaccinRegistration> GetRegistrations()
    {
        return _registrations.ToList<VaccinRegistration>();
    }

    public void AddRegistration(VaccinRegistration registration)
    {
        ArgumentNullException.ThrowIfNull(registration);
        _registrations.Add(registration);
    }
}

public class CsvVaccinRegistrationRepository : IVaccinRegistrationRepository
{
    private CsvConfig _csvConfig;

    public CsvVaccinRegistrationRepository(IOptions<CsvConfig> csvConfig)
    {
        _csvConfig = csvConfig.Value;

    }

    public void AddRegistration(VaccinRegistration registration)
    {
        var registrations = GetRegistrations();
        registrations.Add(registration);
        try
        {
            using var writer = new StreamWriter(_csvConfig.CsvRegistrations);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            using var csv = new CsvWriter(writer, config);
            csv.WriteRecords(registrations);

        }
        catch (System.Exception ex)
        {
             // TODO
        }
    }

    public List<VaccinRegistration> GetRegistrations()
    {
        try
        {
            using var reader = new StreamReader(_csvConfig.CsvRegistrations);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };

            using (var csv = new CsvReader(reader, config))
            {
                return csv.GetRecords<VaccinRegistration>().ToList();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
