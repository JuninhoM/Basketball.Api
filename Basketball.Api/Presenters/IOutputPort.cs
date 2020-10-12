namespace Basketball.Api.Presenters
{
    public interface IOutputPort<in TUseCaseResponse>
    {
        void Handle(TUseCaseResponse response, bool camelCaseReturn = false);
    }
}
