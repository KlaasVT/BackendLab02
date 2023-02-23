namespace lab02_vaccin.Repositories;

public interface IVaccinTypeRepository

{
    List<VaccinType> GetTypes();
    VaccinType GetOneById(Guid id);
}

public class VaccinTypeRepository : IVaccinTypeRepository
{

    private static List<VaccinType> _types = new List<VaccinType>();

    public VaccinTypeRepository()
    {
        if (!(_types.Any()))
        {
            // using (var reader = new StreamReader("./Data/Vaccins.txt"))
            // using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            // {
            //     var records = csv.GetRecords<VaccinType>();
            //     foreach (var record in records){
            //         _types.Add(record);
            //     }
            // }   
            _types.Add(new VaccinType()
            {
                VaccinTypeId = Guid.Parse("4e2a72fb-f4fa-416f-87cd-ea338b518519"),
                Name = "BioNTech, Pfizer"
            });
            _types.Add(new VaccinType()
            {
                VaccinTypeId = Guid.Parse("7fa73e42-77d6-4a5b-aef6-0d36779bc989"),
                Name = "Pfizer"
            });
        }
    }

    public List<VaccinType> GetTypes()
    {
        return _types.ToList<VaccinType>();
    }

    public VaccinType GetOneById(Guid id)
    {
        throw new NotImplementedException();
    }
}


public class CsvVaccinTypeRepository : IVaccinTypeRepository
{
    private CsvConfig _csvConfig;

    public CsvVaccinTypeRepository(IOptions<CsvConfig> csvConfig)
    {
        _csvConfig = csvConfig.Value;

    }

    public List<VaccinType> GetTypes()
    {
        try
        {
            using var reader = new StreamReader(_csvConfig.CsvVaccins);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ";"
            };

            using var csv = new CsvReader(reader, config);
            return csv.GetRecords<VaccinType>().ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public VaccinType GetOneById(Guid vaccinTypeId)
    {
        var types = GetTypes();
        return types.FirstOrDefault(x => x.VaccinTypeId == vaccinTypeId);
    }

}
