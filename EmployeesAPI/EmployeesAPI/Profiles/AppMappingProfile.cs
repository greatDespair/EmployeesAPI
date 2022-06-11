using AutoMapper;
using EmployeesAPI.Data.Models.Dto;
using EmployeesAPI.Data.Models;

namespace EmployeesAPI.Profiles
{
    public class AppMappingProfile: Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Passport, PassportDto>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.dName, opt => opt.MapFrom(src => src.dName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Passport, opt => opt.MapFrom(src => new PassportDto { Number = src.Passport.Number, Type = src.Passport.Type }))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => new DepartmentDto { dName = src.Department.dName, Phone = src.Department.Phone }));
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Passport, opt => opt.MapFrom(src => new Passport { Type = src.Passport.Type, Number = src.Passport.Number }))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => new Department { Phone = src.Department.Phone, dName = src.Department.dName }));

        }
    }
}
