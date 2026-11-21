using OpenQiraa.Domain.Common;

namespace OpenQiraa.Domain.Entities;

public sealed class UserLemma : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid LemmaId { get; private set; }
    public WordStatus Status { get; private set; }

    public UserLemma(Guid userId, Guid lemmaId)
    {
        if (userId == Guid.Empty)
            throw new DomainException("Un utilisateur est requis.");
        UserId = userId;
        if (lemmaId == Guid.Empty)
            throw new DomainException("Un lemme est requis.");
        LemmaId = lemmaId;
        Status = WordStatus.New;
    }
}
