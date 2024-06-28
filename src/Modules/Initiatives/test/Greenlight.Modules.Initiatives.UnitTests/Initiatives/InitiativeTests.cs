using Greenlight.Common.Domain;
using Greenlight.Modules.Initiatives.Domain.Categories;
using Greenlight.Modules.Initiatives.Domain.Initiatives;
using Greenlight.Modules.Initiatives.UnitTests.Abstractions;
using Shouldly;

namespace Greenlight.Modules.Initiatives.UnitTests.Initiatives;

public class InitiativeTests : BaseTest
{
    [Fact]
    public void SubmitForApproval_ShouldReturnError_WhenStatusNotDraft()
    {
        // Arrange
        Result<Initiative> createResult = Initiative.Create("Initiative 1");
        createResult.IsSuccess.ShouldBeTrue();

        Result cancelResult = createResult.Value.Cancel("For unit test");
        cancelResult.IsSuccess.ShouldBeTrue();

        // Act
        Result submitForApprovalResult = createResult.Value.SubmitForApproval();

        // Assert
        submitForApprovalResult.Error.ShouldBe(InitiativeErrors.NotDraft);
        submitForApprovalResult.IsSuccess.ShouldBeFalse();

        AssertDomainEventWasNotPublished<InitiativeSubmittedForApprovalDomainEvent>(createResult.Value);
    }

    [Fact]
    public void SubmitForApproval_ShouldSucceed_WhenStatusIstDraft()
    {
        // Arrange
        Result<Initiative> createResult = Initiative.Create("Initiative 1");
        createResult.IsSuccess.ShouldBeTrue();

        // Act
        Result submitForApprovalResult = createResult.Value.SubmitForApproval();

        // Assert
        submitForApprovalResult.Error.ShouldBe(Error.None);
        submitForApprovalResult.IsSuccess.ShouldBeTrue();

        Initiative initiative = createResult.Value;
        InitiativeSubmittedForApprovalDomainEvent domainEvent =
        AssertDomainEventWasPublished<InitiativeSubmittedForApprovalDomainEvent>(createResult.Value);
        domainEvent.InitiativeId.ShouldBe(initiative.Id);
    }

    [Fact]
    public void Create_ShouldHaveCorrectValues_WhenCreated()
    {
        // Arrange

        // Act
        const string initiativeTitle = "Initiative 1";
        Result<Initiative> result = Initiative.Create(initiativeTitle);

        // Assert
        result.Error.ShouldBe(Error.None);
        result.IsSuccess.ShouldBeTrue();

        Initiative initiative = result.Value;

        initiative.Title.ShouldBe(initiativeTitle);
        initiative.CategoryId.ShouldBe(Category.CategoryNotSet.Id);
        initiative.ProcessStatus.ShouldBe(InitiativeProcessStatus.Draft);
        InitiativeCreatedDomainEvent domainEvent =
            AssertDomainEventWasPublished<InitiativeCreatedDomainEvent>(initiative);

        domainEvent.InitiativeId.ShouldBe(initiative.Id);
    }

}
