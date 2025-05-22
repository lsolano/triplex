/// <summary>
/// Setup fixture
/// </summary>
[SetUpFixture]
[Parallelizable(scope: ParallelScope.All)]
#pragma warning disable CA1050 // Declare types in namespaces
public class OneTimeSetupFixture
#pragma warning restore CA1050 // Declare types in namespaces
{
    /// <summary>
    /// Setup fixture
    /// </summary>
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
    }

    /// <summary>
    /// Setup fixture
    /// </summary>
    [OneTimeTearDown]
    public void RunAfterAllTests()
    {
    }
}
