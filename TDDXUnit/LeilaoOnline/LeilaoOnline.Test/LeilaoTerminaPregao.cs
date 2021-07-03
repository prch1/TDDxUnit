using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeilaoOnline.Test
{
    public class LeilaoTerminaPregao
    {

        [Fact]
        public void LeilaoComTresClientes()
        {
            var modalidade = new MaiorValor();

            //Arrajed
            var leilao = new Leilao("Van Gogh",modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            var beltrano = new Interessada("Beltrano", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 990);
            leilao.RecebeLance(beltrano, 1400);

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1400;
            var clienteEsperado = beltrano;
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
            Assert.Equal(beltrano, leilao.Ganhador.Cliente);
            Assert.Equal(beltrano, leilao.Ganhador.Cliente);
        }


        [Fact]
        public void LeilaoComLancesOrdenadosPorValor()
        {

            var modalidade = new MaiorValor();

            var leilao = new Leilao("Van Gogh",modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(maria, 990);
            leilao.RecebeLance(fulano, 1000);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

        }


        [Fact]
        public void LeilaoComVariosLances()
        {
            var modalidade = new MaiorValor();

            //Arranje - cenario
            var leilao = new Leilao("Van Gogh",modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 990);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }


        [Fact]
        public void LeilaoComUmLance()
        {
            var modalidade = new MaiorValor();

            //Arranje - cenario
            var leilao = new Leilao("Van Gogh",modalidade);
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 800;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLance()
        {
            var modalidade = new MaiorValor();

            //Arranje - cenario
            var leilao = new Leilao("Van Gogh",modalidade);
            leilao.IniciaPregao();

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData (1400, new double[] {800,900,1000,1400})]
        [InlineData (1000, new double[] {800,900,1000,990})]
        [InlineData (800, new double[]  { 800 })]
        public void RetornaMaiorValorDoLeilaoComPeloMenosUmLance(double valorEsperado ,double[] ofertas)
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh",modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {

                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            leilao.TerminaPregao();

            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            var modalidade = new MaiorValor();

            var leilao = new Leilao("Van Gogh",modalidade);

            var excecaoObtida =   Assert.Throws<System.InvalidOperationException>(
                                         () => leilao.TerminaPregao()          
                                         );

            var mensagemEsperada = "Não é possível terminar o pregão sem que ele tenha começado.Para isso, utilize o método IniciaPregao().";
            Assert.Equal(mensagemEsperada, excecaoObtida.Message);

            //try
            //{
            //    leilao.TerminaPregao();
            //    Assert.True(false);
            //}
            //catch(System.Exception e)
            //{
            //    Assert.IsType<System.InvalidOperationException>(e);
            //}
        }

        [Theory]
        [InlineData(1200,1250, new double[] {800,1150,1400,1250})]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(
           double valorDestino,
           double valorEsperado,
           double[] ofertas)
        {

            var modalidade = new OfertaSuperiorMaisProximoa(valorDestino);


            //Arrange - Cenario
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                if ((i % 2 == 0))
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }
            //Act
            leilao.TerminaPregao();
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }



    }
}
