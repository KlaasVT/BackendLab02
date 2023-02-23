namespace lab02_vaccin.Services;
public interface IVaccinService
{
    VaccinRegistration AddRegistration(VaccinRegistration registration);
    List<VaccinationLocation> GetLocations();
    List<VaccinRegistration> GetRegistrations();
    List<VaccinType> GetTypes();
    VaccinType GetOneById(Guid id);
}

public class VaccinService : IVaccinService
{
    private readonly IVaccinTypeRepository _typeRepository;
    private readonly IVaccinRegistrationRepository _registrationRepository;
    private readonly IVaccinationLocationRepository _locationRepository;
    
    public VaccinService(IVaccinTypeRepository typeRepository, IVaccinRegistrationRepository registrationRepository, IVaccinationLocationRepository locationRepository)
    {
        _typeRepository = typeRepository;
        _registrationRepository = registrationRepository;
        _locationRepository = locationRepository;
    }

    public VaccinRegistration AddRegistration(VaccinRegistration registration)
    {
        ArgumentNullException.ThrowIfNull(registration);
        _registrationRepository.AddRegistration(registration);
        return registration;
    }

    public List<VaccinationLocation> GetLocations()
    {
        return _locationRepository.GetLocations();
    }

    public List<VaccinRegistration> GetRegistrations()
    {
        return _registrationRepository.GetRegistrations();
    }

    public List<VaccinType> GetTypes()
    {
        return _typeRepository.GetTypes();
    }

    public VaccinType GetOneById(Guid id){
        return _typeRepository.GetOneById(id);
    }
}