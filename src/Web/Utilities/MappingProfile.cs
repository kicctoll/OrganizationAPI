using AutoMapper;
using ApplicationCore.Entities;
using Web.ViewModels;

namespace Web.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organization, OrganizationViewModel>();
            CreateMap<OrganizationViewModel, Organization>();

            CreateMap<Country, CountryViewModel>();
            CreateMap<CountryViewModel, Country>();

            CreateMap<Business, BusinessViewModel>();
            CreateMap<BusinessViewModel, Business>();

            CreateMap<Family, FamilyViewModel>();
            CreateMap<FamilyViewModel, Family>();

            CreateMap<Offering, OfferingViewModel>();
            CreateMap<OfferingViewModel, Offering>();

            CreateMap<Department, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, Department>();
        }
    }
}
