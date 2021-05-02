using NUnit.Framework;

/// <summary>
/// Setup fixture
/// </summary>
[SetUpFixture]
[Parallelizable(scope: ParallelScope.All)]
public class OneTimeSetupFixture
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