using OpenQiraa.Domain.Common;

namespace OpenQiraa.Domain.Entities;

public sealed class UserLemma : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid LemmaId { get; private set; }
    public LemmaStatus Status { get; private set; }
    public DateTime StatusChangedAt { get; private set; }

    public UserLemma(Guid userId, Guid lemmaId)
    {
        if (userId == Guid.Empty)
        {
            throw new DomainException("A user is required.");
        }
        if (lemmaId == Guid.Empty)
        {
            throw new DomainException("A lemma is required.");
        }
        UserId = userId;
        LemmaId = lemmaId;
        Status = LemmaStatus.New;
        StatusChangedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(LemmaStatus newStatus, DateTime date)
    {
        if (Status == newStatus)
        {
            return;
        }
        // value should always be defined in the enum
        if (!Enum.IsDefined(newStatus))
        {
            throw new ArgumentOutOfRangeException(nameof(newStatus));
        }

        Status = newStatus;
        StatusChangedAt = date;
    }
}
