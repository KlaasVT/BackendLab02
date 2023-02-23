namespace lab02_vaccin.Repositories;
public interface IVaccinationLocationRepository
{
    List<VaccinationLocation> GetLocations();
}

public class VaccinationLocationRepository : IVaccinationLocationRepository
{
    private static List<VaccinationLocation> _locations = new List<VaccinationLocation>();

    public VaccinationLocationRepository()
    {
        if (!(_locations.Any()))
        {

            using (var reader = new StreamReader("./Data/Locaties.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<VaccinationLocation>();
                foreach (var record in records)
                {
                    _locations.Add(record);
                }
            }


            // _locations.Add(new VaccinationLocation()
            // {
            //     VaccinationLocationId = Guid.Parse("2774e3d1-2b0f-47ab-b391-8ea43e6f9d80"),
            //     Name = "Kortrijk Expo"
            // });
            // _locations.Add(new VaccinationLocation()
            // {
            //     VaccinationLocationId = Guid.Parse("0bb537ea-8209-422f-a9e1-2c1e37d0cb4d"),
            //     Name = "Gent Expo"
            // });
        }
    }

    public List<VaccinationLocation> GetLocations()
    {
        return _locations.ToList<VaccinationLocation>();
    }
}

public class CsvVaccinationLocationRepository : IVaccinationLocationRepository
{
    private CsvConfig _csvConfig;

    public CsvVaccinationLocationRepository(IOptions<CsvConfig> csvConfig)
    {
        _csvConfig = csvConfig.Value;

    }

    public List<VaccinationLocation> GetLocations()
    {
        try
        {
            using var reader = new StreamReader(_csvConfig.CsvLocations);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };

            using (var csv = new CsvReader(reader, config))
            {
                return csv.GetRecords<VaccinationLocation>().ToList();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
