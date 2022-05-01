using Application;
using AutoMapper;
using StoreApi;
using Xunit;

namespace UnitTests;

public class MappingTests
{
    [Fact]
    public void AutoMapper_ProfileIsValid()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<InfrastructureMappingProfile>();
            cfg.AddProfile<RequestResponseMapper>();
        });
        config.AssertConfigurationIsValid();
    }
}