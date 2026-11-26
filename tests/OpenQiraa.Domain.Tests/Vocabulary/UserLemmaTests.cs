using OpenQiraa.Domain.Common;
using OpenQiraa.Domain.Entities;

namespace OpenQiraa.Domain.Tests.Vocabulary;

public class UserLemmaTests
{
    [Fact]
    public void Constructor_WithoutUserId_ThrowsDomainException()
    {
        Action act = () => new UserLemma(Guid.Empty, Guid.NewGuid());

        Assert.Throws<DomainException>(act);
    }

    [Fact]
    public void Constructor_WithoutLemmaId_ThrowsDomainException()
    {
        Action act = () => new UserLemma(Guid.NewGuid(), Guid.Empty);

        Assert.Throws<DomainException>(act);
    }

    [Fact]
    public void Constructor_WithValidParameters_CreatesUserLemma()
    {
        Guid userId = Guid.NewGuid();
        Guid lemmaId = Guid.NewGuid();

        UserLemma userLemma = new(userId, lemmaId);

        Assert.Equal(userId, userLemma.UserId);
        Assert.Equal(lemmaId, userLemma.LemmaId);
        Assert.Equal(LemmaStatus.New, userLemma.Status);
    }

    [Theory]
    [InlineData(LemmaStatus.New)]
    [InlineData(LemmaStatus.Learning1)]
    [InlineData(LemmaStatus.Ignore)]
    public void ChangeStatus_WithSameStatus_DoesNotChangeStatusChangedAt(LemmaStatus status)
    {
        UserLemma userLemma = new(Guid.NewGuid(), Guid.NewGuid());
        // first status assignation
        userLemma.ChangeStatus(status, userLemma.StatusChangedAt.AddHours(1));
        DateTime dateTime = userLemma.StatusChangedAt;
        // assigning new status
        userLemma.ChangeStatus(status, userLemma.StatusChangedAt.AddHours(2));

        Assert.Equal(dateTime, userLemma.StatusChangedAt);
    }

    [Theory]
    [InlineData((LemmaStatus)123)]
    [InlineData((LemmaStatus)99)]
    [InlineData((LemmaStatus)34)]
    public void ChangeStatus_WithUndefinedStatus_ThrowsExceptionAndLeavesStateUnchanged(
        LemmaStatus status
    )
    {
        UserLemma userLemma = new(Guid.NewGuid(), Guid.NewGuid());
        LemmaStatus currentStatus = userLemma.Status;
        DateTime currentDate = userLemma.StatusChangedAt;
        Action act = () => userLemma.ChangeStatus(status, userLemma.StatusChangedAt.AddHours(1));

        Assert.Throws<ArgumentOutOfRangeException>(act);
        Assert.Equal(currentStatus, userLemma.Status);
        Assert.Equal(currentDate, userLemma.StatusChangedAt);
    }

    [Theory]
    [InlineData(LemmaStatus.Learning1)]
    [InlineData(LemmaStatus.Ignore)]
    public void ChangeStatus_WithNewStatus_ChangesStatus(LemmaStatus status)
    {
        UserLemma userLemma = new(Guid.NewGuid(), Guid.NewGuid());
        userLemma.ChangeStatus(status, userLemma.StatusChangedAt.AddHours(1));

        Assert.Equal(status, userLemma.Status);
    }

    [Theory]
    [InlineData(LemmaStatus.Ignore)]
    [InlineData(LemmaStatus.Known)]
    [InlineData(LemmaStatus.Learning3)]
    public void ChangeStatus_WithNewStatus_ChangesStatusChangedAt(LemmaStatus status)
    {
        UserLemma userLemma = new(Guid.NewGuid(), Guid.NewGuid());
        DateTime newDate = userLemma.StatusChangedAt.AddHours(1);

        userLemma.ChangeStatus(status, newDate);

        Assert.Equal(newDate, userLemma.StatusChangedAt);
    }
}
