using AutoMapper;
using MetricsAgent.DAL.Responses;
using MetricsAgent.Models;

namespace MetricsAgent
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricModel, CpuMetric>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<DotNetMetricModel, DotNetMetric>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<HddMetricModel, DAL.Responses.HddMetric>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<NetWorkMetricModel, NetworkMetric>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<RamMetricModel, RamMetric>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

        }

    }
}