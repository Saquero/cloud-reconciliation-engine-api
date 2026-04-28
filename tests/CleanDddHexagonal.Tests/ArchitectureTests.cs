using CleanDddHexagonal.Application.UseCases.Reconciliation;
using CleanDddHexagonal.Domain.Entities;
using NetArchTest.Rules;

namespace CleanDddHexagonal.Tests;

public class ArchitectureTests
{
    [Fact]
    public void Domain_ShouldNotDependOnInfrastructureApiOrEfCore()
    {
        var domainAssembly = typeof(CustomerAccount).Assembly;

        var infrastructureResult = Types.InAssembly(domainAssembly)
            .ShouldNot()
            .HaveDependencyOn("CleanDddHexagonal.Infrastructure")
            .GetResult();

        var apiResult = Types.InAssembly(domainAssembly)
            .ShouldNot()
            .HaveDependencyOn("CleanDddHexagonal.Api")
            .GetResult();

        var efResult = Types.InAssembly(domainAssembly)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.EntityFrameworkCore")
            .GetResult();

        Assert.True(infrastructureResult.IsSuccessful);
        Assert.True(apiResult.IsSuccessful);
        Assert.True(efResult.IsSuccessful);
    }

    [Fact]
    public void Application_ShouldNotDependOnInfrastructureApiOrEfCore()
    {
        var applicationAssembly = typeof(RunReconciliationUseCase).Assembly;

        var infrastructureResult = Types.InAssembly(applicationAssembly)
            .ShouldNot()
            .HaveDependencyOn("CleanDddHexagonal.Infrastructure")
            .GetResult();

        var apiResult = Types.InAssembly(applicationAssembly)
            .ShouldNot()
            .HaveDependencyOn("CleanDddHexagonal.Api")
            .GetResult();

        var efResult = Types.InAssembly(applicationAssembly)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.EntityFrameworkCore")
            .GetResult();

        Assert.True(infrastructureResult.IsSuccessful);
        Assert.True(apiResult.IsSuccessful);
        Assert.True(efResult.IsSuccessful);
    }
}
