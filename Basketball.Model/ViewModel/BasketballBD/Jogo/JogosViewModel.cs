using System;

namespace Basketball.Model.ViewModel.BasketballBD.Jogo
{
    /// <summary>
    /// Modelo de exibição com dados combinados de todos os jogos registrados para o jogador
    /// </summary>
    public class JogosViewModel
    {
        // Data do primeiro jogo disputado
        public DateTime DataInicial { get; set; }

        // Data do último jogo disputado
        public DateTime DataFinal { get; set; }

        public int QtdeJogosDisputados { get; set; }

        public int TotalPontosMarcados { get; set; }

        public double MediaPontosPorJogo { get; set; }

        // Menor pontuação feita em um jogo
        public int MenorPontuacao { get; set; }

        // Maior pontuação feita em um jogo
        public int MaiorPontuacao { get; set; }    

        // Quantidade de vezes que um registro de pontos foi maior que todos os anteriores
        public int QtdeVezesRecord { get; set; }
    }
}
