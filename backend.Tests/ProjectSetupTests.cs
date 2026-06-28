/*
 * ProjectSetupTests.cs contains the first backend test for the demo.
 *
 * Responsibility:
 * - Prove that the xUnit test project is wired correctly.
 * - Give the CI pipeline a real command to run in Phase 6.
 *
 * Connection to other files:
 * - The test project references EmployeeHub.Api.csproj.
 * - Later tests will call services, repositories, and controllers from the backend project.
 */

namespace EmployeeHub.Api.Tests;

public class ProjectSetupTests
{
    [Fact]
    public void DemoProjectName_ShouldBeEmployeeHubDemo()
    {
        const string projectName = "EmployeeHub Demo";

        Assert.Equal("EmployeeHub Demo", projectName);
    }
}
