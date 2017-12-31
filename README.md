# AutoMapperDependencyInjectionSample
The code sample that shows how to use Dependency Injection using AutoMapper and ASP.NET Core.
The code also uses the `AutoMapper.Extensions.Microsoft.DependencyInjection` package ([source code on GitHub](https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection)).

## Some facts
1. Dependencies can't be injected into `Profile` classes. 
2. Dependencies can be injected into smaller mapping logic block like `IMappingAction<in TSource, in TDestination>` implementations.

## How it works
To use Dependency Injection, I created `MyAfterMapAction` class that is connected to `SomeProfile` as follow:

``` csharp
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
```

This is possible because of `AutoMapper.Extensions.Microsoft.DependencyInjection`.

In `Startup.ConfigureServices`, the following line of code do all the job for us and  scans my assembly for implementations of `IValueResolver<,,>`, `IMemberValueResolver<,,,>`, `ITypeConverter<,>` and `IMappingAction<,>`.

``` csharp
services.AddAutoMapper(typeof(Startup).Assembly);
```

And voila!
