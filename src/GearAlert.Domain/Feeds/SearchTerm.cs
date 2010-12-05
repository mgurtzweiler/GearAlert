namespace GearAlert.Domain.Feeds
{
    public class SearchTerm : EntityBase, IMappable {
        public virtual string Phrase { get; protected set; }

    }
}