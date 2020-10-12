using Basketball.Api.Serialization;
using Basketball.Model.ViewModel;
using System.Net;

namespace Basketball.Api.Presenters
{
    public sealed class ResponsePresenter : IOutputPort<ResponseViewModel>
    {
        public JsonContentResult ContentResult { get; }
        public ResponsePresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(ResponseViewModel response, bool camelCaseReturn = false)
        {
            Handle<dynamic>(response, camelCaseReturn);
        }

        public void Handle<T>(ResponseViewModel<T> response, bool camelCaseReturn = false)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response, camelCaseReturn);
        }
    }
}
