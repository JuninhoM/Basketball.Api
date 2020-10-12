using System.Collections.Generic;
using Basketball.Model.Models.BasketballBD.Jogo;

namespace Basketball.Repository.IRepositories.BasketballBD.Jogo
{
    public interface IJogoRepository
    {
        bool Add(JogoModel model);
        List<JogoModel> GetAll();
    }
}