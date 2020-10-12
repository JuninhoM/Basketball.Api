using System;
using System.Collections.Generic;
using Basketball.Model.Models.BasketballBD.Jogo;
using Basketball.Model.ViewModel;
using Basketball.Model.ViewModel.BasketballBD.Jogo;
using Basketball.Repository.IRepositories.BasketballBD.Jogo;

namespace Basketball.Service.IServices.BasketballBD.Jogo
{
    public interface IJogoService
    {
        IJogoRepository _jogoRepository { get; set; }

        ResponseViewModel<bool> Add(JogoModel model);
        ResponseViewModel<JogosViewModel> GetAll();

        #region Cálculo das propriedades de retorno
        DateTime GetDataInicial(List<JogoModel> pontosBd);
        DateTime GetDataFinal(List<JogoModel> pontosBd);
        int GetQtdeJogosDisputados(List<JogoModel> pontosBd);
        int GetTotalPontos(List<JogoModel> pontosBd);
        double GetMediaPontos(List<JogoModel> pontosBd);
        int GetMenorPontuacao(List<JogoModel> pontosBd);
        int GetMaiorPontuacao(List<JogoModel> pontosBd);
        int GetQuantidadeRecords(List<JogoModel> pontosBd);
        #endregion
    }
}