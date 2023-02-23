namespace lab02_vaccin.Profiles;

public class DTOProfile : Profile
{
    public DTOProfile()
    {
        CreateMap<VaccinRegistration, VaccinRegistrationDTO>()
            .ForMember(dest => dest.VaccinName , opt => opt.MapFrom<VaccinResolver>())
            .ForMember(dest => dest.Location , opt => opt.MapFrom<VaccinLocationResolver>());
    }

}
public class VaccinLocationResolver : IValueResolver<VaccinRegistration, VaccinRegistrationDTO, string>
{
    public string Resolve(VaccinRegistration source, VaccinRegistrationDTO destination, string dest, ResolutionContext context)
    {
        List<VaccinationLocation> locations = context.Items["locations"] as List<VaccinationLocation>;
        return locations.Where(l => l.VaccinationLocationId == source.VaccinationLocationId).Single().Name;
    }
}

public class VaccinResolver : IValueResolver<VaccinRegistration, VaccinRegistrationDTO, string>
{
    public string Resolve(VaccinRegistration source, VaccinRegistrationDTO destination, string dest, ResolutionContext context)
    {
        List<VaccinType> vaccins = context.Items["vaccins"] as List<VaccinType>;
        return vaccins.Where(l => l.VaccinTypeId == source.VaccinTypeId).Single().Name;
    }
}
