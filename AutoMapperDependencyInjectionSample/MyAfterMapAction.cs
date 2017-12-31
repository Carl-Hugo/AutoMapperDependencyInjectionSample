using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;

namespace AutoMapperDependencyInjectionSample
{
    public class MyAfterMapAction : IMappingAction<SomeModel, SomeOtherModel>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyAfterMapAction(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public void Process(SomeModel source, SomeOtherModel destination)
        {
            destination.TraceIdentifier = _httpContextAccessor.HttpContext.TraceIdentifier;
        }
    }
}
