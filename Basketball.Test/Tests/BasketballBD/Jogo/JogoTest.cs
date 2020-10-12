using Basketball.Model.Models.BasketballBD.Jogo;
using Basketball.Repository.IRepositories.BasketballBD.Jogo;
using Basketball.Service.Resources;
using Basketball.Service.Services.BasketballBD.Jogo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basketball.Test.Tests.BasketballBD.Jogo
{
    [TestClass]
    public class JogoTest
    {
        public Mock<IJogoRepository> _jogoRepositoryMock;
        public JogoService _jogoService;

        [TestInitialize]
        public void Setup()
        {
            _jogoRepositoryMock = new Mock<IJogoRepository>();

            _jogoService = new JogoService
            {
                _jogoRepository = _jogoRepositoryMock.Object
            };
        }

        // Lista base para utilizar em vários testes
        private List<JogoModel> _pontosBd = new List<JogoModel>() {
            new JogoModel() { Id = 1, Data = Convert.ToDateTime("10/10/2020"), QtdePontos = 10 },
            new JogoModel() { Id = 2, Data = Convert.ToDateTime("01/10/2020"), QtdePontos = 8 },
            new JogoModel() { Id = 3, Data = Convert.ToDateTime("06/10/2020"), QtdePontos = 10 },
            new JogoModel() { Id = 4, Data = Convert.ToDateTime("15/10/2020"), QtdePontos = 12 }
        };


        [TestMethod]
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestarCadastroJogo(bool success)
        {
            // ARRANGE
            var model = new JogoModel();
            _jogoRepositoryMock.Setup(x => x.Add(model)).Returns(success);

            // ACT
            var response = _jogoService.Add(model);

            // ASSERT
            _jogoRepositoryMock.Verify(x => x.Add(model), Times.Once);
            Assert.AreEqual(response.Success, true);

            // De acordo com parâmetro setado no mock, simula comportamento de sucesso e de erro no cadastro
            var mensagemRetorno = success ? ResourceResponse.CadastroSucesso : ResourceResponse.CadastroErro;
            Assert.AreEqual(response.Feedback, mensagemRetorno);
        }

        [TestMethod]
        public void TestarCadastroJogoErroCatch()
        {
            // ARRANGE
            var model = new JogoModel();
            _jogoRepositoryMock.Setup(x => x.Add(model)).Throws(new Exception());

            // ACT
            var response = _jogoService.Add(model);

            // ASSERT
            _jogoRepositoryMock.Verify(x => x.Add(model), Times.Once);
            Assert.AreEqual(response.Success, false);
            Assert.AreEqual(response.Feedback, ResourceResponse.ErroInterno);
        }

        [TestMethod]
        public void TestarRetornoObjeto()
        {
            // ARRANGE
            var pontosBd = _pontosBd;
            _jogoRepositoryMock.Setup(x => x.GetAll()).Returns(pontosBd);

            // ACT
            var response = _jogoService.GetAll();

            // ASSERT
            Assert.AreEqual(response.Success, true);
            Assert.AreEqual(response.Content.DataInicial, Convert.ToDateTime("01/10/2020"));
            Assert.AreEqual(response.Content.DataFinal, Convert.ToDateTime("15/10/2020"));
            Assert.AreEqual(response.Content.QtdeJogosDisputados, 4);
            Assert.AreEqual(response.Content.TotalPontosMarcados, 40);
            Assert.AreEqual(response.Content.MediaPontosPorJogo, 10);
            Assert.AreEqual(response.Content.MenorPontuacao, 8);
            Assert.AreEqual(response.Content.MaiorPontuacao, 12);
            Assert.AreEqual(response.Content.QtdeVezesRecord, 2);
        }

        #region Cálculo das propriedades de retorno
        [TestMethod]
        public void TestarRetornoDataInicial()
        {
            // ARRANGE
            var pontosBd = _pontosBd.OrderBy(x => x.Data).ToList();

            // ACT
            var response = _jogoService.GetDataInicial(pontosBd);

            // ASSERT
            Assert.AreEqual(response, Convert.ToDateTime("01/10/2020"));
        }

        [TestMethod]
        public void TestarRetornoDataFinal()
        {
            // ARRANGE
            var pontosBd = _pontosBd.OrderBy(x => x.Data).ToList();

            // ACT
            var response = _jogoService.GetDataFinal(pontosBd);

            // ASSERT
            Assert.AreEqual(response, Convert.ToDateTime("15/10/2020"));
        }

        [TestMethod]
        public void TestarRetornoQtdeJogosDisputados()
        {
            // ARRANGE
            var pontosBd = _pontosBd.OrderBy(x => x.Data).ToList();

            // ACT
            var response = _jogoService.GetQtdeJogosDisputados(pontosBd);

            // ASSERT
            Assert.AreEqual(response, 4);
        }

        [TestMethod]
        public void TestarRetornoTotalPontosMarcados()
        {
            // ARRANGE
            var pontosBd = _pontosBd.OrderBy(x => x.Data).ToList();

            // ACT
            var response = _jogoService.GetTotalPontos(pontosBd);

            // ASSERT
            Assert.AreEqual(response, 40);
        }

        [TestMethod]
        public void TestarRetornoMediaPontosMarcados()
        {
            // ARRANGE
            var pontosBd = _pontosBd.OrderBy(x => x.Data).ToList();

            // ACT
            var response = _jogoService.GetMediaPontos(pontosBd);

            // ASSERT
            Assert.AreEqual(response, 10);
        }

        [TestMethod]
        public void TestarRetornoMenorPontuacao()
        {
            // ARRANGE
            var pontosBd = _pontosBd.OrderBy(x => x.Data).ToList();

            // ACT
            var response = _jogoService.GetMenorPontuacao(pontosBd);

            // ASSERT
            Assert.AreEqual(response, 8);
        }

        [TestMethod]
        public void TestarRetornoMaiorPontuacao()
        {
            // ARRANGE
            var pontosBd = _pontosBd.OrderBy(x => x.Data).ToList();

            // ACT
            var response = _jogoService.GetMaiorPontuacao(pontosBd);

            // ASSERT
            Assert.AreEqual(response, 12);
        }

        [TestMethod]
        public void TestarRetornoQtdeRecord()
        {
            // ARRANGE
            var pontosBd = _pontosBd.OrderBy(x => x.Data).ToList();

            // ACT
            var response = _jogoService.GetQuantidadeRecords(pontosBd);

            // ASSERT
            Assert.AreEqual(response, 2);
        }
        #endregion
    }
}
