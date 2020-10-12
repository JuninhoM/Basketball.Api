using Basketball.Model.Models.BasketballBD.Jogo;
using Basketball.Model.ViewModel;
using Basketball.Model.ViewModel.BasketballBD.Jogo;
using Basketball.Repository.IRepositories.BasketballBD.Jogo;
using Basketball.Service.IServices.BasketballBD.Jogo;
using Basketball.Service.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basketball.Service.Services.BasketballBD.Jogo
{
    public class JogoService : IJogoService
    {
        public IJogoRepository _jogoRepository { get; set; }

        /// <summary>
        /// Cadastra novo registro de jogo disputado
        /// </summary>
        /// <returns></returns>
        public ResponseViewModel<bool> Add(JogoModel model)
        {
            var response = new ResponseViewModel<bool>();

            try
            {
                var add = _jogoRepository.Add(model);
                response.Success = true;

                // De acordo com sucesso de cadastro no banco, popula saída do objeto
                if (add)
                {
                    response.Content = true;
                    response.Feedback = ResourceResponse.CadastroSucesso;
                }
                else
                {
                    response.Content = false;
                    response.Feedback = ResourceResponse.CadastroErro;
                }
            }
            catch
            {
                response.Success = false;
                response.Feedback = ResourceResponse.ErroInterno;
            }

            return response;
        }

        /// <summary>
        /// Popula e retorna objeto com dados de jogos registrados
        /// É buscado todos os registros de pontos do Banco de Dados, trabalhando em cima destes dados para calcular e retornar as propriedades
        /// </summary>
        /// <returns></returns>
        public ResponseViewModel<JogosViewModel> GetAll()
        {
            var response = new ResponseViewModel<JogosViewModel>();

            try
            {
                // Objeto de retorno
                var dadosRetorno = new JogosViewModel();

                // Busca todos os registros do banco de dados
                var pontosBd = _jogoRepository.GetAll();

                // Caso hajam registros no banco, preenche propriedades de retorno
                if (pontosBd?.Count > 0)
                {
                    // Ordena registros pela data do jogo
                    pontosBd = pontosBd.OrderBy(x => x.Data).ToList();

                    // Data do primeiro e do último jogo registrado
                    dadosRetorno.DataInicial = GetDataInicial(pontosBd);
                    dadosRetorno.DataFinal = GetDataFinal(pontosBd);

                    // Quantidade de jogos disputados
                    dadosRetorno.QtdeJogosDisputados = GetQtdeJogosDisputados(pontosBd);

                    // Total de pontos marcados
                    dadosRetorno.TotalPontosMarcados = GetTotalPontos(pontosBd);

                    // Média de pontos marcados
                    dadosRetorno.MediaPontosPorJogo = GetMediaPontos(pontosBd);

                    // Menor e maior pontuação em um jogo
                    dadosRetorno.MenorPontuacao = GetMenorPontuacao(pontosBd);
                    dadosRetorno.MaiorPontuacao = GetMaiorPontuacao(pontosBd);

                    // Quantidade de vezes que bateu o próprio record 
                    dadosRetorno.QtdeVezesRecord = GetQuantidadeRecords(pontosBd);
                }
                
                response.Success = true;
                response.Content = dadosRetorno;
            }
            catch
            {
                response.Success = false;
                response.Feedback = ResourceResponse.ErroInterno;
            }

            return response;
        }

        #region Cálculo das propriedades de retorno
        // OBSERVAÇÃO: Os métodos desta sessão estão setados como públicos para que sejam acessados e testados pela camada de testes unitários

        /// <summary>
        /// Data de realização do primeiro jogo
        /// </summary>
        /// <param name="pontosBd"></param>
        /// <returns></returns>
        public DateTime GetDataInicial(List<JogoModel> pontosBd)
        {
            var result = pontosBd.First().Data;
            return result;
        }

        /// <summary>
        /// Data de realização do último jogo
        /// </summary>
        /// <param name="pontosBd"></param>
        /// <returns></returns>
        public DateTime GetDataFinal(List<JogoModel> pontosBd)
        {
            var result = pontosBd.Last().Data;
            return result;
        }

        /// <summary>
        /// Quantidade de jogos disputados
        /// </summary>
        /// <param name="pontosBd"></param>
        /// <returns></returns>
        public int GetQtdeJogosDisputados(List<JogoModel> pontosBd)
        {
            var result = pontosBd.Count();
            return result;
        }

        /// <summary>
        /// Total de pontos marcados
        /// </summary>
        /// <param name="pontosBd"></param>
        /// <returns></returns>
        public int GetTotalPontos(List<JogoModel> pontosBd)
        {
            var result = pontosBd.Sum(x => x.QtdePontos);
            return result;
        }

        /// <summary>
        /// Média de pontos marcados
        /// </summary>
        /// <param name="pontosBd"></param>
        /// <returns></returns>
        public double GetMediaPontos(List<JogoModel> pontosBd)
        {
            var result = pontosBd.Average(x => x.QtdePontos);
            return result;
        }

        /// <summary>
        /// Menor pontuação em um jogo
        /// </summary>
        /// <param name="pontosBd"></param>
        /// <returns></returns>
        public int GetMenorPontuacao(List<JogoModel> pontosBd)
        {
            var result = pontosBd.Min(x => x.QtdePontos);
            return result;
        }

        /// <summary>
        /// Maior pontuação em um jogo
        /// </summary>
        /// <param name="pontosBd"></param>
        /// <returns></returns>
        public int GetMaiorPontuacao(List<JogoModel> pontosBd)
        {
            var result = pontosBd.Max(x => x.QtdePontos);
            return result;
        }

        /// <summary>
        /// Maior pontuação em um jogo
        /// </summary>
        /// <param name="pontosBd"></param>
        /// <returns></returns>
        public int GetQuantidadeRecords(List<JogoModel> pontosBd)
        {
            // IMPORTANTE: Calculado de acordo com a data de cada partida informada no campo corresponde do cadastro, e não na data de inserção do registro em si
            var qtdeRecord = 0;

            // Seta a maior pontuação de record como a do primeiro jogo, para iniciar comparações
            var maiorPontuacaoRecord = pontosBd.First().QtdePontos;

            for (int i = 0; i < pontosBd.Count(); i++)
            {
                // Valida se não é o último jogo disputado
                if ((i + 1) < pontosBd.Count())
                {
                    if (pontosBd[i + 1].QtdePontos > maiorPontuacaoRecord)
                    {
                        qtdeRecord++;
                        maiorPontuacaoRecord = pontosBd[i + 1].QtdePontos;
                    }
                }
            }

            return qtdeRecord;
        }
        #endregion
    }
}
