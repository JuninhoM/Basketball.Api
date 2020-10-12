using Basketball.Model.Models.BasketballBD.Jogo;
using Basketball.Repository.IRepositories.BasketballBD.Jogo;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Basketball.Repository.Repositories.BasketballBD.Jogo
{
    public class JogoRepository : BaseRepository, IJogoRepository
    {
        public List<JogoModel> GetAll()
        {
            var query = $@"SELECT * FROM Jogo";
            var result = _conn.Query<JogoModel>(query).ToList();

            return result;
        }

        public bool Add(JogoModel model)
        {
            var query = $@"INSERT INTO Jogo (Data, QtdePontos) VALUES (@Data, @QtdePontos)";
            var result = _conn.Execute(query, model);

            return result > 0;
        }
    }
}
