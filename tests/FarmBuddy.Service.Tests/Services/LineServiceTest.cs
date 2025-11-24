using FarmBuddy.Service.Services;
using LineMessaging;

namespace FarmBuddy.Service.Tests.Services;

public class LineServiceTest
{
    private readonly ILineService _lineService;
    private readonly ITestOutputHelper _testOutputHelper;

    public LineServiceTest(ILineService lineService, ITestOutputHelper testOutputHelper)
    {
        _lineService = lineService;
        _testOutputHelper = testOutputHelper;
    }
    

}