using AutoMapper;

namespace AutoMapperDependencyInjectionSample
{
    public class SomeProfile : Profile
    {
        public SomeProfile()
        {
            CreateMap<SomeModel, SomeOtherModel>()
                .AfterMap<MyAfterMapAction>();
        }
    }
}
