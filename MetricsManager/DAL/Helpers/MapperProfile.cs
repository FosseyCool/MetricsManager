using AutoMapper;
using MetricsManager.DTO;
using MetricsManager.Models;

namespace MetricsManager.DAL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<DotNetMetric, DotNetMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<HardDriveMetric, HardDriveMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<NetworkMetric, NetworkMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<RamMetric, RamMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());
        }
    }
}