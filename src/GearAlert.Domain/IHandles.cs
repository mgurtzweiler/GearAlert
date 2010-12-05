namespace GearAlert.Domain
{
    public interface IHandles<T> where T : IDomainEvent {
        void Handle(T args);
    }
}