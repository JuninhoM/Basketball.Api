using Microsoft.AspNetCore.Mvc;

namespace Basketball.Api.Presenters
{
    public sealed class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            ContentType = "application/json";
        }
    }
}
