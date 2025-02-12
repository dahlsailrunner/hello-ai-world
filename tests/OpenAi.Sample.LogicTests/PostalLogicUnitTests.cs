using FluentAssertions;
using OpenAi.Sample.Api.BusinessLogic;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace OpenAi.Sample.LogicTests;

public class PostalLogicUnitTests
{
    private readonly PostalCodeLogic _logic;

    public PostalLogicUnitTests()
    {
        // NOTE: if the logic object below has dependencies for its constructor you would use the Moq library to set them up.
        var mockLogger = new Mock<ILogger<PostalCodeLogic>>();
        _logic = new PostalCodeLogic(mockLogger.Object);
    }

    [Fact]
    public void FiveOnesShouldThrowException()
    {
        Func<string> act = () => _logic.GetCityForPostalCode("11111");
        act.Should().Throw<Exception>();
    }

    [Fact]
    public void FiveTwosShouldThrowApplicationException()
    {
        Func<string> act = () => _logic.GetCityForPostalCode("22222");
        act.Should().Throw<ApplicationException>();
    }

    [Fact]
    public void NormalPostalCodeReturnsCityName()
    {
        var city = _logic.GetCityForPostalCode("12345");
        var citiesInLogic = new List<string>
            { "New York", "Chicago", "Minneapolis", "Seattle", "Huntington Beach", "Dallas" };
        city.Should().BeOneOf(citiesInLogic);
    }
}
