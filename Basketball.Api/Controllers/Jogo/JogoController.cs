using Basketball.Api.Presenters;
using Basketball.Model.Models.BasketballBD.Jogo;
using Basketball.Service.IServices.BasketballBD.Jogo;
using Microsoft.AspNetCore.Mvc;

namespace Basketball.Api.Controllers.Jogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly ResponsePresenter _responsePresenter;
        private readonly IJogoService _jogoService;

        public JogoController(ResponsePresenter responsePresenter, IJogoService jogoService)
        {
            _responsePresenter = responsePresenter;
            _jogoService = jogoService;
        }

        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            _responsePresenter.Handle(_jogoService.GetAll());
            return _responsePresenter.ContentResult;
        }

        [HttpPost]
        public ActionResult Post([FromBody] JogoModel model)
        {
            _responsePresenter.Handle(_jogoService.Add(model));
            return _responsePresenter.ContentResult;
        }
    }
}
